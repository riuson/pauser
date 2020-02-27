using Pauser.Logic.Interfaces;
using System;
using System.Management;
using System.Windows.Forms;

namespace Pauser.Logic.Implementations {
    public class AdapterControl : IAdapterControl {
        public void Enable(IAdapter adapter) {
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

        public void Disable(IAdapter adapter) {
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
