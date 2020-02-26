using System.Windows.Forms;

namespace Pauser.UI {
    public partial class FormMain : Form {
        //KeyboardHook _hook = new KeyboardHook();
        private ControlAdapters _controlAdapters;
        private ControlProcesses _controlProcesses;
        private ControlCombined _controlCombined;

        public FormMain() {
            InitializeComponent();

            this.CreateNetworksUI();
            this.CreateProcessesUI();
            this.CreateCombinedUI();

            this.tabControlMain.Selected += this.TabControlMain_Selected;

            // register the event that is fired after the key press.
            //_hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            // register the control + alt + F12 combination as hot key.
            //_hook.RegisterHotKey(Hooks.ModifierKeys.Control | Hooks.ModifierKeys.Alt, Keys.F12);
            //void hook_KeyPressed(object sender, KeyPressedEventArgs e) {
            //    // show the keys pressed in a label.
            //    this.Text = e.Modifier.ToString() + " + " + e.Key.ToString();
            //}
        }

        private void CreateNetworksUI() {
            var page = new TabPage("Network Adapters");
            this.tabControlMain.TabPages.Add(page);
            this._controlAdapters = new ControlAdapters();
            page.Controls.Add(this._controlAdapters);
            this._controlAdapters.Dock = DockStyle.Fill;
        }

        private void CreateProcessesUI() {
            var page = new TabPage("Processes");
            this.tabControlMain.TabPages.Add(page);
            this._controlProcesses = new ControlProcesses();
            page.Controls.Add(this._controlProcesses);
            this._controlProcesses.Dock = DockStyle.Fill;
        }

        private void CreateCombinedUI() {
            var page = new TabPage("Batch");
            this.tabControlMain.TabPages.Add(page);
            this._controlCombined = new ControlCombined();
            page.Controls.Add(this._controlCombined);
            this._controlCombined.Dock = DockStyle.Fill;
        }

        private void TabControlMain_Selected(object sender, TabControlEventArgs e) {
            if (e.Action == TabControlAction.Selected) {
                if (e.TabPageIndex == 2) {
                    this._controlAdapters.SaveSettings();
                    this._controlProcesses.SaveSettings();
                }
            }
        }
    }
}
