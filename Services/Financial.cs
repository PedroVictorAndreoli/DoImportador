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
    public class Financial
    {

        string fileName = "duplicata.json";
        string fileQuitaName = "quita_duplicata.json";
        string folder = "Schemas";

        private Form1 _form;

        public Financial(Form1 form)
        {
            _form = form;
        }


        public void ImportData(List<IDictionary> data)
        {
            var headers = new Hashtable();

            string filePath = Path.Combine(Application.StartupPath, folder, fileName);
            string filePathQuita = Path.Combine(Application.StartupPath, folder, fileQuitaName);
            var loadModel = GenericUtil.LoadFile(filePath);
            var loadModelQuita = GenericUtil.LoadFile(filePathQuita);


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

                        // Duplicata
                        model.GuidKey = Guid.NewGuid();
                        model.IDNota = item["TipoDuplicata"]; // 1 - Receita 0 - Despesa
                        model.IDPessoa = item["IDPessoa"];
                        model.IDPC_Gestao = (Int32.Parse(item["TipoDuplicata"].ToString()) == 1 ? 8 : 71);
                        model.IDCC_Gestao = 1;
                        model.B09_dhEmi = GenericUtil.OnConvertDateToString(item["DataEmissao"]);
                        model.DataCompetencia = GenericUtil.OnConvertDateToString(item["DataCompetencia"]);
                        model.Y09_dVenc = GenericUtil.OnConvertDateToString(item["DataVencimento"]);
                        model.DataRecebimentoPagamento = GenericUtil.OnConvertDateToString(item["DataPagamento"]);
                        model.Y10_vDup = item["Valor"];
                        model.Descricao = GenericUtil.TruncateString($"{item["Descricao"]} - Importada", 200);
                        model.B11_tpNF = item["TipoDuplicata"];
                        model.YA02_tPagPrev = item["TipoPagamento"];


                        // Formas Pagamento
                        model.FormasPgto[0].IDContaBaixa = 1;
                        model.FormasPgto[0].DataPagamento = GenericUtil.OnConvertDateToString(item["DataEmissao"]);
                        model.FormasPgto[0].DataMovimento = GenericUtil.OnConvertDateToString(item["DataEmissao"]);
                        model.FormasPgto[0].BomPara = GenericUtil.OnConvertDateToString(item["DataEmissao"]);
                        model.FormasPgto[0].DataVencFormaPgtoPOS = GenericUtil.OnConvertDateToString(item["DataEmissao"]);
                        model.FormasPgto[0].IDCaixa = 1;

                        //Cheque
                        model.FormasPgto[0].Cheque.BomPara = GenericUtil.OnConvertDateToString(item["DataEmissao"]);
                        model.FormasPgto[0].Cheque.DataMovimento = GenericUtil.OnConvertDateToString(item["DataEmissao"]);

                        //Cartao
                        model.FormasPgto[0].Cartao.BomPara = GenericUtil.OnConvertDateToString(item["DataEmissao"]);
                        model.FormasPgto[0].Cartao.CompensadoEm = GenericUtil.OnConvertDateToString(item["DataEmissao"]);

                        if(GenericUtil.OnConvertDateToString(item["DataPagamento"]) != null)
                        {
                            model.Quitar = 0;
                            model.FormasPgto[0].DataPagamento = GenericUtil.OnConvertDateToString(item["DataPagamento"]);
                        }


                        if (GenericUtil.OnConvertDateToString(item["DataEmissao"]) != null)
                        {
                            var response = HttpUtil.DoPost<dynamic>($"{DOFunctions._connectionProperties.url}nfe/NFeCobrancaDuplicatas/SaveData?doID={DOFunctions._connectionProperties.dbNameDestination.Replace("atmusinf_Control-", "")}&doIDUser=-100", JsonUtil.DoJsonSerializer(model), headers);

                            if (response.RetWm.ToString().Equals("success"))
                            {
                                
                            }

                            _form.OnSetLog($"Importou duplicata: {item["ID"]} - {item["Descricao"]} - {response.RetWm}");
                        }
                        else
                        {
                            _form.OnSetLog($"Importou duplicata: {item["ID"]} - {item["Descricao"]} - NÃO IMPORTADO");
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
