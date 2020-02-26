using Pauser.Properties;
using Pauser.Utils;
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
            var adapters = Network.GetAdapters();

            foreach (var adapter in adapters) {
                var row = table.NewRow();
                row["DeviceId"] = adapter.DeviceId;
                row["Name"] = adapter.Name;
                row["NetConnectionID"] = adapter.NetConnectionId;
                row["Description"] = adapter.Description;
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

            foreach (var adapter in adapters) {
                Network.EnableAdapter(adapter);
            }
        }

        private void DisableAdapters(object sender, EventArgs e) {
            var adapters = this.CollectSelectedAdapters();

            foreach (var adapter in adapters) {
                Network.DisableAdapter(adapter);
            }
        }

        private IEnumerable<string> CollectSelectedAdapters() {
            var result = new List<string>();

            foreach (DataRow row in this._table.Rows) {
                if (Convert.ToBoolean(row["Selection"])) {
                    result.Add(Convert.ToString(row["DeviceId"]));
                }
            }

            return result;
        }

        private void LoadSettings() {
            var adapters = Saved<Options>.Instance.NetworkAdapters;

            foreach (DataRow row in this._table.Rows) {
                if (adapters.Contains(Convert.ToString(row["DeviceId"]))) {
                    row["Selection"] = true;
                }
            }
        }

        private void SaveSettings() {
            Saved<Options>.Instance.NetworkAdapters = this.CollectSelectedAdapters().ToArray();
            Saved<Options>.Save();
        }
    }
}
