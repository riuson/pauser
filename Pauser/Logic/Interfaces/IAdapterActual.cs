using System.ComponentModel;

namespace Pauser.Logic.Interfaces {
    public interface IAdapterActual {
        BindingList<IAdapter> Adapters { get; }

        void UpdateList();
        void LoadSettings();
        void SaveSettings();
    }
}
