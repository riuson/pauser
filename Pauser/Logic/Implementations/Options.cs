using Pauser.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Pauser.Logic.Implementations {
    public class Options {
        private AdapterInfo[] _adapters;
        private Filter[] _filters;

        public Options() {
            this.Adapters = new AdapterInfo[] { };
            this.Filters = new Filter[] { };
        }

        [XmlElement("Adapters")]
        public AdapterInfo[] Adapters {
            get => this._adapters;
            set => this._adapters = value;
        }

        [XmlIgnore]
        public IEnumerable<IAdapterInfo> IAdapters {
            get => this._adapters?.Cast<IAdapterInfo>() ?? new IAdapterInfo[] { };
            set => this._adapters = value
                   .Select(x => new AdapterInfo(x))
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
    }
}
