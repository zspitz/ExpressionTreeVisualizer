using ExpressionTreeVisualizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Linq.Expressions.Expression;
using ExpressionToString.Util;
using System.Reflection;
using System.Collections;

namespace _visualizerTests.dotnetcore {
    class Program {
        static void Main(string[] args) {
            var x = Parameter(typeof(int), "x");
            var y = Parameter(typeof(int), "y");
            var expr = Multiply(
                Add(x, y),
                Constant(5)
            );

            //var data = new VisualizerData(expr);
            //Console.WriteLine(CanSerialize(data));

            var endnodeData = new EndNodeData {
                Closure = null,
                Name = null,
                Type = "int",
                Value = "5"
            };
            //Console.WriteLine(CanSerialize(endnodeData));

            var stream = File.Create(Path.GetTempFileName());
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, endnodeData);

            //var visualizerHost = new VisualizerDevelopmentHost(expr, typeof(Visualizer), typeof(VisualizerDataObjectSource));
            //visualizerHost.ShowVisualizer();

        }

        static (bool success, string failPath, string errorMessage) CanSerialize(object o, string path = "") {
            var formatter = new BinaryFormatter();
            using (var stream = File.Create(Path.GetTempFileName())) {
                return CanSerialize(o, path, formatter, stream);
            }
        }

        static (bool success, string failPath, string errorMessage) CanSerialize(object o, string path, BinaryFormatter formatter, Stream stream) {
            if (o == null) { return (false, path, "Null object"); }

            string msg;
            var t = o.GetType();
            if (t.IsPrimitive || t == typeof(string)) { return (true, path, null); }
            try {
                formatter.Serialize(stream, o);
                return (true, path, null);
            } catch (Exception ex) {
                msg = ex.Message;
            }

            List<(string, object)> values;
            if (t.IsArray) {
                values = (o as IEnumerable).ToObjectList()
                    .Select((x, index) => ($"[{index}]", x))
                    .Where(x => x.Item2 != null)
                    .ToList();
            } else {
                values = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(fld => !fld.IsStatic)
                    .Select(fld => ($".{fld.Name}", fld.GetValue(o)))
                    .Where(x => x.Item2 != null)
                    .ToList();
            }

            foreach (var (name, value) in values) {
                var ret = CanSerialize(value, path + name, formatter, stream);
                if (!ret.success) { return ret; }
            }
            return (false, path, msg);
        }
    }
}
