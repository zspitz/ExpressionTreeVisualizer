using Periscope.Debuggee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periscope {
    public struct ViewState<TConfig> where TConfig : ConfigBase<TConfig> {
        public readonly object DataContext;
        public readonly TConfig Config;
        public void Deconstruct(out object dataContext, out TConfig config) {
            dataContext = DataContext;
            config = Config;
        }

        public ViewState(object context, TConfig config) {
            DataContext = context;
            Config = config;
        }
    }
}
