using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamePauserTests {
    class ProcessesTest {
        private readonly string _appname = "SampleApp";
        private string _path;

        [OneTimeSetUp]
        public void SetUp() {
            this._path = Path.Combine(this.GetDirectory(), $"{this._appname}.exe");
        }

        private string GetDirectory() {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            path = Path.GetDirectoryName(path);
            return path;
        }

        [Test]
        public void CanRunProcess() {
            var startInfo = new ProcessStartInfo(this._path, "echo") {
                UseShellExecute = false
            };

            var process = new Process { StartInfo = startInfo };
            var t1 = DateTime.Now;
            var started = process.Start();

            Assert.That(started, Is.True);

            var success = process.WaitForExit(2000);
            var t2 = DateTime.Now;

            Assert.That(success, Is.True);
            Assert.That(process.HasExited, Is.True);
            Assert.That(t2 - t1, Is.LessThan(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void CanKillProcess() {
            var startInfo = new ProcessStartInfo(this._path, "long") {
                UseShellExecute = false
            };

            var process = new Process { StartInfo = startInfo };
            var t1 = DateTime.Now;
            var started = process.Start();

            Assert.That(started, Is.True);

            process.Kill();
            var t2 = DateTime.Now;

            Assert.That(process.HasExited, Is.True);
            Assert.That(t2 - t1, Is.LessThan(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void CanFindProcess() {
            var startInfo = new ProcessStartInfo(this._path, "long") {
                UseShellExecute = false
            };

            var process = new Process { StartInfo = startInfo };
            var started = process.Start();
            var processes = Process.GetProcessesByName(_appname);

            Assert.That(started, Is.True);
            Assert.That(processes.Length, Is.GreaterThan(0));

            process.Kill();
        }

        [Test]
        public void CanReadOutput() {
            var startInfo = new ProcessStartInfo(this._path, "echo") {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.Default
            };

            var process = new Process { StartInfo = startInfo };

            var started = process.Start();
            var outputReader = process.StandardOutput;

            Assert.That(started, Is.True);

            var readed = outputReader.ReadToEnd();
            readed = readed.Trim('\r', '\n', ' ');

            Assert.That(readed, Is.EqualTo("Echo"));
        }

        [Test]
        public void CanSuspendResume() {
            var startInfo = new ProcessStartInfo(this._path, "runner") {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.Default
            };

            var process = new Process { StartInfo = startInfo };

            var started = process.Start();
            var outputReader = process.StandardOutput;

            Assert.That(started, Is.True);

            var list = new List<DateTime>();
            var needRead = true;

            var task = Task.Run(() => {
                while (needRead) {
                    var str = outputReader.ReadLine();

                    if (DateTime.TryParseExact(str, "yyyy-MM-ddTHH:mm:ss.ffff", CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeLocal, out var dt)) {
                        list.Add(dt);
                    }
                }
            });

            Thread.Sleep(200);
            Pauser.Utils.Processes.SuspendProcess(process);
            Thread.Sleep(2000);
            var stopTime = DateTime.Now;
            Pauser.Utils.Processes.ResumeProcess(process);
            Thread.Sleep(200);
            needRead = false;
            task.Wait();

            process.Kill();

            var timeBefore = list.LastOrDefault(x => x <= stopTime);
            var timeAfter = list.FirstOrDefault(x => x >= stopTime);

            Assert.That(timeAfter - timeBefore, Is.GreaterThan(TimeSpan.FromSeconds(1.9)));
        }
    }
}
