using Azure;
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
    internal class VetPacotes 
    {

        string fileName = "vet_pacotes.json";
        string folder = "Schemas";


        /*
         sisFlexApp.StatusAgen = [
        [4, 'Agendado'],
        [7, 'Pré-Agendado'],
        [1, 'Aguardando'],
        [2, 'Em andamento'],
        [3, 'Finalizado'],
        [5, 'Entregue'],
        [6, 'Cancelado'],
        [8, 'Não compareceu'],
        [9, 'Confirmado web'],
         ];
         */

        private Form1 _form;

        public VetPacotes(Form1 form)
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

                    model[0].GuidKey = Guid.NewGuid();
                    model[0].Detalhe = $"{item["Descricao"]} - Importado";
                    model[0].IDProduto = GenericUtil.LoadID(iConn, item["IDProduto"], "produtos_grades_estoque", "IDProduto");
                    model[0].IDProdutoOrigem = item["IDProduto"];
                    model[0].NomeProduto = item["Descricao"];
                    model[0].IDAnimal = item["IDAnimal"];
                    model[0].NomeAnimal = item["NomeAnimal"];
                    model[0].NomeCliente = item["NomePessoa"];
                    model[0].DataAgendamento = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                    model[0].Status = item["Status"];
                    model[0].IDCliente = item["IDPessoa"];
                    model[0].Faturamento.GuidKey = Guid.NewGuid();
                    model[0].Faturamento.ValorUnitario = item["Valor"];
                    model[0].Faturamento.Status = 2;
                    model[0].Faturamento.Data = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                    model[0].Faturamento.I83_dFab = DateTime.Now;
                    model[0].Faturamento.I84_dVal = DateTime.Now;
                    model[0].FaturaValor = item["Valor"];
                    model[0].FaturaTotal = item["Valor"];
                    model[0].FaturamentoStatusID = 2;
                    model[0].Obs = item["Observacoes"];
                    model[0].Agendamento.GuidKey = Guid.NewGuid();
                    model[0].Agendamento.StartDate = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                    model[0].Agendamento.EndDate = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                    model[0].Agendamento.Ce.start = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                    model[0].Agendamento.Ce.end = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                    model[0].DataAplicacao = GenericUtil.OnConvertDateToString(item["DataExecutado"]);


                    if (GenericUtil.OnConvertDateToString(item["DataAgendamento"]) != null)
                    {
                            var teste = $"{HttpUtil._url}vet/VetPacotes/SaveData?doID={DOFunctions._connectionProperties.dbNameDestination.Replace("atmusinf_Control-", "")}&doIDUser=-100";
                            var teste2 = JsonUtil.DoJsonSerializer(model);
                            var teste3 = headers;

                            var response = HttpUtil.DoPost<dynamic>($"{HttpUtil._url}vet/VetPacotes/SaveData?doID={DOFunctions._connectionProperties.dbNameDestination.Replace("atmusinf_Control-", "")}&doIDUser=-100", JsonUtil.DoJsonSerializer(model), headers);

                            if (response.RetWm.ToString().Equals("success"))
                            {
                                var param = new Hashtable();
                                param.Add("ID", item["IDProduto"]);
                                param.Add("TipoVet", -100);
                                var query = "UPDATE produtos set TipoVet=@TipoVet WHERE ID=@ID";
                                CrudUtils.ExecuteQuery(iConn, param, query);
                            }

                            _form.OnSetLog($"Importou pacote: {item["ID"]} - {item["Descricao"]} - {response.RetWm}");
                        } else
                        {
                            _form.OnSetLog($"Importou pacote: {item["ID"]} - {item["Descricao"]} - NÃO IMPORTADO");
                        }
                        
                    });
                }


            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }finally
            {
                iConn.ConnectionClose(iConn.DoConnection);
                iConn.Dispose();
            }
        }
    }
}
