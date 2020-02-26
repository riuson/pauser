using System.Collections.Generic;

namespace Pauser.Logic.Interfaces {
    public interface IFilterProvider {
        IEnumerable<IFilter> FromStorage();
        void ToStorage(IEnumerable<IFilter> list);
    }
}
