using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = System.Tuple<int, string>;

namespace Pauser.UI {
    public partial class ControlCombined : UserControl {
        private Task _task;
        private IProgress<Message> _progress;

        public ControlCombined() {
            this.InitializeComponent();
            this.CreateUI();
        }

        private void CreateUI() {
            var commandStart = new CommandLink {
                Text = "Start",
                Description = "Execute required operations now!",
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Size = new Size(250, 70)
            };
            commandStart.Click += this.StartSequence;
            this.tableLayoutPanel1.Controls.Add(commandStart, 1, 1);

            this._progress = new Progress<Message>(this.ProgressHandler);
        }

        private void BeforeDisposing() {
            //throw new NotImplementedException();
        }

        private void StartSequence(object sender, EventArgs e) {
            //var filters = 
            this._task = Task.Factory.StartNew(this.Sequence);
        }

        private void Sequence() {
            throw new NotImplementedException();
        }

        private void ProgressHandler(Message msg) {
            this.progressBar.Value = msg.Item1;
            this.labelStatus.Text = msg.Item2;
        }
    }
}
