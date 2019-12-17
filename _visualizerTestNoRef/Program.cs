using System;
using System.Linq.Expressions;

namespace _visualizerTestNoRef {
    class Program {
        static void Main(string[] args) {
            Expression<Func<bool>> expr = () => true;


            Console.WriteLine("Hello World!");
        }
    }
}
