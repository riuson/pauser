using System.Windows.Forms;
using Pauser.Logic.Implementations;
using Pauser.Logic.Interfaces;

namespace Pauser.UI {
    public partial class FormMain : Form {
        //KeyboardHook _hook = new KeyboardHook();
        private ControlAdapters _controlAdapters;
        private ControlProcesses _controlProcesses;
        private ControlCombined _controlCombined;

        private IFilterActual _filterActual;
        private IFilterProvider _filterProvider;
        private IAdapterActual _adapterActual;
        private IAdapterProvider _adapterInfoProvider;
        private IAdapterControl _adapterControl;
        private IProcessProvider _processProvider;
        private IProcessControl _processControl;

        public FormMain() {
            InitializeComponent();

            this.CreateData();

            this.CreateNetworksUI();
            this.CreateProcessesUI();
            this.CreateCombinedUI();

            // register the event that is fired after the key press.
            //_hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            // register the control + alt + F12 combination as hot key.
            //_hook.RegisterHotKey(Hooks.ModifierKeys.Control | Hooks.ModifierKeys.Alt, Keys.F12);
            //void hook_KeyPressed(object sender, KeyPressedEventArgs e) {
            //    // show the keys pressed in a label.
            //    this.Text = e.Modifier.ToString() + " + " + e.Key.ToString();
            //}
        }

        private void CreateData() {
            this._filterProvider = new FilterProvider();
            this._filterActual = new FilterActual(this._filterProvider);
            this._filterActual.LoadSettings();

            this._adapterInfoProvider = new AdapterProvider();
            this._adapterControl = new AdapterControl();
            this._adapterActual = new AdapterActual(this._adapterInfoProvider);
            this._adapterActual.LoadSettings();

            this._processProvider = new ProcessProvider();
            this._processControl = new ProcessControl();
        }

        private void CreateNetworksUI() {
            var page = new TabPage("Network Adapters");
            this.tabControlMain.TabPages.Add(page);
            this._controlAdapters = new ControlAdapters(
                this._adapterActual,
                this._adapterControl);
            page.Controls.Add(this._controlAdapters);
            this._controlAdapters.Dock = DockStyle.Fill;
        }

        private void CreateProcessesUI() {
            var page = new TabPage("Processes");
            this.tabControlMain.TabPages.Add(page);
            this._controlProcesses = new ControlProcesses(
                this._filterActual,
                this._filterProvider,
                this._processProvider,
                this._processControl);
            page.Controls.Add(this._controlProcesses);
            this._controlProcesses.Dock = DockStyle.Fill;
        }

        private void CreateCombinedUI() {
            var page = new TabPage("Batch");
            this.tabControlMain.TabPages.Add(page);
            this._controlCombined = new ControlCombined(
                this._adapterActual,
                this._adapterControl,
                this._adapterInfoProvider,
                this._filterActual,
                this._filterProvider,
                this._processProvider,
                this._processControl);
            page.Controls.Add(this._controlCombined);
            this._controlCombined.Dock = DockStyle.Fill;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            this._filterActual.SaveSettings();
            this._adapterActual.SaveSettings();
        }
    }
}
