using doAPI.Utils;
using DoImportador.Connection;
using DoImportador.Enum;
using DoImportador.Model;
using DoImportador.Services;
using DoImportador.Utils;
using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Asn1.X509;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DoImportador
{
    public partial class Form1 : Form
    {
        public static string DbLocation = "";
        public Form1()
        {
            InitializeComponent();
        }



        private string loadPath()
        {
            var folder = new OpenFileDialog();

            if (folder.ShowDialog() == DialogResult.OK)
            {
                return folder.FileName;
            }
            return "";
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());

            var person = new Person(this);

            if (che_sql.Checked)
            {
                var data = LoadData.LoadDataDb(db_origin.Text, txt_sql.Text);
                if (data != null)
                {
                    var thread = new Thread(() => person.ImportData(data, che_person_original_id.Checked));
                    thread.Start();
                }
                else
                {
                    MessageBox.Show("Nenhum dado foi carregado");
                }


            }
        }

        public void OnSetLog(string log)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnSetLog(log)));
            }
            else
            {
                txt_logs.AppendText(log + Environment.NewLine);
                txt_logs.SelectionStart = txt_logs.Text.Length;
                txt_logs.ScrollToCaret();
            }
        }

        public void OnClearLogs()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnClearLogs()));
            }
            else
            {
                txt_logs.Clear();
            }
        }

        public void OnSetLogCurrentLine(string log)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnSetLogCurrentLine(log)));
            }
            else
            {
                txt_logs.AppendText(log);
                txt_logs.SelectionStart = txt_logs.Text.Length;
                txt_logs.ScrollToCaret();
            }
        }

        public ConnectionProperties LoadPropertiesConnection()
        {
            var connection = new ConnectionProperties();
            connection.hostOrigin = host_origin.Text;
            connection.dbNameOrigin = db_origin.Text;
            connection.userOrigin = user_origin.Text;
            connection.passwordOrigin = password_origin.Text;
            connection.portOrigin = port_origin.Text;

            connection.dbType = cmb_db.Text switch
            {
                "SQLSERVER" => EnumProviderType.SQLServer,
                "POSTGRESSQL" => EnumProviderType.PostGreSQL,
                "MYSQL" => EnumProviderType.MySql,
                _ => EnumProviderType.SQLServer
            };

            connection.url = cmb_url.Text switch
            {
                "LOCAL" => "https://localhost:5001/api/",
                "SERVER" => "https://api.dataon.com.br/v2/api/",
                "SERVER - DEV" => "https://api.dataon.com.br/v2/api/",
                _ => "https://localhost:5001/api/"
            };

            connection.hostDestination = host_destination.Text;
            connection.dbNameDestination = db_destination.Text;
            connection.userDestination = user_destination.Text;
            connection.passwordDestination = password_destination.Text;

            return connection;
        }


        public List<ConnectionProperties> LoadPropertiesConnectionAllDataBases()
        {
            string connectionString = "Server=127.0.0.1;Database=master;Integrated Security=True;TrustServerCertificate=True;";
            List<string> connectionStrings = new List<string>();

            using (SqlConnection connectionn = new SqlConnection(connectionString))
            {
                try
                {
                    connectionn.Open();
                    Console.WriteLine("Conectado ao SQL Server!");


                    string query = "SELECT name FROM sys.databases";
                    using (SqlCommand command = new SqlCommand(query, connectionn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.GetString(0).Contains("atmusinf"))
                                    connectionStrings.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar ou executar a consulta: " + ex.Message);
                }
            }

            List<ConnectionProperties> connectionProperties = new List<ConnectionProperties>();
            foreach (string con in connectionStrings)
            {
                var connection = new ConnectionProperties();
                connection.hostOrigin = "127.0.0.1";
                connection.dbNameOrigin = con;
                connection.userOrigin = "atmusinf";
                connection.passwordOrigin = "Atmus@#4080";
                connection.portOrigin = port_origin.Text;
                connection.dbType = EnumProviderType.SQLServer;
                connection.url = "https://localhost:5001/api/";
                connectionProperties.Add(connection);
            }

            return connectionProperties;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var iConn = new DOConn();
            try
            {
                DOFunctions.LoadHost(LoadPropertiesConnection());
                iConn.ConnectionOpen("", Enum.EnumDataLake.ORIGIN);

                MessageBox.Show("Conex�o obtida com sucesso");
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

        private void button8_Click(object sender, EventArgs e)
        {
            var iConn = new DOConn();
            try
            {
                DOFunctions.LoadHost(LoadPropertiesConnection());
                iConn.ConnectionOpen("", Enum.EnumDataLake.DESTINATION);

                MessageBox.Show("Conex�o obtida com sucesso");
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

        private void button5_Click(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());

            var animals = new Animals(this);

            if (che_sql.Checked)
            {
                var data = LoadData.LoadDataDb(db_origin.Text, txt_sql.Text);
                var thread = new Thread(() => animals.ImportData(data));
                thread.Start();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());

            var product = new Product(this);

            if (che_sql.Checked)
            {
                var data = LoadData.LoadDataDb(db_origin.Text, txt_sql.Text);
                var thread = new Thread(() => product.ImportData(data, check_original_id.Checked));
                thread.Start();

            }
        }

        private void cmb_db_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmb_db.Text.Equals("SQLSERVER"))
            {
                db_origin.Text = "";
                user_origin.Text = "sa";
                password_origin.Text = "Atmus@#4080";
                port_origin.Text = "";
            }
            else if (cmb_db.Text.Equals("POSTGRESSQL"))
            {
                db_origin.Text = "";
                user_origin.Text = "postgres";
                password_origin.Text = "553322";
                port_origin.Text = "5434";
            }
            else if (cmb_db.Text.Equals("MYSQL"))
            {
                db_origin.Text = "";
                user_origin.Text = "root";
                password_origin.Text = "553322";
                port_origin.Text = "3306";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Config_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());

            var pacotes = new VetPacotes(this);

            if (che_sql.Checked)
            {
                var data = LoadData.LoadDataDb(db_origin.Text, txt_sql.Text);
                var thread = new Thread(() => pacotes.ImportData(data));
                thread.Start();

            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ID;\nCodigo;\nTipo(Fisica,Juridica);\nNome;\nDataNascimento;\nCPF;\nRG;\nCNPJ;\nRazaoSocial;\nInscricaoEstadual;\nFone_Residencial;\nFone_Celular;\nEndereco_Principal;\nBairro_Principal;\nComplemento_Principal;\nNumero_Principal;\nCEP_Principal;\nEmail_Principal;\nEmail_Cobranca;\nFone_Comercial;\nTipoPessoa(Cliente;Fornecedor)", "Campos Obrigatirios");

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ID;\nTipo;\nDescricao;\nCodigoBarra;\nUnidade;\nMarca;\nGrupo;\nEstoqueMinimo;\nEstoqueMaximo;\nEstoqueAtual;\nValorCompra;\nMargemLucro;\nValorVenda;\nDescontoPermitido;\nNCM;\nCEST;\nEAN;\nCFOP;\nTributacao;\nTipoVet", "Campos Obrigatirios");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ID;\nNomeAnimal;\nCodigoDono;\nNomeDono;\nDataNascimento;\nSexo;\nRaca;\nEspecie;\nCor;\nTemperamento;\nPelo;\nDieta;\nOlhos;\nObservacao;\nChipNumero;\nObito;\nPorte;\nPeso", "Campos Obrigatirios");


        }

        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ID;\nDataAgendamento;\nDataExecutado;\nIDProduto;\nDescricao;\nIDAnimal;\nNomeAnimal;\nIDPessoa;\nNomePessoa;\nValor;\nObservacoes;\nStatus;\nStatusAgenda(Tabela agenda_status_agendamentos)", "Campos Obrigatirios");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Descricao;\nIDProduto;\nIDAnimal;\nNomeAnimal;\nDataAgendamento;\nStatusAgenda;\nDataExecutado;\nIDPessoa;\nNomePEssoa;\nAnamnese;\nStatus;\nValor;\nTemperatura;\nPeso;\nExameFisico;\nDiagnostico;\nCondutaClinica", "Campos Obrigatirios");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ID;\nTipoDuplicata;\nIDPessoa;\nDataEmissao;\nDataVencimento;\nDataPagamento;\nValor;\nDescricao;\nTipoPagamento", "Campos Obrigatirios");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            MessageBox.Show("N�o Implementado", "Campos Obrigatirios");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            MessageBox.Show("N�o Implementado", "Campos Obrigatirios");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            MessageBox.Show("N�o Implementado", "Campos Obrigatirios");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Descricao;\nIDProduto;\nIDAnimal;\nNomeAnimal;\nDataAgendamento;\nStatus;\nDataExecutado;\nNomePessoa;\nIDPessoa;\nObservacoes;\nValor;\nStatusAgenda;", "Campos Obrigatirios");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            MessageBox.Show("N�o Implementado", "Campos Obrigatirios");
        }

        private void button28_Click(object sender, EventArgs e)
        {

            DOFunctions.LoadHost(LoadPropertiesConnection());

            var import = new VetVacinas(this);

            if (che_sql.Checked)
            {
                var data = LoadData.LoadDataDb(db_origin.Text, txt_sql.Text);
                var thread = new Thread(() => import.ImportData(data));
                thread.Start();

            }
        }

        private void button33_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());

            var import = new Financial(this);

            if (che_sql.Checked)
            {
                var data = LoadData.LoadDataDb(db_origin.Text, txt_sql.Text);
                var thread = new Thread(() => import.ImportData(data));
                thread.Start();

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());

            var import = new VetConsulta(this);

            if (che_sql.Checked)
            {
                var data = LoadData.LoadDataDb(db_origin.Text, txt_sql.Text);
                var thread = new Thread(() => import.ImportData(data));
                thread.Start();

            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());

            var import = new Product(this);

            var thread = new Thread(() => import.ImportProductsAllDatabase());
            thread.Start();


        }

        private void button40_Click(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());
            if (txtbase.Text.Trim() == "")
            {
                MessageBox.Show("Digite a base que se quer o backup");
                return;
            }

            var bk = new BackupService(this, txtbase.Text);

            var thread = new Thread(() => bk.GetBackup());
            thread.Start();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            OnClearLogs();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());

            var import = new Person(this);

            if (che_sql.Checked)
            {
                var data = LoadData.LoadDataDb(db_origin.Text, txt_sql.Text);
                var thread = new Thread(() => import.CorretionCity(data));
                thread.Start();

            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());

            var import = new Product(this);

            var thread = new Thread(() => import.UpdateProductEcommerce());
            thread.Start();
        }

        private void button31_Click(object sender, EventArgs e)
        {

        }

        private void button35_Click(object sender, EventArgs e)
        {

        }

        private void button44_Click(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());

            var import = new VetExame(this);

            var thread = new Thread(() => import.UpdateProductToExam());
            thread.Start();
        }

        private void button45_Click(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());

            var person = new Person(this);

            if (che_sql.Checked)
            {
                var data = LoadData.LoadDataDb(db_origin.Text, txt_sql.Text);
                var dataOriginal = LoadData.LoadDataDb("SQLSERVER", "Select * from Pessoas", EnumDataLake.DESTINATION);
                if (data != null)
                {
                    var iConn = new DOConn();
                    iConn.ConnectionOpen("", Enum.EnumDataLake.DESTINATION);
                    var listData = new List<IDictionary>();
                    data.ForEach(x =>
                    {
                        try
                        {
                            var has = dataOriginal.Find(e => e["Nome"].ToString().Equals(x["Nome"].ToString()));
                            if (has == null)
                                listData.Add(x);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    });

                    var thread = new Thread(() => person.ImportData(listData, che_person_original_id.Checked));
                    thread.Start();
                }
                else
                {
                    MessageBox.Show("Nenhum dado foi carregado");
                }


            }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(LoadPropertiesConnection());

            var person = new Product(this);

            if (che_sql.Checked)
            {
                var data = LoadData.LoadDataDb(db_origin.Text, txt_sql.Text);
                var dataOriginal = LoadData.LoadDataDb("SQLSERVER", "Select * from produtos", EnumDataLake.DESTINATION);
                if (data != null)
                {
                    var iConn = new DOConn();
                    iConn.ConnectionOpen("", Enum.EnumDataLake.DESTINATION);
                    var listData = new List<IDictionary>();
                    data.ForEach(x =>
                    {
                        try
                        {
                            var has = dataOriginal.Find(e => e["Descricao"].ToString().Equals(x["Descricao"].ToString()));
                            if (has == null)
                                listData.Add(x);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    });

                    var thread = new Thread(() => person.ImportData(listData, check_original_id.Checked));
                    thread.Start();
                }
                else
                {
                    MessageBox.Show("Nenhum dado foi carregado");
                }
            }
        }

        private void button47_Click(object sender, EventArgs e)
        {
            Integracao integracao = new(this);
            var thread = new Thread(() => integracao.migraProduto(LoadPropertiesConnectionAllDataBases()));
            thread.Start();
        }

        private void button48_Click(object sender, EventArgs e)
        {
            Integracao integracao = new(this);
            var thread = new Thread(() => integracao.migraPessoa(LoadPropertiesConnectionAllDataBases()));
            thread.Start();
        }

        private void button49_Click(object sender, EventArgs e)
        {
            Integracao integracao = new(this);
            var thread = new Thread(() => integracao.migraGrupoSubGrupo(LoadPropertiesConnectionAllDataBases()));
            thread.Start();
        }

        private void button50_Click(object sender, EventArgs e)
        {
            Integracao integracao = new(this);
            var thread = new Thread(() => integracao.migraProdutoMarca(LoadPropertiesConnectionAllDataBases()));
            thread.Start();
        }

        private void button51_Click(object sender, EventArgs e)
        {
            Integracao integracao = new(this);
            var thread = new Thread(() => integracao.migraProdutosUnidades(LoadPropertiesConnectionAllDataBases()));
            thread.Start();
        }
    }
}
