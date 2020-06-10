using System;
using System.Linq;
using System.Reflection;
using static System.IO.Path;
using static System.StringComparison;
using static System.Reflection.Assembly;
using System.IO;

namespace Periscope.Debuggee {
    public static class SubfolderAssemblyResolver {
        private static bool EndsWithAny(this string s, StringComparison comparisonType, params string[] testStrings) => testStrings.Any(x => s.EndsWith(x, comparisonType));

        private static readonly string[] exclusions = new[] { ".xmlserializers", ".resources" };
        private static readonly string[] patterns = new[] { "*.dll", "*.exe" };

        private static string? basePath;
        private static string? subfolderPath;

        private static Assembly? resolver(object sender, ResolveEventArgs e) {
            var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == e.Name);
            if (loadedAssembly is { }) { return loadedAssembly; }

            var n = new AssemblyName(e.Name);
            if (exclusions.Any(x => n.Name.EndsWith(x, OrdinalIgnoreCase))) { return null; }
            //if (n.Name.EndsWithAny(OrdinalIgnoreCase, exclusions)) { return null; }

            // search in basePath first, as it's probably the better dependency
            var assemblyPath =
                resolveFromFolder(basePath!) ??
                resolveFromFolder(subfolderPath!) ??
                throw new Exception($"Assembly {e.Name} not found");

            return LoadFrom(assemblyPath);

            string resolveFromFolder(string folder) =>
                patterns
                    .SelectMany(pattern => Directory.EnumerateFiles(folder, pattern))
                    .FirstOrDefault(filePath => {
                        try {
                            return n.Name.Equals(AssemblyName.GetAssemblyName(filePath).Name, OrdinalIgnoreCase);
                        } catch (BadImageFormatException) {
                            return false;
                        } catch (Exception ex) {
                            throw new Exception($"Error loading assembly {filePath}", ex);
                        }
                    });
        }

        public static void Hook(string subfolderKey) {
            if (string.IsNullOrWhiteSpace(subfolderKey)) { return; }
            if (!string.IsNullOrWhiteSpace(subfolderPath)) { return; }

            subfolderPath = Combine(
                GetDirectoryName(GetCallingAssembly().Location),
                subfolderKey
            );
            basePath = GetDirectoryName(subfolderPath);

            AppDomain.CurrentDomain.AssemblyResolve += resolver;
        }
    }
}