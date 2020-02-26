using Pauser.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Pauser.Logic.Implementations {
    public class FilterProvider : IFilterProvider {
        public IEnumerable<IFilter> FromStorage() => Saved<Options>.Instance.IFilters;

        public void ToStorage(IEnumerable<IFilter> list) {
            Saved<Options>.Instance.IFilters = list?.ToArray() ?? new IFilter[] { };
            Saved<Options>.Save();
        }
    }
}
