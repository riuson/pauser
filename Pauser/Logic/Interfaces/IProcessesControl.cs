using System.Collections.Generic;

namespace Pauser.Logic.Interfaces {
    interface IProcessesControl {
        void Suspend(IEnumerable<IProcess> list);
        void Resume(IEnumerable<IProcess> list);
    }
}
