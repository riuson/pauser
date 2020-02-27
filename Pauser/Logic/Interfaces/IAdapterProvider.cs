using System.Collections.Generic;

namespace Pauser.Logic.Interfaces {
    public interface IAdapterProvider {
        IEnumerable<IAdapter> FromStorage();
        void ToStorage(IEnumerable<IAdapter> list);

        IEnumerable<IAdapter> FromSystem();
    }
}
