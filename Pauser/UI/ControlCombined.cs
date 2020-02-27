using Pauser.Logic.Interfaces;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = System.Tuple<int, string>;

namespace Pauser.UI {
    public partial class ControlCombined : UserControl {
        private readonly IAdapterActual _adapterActual;
        private readonly IAdapterControl _adapterControl;
        private readonly IAdapterProvider _adapterInfoProvider;
        private readonly IFilterActual _filterActual;
        private readonly IFilterProvider _filterProvider;
        private readonly IProcessProvider _processProvider;
        private readonly IProcessControl _processControl;
        private Task _task;
        private IProgress<Message> _progress;

        public ControlCombined(
            IAdapterActual adapterActual,
            IAdapterControl adapterControl,
            IAdapterProvider adapterInfoProvider,
            IFilterActual filterActual,
            IFilterProvider filterProvider,
            IProcessProvider processProvider,
            IProcessControl processControl) {
            this._adapterActual = adapterActual;
            this._adapterControl = adapterControl;
            this._adapterInfoProvider = adapterInfoProvider;
            this._filterActual = filterActual;
            this._filterProvider = filterProvider;
            this._processProvider = processProvider;
            this._processControl = processControl;
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
            var filters = this._filterActual.Filters;
            var processInfos = this._processProvider.Find(filters);
            this._processControl.Suspend(processInfos);
            var adapterInfos = this._adapterActual.Adapters.Where(x => x.Selected);

            foreach (var adapterInfo in adapterInfos) {
                this._adapterControl.Disable(adapterInfo);
            }

            this._processControl.Resume(processInfos);

            Thread.Sleep(10000);

            foreach (var adapterInfo in adapterInfos) {
                this._adapterControl.Enable(adapterInfo);
            }
        }

        private void ProgressHandler(Message msg) {
            this.progressBar.Value = msg.Item1;
            this.labelStatus.Text = msg.Item2;
        }
    }
}
