namespace Pauser.Logic.Interfaces {
    public interface IAdapter {
        string Name { get; }
        string Description { get; }
        string DeviceId { get; }
        string NetConnectionId { get; }
    }
}
