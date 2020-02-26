namespace Pauser.Logic.Interfaces {
    public interface IAdapterControl {
        void Enable(IAdapterInfo adapter);
        void Disable(IAdapterInfo adapter);
    }
}
