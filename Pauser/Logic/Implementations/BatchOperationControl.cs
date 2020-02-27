using Pauser.Logic.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pauser.Logic.Implementations {
    public class BatchOperationControl : IBatchOperationControl {
        private readonly IAdapterControl _adaptersControl;
        private readonly IProcessControl _processControl;
        private readonly IBatchOperationActual _operationsActual;

        public BatchOperationControl(
            IAdapterControl adaptersControl,
            IProcessControl processControl,
            IBatchOperationActual operationsActual) {
            this._adaptersControl = adaptersControl;
            this._processControl = processControl;
            this._operationsActual = operationsActual;
        }

        public Task ExecuteAsync() => Task.Run(this.Execute);

        public event EventHandler<BatchOperationsProgress> Progress;

        private void ReportProgress(IBatchOperation operation) =>
            this.Progress?.Invoke(this, new BatchOperationsProgress(operation));

        private void Execute() {
            var operations = this._operationsActual.Operations;

            foreach (var operation in operations) {
                this.ExecuteOperation(operation);
            }
        }

        private void ExecuteOperation(IBatchOperation operation) {
            this.ReportProgress(operation);

            switch (operation.Operation) {
                case Operation.DisableNetworks: {
                        this._adaptersControl.Disable();
                        break;
                    }
                case Operation.EnableNetworks: {
                        this._adaptersControl.Enable();
                        break;
                    }
                case Operation.SuspendProcesses: {
                        this._processControl.Suspend();
                        break;
                    }
                case Operation.ResumeProcesses: {
                        this._processControl.Resume();
                        break;
                    }
                case Operation.Delay: {
                        if (!int.TryParse(operation.Argument, out var period)) {
                            period = 1000;
                        }

                        Thread.Sleep(period);

                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
