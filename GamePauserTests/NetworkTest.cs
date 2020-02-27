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
            IAdapterProvider adapterProvider = new AdapterProvider();
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
            IAdapterProvider adapterProvider = new AdapterProvider();
            IAdapterActual adapterActual = new AdapterActual(adapterProvider);
            IAdapterControl adapterControl = new AdapterControl(adapterActual);
            var adapterInfo = adapterProvider.FromSystem()
                .FirstOrDefault(x => x.DeviceId == "4"); // Main adapter on my PC.

            adapterInfo.Selected = true;
            adapterActual.Adapters.Add(adapterInfo);

            var result = this.CheckPing();
            Assert.That(result, Is.True);

            adapterControl.Disable();

            Thread.Sleep(5000);

            result = this.CheckPing();
            Assert.That(result, Is.False);

            adapterControl.Enable();

            Thread.Sleep(5000);

            result = this.CheckPing();
            Assert.That(result, Is.True);
        }
    }
}
