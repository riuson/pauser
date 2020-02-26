using System.Collections.Generic;

namespace Pauser.Logic.Interfaces {
    public interface IFiltersProvider {
        IEnumerable<IFilter> FromStorage();
        void ToStorage(IEnumerable<IFilter> list);
    }
}
