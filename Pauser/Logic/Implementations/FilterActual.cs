using Pauser.Logic.Interfaces;
using System.ComponentModel;
using System.Linq;

namespace Pauser.Logic.Implementations {
    public class FilterActual : IFilterActual {
        private readonly IFilterProvider _provider;

        public FilterActual(IFilterProvider provider) {
            this._provider = provider;
            this.Filters = new BindingList<IFilter> {
                AllowNew = true,
                AllowRemove = true,
                AllowEdit = true
            };
            this.Filters.AddingNew += this.AddingNew;

        }

        public BindingList<IFilter> Filters { get; }

        public void LoadSettings() {
            var filters = this._provider.FromStorage();

            this.Filters.Clear();

            foreach (var filter in filters) {
                this.Filters.Add(filter);
            }
        }

        public void SaveSettings() {
            var list = this.Filters.ToArray();
            this._provider.ToStorage(list);
        }

        private void AddingNew(object sender, AddingNewEventArgs e) => e.NewObject = new Filter();
    }
}
