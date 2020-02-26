using Pauser.Logic.Implementations;
using Pauser.Logic.Interfaces;
using Pauser.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pauser.UI {
    public partial class ControlAdapters : UserControl {
        private readonly BindingList<AdapterInfoItem> _adapters;

        public ControlAdapters() {
            this.InitializeComponent();

            this._adapters = new BindingList<AdapterInfoItem>();
            this.FillList();
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

        private void FillList() {
            IAdapterInfoProvider adapterProvider = new AdapterInfoProvider();
            var systemAdapters = adapterProvider.FromSystem();
            this._adapters.Clear();

            foreach (var adapter in systemAdapters) {
                this._adapters.Add(new AdapterInfoItem(adapter) {
                    Selected = false
                });
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
            this.dataGridViewAdapters.DataSource = this._adapters;
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

        private IEnumerable<IAdapterInfo> CollectSelectedAdapters() =>
            this._adapters.Where(x => x.Selected).ToArray();

        private void LoadSettings() {
            IAdapterInfoProvider adapterInfoProvider = new AdapterInfoProvider();
            var adapters = adapterInfoProvider.FromStorage();
            var ids = adapters.Select(x => x.DeviceId).ToArray();

            foreach (var item in this._adapters) {
                item.Selected = ids.Contains(Convert.ToString(item.DeviceId));
            }
        }

        public void SaveSettings() {
            IAdapterInfoProvider adapterInfoProvider = new AdapterInfoProvider();
            adapterInfoProvider.ToStorage(this.CollectSelectedAdapters());
        }

        private class AdapterInfoItem : IAdapterInfo {
            public AdapterInfoItem(IAdapterInfo info) {
                this.Name = info.Name;
                this.Description = info.Description;
                this.DeviceId = info.DeviceId;
                this.NetConnectionId = info.NetConnectionId;
                this.Selected = false;
            }

            public string Name { get; set; }
            public string Description { get; set; }
            public string DeviceId { get; set; }
            public string NetConnectionId { get; set; }
            public bool Selected { get; set; }
        }
    }
}
