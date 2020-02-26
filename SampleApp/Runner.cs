using System;
using System.Threading;

namespace SampleApp {
    class Runner {
        public int Run(CancellationToken token) {
            while (!token.IsCancellationRequested) {
                Console.WriteLine("{0:yyyy-MM-ddTHH:mm:ss.ffff}", DateTime.Now);
                Thread.Sleep(10);
            }

            return 0;
        }
    }
}
