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
    public partial class ControlProcesses : UserControl {
        private readonly DataTable _tableFitlers;
        private readonly DataTable _tableResults;

        public ControlProcesses() {
            this.InitializeComponent();

            this._tableFitlers = this.CreateTableFilters();
            this._tableResults = this.CreateTableResults();
            this.FillResults(this._tableFitlers, this._tableResults);
            this.CreateUI();
            this.LoadSettings(this._tableFitlers);
        }

        private void BeforeDisposing() => this.SaveSettings(this._tableFitlers);

        private DataTable CreateTableFilters() {
            var result = new DataTable("filters");
            result.Columns.Add("Name", typeof(string)).DefaultValue = string.Empty;
            result.Columns.Add("Selection", typeof(bool)).DefaultValue = false;
            return result;
        }

        private DataTable CreateTableResults() {
            var result = new DataTable("results");
            result.Columns.Add("ProcessName", typeof(string));
            result.Columns.Add("FileName", typeof(string));
            return result;
        }

        private void FillResults(DataTable tableFilters, DataTable tableResults) {
            var filters = this.CollectFilters(tableFilters);
            IProcessProvider processProvider = new ProcessProvider();

            var processInfos = processProvider.Find(filters);

            tableResults.Rows.Clear();

            foreach (var processInfo in processInfos) {
                var row = tableResults.NewRow();
                row["ProcessName"] = processInfo.ProcessName;
                row["FileName"] = processInfo.FileName;
                tableResults.Rows.Add(row);
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
            commandRefresh.Click += (sender, args) => this.FillResults(this._tableFitlers, this._tableResults);
            commandRefresh.SetImage(Resources.Refresh);
            this.tableLayoutPanel1.Controls.Add(commandRefresh, 1, 2);

            this.dataGridViewFilters.AutoGenerateColumns = false;
            this.dataGridViewFilters.DataSource = this._tableFitlers;
            this.dataGridViewResults.AutoGenerateColumns = false;
            this.dataGridViewResults.DataSource = this._tableResults;
        }

        private IEnumerable<IFilter> CollectFilters(DataTable tableFilters) {
            var filters = new List<IFilter>();

            foreach (DataRow tableFiltersRow in tableFilters.Rows) {
                if (Convert.ToBoolean(tableFiltersRow["Selection"])) {
                    filters.Add(new Filter() {
                        Value = Convert.ToString(tableFiltersRow["Name"]),
                        Enabled = Convert.ToBoolean(tableFiltersRow["Selection"]),
                    });
                }
            }

            return filters;
        }

        public void Suspend() {
            var filters = this.CollectFilters(this._tableFitlers);
            IProcessControl processControl = new ProcessControl();
            IProcessProvider processProvider = new ProcessProvider();
            var processInfos = processProvider.Find(filters);
            processControl.Suspend(processInfos);
        }

        public void Resume() {
            var filters = this.CollectFilters(this._tableFitlers);
            IProcessControl processControl = new ProcessControl();
            IProcessProvider processProvider = new ProcessProvider();
            var processInfos = processProvider.Find(filters);
            processControl.Resume(processInfos);
        }

        private void LoadSettings(DataTable table) {
            IFilterProvider filterProvider = new FilterProvider();
            var filters = filterProvider.FromStorage();

            table.Rows.Clear();

            foreach (var filter in filters) {
                var row = table.NewRow();
                row["Name"] = filter.Value;
                row["Selection"] = filter.Enabled;
                table.Rows.Add(row);
            }
        }

        private void SaveSettings(DataTable table) {
            var list = new List<IFilter>();

            foreach (DataRow row in table.Rows) {
                list.Add(new Filter() {
                    Value = Convert.ToString(row["Name"]),
                    Enabled = Convert.ToBoolean(row["Selection"])
                });
            }

            IFilterProvider filterProvider = new FilterProvider();
            filterProvider.ToStorage(list);
        }
    }
}
