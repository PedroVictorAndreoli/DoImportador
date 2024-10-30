﻿using doAPI.Utils;
using DoImportador.Connection;
using DoImportador.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DoImportador.Services
{
    public class Product
    {

        private Form1 _form;

        public Product(Form1 form)
        {
            _form = form;
        }

        public void ImportData(List<IDictionary> data, bool isChecked)
        {
            var iConn = new DOConn();
            var query = "";
            var input = new Hashtable();

            try
            {
                iConn.ConnectionOpen("", Enum.EnumDataLake.DESTINATION);

                var grupos = data.GroupBy(p => p["Grupo"]).ToList();
                var unidades = data.GroupBy(p => p["Unidade"]).ToList();
                var marcas = data.GroupBy(p => p["Marca"]).ToList();


                marcas.ForEach(data =>
                {
                    if (data.Key != null)
                    {
                        query = "INSERT INTO dbo.produtos_marcas (Descricao) VALUES (@Descricao)";
                        input = new Hashtable();
                        input.Add("Descricao", data.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {data.Key}");
                    }
                });

                unidades.ForEach(data =>
                {
                    if (data.Key != null)
                    {
                        query = "INSERT INTO dbo.produtos_unidades (Descricao,abreviatura) VALUES (@Descricao, @Abreviatura)";
                        input = new Hashtable();
                        input.Add("Descricao", data.Key);
                        input.Add("abreviatura", data.Key);
                        CrudUtils.ExecuteQuery(iConn, input, query);

                        _form.OnSetLog($"Importou: {data.Key}");
                    }
                });
                int i = 1;
                grupos.ForEach(data =>
                {
                    if (data.Key != null)
                    {
                        query = "INSERT INTO dbo.produtos_grupos_subgrupos (IDMultiEmpresa,Descricao,CodigoEspecifico,CodigoPaiTreeView,Tipo,DescricaoCompleta ,IDSistemaContexto,FidelidadePorc) VALUES (@IDMultiEmpresa,@Descricao,@CodigoEspecifico,@CodigoPaiTreeView,@Tipo,@DescricaoCompleta ,@IDSistemaContexto,@FidelidadePorc)";
                        input = new Hashtable();
                        input.Add("IDMultiEmpresa", 0);
                        input.Add("Descricao", data.Key);

                        input.Add("CodigoEspecifico", $"1.{i}");
                        input.Add("CodigoPaiTreeView", 1);
                        input.Add("Tipo", 0);
                        input.Add("DescricaoCompleta", $"//1 - Grupos - Sub-Grupos de Produtos/1.{i} - {data.Key}");
                        input.Add("IDSistemaContexto", 0);
                        input.Add("FidelidadePorc", 0.00);
                        CrudUtils.ExecuteQuery(iConn, input, query);
                        i += 1;

                        _form.OnSetLog($"Importou: {data.Key}");
                    }
                });


                data.ForEach(dt => {

                    input = new Hashtable();
                    if (isChecked)
                    {
                        query = "SET IDENTITY_INSERT produtos ON; INSERT INTO produtos(ID,IDMultiEmpresa, IDGrupo, IDLocalArmazenamento, IDMarca, Tipo, Inativo, ControlaEstoque, Descricao,EstoqueMinimo, EstoqueMaximo, CEST, EAN_Tributavel, EAN, ExTIPI, Genero, IDUnidadeComercial, IDUnidadeTributavel, ValorCusto, ValorVendaVista, ValorVendaPrazo, ValorVendaPromocional, " +
                                            "DescontoPermitido, MargemLucro, TipoItemFiscal, CodigoBarras, EstoqueAtual, IDTipoGrade, V01_infAdProd, IDNcm, IDRegraICMSSaida, IDRegraICMSEntrada, IDSistemaContexto, TipoVet, IDLaboratorio, QtdePlano,PesoLiquido,PesoBruto,Largura,Altura,Comprimento,Volume,VendeEcommerce,DescricaoECommerce,Observacoes)" +
                                                " VALUES " +
                                                "(@ID,@IDMultiEmpresa,@IDGrupo,@IDLocalArmazenamento,@IDMarca,@Tipo,@Inativo,@ControlaEstoque,@Descricao,@EstoqueMinimo,@EstoqueMaximo,@CEST,@EAN_Tributavel,@EAN,@ExTIPI,@Genero,@IDUnidadeComercial,@IDUnidadeTributavel,@ValorCusto,@ValorVendaVista,@ValorVendaPrazo,@ValorVendaPromocional,@DescontoPermitido,@MargemLucro,@TipoItemFiscal,@CodigoBarras,@EstoqueAtual,@IDTipoGrade,@V01_infAdProd,@IDNcm,@IDRegraICMSSaida,@IDRegraICMSEntrada,@IDSistemaContexto,@TipoVet,@IDLaboratorio,@QtdePlano,@PesoLiquido,@PesoBruto,@Largura,@Altura,@Comprimento,@Volume,@VendeEcommerce,@DescricaoECommerce,@Observacoes); SET IDENTITY_INSERT vet_animais OFF;";
                        input.Add("ID", dt["ID"]);
                    } else
                    {
                        query = "INSERT INTO produtos(IDMultiEmpresa, IDGrupo, IDLocalArmazenamento, IDMarca, Tipo, Inativo, ControlaEstoque, Descricao,EstoqueMinimo, EstoqueMaximo, CEST, EAN_Tributavel, EAN, ExTIPI, Genero, IDUnidadeComercial, IDUnidadeTributavel, ValorCusto, ValorVendaVista, ValorVendaPrazo, ValorVendaPromocional, " +
                                            "DescontoPermitido, MargemLucro, TipoItemFiscal, CodigoBarras, EstoqueAtual, IDTipoGrade, V01_infAdProd, IDNcm, IDRegraICMSSaida, IDRegraICMSEntrada, IDSistemaContexto, TipoVet, IDLaboratorio, QtdePlano,PesoLiquido,PesoBruto,Largura,Altura,Comprimento,Volume,VendeEcommerce,DescricaoECommerce,Observacoes)" +
                                                " VALUES " +
                                                "(@IDMultiEmpresa,@IDGrupo,@IDLocalArmazenamento,@IDMarca,@Tipo,@Inativo,@ControlaEstoque,@Descricao,@EstoqueMinimo,@EstoqueMaximo,@CEST,@EAN_Tributavel,@EAN,@ExTIPI,@Genero,@IDUnidadeComercial,@IDUnidadeTributavel,@ValorCusto,@ValorVendaVista,@ValorVendaPrazo,@ValorVendaPromocional,@DescontoPermitido,@MargemLucro,@TipoItemFiscal,@CodigoBarras,@EstoqueAtual,@IDTipoGrade,@V01_infAdProd,@IDNcm,@IDRegraICMSSaida,@IDRegraICMSEntrada,@IDSistemaContexto,@TipoVet,@IDLaboratorio,@QtdePlano,@PesoLiquido,@PesoBruto,@Largura,@Altura,@Comprimento,@Volume,@VendeEcommerce,@DescricaoECommerce,@Observacoes); Select Scope_Identity()";
                    }

                    
                    input.Add("IDMultiEmpresa", 0);
                    input.Add("IDGrupo", GenericUtil.LoadByID(iConn, GenericUtil.NullForEmpty(dt["Grupo"]).ToString(), "produtos_grupos_subgrupos"));
                    input.Add("IDLocalArmazenamento", 1);
                    input.Add("IDMarca", GenericUtil.LoadByID(iConn, GenericUtil.NullForEmpty(dt["Marca"]).ToString(), "produtos_marcas"));
                    input.Add("Tipo", GenericUtil.ReturnTypeItem(dt["Tipo"]));
                    input.Add("Inativo", 0);
                    input.Add("ControlaEstoque", (GenericUtil.ReturnTypeItem(dt["Tipo"]) == 0 ? 1 : 0));
                    input.Add("Descricao", dt["Descricao"]);
                    input.Add("EstoqueMinimo", 0);
                    input.Add("EstoqueMaximo", 0);
                    input.Add("CEST", "");

                    input.Add("EAN_Tributavel", "");
                    input.Add("EAN", GenericUtil.TruncateString(GenericUtil.NullForEmpty(dt["CodigoBarra"]).ToString(),14));
                    input.Add("ExTIPI", "");
                    input.Add("Genero", "");
                    var unidade = GenericUtil.LoadByID(iConn, GenericUtil.NullForEmpty(dt["Unidade"]).ToString(), "produtos_unidades");
                    input.Add("IDUnidadeComercial", unidade);
                    input.Add("IDUnidadeTributavel", unidade);

                    input.Add("ValorCusto", dt["ValorCompra"]);
                    input.Add("ValorVendaVista", dt["ValorVenda"]);
                    input.Add("ValorVendaPrazo", dt["ValorVenda"]);
                    input.Add("ValorVendaPromocional", 0);

                    input.Add("DescontoPermitido", 0);
                    var venda = Decimal.Parse(GenericUtil.NullForZero(dt["ValorVenda"]).ToString());
                    var custo = Decimal.Parse(GenericUtil.NullForZero(dt["ValorCompra"]).ToString());
                    var margem = 100.00M;
                    if(venda > 0 && custo > 0)
                        margem = (((venda - custo) / custo) * 100);

                    input.Add("MargemLucro", Decimal.Parse(margem.ToString("F2")));

                    input.Add("TipoItemFiscal", 0);

                    input.Add("CodigoBarras", GenericUtil.NullForEmpty(dt["CodigoBarra"]));

                    input.Add("EstoqueAtual", Decimal.Parse(GenericUtil.NullForZero(dt["EstoqueAtual"]).ToString()));

                    input.Add("IDTipoGrade", 0);
                    input.Add("V01_infAdProd", "");

                    input.Add("IDNcm", GenericUtil.LoadByID(iConn, GenericUtil.NullForEmpty(dt["NCM"]).ToString(), "ncm", "NCM"));

                    var tributacao = dt["Tributacao"].ToString() switch
                    {
                        "102" or "500" => 1,
                        "101" => 9,
                        "400" => 2,
                        "103" => 11,
                        _ => -1
                    };
                    // Ajustar
                    input.Add("IDRegraICMSSaida", tributacao);

                    input.Add("IDRegraICMSEntrada", -2);

                    input.Add("IDSistemaContexto", 0);
                    input.Add("TipoVet", dt["TipoVet"]);


                    input.Add("IDLaboratorio", 0);
                    input.Add("QtdePlano", 0);

                    input.Add("PesoLiquido", 0);
                    input.Add("PesoBruto", 0);
                    input.Add("Largura", 0);
                    input.Add("Altura", 0);
                    input.Add("Comprimento", 0);
                    input.Add("Volume", 0);
                    input.Add("VendeEcommerce", 0);
                    input.Add("DescricaoECommerce", dt["Descricao"]);
                    input.Add("Observacoes", "");
                    //input.Add("Foto", DBNull.Value);
                    if(isChecked)
                        CrudUtils.ExecuteQuery(iConn, input, query);
                    else
                    {
                        var idProduto = CrudUtils.ExecuteScalar(iConn, input, query);
                        dt["ID"] = idProduto; 
                    }
                   
                    query = " INSERT INTO produtos_grades_estoque(IDProduto,CodigoBarras,ValorCusto,ValorVendaVista,ValorVendaPrazo,DescontoPermitido,EstoqueAtual, ValorVendaPromocional)" +
                   " VALUES " +
                   "(@IDProduto,@CodigoBarras,@ValorCusto,@ValorVendaVista,@ValorVendaPrazo,@DescontoPermitido,@EstoqueAtual, @ValorVendaPromocional); SET IDENTITY_INSERT produtos_grades_estoque OFF";
                    input = new Hashtable();
                    input.Add("IDProduto", dt["ID"]);
                    input.Add("CodigoBarras", dt["CodigoBarra"]);
                    input.Add("ValorCusto", custo);
                    input.Add("ValorVendaVista", venda);
                    input.Add("ValorVendaPrazo", venda);
                    input.Add("DescontoPermitido", 10);
                    input.Add("EstoqueAtual", dt["EstoqueAtual"]);
                    input.Add("ValorVendaPromocional", venda);

                    CrudUtils.ExecuteQuery(iConn, input, query);

                    _form.OnSetLog($"Importou: {dt["Descricao"]}");
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

        public void ImportProductsAllDatabase()
        {
            var iConn = new DOConn();
            try
            {

                var headers = new Hashtable();
                var token = SecurityUtil.OnLoginToken("999");

                headers.Add("DoToken", token);
                headers.Add("Authorization", "Basic ZGF0YW9uOkRhdGFPbkFQSUAj");

                iConn.ConnectionOpen(DOFunctions._connectionProperties.dbNameOrigin, Enum.EnumDataLake.ORIGIN);

                var clientes = HttpUtil.DoGet<dynamic>($"{DOFunctions._connectionProperties.url}dataOn/doExplorer/DynamicQuery?doID=999&doIDUser=3274&route=mnuCadastros_mnuClientes_mnuCadastro&filter=&sorters=ID%20DESC&system=-10&type=0&extraCritSQL=%20AND%20(Pessoas_clientes.Inativo%20%3D%200)%20&page=1&start=0&limit=10000",null, headers);

                foreach( var cliente in clientes.paging.data)
                {
                    if(Int32.Parse(cliente["ID"].ToString()) > 1004)
                    {

                        var produtos = HttpUtil.DoGet<dynamic>($"{DOFunctions._connectionProperties.url}dataOn/doExplorer/DynamicQuery?doID={Int32.Parse(cliente["ID"].ToString())}&doIDUser=-100&route=mnuEstoque_mnuProdutosServicos&filter=&sorters=ID%20DESC&system=0&type=0&extraCritSQL=&page=1&start=0&limit=20000", null, headers);
                        if(Int32.Parse(produtos.paging.totalRecords.ToString()) > 0)
                        {
                            foreach (var produto in produtos.paging.data)
                            {
                                var dataInclude = new Hashtable();
                                var query = "insert into produto (descricao, estoqueatual, codigobarras, valorcusto, valorvenda," +
                                    "  valorpromocional, margemlucro, descontopermitido, ncm, cest, tributacao_saida, grupo, marca," +
                                    " armazenamento, unidade, inativo, tipo, client_id)" +
                                    " values (@descricao, @estoqueatual, @codigobarras, @valorcusto, @valorvenda, @valorpromocional, @margemlucro, " +
                                    "@descontopermitido, @ncm, @cest, @tributacao_saida, @grupo, @marca, @armazenamento, @unidade, @inativo, @tipo, @client_id);";

                                dataInclude.Add("descricao", produto["DESCRICAO"].ToString());
                                dataInclude.Add("estoqueatual", Decimal.Parse(produto["ESTOQUEATUAL"].ToString()));
                                dataInclude.Add("codigobarras", produto["CODIGOBARRAS"].ToString());
                                dataInclude.Add("valorcusto", Decimal.Parse(produto["VALORCUSTO"].ToString()));
                                dataInclude.Add("valorvenda", Decimal.Parse(produto["VALORVENDA"].ToString()));
                                dataInclude.Add("valorpromocional", Decimal.Parse(produto["VALORPROMOCIONAL"].ToString()));
                                dataInclude.Add("margemlucro", Decimal.Parse(produto["MARGEMLUCRO"].ToString()));
                                dataInclude.Add("descontopermitido", Decimal.Parse(produto["DESCONTOPERMITIDO"].ToString()));
                                dataInclude.Add("ncm", produto["NCM"].ToString());
                                dataInclude.Add("cest", produto["CEST"].ToString());
                                dataInclude.Add("tributacao_saida", produto["TRIBUTAÇÃO SAÍDA"].ToString());
                                dataInclude.Add("grupo", produto["GRUPO"].ToString());
                                dataInclude.Add("marca", produto["MARCA"].ToString());
                                dataInclude.Add("armazenamento", produto["ARMAZENAMENTO"].ToString());
                                dataInclude.Add("unidade", produto["UNIDADE"].ToString());
                                dataInclude.Add("inativo", (produto["INATIVO"].ToString().Equals("Não") ? false : true));
                                dataInclude.Add("tipo", produto["TIPO"].ToString());
                                dataInclude.Add("client_id", Int32.Parse(cliente["ID"].ToString()));

                                try
                                {
                                    CrudUtils.ExecuteQuery(iConn, dataInclude, query, DOFunctions._connectionProperties.dbType);
                                }
                                catch(Exception ex)
                                {
                                    //MessageBox.Show(ex.Message);
                                }
                                

                                _form.OnSetLog($"cliente: {cliente["ID"]} - {produto["ID"]} - {produto["DESCRICAO"]}");
                            }
                            _form.OnClearLogs();

                        }
                        
                    }
                    
                }

                
            }
            catch(Exception ex) { 
                MessageBox.Show( ex.Message); 
            } finally
            {
                iConn.ConnectionClose(iConn.DoConnection, DOFunctions._connectionProperties.dbType);
                iConn.Dispose();
            }

            
        }
    }
}
