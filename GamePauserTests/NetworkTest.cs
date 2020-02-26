using NUnit.Framework;
using Pauser.Logic.Implementations;
using Pauser.Logic.Interfaces;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;

namespace GamePauserTests {
    class NetworkTest {
        [Test]
        public void CanFindAdapters() {
            IAdapterInfoProvider adapterProvider = new AdapterInfoProvider();
            var adapters = adapterProvider.FromSystem();
            Assert.That(adapters.Count(), Is.GreaterThan(0));
        }

        private bool CheckPing() {
            try {
                var ping = new Ping();
                var reply = ping.Send("8.8.8.8");

                return reply.Status == IPStatus.Success;
            } catch {
                return false;
            }
        }

        [Test]
        public void CanPing() {
            var result = this.CheckPing();
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanControlAdapter() {
            IAdapterControl adapterControl = new AdapterControl();
            IAdapterInfoProvider adapterProvider = new AdapterInfoProvider();
            var adapterInfo = adapterProvider.FromSystem()
                .FirstOrDefault(x => x.DeviceId == "4"); // Main adapter on my PC.

            var result = this.CheckPing();
            Assert.That(result, Is.True);

            adapterControl.Disable(adapterInfo);

            Thread.Sleep(5000);

            result = this.CheckPing();
            Assert.That(result, Is.False);

            adapterControl.Enable(adapterInfo);

            Thread.Sleep(5000);

            result = this.CheckPing();
            Assert.That(result, Is.True);
        }
    }
}
