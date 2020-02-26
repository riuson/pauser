using Pauser.Logic.Interfaces;

namespace Pauser.Logic.Implementations {
    public class AdapterInfo : IAdapterInfo {
        public AdapterInfo() {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.DeviceId = string.Empty;
            this.NetConnectionId = string.Empty;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string DeviceId { get; set; }
        public string NetConnectionId { get; set; }
    }
}
