using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoImportador.Model
{
    public class Animal
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int IDDono { get; set; }
        public string NomeDono { get; set; }
        public string Raca { get; set; }
        public string Especie { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Pelagem { get; set; }
        public double Peso { get; set; }
        public string Cor { get; set; }
    }
}
