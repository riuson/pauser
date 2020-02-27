using Pauser.Logic.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;
using Message = System.Tuple<int, string>;

namespace Pauser.UI {
    public partial class ControlCombined : UserControl {
        private readonly IBatchOperationControl _batchOperationControl;
        private readonly IBatchOperationActual _batchOperationActual;
        private IProgress<Message> _progress;
        private CommandLink _commandStart;

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

            this._progress = new Progress<Message>(this.ProgressHandler);

            this.ColumnOperation.DataSource = Enum.GetValues(typeof(Operation));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = this._batchOperationActual.Operations;
        }

        private async void StartSequence(object sender, EventArgs e) {
            this._commandStart.Enabled = false;
            await this._batchOperationControl.ExecuteAsync();
            this._commandStart.Enabled = true;
        }

        private void ProgressHandler(Message msg) {
            //this.progressBar.Value = msg.Item1;
            //this.labelStatus.Text = msg.Item2;
        }
    }
}
