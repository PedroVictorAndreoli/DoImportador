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
    public class VetExame
    {

        string fileName = "vet_exame.json";
        string folder = "Schemas";

        private Form1 _form;

        public VetExame(Form1 form)
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

                        // Vacinaa
                        model[0].GuidKey = Guid.NewGuid();
                        model[0].Detalhe = $"{item["Descricao"]} - Importado";
                        model[0].IDProduto = GenericUtil.LoadID(iConn, item["IDProduto"], "produtos_grades_estoque", "IDProduto");
                        model[0].IDProdutoOrigem = item["IDProduto"];
                        model[0].NomeProduto = item["Descricao"];
                        model[0].IDAnimal = item["IDAnimal"];
                        model[0].NomeAnimal = item["NomeAnimal"];
                        model[0].DataAgendamento = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                        model[0].Status = (item["StatusAgenda"].ToString() == "3" ? 1 : 0);
                        model[0].DataAplicacao = GenericUtil.OnConvertDateToString(item["DataExecutado"]);
                        model[0].NomeCliente = item["NomePessoa"];
                        model[0].IDCliente = item["IDPessoa"];
                        model[0].Obs = item["Observacoes"];



                        // Faturamento
                        model[0].Faturamento.GuidKey = Guid.NewGuid();
                        model[0].Faturamento.ValorUnitario = item["Valor"];
                        model[0].Faturamento.Status = item["StatusAgenda"];
                        model[0].Faturamento.Data = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                        model[0].Faturamento.I83_dFab = DateTime.Now;
                        model[0].Faturamento.I84_dVal = DateTime.Now;
                        model[0].FaturaValor = item["Valor"];
                        model[0].FaturaTotal = item["Valor"];
                        model[0].FaturamentoStatusID = item["Status"];
                        
                        // Agendamento
                        model[0].Agendamento.GuidKey = Guid.NewGuid();
                        model[0].Agendamento.StartDate = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                        model[0].Agendamento.IDStatus = item["StatusAgenda"];
                        model[0].Agendamento.EndDate = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                        model[0].Agendamento.Ce.start = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);
                        model[0].Agendamento.Ce.end = GenericUtil.OnConvertDateToString(item["DataAgendamento"]);



                        if (GenericUtil.OnConvertDateToString(item["DataAgendamento"]) != null)
                        {
                            var response = HttpUtil.DoPost<dynamic>($"{HttpUtil._url}vet/VetVacinas/SaveData?doID={DOFunctions._connectionProperties.dbNameDestination.Replace("atmusinf_Control-", "")}&doIDUser=-100", JsonUtil.DoJsonSerializer(model), headers);

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


        public void UpdateProductToExam()
        {
            var iConn = new DOConn();
            try
            {
                var idbase = "4860";
                var headers = new Hashtable();
                var token = SecurityUtil.OnLoginToken("999");

                headers.Add("DoToken", token);
                headers.Add("Authorization", "Basic ZGF0YW9uOkRhdGFPbkFQSUAj");


                var produtos = HttpUtil.DoGet<dynamic>($"{DOFunctions._connectionProperties.url}dataOn/doExplorer/DynamicQuery?doID={idbase}&doIDUser=-100&route=mnuEstoque_mnuProdutosServicos&filter=&sorters=ID%20DESC&system=0&type=0&extraCritSQL=%20AND%20(produtos.idgrupo%20%3D%2045)&page=1&start=0&limit=500", null, headers);




                foreach (var produto in produtos.paging.data)
                {

                    var item = HttpUtil.DoGet<dynamic>($"{DOFunctions._connectionProperties.url}cadastros/Produto/GetData?doID={idbase}&id={produto["ID"]}", null, headers);

                    if (item["RetWm"].ToString().Equals("success"))
                    {
                        var obj = item["obj"];
                        obj.TipoVet = -90;
                        var json = JsonUtil.DoJsonSerializer(obj);
                        var result = HttpUtil.DoPost<dynamic>($"{DOFunctions._connectionProperties.url}cadastros/Produto/SaveData?doID={idbase}&doIDUser=-100", json, headers);

                        _form.OnSetLog($"Atualizou produto: {result["RetWm"]} - {item.obj.Descricao}");

                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                iConn.ConnectionClose(iConn.DoConnection, DOFunctions._connectionProperties.dbType);
                iConn.Dispose();
            }


        }
    }
}
