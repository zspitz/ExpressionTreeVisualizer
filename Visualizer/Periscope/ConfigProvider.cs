using Newtonsoft.Json.Linq;
using Periscope.Debuggee;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using ZSpitz.Util;
using static System.IO.Path;
using static System.Environment;
using Newtonsoft.Json;

namespace Periscope {
    [Obsolete("Use Persistence instead")]
    public static class ConfigProvider {
        public static string? ConfigFolder { get; private set; }
        public static void LoadConfigFolder(Type t) {
            var description = 
                t.Assembly.GetAttributes<DebuggerVisualizerAttribute>(false)
                    .Select(x => x.Description)
                    .Distinct()
                    .Single();
            LoadConfigFolder(description);
        }
        public static void LoadConfigFolder(string subfolderName) {
            ConfigFolder = Combine(
                GetFolderPath(SpecialFolder.LocalApplicationData),
                subfolderName.Replace(" ", "")
            );
        }

        private static bool TryReadParseFile(string key, [NotNullWhen(true)] out JObject? data) {
            data = null;

            var path = Combine(
                ConfigFolder,
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

            if (Directory.Exists(ConfigFolder)) {
                if (TryReadParseFile("", out var globalConfig)) {
                    if (globalConfig.TryGetValue("globals", out var globals)) {
                        ret.Merge(globals);
                    }
                    if (globalConfig.TryGetValue("lastKeyed", out var lastKeyed)) {
                        ret.Merge(lastKeyed);
                    }
                }

                if (!key.IsNullOrWhitespace() && TryReadParseFile(key, out var keyedConfig)) {
                    ret.Merge(keyedConfig);
                }
            }

            return ret.ToObject<TConfig>()!;
        }

        public static void Write<TConfig>(TConfig config, string key = "") where TConfig : ConfigBase<TConfig> {
            if (!Directory.Exists(ConfigFolder)) {
                try {
                    Directory.CreateDirectory(ConfigFolder);
                } catch {
                    // we can't create the directory, so we can't write the configuration
                    return;
                }
            }

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

            var final = new JObject {
                ["globals"] = globals,
                ["lastKeyed"] = keyed
            };
            try {
                File.WriteAllText(Combine(ConfigFolder, ".json"), final.ToString());
            } catch {}

            if (keyed.Children().Any() && key != "") {
                try {
                    File.WriteAllText(Combine(ConfigFolder, key), keyed.ToString());
                } catch { }
            }
        }
    }
}
