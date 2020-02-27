namespace Pauser.Logic.Interfaces {
    public interface IAdapterControl {
        void Enable(IAdapter adapter);
        void Disable(IAdapter adapter);
    }
}
