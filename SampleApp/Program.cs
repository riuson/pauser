using System.Linq;
using System.Threading;

namespace SampleApp {
    class Program {
        static int Main(string[] args) {
            if (args.Contains("echo")) {
                var app = new Echo();
                return app.Run();
            }

            if (args.Contains("long")) {
                var app = new Long();
                return app.Run();
            }

            if (args.Contains("runner")) {
                var token = new CancellationToken();
                var app = new Runner();
                return app.Run(token);
            }

            return -1;
        }
    }
}
