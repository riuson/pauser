using Pauser.Logic.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pauser.Logic.Implementations {
    public class ProcessProvider : IProcessProvider {
        public IEnumerable<IProcessInfo> Find(IEnumerable<IFilter> filters) {
            var names = filters.Select(x => x.Value).ToArray();

            var processes = Process.GetProcesses()
                .Where(x => names.Contains(x.ProcessName))
                .Select(x => new ProcessInfo() {
                    FileName = x.MainModule?.FileName ?? string.Empty,
                    ProcessName = x.ProcessName,
                    Process = x
                })
                .ToArray();

            return processes;
        }
    }
}
