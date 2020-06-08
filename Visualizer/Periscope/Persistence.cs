using System;
using static System.IO.Path;
using static System.Environment;
using System.Linq;
using ZSpitz.Util;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using Periscope.Debuggee;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Periscope {
    public class VersionCheckInfo {
        public bool AutoCheck { get; set; }
        public DateTime? LastChecked { get; set; }
        [JsonConverter(typeof(VersionConverter))] public Version? LastVersion { get; set; }
        public void Deconstruct(out bool autoCheck, out DateTime? lastChecked, out Version? lastVersion) {
            autoCheck = AutoCheck;
            lastChecked = LastChecked;
            lastVersion = LastVersion;
        }
    }

    public static class Persistence {
        public static string? Folder { get; private set; }

        public static void SetFolder(string description) =>
            Folder = Combine(
                GetFolderPath(SpecialFolder.LocalApplicationData),
                description.Replace(" ", "")
            );

        private static bool tryReadParseFile(string key, [NotNullWhen(true)] out JObject? data) {
            data = null;

            var path = Combine(
                Folder,
                $"{key}.json"
            );
            if (!File.Exists(path)) { return false; }

            string? fileText = null;
            try {
                fileText = File.ReadAllText(path);
            } catch { }
            if (fileText.IsNullOrWhitespace()) { return false; }

            try {
                data = JObject.Parse(fileText);
            } catch { }
            return !(data is null);
        }

        // config data is stored in two files: global config (".json"), and keyed files
        // the global config stores globals (at the globals property), and the last used keyed config (at the lastKeyed property)
        // each visualizer can determine what should be the key
        // keyed config overrides last used keyed config

        public static TConfig Get<TConfig>(string key = "") where TConfig : ConfigBase<TConfig> {
            var ret = new JObject();

            if (Directory.Exists(Folder)) {
                if (tryReadParseFile("", out var globalConfig)) {
                    if (globalConfig.TryGetValue("globals", out var globals)) {
                        ret.Merge(globals);
                    }
                    if (globalConfig.TryGetValue("lastKeyed", out var lastKeyed)) {
                        ret.Merge(lastKeyed);
                    }
                }

                if (!key.IsNullOrWhitespace() && tryReadParseFile(key, out var keyedConfig)) {
                    ret.Merge(keyedConfig);
                }
            }

            return ret.ToObject<TConfig>()!;
        }

        public static void Write<TConfig>(TConfig config, string key = "") where TConfig : ConfigBase<TConfig> {
            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented,
                ContractResolver = new ConfigContractResolver(false)
            };
            var serializer = JsonSerializer.CreateDefault(settings);
            var globals = JObject.FromObject(config, serializer);

            // https://github.com/JamesNK/Newtonsoft.Json/issues/2155
            // it seems that the contract resolver can't be modified after it's been passed in
            settings.ContractResolver = new ConfigContractResolver(true);
            serializer = JsonSerializer.CreateDefault(settings);
            var keyed = JObject.FromObject(config, serializer);

            var versionCheckInfo = GetVersionCheckInfo();

            var final = new JObject {
                ["globals"] = globals,
                ["lastKeyed"] = keyed
            };
            if (versionCheckInfo is { }) {
                final["versionCheck"] = JObject.FromObject(versionCheckInfo);
            }

            if (!Directory.Exists(Folder)) {
                try {
                    Directory.CreateDirectory(Folder);
                } catch {
                    // we can't create the directory, so we can't write the configuration
                    return;
                }
            }

            try {
                File.WriteAllText(Combine(Folder, ".json"), final.ToString());
            } catch { }

            if (keyed.Children().Any() && key != "") {
                try {
                    File.WriteAllText(Combine(Folder, key), keyed.ToString());
                } catch { }
            }
        }

        public static VersionCheckInfo? GetVersionCheckInfo() {
            tryReadParseFile("", out var data);
            return data?["versionCheck"]?.ToObject<VersionCheckInfo>();
        }

        public static void Write(VersionCheckInfo versionCheck) {
            if (!tryReadParseFile("", out var current)) {
                current = new JObject();
            }
            current["versionCheck"] = JObject.FromObject(versionCheck);

            if (Directory.Exists(Folder)) {
                try {
                    Directory.CreateDirectory(Folder);
                } catch {
                    // we can't create the directory, so we can't write the configuration
                    return;
                }
            }

            try {
                File.WriteAllText(Combine(Folder, ".json"), current.ToString());
            } catch { }
        }
    }
}
