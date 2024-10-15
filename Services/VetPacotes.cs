using DoImportador.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoImportador.Services
{
    internal class VetPacotes 
    {

        string fileName = "vet_pacotes.json";
        string folder = "Schemas";

        private Form1 _form;

        public VetPacotes(Form1 form)
        {
            _form = form;
        }


        public void ImportData(List<IDictionary> data)
        {
            string filePath = Path.Combine(Application.StartupPath, folder, fileName);
            var loadModel = GenericUtil.LoadFile(filePath);

            Console.WriteLine(loadModel);
            

            var token = SecurityUtil.OnLoginToken("999");

            if(data != null)
            {
                data.ForEach(item =>
                {
                    var model = loadModel;


                    model = model
                        .Replace("<<UUID>>", Guid.NewGuid().ToString())
                        .Replace("<<PRODUTO>>", item["Descricao"].ToString())
                        .Replace("<<IDPRODUTO>>", item["IDProduto"].ToString())
                        .Replace("<<IDANIMAL>>", item["IDAnimal"].ToString())
                        .Replace("<<ANIMAL>>", item["NomeAnimal"].ToString())
                        .Replace("<<CLIENTE>>", item["NomePessoa"].ToString())
                        .Replace("<<DATA_AGENDAMENTO>>", item["DataAgendamento"].ToString())
                        .Replace("<<DATA_EXECUCAO>>", "")
                        .Replace("<<IDCLIENTE>>", item["NomePessoa"].ToString())
                        .Replace("<<STATUSFATURAMENTO>>", "2")
                        .Replace("<<CURRENT_DATA>>", DateTime.Now.ToString())
                        .Replace("<<VALOR>>", item["Valor"].ToString());

                    _form.OnSetLog($"Carregou arquivo de modelo: {model}");
                });
            }

            _form.OnSetLog($"Carregou arquivo de modelo: {loadModel}");
        }
    }
}
