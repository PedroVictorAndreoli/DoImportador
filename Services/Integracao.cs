using Azure;
using doAPI.Utils;
using DoImportador.Connection;
using DoImportador.Enum;
using DoImportador.Model;
using DoImportador.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoImportador.Services
{
    public class Integracao
    {
        private Form1 _form;
        public Integracao(Form1 form)
        {
            _form = form;
        }
        public void migraProduto(List<ConnectionProperties> connectionProperties)
        {
            try
            {
                foreach (ConnectionProperties conn in connectionProperties)
                {
                    DOFunctions.LoadHost(conn);
                    var dataOriginal = LoadData.LoadDataDb("SQLSERVER", "SELECT EloIntegracao,Id FROM produtos where EloIntegracao is not null AND EloIntegracao<> ''", EnumDataLake.ORIGIN);
                    var iConn = new DOConn();
                    iConn.ConnectionOpen("", Enum.EnumDataLake.ORIGIN);
                    string query;
                    Hashtable input;
                    insertVinculoIntegracao(dataOriginal, iConn, EnumVinculoIntegracao.PRODUTO, "EloIntegracao", "Id");
                    _form.OnSetLog($"{conn.dbNameOrigin} migrou  o EloIntegracao");
                    dataOriginal = LoadData.LoadDataDb("SQLSERVER", "SELECT EloIntegracaoVariant,Id FROM produtos where EloIntegracaoVariant is not null AND EloIntegracaoVariant<> ''", EnumDataLake.ORIGIN);
                    insertVinculoIntegracao(dataOriginal, iConn, EnumVinculoIntegracao.PRODUTO, "EloIntegracaoVariant", "Id");
                    _form.OnSetLog($"{conn.dbNameOrigin} migrou  o EloIntegracaoVariant");
                    dataOriginal = LoadData.LoadDataDb("SQLSERVER", "select id_vipCommerce,Id from produtos where id_vipCommerce is not null and id_vipCommerce<>'';", EnumDataLake.ORIGIN);
                    foreach (var d in dataOriginal)
                    {
                        query = "INSERT INTO vinculo_integracao (IDVinculo, IDTipoVinculo, IDIntegracao, ExternalID) VALUES (@IDVinculo, @IDTipoVinculo, @IDIntegracao, @ExternalID)";
                        input = new Hashtable();
                        input.Add("IDVinculo", d["Id"].ToString());
                        input.Add("IDTipoVinculo", 0);
                        input.Add("IDIntegracao", Enum.EnumIntegracao.VIPCOMERCE);
                        input.Add("ExternalID", d["id_vipCommerce"].ToString());
                        CrudUtils.ExecuteQuery(iConn, input, query);
                    }
                    _form.OnSetLog($"{conn.dbNameOrigin} migrou o Vip Comerce");
                }
            }catch(Exception ex)
            {
                _form.OnSetLog($"{ex}");
            }
        }

        public void migraPessoa(List<ConnectionProperties> connectionProperties)
        {
            try
            {
                foreach (ConnectionProperties conn in connectionProperties)
                {
                    DOFunctions.LoadHost(conn);
                    var dataOriginal = LoadData.LoadDataDb("SQLSERVER", "SELECT EloIntegracao,IDPessoa FROM pessoas_clientes where EloIntegracao is not null AND EloIntegracao<> ''", EnumDataLake.ORIGIN);
                    var iConn = new DOConn();
                    iConn.ConnectionOpen("", Enum.EnumDataLake.ORIGIN);
                    insertVinculoIntegracao(dataOriginal, iConn, EnumVinculoIntegracao.PESSOAS, "EloIntegracao", "IDPessoa");
                    _form.OnSetLog($"{conn.dbNameOrigin} migrou  o EloIntegracao");
                }
            }
            catch (Exception ex)
            {
                _form.OnSetLog($"{ex}");
            }
        }
        private void insertVinculoIntegracao(List<IDictionary> dataOriginal, DOConn iConn, EnumVinculoIntegracao enumVinculoIntegracao,string EloIntegracao,string id)
        {
            foreach (var d in dataOriginal)
            {
                EloIntegracao elo = new EloIntegracao
                {
                    eloIntegracao = d[EloIntegracao].ToString(),
                    Id = int.Parse(d[id].ToString()),
                    vinculoIntegracao = enumVinculoIntegracao
                };
                if (elo.eloIntegracao.Contains("TR"))
                {
                    elo.eloIntegracao = elo.eloIntegracao.Replace("TR", "");
                    elo.integracao = Enum.EnumIntegracao.TRAY;
                }
                else if (elo.eloIntegracao.Contains("AM"))
                {
                    elo.eloIntegracao = elo.eloIntegracao.Replace("AM", "");
                    elo.integracao = Enum.EnumIntegracao.AMERICANAS;
                }
                else if (elo.eloIntegracao.Contains("NS"))
                {
                    elo.eloIntegracao = elo.eloIntegracao.Replace("NS", "");
                    elo.integracao = Enum.EnumIntegracao.NUVEMSHOP;
                }
                else if (elo.eloIntegracao.Contains("SM"))
                {
                    elo.eloIntegracao = elo.eloIntegracao.Replace("SM", "");
                    elo.integracao = Enum.EnumIntegracao.IFOOD;
                }
                else if (elo.eloIntegracao.Contains("SF"))
                {
                    elo.eloIntegracao = elo.eloIntegracao.Replace("SF", "");
                    elo.integracao = Enum.EnumIntegracao.SHOPFY;
                }
                else if (elo.eloIntegracao.Contains("IF"))
                {
                    elo.eloIntegracao = elo.eloIntegracao.Replace("IF", "");
                    elo.integracao = Enum.EnumIntegracao.IFOOD;
                }
                else if (elo.eloIntegracao.Contains("VP"))
                {
                    elo.eloIntegracao = elo.eloIntegracao.Replace("VP", "");
                    elo.integracao = Enum.EnumIntegracao.VIPCOMERCE;
                }

                if (elo.integracao != 0 && elo.integracao != null) 
                { 
                    string query = "INSERT INTO vinculo_integracao (IDVinculo, IDTipoVinculo, IDIntegracao, ExternalID) VALUES (@IDVinculo, @IDTipoVinculo, @IDIntegracao, @ExternalID)";
                    Hashtable input = new Hashtable();
                    input.Add("IDVinculo", elo.Id);
                    input.Add("IDTipoVinculo", elo.vinculoIntegracao);
                    input.Add("IDIntegracao", elo.integracao);
                    input.Add("ExternalID", elo.eloIntegracao);
                    CrudUtils.ExecuteQuery(iConn, input, query);
                }
            }
        }
    }
}
