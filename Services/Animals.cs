using DoImportador.Connection;
using DoImportador.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoImportador.Services
{
    public class Animals
    {
        private Form1 _form;

        public Animals(Form1 form)
        {
            _form = form;
        }

        public void ImportData(List<Animal> data)
        {
            var iConn = new DOConn();

            try
            {
                iConn.ConnectionOpen("", Enum.EnumDataLake.DESTINATION);


                var racas = data.GroupBy(p => p.Raca);

                //var cor = data.GroupBy(p => p["Cor"]);
                //var especie = data.GroupBy(p => p["Especie"]);
                //var Pelagem = data.GroupBy(p => p["Pelagem"]);

                data.ForEach(dt => 
                {




                    _form.OnSetLog($"Importou: {dt.Nome}");
                });

                MessageBox.Show("Dados importados com sucesso!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                iConn.ConnectionClose(iConn.DoConnection);
                iConn.Dispose();
            }
        }
    }
}
