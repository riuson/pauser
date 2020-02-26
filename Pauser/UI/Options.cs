namespace Pauser.UI {
    public class Options {
        public Options() {
            this.NetworkAdapters = new string[] { };
            this.ProcessFilters = new ProcessFilter[] { };
        }

        public string[] NetworkAdapters { get; set; }

        public ProcessFilter[] ProcessFilters { get; set; }
    }

    public class ProcessFilter {
        public ProcessFilter() {
            this.Name = string.Empty;
            this.Selected = false;
        }

        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
