using System.Collections.Generic;

namespace Pauser.Logic.Interfaces {
    public interface IBatchOperationProvider {
        IEnumerable<IBatchOperation> FromStorage();
        void ToStorage(IEnumerable<IBatchOperation> list);
    }
}
