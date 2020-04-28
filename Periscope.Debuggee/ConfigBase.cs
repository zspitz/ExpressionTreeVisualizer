using System;
using System.Collections.Generic;
using System.Text;

namespace Periscope.Debuggee {
    [Serializable]
    public abstract class ConfigBase<TConfig> where TConfig : ConfigBase<TConfig> {
        public abstract TConfig Clone();
        public abstract bool NeedsTransferData(TConfig original);
    }
    // TODO can we add VisualizerObjectDataSource to this project somehow?
}
