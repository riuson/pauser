using System.Collections.Generic;
using Pauser.Logic.Interfaces;

namespace Pauser.Logic {
    public interface IProcessesProvider {
        IEnumerable<IProcess> Find(IEnumerable<IFilter> filters);
    }
}
