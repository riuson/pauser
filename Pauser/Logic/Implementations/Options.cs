namespace Pauser.Logic.Implementations {
    public class Options {
        public Options() {
            this.Adapters = new AdapterInfo[] { };
            this.Filters = new Filter[] { };
        }

        public AdapterInfo[] Adapters { get; set; }

        public Filter[] Filters { get; set; }
    }
}
