using Pauser.Logic.Interfaces;
using Pauser.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Pauser.UI {
    public partial class ControlProcesses : UserControl {
        private readonly IFilterActual _filterActual;
        private readonly IProcessProvider _processProvider;
        private readonly IProcessControl _processControl;
        private readonly BindingList<IProcessInfo> _processes;

        public ControlProcesses(
            IFilterActual filterActual,
            IProcessProvider processProvider,
            IProcessControl processControl) {
            this.InitializeComponent();

            this._filterActual = filterActual;
            this._processProvider = processProvider;
            this._processControl = processControl;
            this._processes = new BindingList<IProcessInfo>();
            this.CreateUI();
            this.FillProcesses();
        }

        private void FillProcesses() {
            var processInfos = this._processProvider.Find(this._filterActual.Filters);
            this._processes.Clear();

            foreach (var processInfo in processInfos) {
                this._processes.Add(processInfo);
            }
        }

        private void CreateUI() {
            var commandSuspend = new CommandLink {
                Text = "Suspend",
                Description = "Suspend processes with matched name",
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Size = new Size(250, 70)
            };
            commandSuspend.Click += (sender, args) => this.Suspend();
            commandSuspend.SetImage(Resources.Pause);
            this.tableLayoutPanel1.Controls.Add(commandSuspend, 1, 0);

            var commandResume = new CommandLink {
                Text = "Resume",
                Description = "Resume processes with matched name",
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Size = new Size(250, 70)
            };
            commandResume.Click += (sender, args) => this.Resume();
            commandResume.SetImage(Resources.Start);
            this.tableLayoutPanel1.Controls.Add(commandResume, 1, 1);

            var commandRefresh = new CommandLink {
                Text = "Refresh",
                Description = "Refresh list of matched processes",
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Size = new Size(250, 70)
            };
            commandRefresh.Click += (sender, args) => this.FillProcesses();
            commandRefresh.SetImage(Resources.Refresh);
            this.tableLayoutPanel1.Controls.Add(commandRefresh, 1, 2);

            this.dataGridViewFilters.AutoGenerateColumns = false;
            this.dataGridViewFilters.DataSource = this._filterActual.Filters;
            this.dataGridViewResults.AutoGenerateColumns = false;
            this.dataGridViewResults.DataSource = this._processes;
        }

        public void Suspend() => this._processControl.Suspend();

        public void Resume() => this._processControl.Resume();
    }
}
