using System.Collections.Generic;

namespace Pauser.Logic.Interfaces {
    interface IAdaptersProvider {
        IEnumerable<IAdapter> FromStorage();
        void ToStorage(IEnumerable<IAdapter> list);
    }
}
