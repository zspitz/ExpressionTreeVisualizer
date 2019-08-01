using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Tests.Common {
    public class FormatterData : DataAttribute {
        private readonly string _fromatter;

        public FormatterData(string Formatter) {
            _fromatter = Formatter;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod) {
            // resolve path from current dll + formatter name

            // parse file
            // for each line in File.ReadAllLines(path)
            //      if line.StartsWith("---") - new test
            // (test name embedded in ---- lines)
            // add to TheoryData<string, string, string>
            //      expression name
            //      formatter name
            //      result

            // return TheoryData

            throw new NotImplementedException();
        }
    }
}
/*
test objects
	namespace: ExpressionToString.Tests.Objects
	ExpressionToString.Tests.Common
		CSCompiler
		FactoryMethods
	ExpressionToString.Tests.Common.VB
		VBCompiler
	spread across partial static classes/modules, based on category
	decorate with category custom attribute

base abstract class
	overridable RunTest
		parameters - object to test, object name
	protected PreRunTest
		parameters - caller member name
		resolve object from type of 'this' and member name
		calls RunTest with object, object name

three abstract classes, by source
	decorate class with source trait
	each one spread across multiple files, using partial
	each test method
		decorate with category trait
		corresponds to expression object
		calls PreRunTest

formatter tests
	static dictionary<string formatter, string objectName), string> with results
		for each registered formatter, load from files
	inherits three abstract classes
		RunTest calls static RunToStringTest, passing in object name and object

	get results + pathspans from ToString("Factory methods")

	for each formatter
		get expected using the formatter and the object name as a multipart key, against the static dictionary
		get actual using ToString(formatter) on the object
			if formatter == factory methods, use above
		Assert.Equals

		do pathspans checks against factory methods pathspans

visualizer data tests
	inherits three abstract classes
		RunTest calls static VisualizerDataTest, passing in object
	static method creaates new VisualizerData from object

validation of test methods vs expressions - each expression object must have a corresponding test method

*/
