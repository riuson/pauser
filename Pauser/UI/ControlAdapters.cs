using Pauser.Logic.Implementations;
using Pauser.Logic.Interfaces;
using Pauser.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pauser.UI {
    public partial class ControlAdapters : UserControl {
        private readonly DataTable _table;

        public ControlAdapters() {
            this.InitializeComponent();

            this._table = this.CreateTable();
            this.FillTable(this._table);
            this.CreateUI();
            this.LoadSettings();
        }

        private void BeforeDisposing() => this.SaveSettings();

        private DataTable CreateTable() {
            var result = new DataTable("adapters");
            result.Columns.Add("DeviceId", typeof(string));
            result.Columns.Add("NetConnectionID", typeof(string));
            result.Columns.Add("Name", typeof(string));
            result.Columns.Add("Description", typeof(string));
            result.Columns.Add("Selection", typeof(bool)).DefaultValue = false;
            return result;
        }

        private void FillTable(DataTable table) {
            IAdapterInfoProvider adapterProvider = new AdapterProvider();
            var systemAdapters = adapterProvider.FromSystem();

            foreach (var adapter in systemAdapters) {
                var row = table.NewRow();
                row["DeviceId"] = adapter.DeviceId;
                row["Name"] = adapter.Name;
                row["NetConnectionID"] = adapter.NetConnectionId;
                row["Description"] = adapter.Description;
                row["Selection"] = false;
                table.Rows.Add(row);
            }
        }

        private void CreateUI() {
            var commandEnable = new CommandLink {
                Text = "Enable",
                Description = "Enable selected network adapters",
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Size = new Size(250, 70)
            };
            commandEnable.SetImage(Resources.PowerOn);
            commandEnable.Click += this.EnableAdapters;
            this.tableLayoutPanel1.Controls.Add(commandEnable, 1, 0);

            var commandDisable = new CommandLink {
                Text = "Disable",
                Description = "Disable selected network adapters",
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Size = new Size(250, 70)
            };
            commandDisable.SetImage(Resources.PowerOff);
            commandDisable.Click += this.DisableAdapters;
            this.tableLayoutPanel1.Controls.Add(commandDisable, 1, 1);

            this.dataGridViewAdapters.AutoGenerateColumns = false;
            this.dataGridViewAdapters.DataSource = this._table;
        }

        private void EnableAdapters(object sender, EventArgs e) {
            var adapters = this.CollectSelectedAdapters();
            IAdapterControl adapterControl = new AdapterControl();

            foreach (var adapter in adapters) {
                adapterControl.Enable(adapter);
            }
        }

        private void DisableAdapters(object sender, EventArgs e) {
            var adapters = this.CollectSelectedAdapters();
            IAdapterControl adapterControl = new AdapterControl();

            foreach (var adapter in adapters) {
                adapterControl.Disable(adapter);
            }
        }

        private IEnumerable<IAdapterInfo> CollectSelectedAdapters() {
            var result = new List<IAdapterInfo>();

            foreach (DataRow row in this._table.Rows) {
                if (Convert.ToBoolean(row["Selection"])) {
                    result.Add(new AdapterInfo() {
                        Name = Convert.ToString(row["Name"]),
                        Description = Convert.ToString(row["Description"]),
                        DeviceId = Convert.ToString(row["DeviceId"]),
                        NetConnectionId = Convert.ToString(row["NetConnectionId"]),
                    });
                }
            }

            return result;
        }

        private void LoadSettings() {
            IAdapterInfoProvider adapterInfoProvider = new AdapterProvider();
            var adapters = adapterInfoProvider.FromStorage();
            var ids = adapters.Select(x => x.DeviceId).ToArray();

            foreach (DataRow row in this._table.Rows) {
                row["Selection"] = ids.Contains(Convert.ToString(row["DeviceId"]));
            }
        }

        private void SaveSettings() {
            IAdapterInfoProvider adapterInfoProvider = new AdapterProvider();
            adapterInfoProvider.ToStorage(this.CollectSelectedAdapters());
        }
    }
}
