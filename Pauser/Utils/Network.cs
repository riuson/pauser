using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Forms;

namespace Pauser.Utils {
    public static class Network {
        public class AdapterInfo {
            public string Name { get; internal set; }
            public string Description { get; internal set; }
            public string DeviceId { get; internal set; }
            public string NetConnectionId { get; internal set; }
        }

        public static AdapterInfo[] GetAdapters() {
            var result = new List<AdapterInfo>();

            try {
                var searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT * FROM Win32_NetworkAdapter");

                foreach (var queryObj in searcher.Get()) {
                    Console.WriteLine("Description: {0}", queryObj["Description"]);
                    Console.WriteLine("DeviceID: {0}", queryObj["DeviceID"]);
                    Console.WriteLine("Name: {0}", queryObj["Name"]);
                    Console.WriteLine("NetConnectionID: {0}", queryObj["NetConnectionID"]);
                    result.Add(new AdapterInfo() {
                        Name = Convert.ToString(queryObj["Name"]),
                        Description = Convert.ToString(queryObj["Description"]),
                        DeviceId = Convert.ToString(queryObj["DeviceID"]),
                        NetConnectionId = Convert.ToString(queryObj["NetConnectionID"]),
                    });
                }
            } catch (ManagementException e) {
                MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
            }

            return result.ToArray();
        }

        public static void DisableAdapter(string deviceId) {
            try {
                var classInstance =
                    new ManagementObject("root\\CIMV2",
                        $"Win32_NetworkAdapter.DeviceID='{deviceId}'",
                        null);

                var outParams = classInstance.InvokeMethod("Disable", null, null);

                var result = Convert.ToInt32(outParams["ReturnValue"]);
            } catch (ManagementException err) {
                MessageBox.Show("An error occurred while trying to execute the WMI method: " + err.Message);
            }
        }

        public static void EnableAdapter(string deviceId) {
            try {
                var classInstance =
                    new ManagementObject("root\\CIMV2",
                        $"Win32_NetworkAdapter.DeviceID='{deviceId}'",
                        null);

                var outParams = classInstance.InvokeMethod("Enable", null, null);

                var result = Convert.ToInt32(outParams["ReturnValue"]);
            } catch (ManagementException err) {
                MessageBox.Show("An error occurred while trying to execute the WMI method: " + err.Message);
            }
        }
    }
}
