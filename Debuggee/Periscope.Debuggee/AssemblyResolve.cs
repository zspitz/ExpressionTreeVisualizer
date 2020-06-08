using System;
using System.IO;
using System.Linq;
using System.Reflection;
using ZSpitz.Util;
using static System.StringComparison;
using static System.IO.Path;
using System.Net;
using System.Diagnostics;

namespace Periscope.Debuggee {
    public static class AssemblyResolve {
        private static readonly string[] exclusions = new[] { ".xmlserializers", ".resources" };
        private static readonly string[] patterns = new[] { "*.dll", "*.exe" };
        private static bool handlerAttached = false;

        public static void Attach(string subfolderName) {
            if (handlerAttached) { return; }
            handlerAttached = true;

            // TODO prevent subfoldername from containing invalid characters

            // some inspiration from https://stackoverflow.com/a/33977134/111794
            AppDomain.CurrentDomain.AssemblyResolve += (s, e) => {
                Debugger.Break();

                var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == e.Name);
                if (loadedAssembly is { }) { return loadedAssembly; }

                var n = new AssemblyName(e.Name);
                if (n.Name.EndsWithAny(OrdinalIgnoreCase, exclusions)) { return null; }

                var subfolder = Combine(
                    Assembly.GetExecutingAssembly().Location,
                    subfolderName
                );
                if (!Directory.Exists(subfolder)) { return null; }

                string? assemblyPath = patterns.SelectMany(pattern => Directory.EnumerateFiles(subfolder, pattern)).FirstOrDefault(path => {
                    try {
                        return n.Name.Equals(AssemblyName.GetAssemblyName(path).Name, OrdinalIgnoreCase);
                    } catch (BadImageFormatException) {
                        return false;
                    } catch (Exception ex) {
                        throw new Exception($"Error loading assembly at {path}", ex);
                    }
                });

                if (assemblyPath is null) { throw new Exception($"Assembly {e.Name} not found"); }

                return Assembly.LoadFrom(assemblyPath);
            };
        }
    }
}
