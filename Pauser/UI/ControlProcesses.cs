using Pauser.Logic.Implementations;
using Pauser.Logic.Interfaces;
using Pauser.Properties;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pauser.UI {
    public partial class ControlProcesses : UserControl {
        private readonly BindingList<IFilter> _filters;
        private readonly BindingList<IProcessInfo> _processes;

        public ControlProcesses() {
            this.InitializeComponent();

            this._filters = new BindingList<IFilter> {
                AllowNew = true,
                AllowRemove = true,
                AllowEdit = true
            };
            this._filters.AddingNew += this._filters_AddingNew;
            this._processes = new BindingList<IProcessInfo>();
            this.CreateUI();
            this.LoadSettings();
            this.FillProcesses();
        }

        private void _filters_AddingNew(object sender, AddingNewEventArgs e) {
            e.NewObject = new Filter();
        }

        private void BeforeDisposing() => this.SaveSettings();

        private void FillProcesses() {
            var filters = this._filters.Where(x => x.Enabled).ToArray();
            IProcessProvider processProvider = new ProcessProvider();

            var processInfos = processProvider.Find(filters);

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
            this.dataGridViewFilters.DataSource = this._filters;
            this.dataGridViewResults.AutoGenerateColumns = false;
            this.dataGridViewResults.DataSource = this._processes;
        }

        public void Suspend() {
            var filters = this._filters.Where(x => x.Enabled).ToArray();
            IProcessControl processControl = new ProcessControl();
            IProcessProvider processProvider = new ProcessProvider();
            var processInfos = processProvider.Find(filters);
            processControl.Suspend(processInfos);
        }

        public void Resume() {
            var filters = this._filters.Where(x => x.Enabled).ToArray();
            IProcessControl processControl = new ProcessControl();
            IProcessProvider processProvider = new ProcessProvider();
            var processInfos = processProvider.Find(filters);
            processControl.Resume(processInfos);
        }

        private void LoadSettings() {
            IFilterProvider filterProvider = new FilterProvider();
            var filters = filterProvider.FromStorage();

            this._filters.Clear();

            foreach (var filter in filters) {
                this._filters.Add(filter);
            }
        }

        private void SaveSettings() {
            var list = this._filters.ToArray();
            IFilterProvider filterProvider = new FilterProvider();
            filterProvider.ToStorage(list);
        }
    }
}
