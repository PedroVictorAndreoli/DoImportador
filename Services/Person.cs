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

        public void ImportData(List<IDictionary> data)
        {
            var iConn = new DOConn();
            try
            {

                iConn.ConnectionOpen("",Enum.EnumDataLake.DESTINATION);
                data.ForEach(person =>
                {

                    /*************************************************PESSOA***********************************************************/

                    var query = "SET IDENTITY_INSERT pessoas ON; INSERT INTO dbo.pessoas (ID,DataCadastro,Nome,IDSistemaContexto) VALUES (@ID, @DataCadastro,@Nome,@IDSistemaContexto); SET IDENTITY_INSERT pessoas OFF";
                    var input = new Hashtable();
                    input.Add("ID", person["ID"]);
                    input.Add("DataCadastro", DateTime.Now);
                    input.Add("Nome", person["Nome"]);
                    input.Add("IDSistemaContexto", 0);

                    // Salva as pessoas
                    CrudUtils.ExecuteQuery(iConn, input, query);

                    /*************************************************ENDERECOS***********************************************************/

                    query = "INSERT INTO dbo.pessoas_enderecos (IDPessoa,Tipo,CEP,Endereco,Bairro,Numero,IDCidade,Complemento,Observacao) VALUES (@IDPessoa,@Tipo,@CEP,@Endereco,@Bairro,@Numero,@IDCidade,@Complemento,@Observacao)";
                    input = new Hashtable();
                    input.Add("IDPessoa", person["ID"]);
                    input.Add("Tipo", 0);
                    input.Add("CEP", GenericUtil.NullForEmpty(person["CEP_Principal"]));
                    input.Add("Endereco", GenericUtil.NullForEmpty(person["Endereco_Principal"]));
                    input.Add("Bairro", GenericUtil.NullForEmpty(person["Bairro_Principal"]));
                    input.Add("Numero", GenericUtil.NullForEmpty(person["Numero_Principal"]));
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
                        input.Add("CPF", GenericUtil.ReturnNumber(person["CPF"]));
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

                    /*************************************************PESSOAS CLIENTES***********************************************************/

                    query = "INSERT INTO dbo.pessoas_clientes (IDPessoa, Inativo, ObsGerais, LimiteCredito, InscricaoSUFRAMA) VALUES (@IDPessoa, @Inativo,@ObsGerais,@LimiteCredito,@InscricaoSUFRAMA)";
                    input = new Hashtable();
                    input.Add("IDPessoa", person["ID"]);
                    input.Add("Inativo", 0);
                    input.Add("ObsGerais", "");
                    input.Add("LimiteCredito", 0);
                    input.Add("InscricaoSUFRAMA", "");

                    CrudUtils.ExecuteQuery(iConn, input, query);


                    /*************************************************PESSOAS FORNEEDORES***********************************************************/

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
    }
}
