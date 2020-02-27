using Pauser.Logic.Interfaces;

namespace Pauser.Logic.Implementations {
    public class BatchOperaion : IBatchOperation {
        private static int _lastId = 0;

        public BatchOperaion() {
            this.Id = _lastId++;
            this.Operation = Operation.Delay;
            this.Argument = string.Empty;
        }

        public int Id { get; set; }
        public Operation Operation { get; set; }
        public string Argument { get; set; }
    }
}
