using DoImportador.Services;
using DoImportador.Utils;

namespace DoImportador
{
    public partial class Form1 : Form
    {
        public static string DbLocation = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_json_person.Text = loadPath();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txt_json_product.Text = loadPath();
        }

        private void label2_Click(object sender, EventArgs e)
        {

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

        private void button6_Click(object sender, EventArgs e)
        {
            txt_json_animals.Text = loadPath();
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }

        private void SQL_CheckedChanged(object sender, EventArgs e)
        {

        }
        
        private void button2_Click_1(object sender, EventArgs e)
        {
            DOFunctions.LoadHost(txt_db_origin.Text, txt_db_destination.Text);
            var data = LoadData.LoadDataDb(txt_db_name.Text, txt_sql.Text);

            Console.WriteLine(data);


        }
    }
}
