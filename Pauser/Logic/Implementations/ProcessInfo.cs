using Pauser.Logic.Interfaces;
using System.Diagnostics;

namespace Pauser.Logic.Implementations {
    public class ProcessInfo : IProcessInfo {
        public ProcessInfo() {
            this.ProcessName = string.Empty;
            this.FileName = string.Empty;
            this.Process = null;
        }

        public string ProcessName { get; set; }
        public string FileName { get; set; }
        public Process Process { get; set; }
    }
}
