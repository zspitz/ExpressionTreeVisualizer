using ExpressionTreeVisualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Linq.Expressions.Expression;
using static Microsoft.CSharp.RuntimeBinder.Binder;
using Microsoft.CSharp.RuntimeBinder;
using ExpressionToString;

namespace _visualizerTests {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            ////var i = 7;
            //var j = 8;

            //Expression<Func<int, string, bool>> expr = (i, s) => (i * i * i + 15) >= 10 && s.Length <= 25 || (Math.Pow(j, 3) > 100 && j + 15 < 100) && new Random().Next() > 15 || new DateTime(2001, 10, 12).Month < 5;

            //var i = 5;
            //Expression<Func<int, int>> expr = j => (i + j + 17) * (i + j + 17);

            //Expression<Func<bool>> expr = () => true;

            //Expression<Func<string, int, string>> expr = (s, i) => $"{s}, {i}";

            //Expression<Func<object[]>> expr = () => new object[] { "" };

            //Expression<Func<string[][]>> expr = () => new string[5][];

            //Expression<Func<int, int, string>> expr = (i, j) => (i + j + 5).ToString();

            //var lst = new List<string>();
            //Expression<Func<string>> expr = () => lst[5];

            //var arr = new string[,][] { };
            //Expression<Func<string>> expr = () => arr[5, 2][7];

            //Expression<Func<int, int, int>> expr = (int i, int j) => i + j;

            //Func<int> del = () => DateTime.Now.Day;
            //Expression<Func<int>> expr = () => del();

            //Expression<Func<Foo>> expr = () => new Foo("abcd") {
            //    Bar = "abcd",
            //    Baz = "efgh"
            //};
            //var binding = ((MemberInitExpression)expr.Body).Bindings[0];

            //Expression<Func<Wrapper>> expr = () => new Wrapper { { "ab", "cd" }, "ef" };

            //var foo = new Foo();
            //var expr = foo.GetExpression();

            //var i = 5;
            Expression<Func<int>> expr1 = () => 5;
            Expression<Func<Expression<Func<int>>>> expr = () => expr1;

            //Expression<Func<string>> expr = Lambda<Func<string>>(
            //    MakeMemberAccess(
            //        Constant(foo),
            //        typeof(Foo).GetMember("Bar").Single()
            //    )
            //);

            //var closure = expr.Compile().Target as System.Runtime.CompilerServices.Closure;
            //Console.WriteLine(closure.Constants.Contains(foo));

            //Func<Expression<Func<int>>> outer = () => {
            //    var i = 5;
            //    Func<Expression<Func<int>>> inner = () => {
            //        var j = 10;
            //        return () => i + j;
            //    };
            //    return inner();
            //};
            //var expr = outer();

            //Expression expr = Expression.AddAssign(Expression.Variable(typeof(int)), Expression.Constant(5));

            //Expression<Func<int, double, double[]>> expr = (n, exp) => new[] { Math.Pow(n, exp) };

            //IQueryable<Person> personSource = null;
            //Expression<Func<Person, bool>> expr = person => person.LastName.StartsWith("A");

            //var hour = Variable(typeof(int), "hour");
            //var msg = Variable(typeof(string), "msg");
            //var block = Block(
            //    // specify the variables available within the block
            //    new[] { hour, msg },
            //    // hour =
            //    Assign(hour,
            //        // DateTime.Now.Hour
            //        MakeMemberAccess(
            //            MakeMemberAccess(
            //                null,
            //                typeof(DateTime).GetMember("Now").Single()
            //            ),
            //            typeof(DateTime).GetMember("Hour").Single()
            //        )
            //    ),
            //    // if ( ... ) { ... } else { ... }
            //    IfThenElse(
            //        // ... && ...
            //        AndAlso(
            //            // hour >= 6
            //            GreaterThanOrEqual(
            //                hour,
            //                Constant(6)
            //            ),
            //            // hour <= 18
            //            LessThanOrEqual(
            //                hour,
            //                Constant(18)
            //            )
            //        ),
            //        // msg = "Good day"
            //        Assign(msg, Constant("Good day")),
            //        // msg = Good night"
            //        Assign(msg, Constant("Good night"))
            //    ),
            //    // Console.WriteLine(msg);
            //    Call(
            //        typeof(Console).GetMethod("WriteLine", new[] { typeof(object) }),
            //        msg
            //    ),
            //    hour
            //);
            //Expression<Action> expr = Lambda<Action>(block);

