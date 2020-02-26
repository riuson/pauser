using System.Collections.Generic;

namespace Pauser.Logic.Interfaces {
    public interface IProcessProvider {
        IEnumerable<IProcessInfo> Find(IEnumerable<IFilter> filters);
    }
}
