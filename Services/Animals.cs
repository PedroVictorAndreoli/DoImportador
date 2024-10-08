using doAPI.Utils;
using DoImportador.Connection;
using DoImportador.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

        public void ImportData(List<IDictionary> data)
        {
            var iConn = new DOConn();

            try
            {
                iConn.ConnectionOpen("", Enum.EnumDataLake.DESTINATION);


                var racas = data.GroupBy(p => p["Raca"]).ToList();
                var cores = data.GroupBy(p => p["Cor"]).ToList();
                var especies = data.GroupBy(p => p["Especie"]).ToList();
                var pelagens = data.GroupBy(p => p["Pelagem"]).ToList();

                var query = "";
                var input = new Hashtable();

                racas.ForEach(raca =>
                {
                    if(raca.Key != null)
                    {
                        query = "INSERT INTO vet_racas (Descricao) VALUES (@Descricao)";
                        input = new Hashtable();
                        input.Add("Descricao", raca.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {raca.Key}");
                    }
                });

                cores.ForEach(cor =>
                {
                    if (cor.Key != null)
                    {
                        query = "INSERT INTO vet_cores (Descricao) VALUES (@Descricao)";
                        input = new Hashtable();
                        input.Add("Descricao", cor.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {cor.Key}");
                    }
                });

                especies.ForEach(especie =>
                {
                    if (especie.Key != null)
                    {
                        query = "INSERT INTO vet_especies (Descricao) VALUES (@Descricao)";
                        input = new Hashtable();
                        input.Add("Descricao", especie.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {especie.Key}");
                    }
                });

                pelagens.ForEach(pelagem =>
                {
                    if (pelagem.Key != null)
                    {
                        query = "INSERT INTO vet_pelos (Descricao) VALUES (@Descricao)";
                        input = new Hashtable();
                        input.Add("Descricao", pelagem.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {pelagem.Key}");
                    }

                });

                data.ForEach(dt => 
                {




                    _form.OnSetLog($"Importou: {dt["Nome"]}");
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
