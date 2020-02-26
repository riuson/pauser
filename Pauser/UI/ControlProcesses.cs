using Pauser.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
            var processes = this.FindProcesses(tableFilters);

            tableResults.Rows.Clear();

            foreach (var process in processes) {
                var row = tableResults.NewRow();
                row["ProcessName"] = process.ProcessName;
                row["FileName"] = process.MainModule?.FileName;
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

        private Process[] FindProcesses(DataTable tableFilters) {
            var filters = CollectFilters(tableFilters);
            var a = Process.GetProcesses();
            var processes = Process.GetProcesses()
                .Where(x => filters.Contains(x.ProcessName))
                .ToArray();

            return processes;
        }

        private static List<string> CollectFilters(DataTable tableFilters) {
            var filters = new List<string>();

            foreach (DataRow tableFiltersRow in tableFilters.Rows) {
                if (Convert.ToBoolean(tableFiltersRow["Selection"])) {
                    filters.Add(Convert.ToString(tableFiltersRow["Name"]));
                }
            }

            return filters;
        }

        public void Suspend() {
            var processes = this.FindProcesses(this._tableFitlers);

            foreach (var process in processes) {
                Utils.Processes.SuspendProcess(process);
            }
        }

        public void Resume() {
            var processes = this.FindProcesses(this._tableFitlers);

            foreach (var process in processes) {
                Utils.Processes.ResumeProcess(process);
            }
        }

        private void LoadSettings(DataTable table) {
            var filters = Saved<Options>.Instance.ProcessFilters;
            table.Rows.Clear();

            foreach (var filter in filters) {
                var row = table.NewRow();
                row["Name"] = filter.Name;
                row["Selection"] = filter.Selected;
                table.Rows.Add(row);
            }
        }

        private void SaveSettings(DataTable table) {
            var list = new List<ProcessFilter>();

            foreach (DataRow row in table.Rows) {
                list.Add(new ProcessFilter() {
                    Name = Convert.ToString(row["Name"]),
                    Selected = Convert.ToBoolean(row["Selection"])
                });
            }

            Saved<Options>.Instance.ProcessFilters = list.ToArray();
            Saved<Options>.Save();
        }
    }
}
