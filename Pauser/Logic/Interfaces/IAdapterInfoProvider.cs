using System.Collections.Generic;

namespace Pauser.Logic.Interfaces {
    public interface IAdapterInfoProvider {
        IEnumerable<IAdapterInfo> FromStorage();
        void ToStorage(IEnumerable<IAdapterInfo> list);

        IEnumerable<IAdapterInfo> FromSystem();
    }
}