            //var constant = Constant(new List<int>());
            //Expression expr = Or(
            //    NotEqual(Constant(5), Constant(5)),
            //    ReferenceNotEqual(constant, constant)
            //);

            //var context = typeof(Program);
            //var flags = CSharpBinderFlags.None;
            //var argInfos = new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
            //var binder = GetIndex(flags, context, argInfos);
            //var expr = Dynamic(binder, typeof(object), Parameter(typeof(object), "obj"), Constant("key1"), Constant(42));

            //Expression<Func<int, float, float>> multiplier = (i, f) => i * f;

            //IEnumerable<string> matchingNames = new List<string>() { "Smith", "Doe" };
            //Expression<Func<Person, bool>> expr = p => p.DOB.DayOfWeek == DayOfWeek.Tuesday;


            //expr = p => matchingNames.Contains(p.LastName) && p.DOB.DayOfWeek == DayOfWeek.Tuesday;

            //var i = 5;
            //Expression<Func<int, int>> expr = j => i * j;


            //Expression<Func<Person, bool>> expr = p => p.DOB.DayOfWeek == DayOfWeek.Tuesday;
            //Console.WriteLine(expr.ToString("C#"));
            //Console.ReadKey(true);

            //Expression<Func<Person, bool>> expr = p => p.LastName.StartsWith("A");

            //string s = expr.ToString("C#", out Dictionary<string, (int start, int length)> pathSpans);
            //const int firstColumnAlignment = -25;
            //Console.WriteLine($"{"Path",firstColumnAlignment}Substring");
            //Console.WriteLine(new string('-', 65));
            //foreach (var kvp in pathSpans) {
            //    var path = kvp.Key;
            //    var (start, length) = kvp.Value;
            //    Console.WriteLine(
            //        $"{path,firstColumnAlignment}{new string(' ', start)}{s.Substring(start, length)}"
            //    );
            //}

            //Console.ReadKey(true);

            //var writeline = typeof(Console).GetMethods().Single(x => x.Name == "WriteLine" && x.GetParameters().Length == 0);
            //Expression expr = Block(
            //    Call(writeline),
            //    Block(
            //        new[] { Parameter(typeof(string), "s1") },
            //        Call(writeline),
            //        Call(writeline)
            //    ),
            //    Call(writeline)
            //);
            //Console.WriteLine(expr.ToString("C#"));

            //var writeline = typeof(Console).GetMethods().Single(x => {
            //    if (x.Name != "WriteLine") { return false; }
            //    var parameters = x.GetParameters();
            //    return parameters.Length == 1 && parameters[0].ParameterType == typeof(string);
            //});
            //Expression expr = IfThenElse(
            //    LessThanOrEqual(
            //        Property(
            //            Property(null, typeof(DateTime).GetProperty("Now")),
            //            "Hour"
            //        ),
            //        Constant(18)
            //    ),
            //    Block(
            //        Call(writeline, Constant("Good day!")),
            //        Call(writeline, Constant("Have a nice day!"))
            //    ),
            //    Block(
            //        Call(writeline, Constant("Good night!")),
            //        Call(writeline, Constant("Have a nice night!"))
            //    )
            //);

            var visualizerHost = new VisualizerDevelopmentHost(expr, typeof(Visualizer), typeof(VisualizerDataObjectSource));
            visualizerHost.ShowVisualizer();

            //Console.ReadKey(true);
        }

        static Expression<Func<int, int>> expr1 = ((Func<Expression<Func<int, int>>>)(() => {
            var value = Parameter(typeof(int), "value");
            var result = Parameter(typeof(int), "result");
            var label = Label(typeof(int));

            BlockExpression block = Block(
                new[] { result },
                Assign(result, Constant(1)),
                Loop(
                    IfThenElse(
                        GreaterThan(value, Constant(1)),
                        MultiplyAssign(result,
                            PostDecrementAssign(value)
                        ),
                        Break(label, result)
                    ),
                    label
                )
            );
            return Lambda<Func<int, int>>(block, value);
        })).Invoke();
    }

    class Foo {
        public string Bar { get; set; }
        public string Baz { get; set; }
        public Foo() { }
        public Foo(string baz) { }

        public Expression<Func<string, string>> GetExpression() {
            var s = "abcd";
            return s1 => Bar + s + s1;
        }
    }

    class Wrapper : List<string> {
        public void Add(string s1, string s2) => throw new NotImplementedException();
    }

    class Person {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DOB { get; set; }
    }
}