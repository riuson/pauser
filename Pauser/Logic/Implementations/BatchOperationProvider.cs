using Pauser.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Pauser.Logic.Implementations {
    public class BatchOperationProvider : IBatchOperationProvider {
        public IEnumerable<IBatchOperation> FromStorage() => Saved<Options>.Instance.IBatchOperations;

        public void ToStorage(IEnumerable<IBatchOperation> list) {
            Saved<Options>.Instance.IBatchOperations = list?.ToArray() ?? new IBatchOperation[] { };
            Saved<Options>.Save();
        }
    }
}
