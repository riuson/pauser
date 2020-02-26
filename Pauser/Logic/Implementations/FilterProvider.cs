using Pauser.Logic.Interfaces;
using Pauser.UI;
using System.Collections.Generic;
using System.Linq;

namespace Pauser.Logic.Implementations {
    public class FilterProvider : IFilterProvider {
        public IEnumerable<IFilter> FromStorage() => Saved<Options>.Instance.Filters;

        public void ToStorage(IEnumerable<IFilter> list) {
            Saved<Options>.Instance.Filters = list.OfType<Filter>().ToArray();
            Saved<Options>.Save();
        }
    }
}
