using Pauser.Logic.Interfaces;

namespace Pauser.Logic.Implementations {
    public class Filter : IFilter {
        public Filter() {
            this.Enabled = false;
            this.Value = string.Empty;
        }

        public bool Enabled { get; set; }
        public string Value { get; set; }
    }
}
