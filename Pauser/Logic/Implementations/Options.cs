using Pauser.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Pauser.Logic.Implementations {
    public class Options {
        private Adapter[] _adapters;
        private Filter[] _filters;
        private BatchOperaion[] _operations;

        public Options() {
            this.Adapters = new Adapter[] { };
            this.Filters = new Filter[] { };
            this.Operations = new BatchOperaion[] { };
        }

        [XmlElement("Adapters")]
        public Adapter[] Adapters {
            get => this._adapters;
            set => this._adapters = value;
        }

        [XmlIgnore]
        public IEnumerable<IAdapter> IAdapters {
            get => this._adapters?.Cast<IAdapter>() ?? new IAdapter[] { };
            set => this._adapters = value
                   .Select(x => new Adapter(x))
                   .ToArray();
        }

        [XmlElement("Filters")]
        public Filter[] Filters {
            get => this._filters;
            set => this._filters = value;
        }

        [XmlIgnore]
        public IEnumerable<IFilter> IFilters {
            get => this._filters?.Cast<IFilter>() ?? new IFilter[] { };
            set => this._filters = value.OfType<Filter>().ToArray();
        }

        [XmlElement("Operations")]
        public BatchOperaion[] Operations {
            get => this._operations;
            set => this._operations = value;
        }

        [XmlIgnore]
        public IEnumerable<IBatchOperation> IBatchOperations {
            get => this._operations?.Cast<IBatchOperation>() ?? new IBatchOperation[] { };
            set => this._operations = value.OfType<BatchOperaion>().ToArray();
        }
    }
}
