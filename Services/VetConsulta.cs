using doAPI.Utils;
using DoImportador.Connection;
using DoImportador.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoImportador.Services
{
    public class VetConsulta
    {

        string fileName = "vet_consulta.json";
        string folder = "Schemas";

        private Form1 _form;

        public VetConsulta(Form1 form)
        {
            _form = form;
        }


        public void ImportData(List<IDictionary> data)
        {
            var headers = new Hashtable();

            string filePath = Path.Combine(Application.StartupPath, folder, fileName);
            var loadModel = GenericUtil.LoadFile(filePath);


            var token = SecurityUtil.OnLoginToken("999");
            var iConn = new DOConn();


            headers.Add("DoToken", token);
            headers.Add("Authorization", "Basic ZGF0YW9uOkRhdGFPbkFQSUAj");
            try
            {
                iConn.ConnectionOpen(DOFunctions._connectionProperties.dbNameDestination, Enum.EnumDataLake.DESTINATION);


                if (data != null)
                {
                    data.ForEach(item =>
                    {
                        var model = JsonUtil.DoJsonDeserialize<dynamic>(loadModel);

                        // Consulta
                        model.GuidKey = Guid.NewGuid();
                        model.DescricaoTipoConsulta = $"{item["Descricao"]} - Importado";
                        model.IDProdutoGrade = GenericUtil.LoadID(iConn, item["IDProduto"], "produtos_grades_estoque", "IDProduto");
                        model.IDTipoConsulta = item["IDProduto"];
                        model.NomeProduto = item["Descricao"];
                        model.IDAnimal = item["IDAnimal"];
                        model.NomeAnimal = item["NomeAnimal"];
                        model.Data = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                        model.Status = (item["StatusAgenda"].ToString() == "3" ? 1 : 0);//3 finalizado outro status aguardando
                        model.DataAplicacao = GenericUtil.OnConvertDateToString(item["DataExecutado"]);
                        model.NomeCliente = item["NomePessoa"];
                        model.IDCliente = item["IDPessoa"];
                        model.Anamnese = item["Anamnese"];





                        // Faturamento
                        model.Faturamento.GuidKey = Guid.NewGuid();
                        model.Faturamento.ValorUnitario = item["Valor"];
                        model.Faturamento.Status = item["StatusAgenda"];
                        model.Faturamento.Data = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                        model.Faturamento.I83_dFab = DateTime.Now;
                        model.Faturamento.I84_dVal = DateTime.Now;
                        model.FaturaValor = item["Valor"];
                        model.FaturaTotal = item["Valor"];
                        model.FaturamentoStatusID = item["Status"];
                        
                        // Agendamento
                        model.Agendamento.GuidKey = Guid.NewGuid();
                        model.Agendamento.StartDate = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                        model.Agendamento.IDStatus = item["StatusAgenda"];
                        model.Agendamento.EndDate = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                        model.Agendamento.Ce.start = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                        model.Agendamento.Ce.end = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);



                        if (GenericUtil.OnConvertDateToString(item["DataAgendamento"]) != null)
                        {
                            var response = HttpUtil.DoPost<dynamic>($"{DOFunctions._connectionProperties.url}vet/VetConsultas/SaveData?doID={DOFunctions._connectionProperties.dbNameDestination.Replace("atmusinf_Control-", "")}&doIDUser=-100", JsonUtil.DoJsonSerializer(model), headers);

                            if (response.RetWm.ToString().Equals("success"))
                            {
                                var param = new Hashtable();
                                param.Add("ID", item["IDProduto"]);
                                param.Add("TipoVet", -100);

                                var query = "UPDATE produtos set TipoVet=@TipoVet WHERE ID=@ID";
                                //CrudUtils.ExecuteQuery(iConn, param, query);
                            }

                            _form.OnSetLog($"Importou pacote: {item["ID"]} - {item["Descricao"]} - {response.RetWm}");
                        }
                        else
                        {
                            _form.OnSetLog($"Importou pacote: {item["ID"]} - {item["Descricao"]} - NÃO IMPORTADO");
                        }

                    });
                }


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
