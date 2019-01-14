using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.LanguageNames;

namespace ExpressionTreeVisualizer {
    class VisualizerDataObjectSource : VisualizerObjectSource {
        public override void GetData(object target, Stream outgoingData) {
            var expr = (Expression)target;
            var visualizerData = new VisualizerData(expr, CSharp);
            base.GetData(visualizerData, outgoingData);
        }
    }
}
