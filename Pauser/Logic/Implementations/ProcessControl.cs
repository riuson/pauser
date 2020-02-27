using Pauser.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Pauser.Logic.Implementations {
    public class ProcessControl : IProcessControl {
        #region WinAPI
        [Flags]
        private enum ThreadAccess : int {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);
        #endregion

        private readonly IProcessProvider _processProvider;
        private readonly IFilterActual _filterActual;

        public ProcessControl(IProcessProvider processProvider, IFilterActual filterActual) {
            this._processProvider = processProvider;
            this._filterActual = filterActual;
        }

        public void Suspend() {
            var filters = this._filterActual.Filters;
            var processes = this._processProvider.Find(filters);
            this.Suspend(processes);
        }

        public void Resume() {
            var filters = this._filterActual.Filters;
            var processes = this._processProvider.Find(filters);
            this.Resume(processes);
        }

        private void Suspend(IEnumerable<IProcessInfo> processInfos) {
            foreach (var process in processInfos) {
                this.SuspendProcess(process.Process);
            }
        }

        private void Resume(IEnumerable<IProcessInfo> processInfos) {
            foreach (var process in processInfos) {
                this.ResumeProcess(process.Process);
            }
        }

        private void SuspendProcess(Process process) {
            foreach (ProcessThread pT in process.Threads) {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero) {
                    continue;
                }

                SuspendThread(pOpenThread);

                CloseHandle(pOpenThread);
            }
        }

        private void ResumeProcess(Process process) {
            foreach (ProcessThread pT in process.Threads) {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero) {
                    continue;
                }

                int suspendCount;

                do {
                    suspendCount = ResumeThread(pOpenThread);
                } while (suspendCount > 0);

                CloseHandle(pOpenThread);
            }
        }
    }
}
