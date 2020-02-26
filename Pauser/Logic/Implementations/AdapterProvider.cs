using Pauser.Logic.Interfaces;
using Pauser.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace Pauser.Logic.Implementations {
    public class AdapterProvider : IAdapterInfoProvider {
        public IEnumerable<IAdapterInfo> FromStorage() => Saved<Options>.Instance.Adapters;

        public void ToStorage(IEnumerable<IAdapterInfo> list) {
            Saved<Options>.Instance.Adapters = list.OfType<AdapterInfo>().ToArray();
            Saved<Options>.Save();
        }

        public IEnumerable<IAdapterInfo> FromSystem() {
            var result = new List<IAdapterInfo>();

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

            return result;
        }
    }
}
