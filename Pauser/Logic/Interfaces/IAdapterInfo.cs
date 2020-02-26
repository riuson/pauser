namespace Pauser.Logic.Interfaces {
    public interface IAdapterInfo {
        string Name { get; }
        string Description { get; }
        string DeviceId { get; }
        string NetConnectionId { get; }
    }
}
