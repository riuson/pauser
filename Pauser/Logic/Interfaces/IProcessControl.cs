using System.Collections.Generic;

namespace Pauser.Logic.Interfaces {
    public interface IProcessControl {
        void Suspend(IEnumerable<IProcessInfo> list);
        void Resume(IEnumerable<IProcessInfo> list);
    }
}
