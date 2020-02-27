using Pauser.Logic.Interfaces;
using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = System.Tuple<int, string>;

namespace Pauser.UI {
    public partial class ControlCombined : UserControl {
        private readonly IBatchOperationControl _batchOperationControl;
        private readonly IBatchOperationActual _batchOperationActual;
        private CommandLink _commandStart;
        private TaskScheduler _contextUI;

        public ControlCombined(
            IBatchOperationControl batchOperationControl,
            IBatchOperationActual batchOperationActual) {
            this._batchOperationControl = batchOperationControl;
            this._batchOperationActual = batchOperationActual;
            this.InitializeComponent();
            this.CreateUI();
        }

        private void CreateUI() {
            this._commandStart = new CommandLink {
                Text = "Start",
                Description = "Execute required operations now!",
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Size = new Size(250, 70)
            };
            this._commandStart.Click += this.StartSequence;
            this.tableLayoutPanel1.Controls.Add(this._commandStart, 1, 0);

            this.ColumnOperation.DataSource = Enum.GetValues(typeof(Operation));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = this._batchOperationActual.Operations;

            this._contextUI = TaskScheduler.FromCurrentSynchronizationContext();
            this._batchOperationControl.Progress += BatchOperationControlOnProgress;
        }

        private async void StartSequence(object sender, EventArgs e) {
            this._commandStart.Enabled = false;
            await this._batchOperationControl.ExecuteAsync();
            this.HighlightOperationRemove();
            this._commandStart.Enabled = true;
        }

        private void BatchOperationControlOnProgress(object sender, BatchOperationsProgress e) =>
            Task.Factory.StartNew(o => this.HighlightOperation((IBatchOperation)o), e.Operation, CancellationToken.None, TaskCreationOptions.None, this._contextUI);

        private void HighlightOperation(IBatchOperation operation) {
            foreach (DataGridViewRow dataGridViewRow in this.dataGridView1.Rows) {
                if (dataGridViewRow.DataBoundItem is IBatchOperation rowOperation) {
                    if (rowOperation.Id == operation.Id) {
                        dataGridViewRow.DefaultCellStyle.BackColor = Color.Blue;
                        dataGridViewRow.DefaultCellStyle.ForeColor = Color.White;
                    } else {
                        dataGridViewRow.DefaultCellStyle.BackColor = Color.White;
                        dataGridViewRow.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void HighlightOperationRemove() {
            foreach (DataGridViewRow dataGridViewRow in this.dataGridView1.Rows) {
                if (dataGridViewRow.DataBoundItem is IBatchOperation rowOperation) {
                    dataGridViewRow.DefaultCellStyle.BackColor = Color.White;
                    dataGridViewRow.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
    }
}
