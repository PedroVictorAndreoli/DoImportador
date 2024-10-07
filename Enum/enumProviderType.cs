using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoImportador.Enum
{
    public enum enumProviderType
    {
        Auto = -1,
        SQLServer = 0,
        PostGreSQL = 1
    }

    public enum EnumDataLake
    {
        ORIGIN,
        DESTINATION
    }
}
