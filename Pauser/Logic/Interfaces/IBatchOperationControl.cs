using System;
using System.Threading.Tasks;

namespace Pauser.Logic.Interfaces {
    public interface IBatchOperationControl {
        Task ExecuteAsync();
        event EventHandler<BatchOperationsProgress> Progress;
    }

    public class BatchOperationsProgress : EventArgs {
        public BatchOperationsProgress(IBatchOperation operation) {
            this.Operation = operation;
        }

        public IBatchOperation Operation { get; }
    }
}
