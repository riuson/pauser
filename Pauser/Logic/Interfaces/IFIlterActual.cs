using System.ComponentModel;

namespace Pauser.Logic.Interfaces {
    public interface IFilterActual {
        BindingList<IFilter> Filters { get; }

        void LoadSettings();
        void SaveSettings();
    }
}
