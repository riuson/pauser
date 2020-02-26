using System;
using System.Threading;

namespace SampleApp {
    class Echo {
        public int Run() {
            Console.WriteLine("Echo");
            Thread.Sleep(500);
            return 0;
        }
    }
}
