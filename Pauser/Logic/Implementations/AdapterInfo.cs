using Pauser.Logic.Interfaces;

namespace Pauser.Logic.Implementations {
    public class AdapterInfo : IAdapterInfo {
        public AdapterInfo() {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.DeviceId = string.Empty;
            this.NetConnectionId = string.Empty;
        }

        public AdapterInfo(IAdapterInfo other) {
            this.Name = other.Name;
            this.Description = other.Description;
            this.DeviceId = other.DeviceId;
            this.NetConnectionId = other.NetConnectionId;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string DeviceId { get; set; }
        public string NetConnectionId { get; set; }
    }
}
