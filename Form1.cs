namespace DoImportador
{
    public partial class Form1 : Form
    {
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
    }
}
