namespace Pauser.Logic.Interfaces {
    public interface IBatchOperation {
        int Id { get; set; }
        Operation Operation { get; set; }
        string Argument { get; set; }
    }

    public enum Operation {
        DisableNetworks,
        EnableNetworks,
        SuspendProcesses,
        ResumeProcesses,
        Delay
    }
}
