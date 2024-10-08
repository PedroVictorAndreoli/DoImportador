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
    internal class Person
    {

        public static void ImportPeople(List<IDictionary> persons)
        {
            var iConn = new DOConn();
            try
            {

                iConn.ConnectionOpen("",Enum.EnumDataLake.DESTINATION);
                persons.ForEach(person =>
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
                    input.Add("CEP", GenericUtil.NullForEmpty(person["Cep"]));
                    input.Add("Endereco", GenericUtil.NullForEmpty(person["Endereco"]));
                    input.Add("Bairro", GenericUtil.NullForEmpty(person["Bairro"]));
                    input.Add("Numero", GenericUtil.NullForEmpty(person["Numero"]));
                    input.Add("Complemento", GenericUtil.NullForEmpty(person["Complemento"]));
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
                    input.Add("Fone", GenericUtil.NullForEmpty(person["TelefoneFixo"]));
                    input.Add("Ramal", "");
                    input.Add("Observacao", "");

                    CrudUtils.ExecuteQuery(iConn, input, query);

                    input = new Hashtable();
                    input.Add("IDPessoa", person["ID"]);
                    input.Add("Tipo", 1);
                    input.Add("Fone", GenericUtil.NullForEmpty(person["TelefoneComercial"]));
                    input.Add("Ramal", "");
                    input.Add("Observacao", "");

                    CrudUtils.ExecuteQuery(iConn, input, query);

                    input = new Hashtable();
                    input.Add("IDPessoa", person["ID"]);
                    input.Add("Tipo", 2);
                    input.Add("Fone", GenericUtil.NullForEmpty(person["Celular"]));
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
                        query = "INSERT INTO dbo.pessoas_fisicas (IDPessoa,RG,CPF,DataNascimento,IDSexo) VALUES (@IDPessoa,@RG,@CPF,@DataNascimento,@Sexo)";
                        input = new Hashtable();
                        input.Add("IDPessoa", person["ID"]);
                        input.Add("RG", GenericUtil.ReturnNumber(person["RG"]));
                        input.Add("CPF", GenericUtil.ReturnNumber(person["CPF"]));
                        input.Add("DataNascimento", person["DataNascimento"]);
                        input.Add("IDSexo", GenericUtil.ReturnSexo(person["DataNascimento"].ToString()));

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
                        input.Add("RazaoSocial", person["RazaoSocial"]);
                        input.Add("InscricaoEstadual", GenericUtil.ReturnNumber(person["IE"]));
                        input.Add("InscricaoMunicipal", "");

                        CrudUtils.ExecuteQuery(iConn, input, query);

                        query = $"UPDATE pessoas set Tipo = 1 where id = {person["ID"]}";
                        CrudUtils.ExecuteQuery(iConn, null, query);
                    }

                    /*************************************************PESSOAS CLIENTES***********************************************************/

                    query = "INSERT INTO dbo.pessoas_clientes (IDPessoa, Inativo, ObsGerais, LimiteCredito, InscricaoSUFRAMA) VALUES (@IDPessoa, @Inativo,@ObsGerais,@LimiteCredito,@InscricaoSUFRAMA)";
                    input = new Hashtable();
                    input.Add("IDPessoa", person["ID"]);



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
