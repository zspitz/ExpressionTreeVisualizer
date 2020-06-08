using System;

namespace Periscope.Debuggee {
    [Serializable]
    public abstract class ConfigBase<TConfig> where TConfig : ConfigBase<TConfig> {
        public abstract TConfig Clone();
        public abstract ConfigDiffStates Diff(TConfig baseline);
    }
}
