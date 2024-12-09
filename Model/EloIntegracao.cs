using DoImportador.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoImportador.Model
{
    public class EloIntegracao
    {
        public int Id { get; set; }
        public string eloIntegracao { get; set; }
        public string eloIntegracaoVariant { get; set; }
        public EnumVinculoIntegracao vinculoIntegracao { get; set; }
        public EnumIntegracao integracao { get; set; }
    }
}
