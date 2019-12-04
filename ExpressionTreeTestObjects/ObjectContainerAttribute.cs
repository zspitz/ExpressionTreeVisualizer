using System;
using static System.AttributeTargets;

namespace ExpressionTreeTestObjects {
    [AttributeUsage(Class, AllowMultiple = false, Inherited = false)]
    public class ObjectContainerAttribute : Attribute { }
}
