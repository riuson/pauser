using System.Diagnostics;

namespace Pauser.Logic.Interfaces {
    public interface IProcessInfo {
        string ProcessName { get; }
        string FileName { get; }
        Process Process { get; }
    }
}
