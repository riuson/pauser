using Pauser.Logic.Interfaces;

namespace Pauser.Logic.Implementations {
    public class Adapter : IAdapter {
        public Adapter() {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.DeviceId = string.Empty;
            this.NetConnectionId = string.Empty;
            this.Selected = false;
        }

        public Adapter(IAdapter other) {
            this.Name = other.Name;
            this.Description = other.Description;
            this.DeviceId = other.DeviceId;
            this.NetConnectionId = other.NetConnectionId;
            this.Selected = other.Selected;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string DeviceId { get; set; }
        public string NetConnectionId { get; set; }
        public bool Selected { get; set; }
    }
}
