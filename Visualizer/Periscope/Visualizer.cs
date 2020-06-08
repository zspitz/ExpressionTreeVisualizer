using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.ComponentModel;
using ZSpitz.Util;
using ZSpitz.Util.Wpf;
using System.Windows;
using System.Diagnostics;
using Periscope.Debuggee;
using static System.IO.Path;
using System.Linq;
using System.Runtime.CompilerServices;
using Octokit;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Interop;

namespace Periscope {
    public class Visualizer : INotifyPropertyChanged {
        public static RelayCommand CopyWatchExpression = new RelayCommand(parameter => {
            if (!(parameter is string formatString)) { throw new ArgumentException("'parameter' is not a string."); }

            var rootExpression = Current?.GetRootExpression();
            if (rootExpression.IsNullOrWhitespace()) { return; }

            Clipboard.SetText(string.Format(formatString, rootExpression));
        });

        public static void Show<TWindow, TConfig>(Type referenceType, IVisualizerObjectProvider objectProvider, IProjectInfo? projectInfo = default)
                where TWindow : VisualizerWindowBase<TWindow, TConfig>, new()
                where TConfig : ConfigBase<TConfig> {

            Current.Initialize(referenceType, projectInfo);

            PresentationTraceSources.DataBindingSource.Listeners.Add(new DebugTraceListener());

            ConfigKey = objectProvider.GetObject() as string ?? "";

            var config = Persistence.Get<TConfig>(ConfigKey);

            var window = new TWindow();
            window.Initialize(objectProvider, config);
            window.ShowDialog();
        }

        public readonly static Visualizer Current = new Visualizer();

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyChanged<T>(ref T current, T newValue, [CallerMemberName] string? name = null) =>
            this.NotifyChanged(ref current, newValue, PropertyChanged, name);
        private void NotifyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private string? rootExpression;
        public string? RootExpression {
            get => rootExpression;
            set => NotifyChanged(ref rootExpression, value);
        }

        public string? GetRootExpression() {
            if (rootExpression.IsNullOrWhitespace()) {
                new ExpressionRootPrompt().ShowDialog();
            }
            return rootExpression;
        }

        private Version? version;
        public Version? Version {
            get => version;
            private set => NotifyChanged(ref version, value);
        }

        private bool autoVersionCheck;
        public bool AutoVersionCheck {
            get => autoVersionCheck;
            set => NotifyChanged(ref autoVersionCheck, value);
        }

        private DateTime? versionCheckedOn;
        public DateTime? VersionCheckedOn {
            get => versionCheckedOn;
            private set => NotifyChanged(ref versionCheckedOn, value);
        }

        private Version? latestVersion;
        public Version? LatestVersion {
            get => latestVersion;
            private set => NotifyChanged(ref latestVersion, value);
        }

        private string? latestVersionString = null;
        [AllowNull]
        public string LatestVersionString {
            get => latestVersionString ?? latestVersion?.ToString() ?? "Unknown";
            private set => NotifyChanged(ref latestVersionString, value);
        }

        private string? location;
        public string? Location {
            get => location;
            private set => NotifyChanged(ref location, value);
        }
        private string? filename;
        public string? Filename {
            get => filename;
            private set => NotifyChanged(ref filename, value);
        }
        private string? description;
        public string? Description {
            get => description;
            private set => NotifyChanged(ref description, value);
        }

        /// <summary>URL strings bound to Hyperlink cannot be whtespace</summary>
        private void NotifyUrlChange(ref string? current, string? newValue, [CallerMemberName] string? name = null) {
            if (newValue.IsNullOrWhitespace()) { newValue = null; }
            NotifyChanged(ref current, newValue, name);
        }

        private string? projectUrl;
        public string? ProjectUrl {
            get => projectUrl;
            private set => NotifyUrlChange(ref projectUrl, value);
        }

        private string? feedbackUrl;
        public string? FeedbackUrl {
            get => feedbackUrl;
            private set => NotifyUrlChange(ref feedbackUrl, value);
        }

        private string? releaseUrl;
        public string? ReleaseUrl {
            get => releaseUrl;
            private set => NotifyUrlChange(ref releaseUrl, value);
        }

        public RelayCommand? LatestVersionCheck { get; private set; }

        public (string url, string args) UrlArgs => ("explorer.exe", $"/n /e,/select,\"{location}\"");
        public static string ConfigKey { get; set; } = "";

        public void Initialize(Type t, IProjectInfo? projectInfo, string? description = null) {
            // This requires an externally passed type, otherwise it'll return the Periscope DLL info
            var asm = t.Assembly;
            Version = asm.GetName().Version;
            Location = asm.Location;
            Filename = GetFileName(asm.Location);

            Description =
                description ??
                asm.GetAttributes<DebuggerVisualizerAttribute>(false)
                    .Select(x => x.Description)
                    .Distinct()
                    .Single();

            Persistence.SetFolder(Description);

            if (projectInfo is { }) {
                ProjectUrl = projectInfo.ProjectUrl;
                FeedbackUrl = projectInfo.FeedbackUrl;
                ReleaseUrl = projectInfo.ReleaseUrl;

                if (Persistence.GetVersionCheckInfo() is VersionCheckInfo versionCheckInfo) {
                    (AutoVersionCheck, VersionCheckedOn, LatestVersion) = versionCheckInfo;
                }

                LatestVersionCheck = new RelayCommand(async o => {
                    LatestVersionString = "Checking...";
                    LatestVersion = await projectInfo.GetLatestVersionAsync();
                    LatestVersionString = null;
                    VersionCheckedOn = DateTime.UtcNow;
                    NotifyNewVersion();
                }, o =>
                    VersionCheckedOn is null ||
                    DateTime.UtcNow - VersionCheckedOn >= TimeSpan.FromHours(1)
                );
                NotifyChanged(nameof(LatestVersionCheck));

                PropertyChanged += (s, e) => {
                    if (e.PropertyName.In(
                        nameof(AutoVersionCheck),
                        nameof(VersionCheckedOn),
                        nameof(LatestVersion)
                    )) {
                        Persistence.Write(new VersionCheckInfo {
                            AutoCheck = autoVersionCheck,
                            LastChecked = versionCheckedOn,
                            LastVersion = latestVersion
                        });
                    }
                };

                if (AutoVersionCheck) {
                    if (LatestVersionCheck.CanExecute("")) { 
                        LatestVersionCheck.Execute(""); 
                    } else {
                        NotifyNewVersion();
                    }
                }
            }
        }

        private void NotifyNewVersion() {
            if (LatestVersion <= Version) { return; }
            string msg = $"There is a newer version available:\n\nCurrent: {Version}\n\nNewer: {LatestVersion}\n\nDo you want to open the releases page?";
            var result = MessageBox.Show(msg, "", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes) { return; }
            Commands.LaunchUrlOrFileCommand.Execute(ReleaseUrl);
        }
    }
}
