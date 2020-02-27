using Pauser.Logic.Interfaces;
using Pauser.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pauser.UI {
    public partial class ControlAdapters : UserControl {
        private readonly IAdapterActual _adapterActual;
        private readonly IAdapterControl _adapterControl;

        public ControlAdapters(
            IAdapterActual adapterActual,
            IAdapterControl adapterControl) {
            this.InitializeComponent();

            this._adapterActual = adapterActual;
            this._adapterControl = adapterControl;
            this._adapterActual.UpdateList();
            this.CreateUI();
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
            this.dataGridViewAdapters.DataSource = this._adapterActual.Adapters;
        }

        private void EnableAdapters(object sender, EventArgs e) {
            this._adapterControl.Enable();
        }

        private void DisableAdapters(object sender, EventArgs e) {
            this._adapterControl.Disable();
        }

        private IEnumerable<IAdapter> CollectSelectedAdapters() =>
            this._adapterActual.Adapters
                .Where(x => x.Selected)
                .ToArray();
    }
}
