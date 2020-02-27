using Pauser.Logic.Interfaces;
using System.ComponentModel;
using System.Linq;

namespace Pauser.Logic.Implementations {
    public class BatchOperationActual : IBatchOperationActual {
        private readonly IBatchOperationProvider _provider;

        public BatchOperationActual(IBatchOperationProvider _provider) {
            this._provider = _provider;

            this.Operations = new BindingList<IBatchOperation>() {
                AllowNew = true,
                AllowRemove = true,
                AllowEdit = true
            };
            this.Operations.AddingNew += (sender, args) => args.NewObject = new BatchOperaion();
        }

        public BindingList<IBatchOperation> Operations { get; }

        public void LoadSettings() {
            var filters = this._provider.FromStorage();

            this.Operations.Clear();

            foreach (var filter in filters) {
                this.Operations.Add(filter);
            }
        }

        public void SaveSettings() {
            var list = this.Operations.ToArray();
            this._provider.ToStorage(list);
        }
    }
}
