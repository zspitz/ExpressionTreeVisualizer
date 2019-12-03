using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.AttributeTargets;

namespace ExpressionTreeTestObjects {
    [AttributeUsage(Field, Inherited = false, AllowMultiple = false)]
    public class CategoryAttribute : Attribute {
        public CategoryAttribute(string category) => Category = category;

        public string Category { get; set; }
    }
}
