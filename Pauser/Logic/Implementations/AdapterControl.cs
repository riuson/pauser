using Pauser.Logic.Interfaces;
using System;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace Pauser.Logic.Implementations {
    public class AdapterControl : IAdapterControl {
        private readonly IAdapterActual _adapterActual;

        public AdapterControl(IAdapterActual adapterActual) {
            this._adapterActual = adapterActual;
        }

        public void Enable() {
            foreach (var adapter in this._adapterActual.Adapters.Where(x => x.Selected)) {
                this.Enable(adapter);
            }
        }

        public void Disable() {
            foreach (var adapter in this._adapterActual.Adapters.Where(x => x.Selected)) {
                this.Disable(adapter);
            }
        }

        private void Enable(IAdapter adapter) {
            try {
                var classInstance =
                    new ManagementObject("root\\CIMV2",
                        $"Win32_NetworkAdapter.DeviceID='{adapter.DeviceId}'",
                        null);

                var outParams = classInstance.InvokeMethod("Enable", null, null);

                var result = Convert.ToInt32(outParams["ReturnValue"]);
            } catch (ManagementException err) {
                MessageBox.Show("An error occurred while trying to execute the WMI method: " + err.Message);
            }
        }

        private void Disable(IAdapter adapter) {
            try {
                var classInstance =
                    new ManagementObject("root\\CIMV2",
                        $"Win32_NetworkAdapter.DeviceID='{adapter.DeviceId}'",
                        null);

                var outParams = classInstance.InvokeMethod("Disable", null, null);

                var result = Convert.ToInt32(outParams["ReturnValue"]);
            } catch (ManagementException err) {
                MessageBox.Show("An error occurred while trying to execute the WMI method: " + err.Message);
            }
        }
    }
}
