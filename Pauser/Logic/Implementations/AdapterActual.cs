using Pauser.Logic.Interfaces;
using System.ComponentModel;
using System.Linq;

namespace Pauser.Logic.Implementations {
    public class AdapterActual : IAdapterActual {
        private readonly IAdapterProvider _provider;

        public AdapterActual(IAdapterProvider provider) {
            this._provider = provider;
            this.Adapters = new BindingList<IAdapter>() { AllowEdit = true };
            this.LoadSettings();
        }

        public BindingList<IAdapter> Adapters { get; }

        public void UpdateList() {
            var selected = this.Adapters
                .Where(x => x.Selected)
                .ToArray();

            this.Adapters.Clear();
            var adapters = this._provider.FromSystem();

            foreach (var adapterInfo in adapters) {
                adapterInfo.Selected = selected.Any(x => this.IsAdaptersEqual(x, adapterInfo));
                this.Adapters.Add(adapterInfo);
            }
        }

        public void LoadSettings() {
            var adapters = this._provider.FromStorage();
            this.Adapters.Clear();

            foreach (var adapter in adapters) {
                this.Adapters.Add(adapter);
            }

        }

        public void SaveSettings() => this._provider.ToStorage(this.Adapters);

        private bool IsAdaptersEqual(IAdapter adapter1, IAdapter adapter2) =>
            (adapter1.Name == adapter2.Name)
            && (adapter1.Description == adapter2.Description)
            //&& (adapter1.DeviceId == adapter2.DeviceId)
            && (adapter1.NetConnectionId == adapter2.NetConnectionId);
    }
}
