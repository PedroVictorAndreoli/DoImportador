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
    public class Person
    {

        private Form1 _form;

        public Person(Form1 form) {
            _form = form;
        }

        public void ImportData(List<IDictionary> data, bool isChecked)
        {
            var iConn = new DOConn();
            try
            {

                iConn.ConnectionOpen("",Enum.EnumDataLake.DESTINATION);
                data.ForEach(person =>
                {
                    var input = new Hashtable();
                    /*************************************************PESSOA***********************************************************/
                    var query = "";
                    if (isChecked)
                    {
                        query = "SET IDENTITY_INSERT pessoas ON; INSERT INTO dbo.pessoas (ID,DataCadastro,Nome,IDSistemaContexto) VALUES (@ID, @DataCadastro,@Nome,@IDSistemaContexto); SET IDENTITY_INSERT pessoas OFF";
                        input.Add("ID", person["ID"]);
                    }
                    else
                    {
                        query = "INSERT INTO dbo.pessoas (DataCadastro,Nome,IDSistemaContexto) VALUES (@DataCadastro,@Nome,@IDSistemaContexto); Select Scope_Identity()";
                    }
                    
                    
                    input.Add("DataCadastro", person["DataCadastro"] == null ? DBNull.Value : person["DataCadastro"]);
                    input.Add("Nome", person["Nome"]);
                    input.Add("IDSistemaContexto", 0);

                    // Salva as pessoas
                    if (isChecked)
                        CrudUtils.ExecuteQuery(iConn, input, query);
                    else
                    {
                        person["ID"] = CrudUtils.ExecuteScalar(iConn, input, query);
                    }

                    /*************************************************ENDERECOS***********************************************************/

                    query = "INSERT INTO dbo.pessoas_enderecos (IDPessoa,Tipo,CEP,Endereco,Bairro,Numero,IDCidade,Complemento,Observacao) VALUES (@IDPessoa,@Tipo,@CEP,@Endereco,@Bairro,@Numero,@IDCidade,@Complemento,@Observacao)";
                    input = new Hashtable();
                    input.Add("IDPessoa", person["ID"]);
                    input.Add("Tipo", 0);
                    input.Add("CEP", GenericUtil.NullForEmpty((GenericUtil.ReturnNumber(person["CEP_Principal"])));
                    input.Add("Endereco", GenericUtil.NullForEmpty(person["Endereco_Principal"]));
                    input.Add("Bairro", GenericUtil.NullForEmpty(person["Bairro_Principal"]));
                    input.Add("Numero", GenericUtil.TruncateString(GenericUtil.ReturnNumber(GenericUtil.NullForEmpty(person["Numero_Principal"])),10));
                    input.Add("Complemento", GenericUtil.NullForEmpty(person["Complemento_Principal"]));
                    input.Add("Observacao", "");
                    input.Add("IDCidade", GenericUtil.LoadCity(iConn, GenericUtil.NullForEmpty(person["Cidade"]).ToString()));

                   


                    // Salva as pessoasEndereços
                    CrudUtils.ExecuteQuery(iConn, input, query);

                    input = new Hashtable();
                    input.Add("IDPessoa", person["ID"]);
                    input.Add("Tipo", 1);
                    input.Add("CEP", "");
                    input.Add("Endereco", "");
                    input.Add("Bairro","");
                    input.Add("Numero", "");
                    input.Add("Complemento", "");
                    input.Add("Observacao", "");
                    input.Add("IDCidade", 0);

                    // Salva as pessoasEndereços
                    CrudUtils.ExecuteQuery(iConn, input, query);
                    input["Tipo"] = 2;
                    CrudUtils.ExecuteQuery(iConn, input, query);


                    /*************************************************TELEFONES***********************************************************/
                    query = "INSERT INTO dbo.pessoas_telefones (IDPessoa,Tipo,Fone,Ramal,Observacao) VALUES (@IDPessoa,@Tipo,@Fone,@Ramal,@Observacao)";
                    input = new Hashtable();
                    input.Add("IDPessoa", person["ID"]);
                    input.Add("Tipo", 0);
                    input.Add("Fone", GenericUtil.TruncateString(GenericUtil.ReturnNumber(GenericUtil.NullForEmpty(person["Fone_Residencial"])),15));
                    input.Add("Ramal", "");
                    input.Add("Observacao", "");

                    CrudUtils.ExecuteQuery(iConn, input, query);

                    input = new Hashtable();
                    input.Add("IDPessoa", person["ID"]);
                    input.Add("Tipo", 1);
                    input.Add("Fone", GenericUtil.TruncateString(GenericUtil.ReturnNumber(GenericUtil.NullForEmpty(person["Fone_Comercial"])),15));
                    input.Add("Ramal", "");
                    input.Add("Observacao", "");

                    CrudUtils.ExecuteQuery(iConn, input, query);

                    input = new Hashtable();
                    input.Add("IDPessoa", person["ID"]);
                    input.Add("Tipo", 2);
                    input.Add("Fone", GenericUtil.TruncateString(GenericUtil.ReturnNumber(GenericUtil.NullForEmpty(person["Fone_Celular"])), 15));
                    input.Add("Ramal", "");
                    input.Add("Observacao", "");

                    CrudUtils.ExecuteQuery(iConn, input, query);

                    /*************************************************EMAILS***********************************************************/
                    query = "INSERT INTO dbo.pessoas_emails (IDPessoa,Tipo,Email,Observacao) VALUES (@IDPessoa,@Tipo,@Email,@Observacao)";
                    input = new Hashtable();
                    input.Add("IDPessoa", person["ID"]);
                    input.Add("Tipo", 0);
                    input.Add("Email", GenericUtil.NullForEmpty(person["Email"]));
                    input.Add("Observacao", 0);

                    CrudUtils.ExecuteQuery(iConn, input, query);
                    input["Tipo"] = 1;
                    input["Email"] = "";

                    CrudUtils.ExecuteQuery(iConn, input, query);


                    /*************************************************PESSOAS JURIDICAS/FISICAS***********************************************************/

                    if (person["Tipo"].ToString().Equals("Fisica"))
                    {
                        query = "INSERT INTO dbo.pessoas_fisicas (IDPessoa,RG,CPF,DataNascimento,IDSexo) VALUES (@IDPessoa,@RG,@CPF,@DataNascimento,@IDSexo)";
                        input = new Hashtable();
                        input.Add("IDPessoa", person["ID"]);
                        input.Add("RG", GenericUtil.ReturnNumber(person["RG"]));
                        input.Add("CPF", GenericUtil.TruncateString(GenericUtil.ReturnNumber(person["CPF"]),11));
                        input.Add("DataNascimento", person["DataNascimento"] == null ? DBNull.Value : person["DataNascimento"]);
                        input.Add("IDSexo", GenericUtil.ReturnSexo(person["Sexo"]));

                        CrudUtils.ExecuteQuery(iConn, input, query);
                        
                        
                        query = $"UPDATE pessoas set Tipo = 0 where id = {person["ID"]}";
                        CrudUtils.ExecuteQuery(iConn, null, query);
                    }
                    if (person["Tipo"].ToString().Equals("Juridica"))
                    {
                        query = "INSERT INTO dbo.pessoas_juridicas (IDPessoa,CNPJ,RazaoSocial,InscricaoEstadual,InscricaoMunicipal) VALUES (@IDPessoa,@CNPJ,@RazaoSocial,@InscricaoEstadual,@InscricaoMunicipal)";
                        input = new Hashtable();
                        input.Add("IDPessoa", person["ID"]);
                        input.Add("CNPJ", GenericUtil.ReturnNumber(person["CNPJ"]));
                        input.Add("RazaoSocial", GenericUtil.NullForEmpty(person["RazaoSocial"]));
                        input.Add("InscricaoEstadual", GenericUtil.ReturnNumber(person["InscricaoEstadual"]));
                        input.Add("InscricaoMunicipal", GenericUtil.ReturnNumber(person["InscricaoMunicipal"]));

                        CrudUtils.ExecuteQuery(iConn, input, query);

                        query = $"UPDATE pessoas set Tipo = 1 where id = {person["ID"]}";
                        CrudUtils.ExecuteQuery(iConn, null, query);
                    }

                    

                    if (person["TipoPessoa"].ToString().Equals("Cliente"))
                    {
                        /*************************************************PESSOAS CLIENTES***********************************************************/
                        query = "INSERT INTO pessoas_clientes (IDPessoa, Inativo, ObsGerais, LimiteCredito, InscricaoSUFRAMA,TipoContribuinte) VALUES (@IDPessoa, @Inativo,@ObsGerais,@LimiteCredito,@InscricaoSUFRAMA,@TipoContribuinte)";
                        input = new Hashtable();
                        input.Add("IDPessoa", person["ID"]);
                        input.Add("Inativo", 0);
                        input.Add("ObsGerais", "");
                        input.Add("LimiteCredito", 0);
                        input.Add("InscricaoSUFRAMA", "");
                        input.Add("TipoContribuinte", (person["Tipo"].ToString().Equals("Fisica") ? 9 : 0));

                        CrudUtils.ExecuteQuery(iConn, input, query);
                    } else
                    {
                        /*************************************************PESSOAS FORNEEDORES***********************************************************/
                        query = "INSERT INTO pessoas_fornecedores (IDPessoa, Inativo) VALUES (@IDPessoa, @Inativo)";
                        input = new Hashtable();
                        input.Add("IDPessoa", person["ID"]);
                        input.Add("Inativo", 0);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                    }


                    _form.OnSetLog($"Importou: {person["ID"]} - {person["Nome"]}");

                });



                MessageBox.Show("Dados importados com sucesso!!");

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally
            {
                iConn.ConnectionClose(iConn.DoConnection);
                iConn.Dispose();
            }

            
        }

        public void CorretionCity(List<IDictionary> data)
        {
            var iConn = new DOConn();

            var idbase = "4858";
            try
            {

                var headers = new Hashtable();
                var token = SecurityUtil.OnLoginToken("999");

                headers.Add("DoToken", token);
                headers.Add("Authorization", "Basic ZGF0YW9uOkRhdGFPbkFQSUAj");

                iConn.ConnectionOpen("", Enum.EnumDataLake.DESTINATION);

                var clientes = HttpUtil.DoGet<dynamic>($"{DOFunctions._connectionProperties.url}dataOn/doExplorer/DynamicQuery?doID={idbase}&doIDUser=-100&route=mnuCadastros_mnuClientes_mnuCadastro&filter=&sorters=ID%20DESC&system=-10&type=0&extraCritSQL=%20AND%20(Pessoas_clientes.Inativo%20%3D%200)%20&page=1&start=0&limit=10000", null, headers);

                foreach (var cliente in clientes.paging.data)
                {
                    var person = data.Find(e => Int32.Parse(e["ID"].ToString()) == Int32.Parse(cliente["ID"].ToString()));

                    if (person != null)
                    {

                        var pessoa = HttpUtil.DoGet<dynamic>($"{DOFunctions._connectionProperties.url}cadastros/Pessoa/GetData?doID={idbase}&id={person["ID"]}&pContexto=1", null, headers);

                        if (pessoa["RetWm"].ToString().Equals("success"))
                        {
                            var obj = pessoa["obj"];
                            obj.EnderecoResidencial.IDCidade = GenericUtil.LoadByID(iConn, GenericUtil.NullForEmpty(person["IBGE"]).ToString(),"cidades", "CodigoIBGE");
                            var json = JsonUtil.DoJsonSerializer(obj);
                            var result = HttpUtil.DoPost<dynamic>($"{DOFunctions._connectionProperties.url}cadastros/Pessoa/SaveData?doID={idbase}&doIDUser=-100", json, headers);

                            _form.OnSetLog($"{result["RetWm"]}");

                        }
                    }
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
