using Pauser.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace Pauser.Logic.Implementations {
    public class AdapterProvider : IAdapterProvider {
        public IEnumerable<IAdapter> FromStorage() => Saved<Options>.Instance.IAdapters;

        public void ToStorage(IEnumerable<IAdapter> list) {
            Saved<Options>.Instance.IAdapters = list?.ToArray() ?? new IAdapter[] { };
            Saved<Options>.Save();
        }

        public IEnumerable<IAdapter> FromSystem() {
            var result = new List<IAdapter>();

            try {
                var searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT * FROM Win32_NetworkAdapter");

                foreach (var queryObj in searcher.Get()) {
                    Console.WriteLine("Description: {0}", queryObj["Description"]);
                    Console.WriteLine("DeviceID: {0}", queryObj["DeviceID"]);
                    Console.WriteLine("Name: {0}", queryObj["Name"]);
                    Console.WriteLine("NetConnectionID: {0}", queryObj["NetConnectionID"]);
                    result.Add(new Adapter() {
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
