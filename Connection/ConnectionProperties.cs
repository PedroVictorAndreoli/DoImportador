using DoImportador.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoImportador.Connection
{
    public class ConnectionProperties
    {
        public string hostOrigin { get; set; } = "";
        public string portOrigin { get; set; } = "";
        public string passwordOrigin { get; set; } = "";
        public string userOrigin { get; set; } = "";
        public string dbNameOrigin { get; set; } = "";
        public EnumProviderType dbType { get; set; }

        public string hostDestination { get; set; } = "";
        public string passwordDestination { get; set; } = "";
        public string userDestination { get; set; } = "";
        public string dbNameDestination { get; set; } = "";
    }
}
