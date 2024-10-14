using DoImportador.Connection;
using DoImportador.Enum;
using DoImportador.Model;
using DoImportador.Services;
using DoImportador.Utils;
using System.Windows.Forms;

namespace DoImportador
{
    public partial class Form1 : Form
    {
        public static string DbLocation = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_json_person.Text = loadPath();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txt_json_product.Text = loadPath();
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
                    var thread = new Thread(() => person.ImportData(data));
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
            }

        }

        private ConnectionProperties LoadPropertiesConnection()
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

            connection.hostDestination = host_destination.Text;
            connection.dbNameDestination = db_destination.Text;
            connection.userDestination = user_destination.Text;
            connection.passwordDestination = password_destination.Text;

            return connection;
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
                var thread = new Thread(() => product.ImportData(data));
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
    }
}
