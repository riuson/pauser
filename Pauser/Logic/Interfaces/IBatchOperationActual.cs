using System.ComponentModel;

namespace Pauser.Logic.Interfaces {
    public interface IBatchOperationActual {
        BindingList<IBatchOperation> Operations { get; }

        void LoadSettings();
        void SaveSettings();
    }
}
