using System.Collections.Generic;

namespace Pauser.Logic.Interfaces {
    public interface IProcessControl {
        void Suspend();
        void Resume();
    }
}
