using NUnit.Framework;
using Pauser.Utils;
using System.Net.NetworkInformation;

namespace GamePauserTests {
    class NetworkTest {
        [Test]
        public void CanFindAdapters() {
            var adapters = Network.GetAdapters();
            Assert.That(adapters.Length, Is.GreaterThan(0));
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
            var result = this.CheckPing();
            Assert.That(result, Is.True);

            Network.DisableAdapter("4");

            result = this.CheckPing();
            Assert.That(result, Is.False);


            Network.EnableAdapter("4");

            result = this.CheckPing();
            Assert.That(result, Is.True);
        }
    }
}
