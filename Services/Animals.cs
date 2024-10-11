using doAPI.Utils;
using DoImportador.Connection;
using DoImportador.Model;
using DoImportador.Utils;
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
                var pelagens = data.GroupBy(p => p["Pelo"]).ToList();
                var temperamento = data.GroupBy(p => p["Temperamento"]).ToList();
                var dieta = data.GroupBy(p => p["Dieta"]).ToList();
                var porte = data.GroupBy(p => p["Dieta"]).ToList();

                var query = "";
                var input = new Hashtable();

                racas.ForEach(data =>
                {
                    if (data.Key != null && !data.Key.Equals(""))
                    {
                        query = "INSERT INTO vet_racas (Descricao) VALUES (@Descricao)";
                        input = new Hashtable();
                        input.Add("Descricao", data.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {data.Key}");
                    }
                });

                cores.ForEach(data =>
                {
                    if (data.Key != null && !data.Key.Equals(""))
                    {
                        query = "INSERT INTO vet_cores (Descricao) VALUES (@Descricao)";
                        input = new Hashtable();
                        input.Add("Descricao", data.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {data.Key}");
                    }
                });

                especies.ForEach(data =>
                {
                    if (data.Key != null && !data.Key.Equals(""))
                    {
                        query = "INSERT INTO vet_especies (Descricao) VALUES (@Descricao)";
                        input = new Hashtable();
                        input.Add("Descricao", data.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {data.Key}");
                    }
                });

                pelagens.ForEach(data =>
                {
                    if (data.Key != null && !data.Key.Equals(""))
                    {
                        query = "INSERT INTO vet_pelos (Descricao) VALUES (@Descricao)";
                        input = new Hashtable();
                        input.Add("Descricao", data.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {data.Key}");
                    }

                });

                temperamento.ForEach(data =>
                {
                    if (data.Key != null && !data.Key.Equals(""))
                    {
                        query = "INSERT INTO vet_temperamentos (Descricao) VALUES (@Descricao)";
                        input = new Hashtable();
                        input.Add("Descricao", data.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {data.Key}");
                    }

                });

                dieta.ForEach(data =>
                {
                    if (data.Key != null && !data.Key.Equals(""))
                    {
                        query = "INSERT INTO vet_dietas (Descricao) VALUES (@Descricao)";
                        input = new Hashtable();
                        input.Add("Descricao", data.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {data.Key}");
                    }

                });

                porte.ForEach(data =>
                {
                    if (data.Key != null && !data.Key.Equals(""))
                    {
                        query = "INSERT INTO vet_estaturas (Descricao) VALUES (@Descricao)";
                        input = new Hashtable();
                        input.Add("Descricao", data.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {data.Key}");
                    }

                });

                data.ForEach(dt => 
                {

                    query = "SET IDENTITY_INSERT vet_animais ON; INSERT INTO vet_animais (ID,Nome,ChipNumero,ChipData,Pedigree,DataSeguro,IDEspecie,IDRaca,IDCor,DataNascimento,IDSexo,ObitoData,ObitoObservacao,PesoUltimo, Inativo,ObservacaoAlimentares,ObservacaoClinicas,DataPrimeiraVisita,DataUltimaVisita,IDDono,Obito,ObservacaoAlerta,DataCadastro)" +
                    " VALUES " +
                    "(@ID,@Nome,@ChipNumero,@ChipData,@Pedigree,@DataSeguro,@IDEspecie,@IDRaca,@IDCor,@DataNascimento,@IDSexo,@ObitoData,@ObitoObservacao,@PesoUltimo,@Inativo,@ObservacaoAlimentares,@ObservacaoClinicas,@DataPrimeiraVisita,@DataUltimaVisita,@IDDono,@Obito,@ObservacaoAlerta,@DataCadastro); SET IDENTITY_INSERT vet_animais OFF";
                    input = new Hashtable();
                    input.Add("ID", dt["ID"]);
                    input.Add("Nome", dt["NomeAnimal"]);
                    input.Add("ChipNumero", GenericUtil.NullForEmpty(dt["ChipNumero"]));
                    input.Add("ChipData", DBNull.Value);
                    input.Add("Pedigree", "");
                    input.Add("DataSeguro", DBNull.Value);

                    input.Add("DataNascimento", dt["DataNascimento"] == null ? DBNull.Value : GenericUtil.VerifyValidDateTime(dt["DataNascimento"]));

                    input.Add("IDEspecie", GenericUtil.LoadByID(iConn, GenericUtil.NullForEmpty(dt["Especie"]).ToString(), "vet_especies"));
                    input.Add("IDRaca", GenericUtil.LoadByID(iConn, GenericUtil.NullForEmpty(dt["Raca"]).ToString(), "vet_racas"));
                    input.Add("IDCor", GenericUtil.LoadByID(iConn, GenericUtil.NullForEmpty(dt["Cor"]).ToString(), "vet_cores"));
                    input.Add("IDSexo", GenericUtil.ReturnSexo(dt["Sexo"]));


                    input.Add("ObitoData", DBNull.Value);
                    input.Add("ObitoObservacao", "");
                    input.Add("PesoUltimo", GenericUtil.NullForZero(dt["Peso"]));
                    input.Add("Inativo",0);
                    input.Add("ObservacaoAlimentares", "");
                    input.Add("ObservacaoClinicas", "");
                    input.Add("DataPrimeiraVisita", DBNull.Value);
                    input.Add("DataUltimaVisita", DBNull.Value);
                    input.Add("IDDono", dt["CodigoDono"]);
                    input.Add("Obito", dt["Obito"]);
                    input.Add("ObservacaoAlerta", GenericUtil.NullForEmpty(dt["Observacoes"]));
                    input.Add("DataCadastro", DateTime.Now);

                    CrudUtils.ExecuteQuery(iConn, input, query);

                    query = "INSERT INTO dbo.vet_animais_donos (IDAnimal,IDDonoNew,IDDonoOld,Data) VALUES (@IDAnimal,@IDDonoNew,@IDDonoOld,@Data)";
                    input = new Hashtable();
                    input.Add("IDAnimal", dt["ID"]);
                    input.Add("IDDonoNew", dt["CodigoDono"]);
                    input.Add("IDDonoOld", dt["CodigoDono"]);
                    input.Add("Data", DateTime.Now);

                    CrudUtils.ExecuteQuery(iConn, input, query);


                    _form.OnSetLog($"Importou: {dt["NomeAnimal"]}");
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
