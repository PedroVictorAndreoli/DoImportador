namespace DoImportador
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            Config = new TabPage();
            groupBox2 = new GroupBox();
            label6 = new Label();
            label7 = new Label();
            txt_db_destination = new TextBox();
            textBox3 = new TextBox();
            groupBox1 = new GroupBox();
            dsd = new Label();
            label5 = new Label();
            txt_db_origin = new TextBox();
            txt_db_name = new TextBox();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            SQL = new RadioButton();
            button5 = new Button();
            button6 = new Button();
            label3 = new Label();
            txt_json_animals = new TextBox();
            button3 = new Button();
            button4 = new Button();
            label2 = new Label();
            txt_json_product = new TextBox();
            button2 = new Button();
            button1 = new Button();
            label1 = new Label();
            txt_json_person = new TextBox();
            tabPage2 = new TabPage();
            label4 = new Label();
            txt_sql = new RichTextBox();
            tabControl1.SuspendLayout();
            Config.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Config);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(1, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(876, 556);
            tabControl1.TabIndex = 0;
            // 
            // Config
            // 
            Config.Controls.Add(groupBox2);
            Config.Controls.Add(groupBox1);
            Config.Controls.Add(radioButton3);
            Config.Controls.Add(radioButton2);
            Config.Controls.Add(SQL);
            Config.Controls.Add(button5);
            Config.Controls.Add(button6);
            Config.Controls.Add(label3);
            Config.Controls.Add(txt_json_animals);
            Config.Controls.Add(button3);
            Config.Controls.Add(button4);
            Config.Controls.Add(label2);
            Config.Controls.Add(txt_json_product);
            Config.Controls.Add(button2);
            Config.Controls.Add(button1);
            Config.Controls.Add(label1);
            Config.Controls.Add(txt_json_person);
            Config.Location = new Point(4, 24);
            Config.Name = "Config";
            Config.Padding = new Padding(3);
            Config.Size = new Size(868, 528);
            Config.TabIndex = 0;
            Config.Text = "Config";
            Config.UseVisualStyleBackColor = true;
            Config.Click += tabPage1_Click_1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(txt_db_destination);
            groupBox2.Controls.Add(textBox3);
            groupBox2.Location = new Point(439, 26);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(420, 93);
            groupBox2.TabIndex = 34;
            groupBox2.TabStop = false;
            groupBox2.Text = "Destination";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 30);
            label6.Name = "label6";
            label6.Size = new Size(71, 15);
            label6.TabIndex = 15;
            label6.Text = "DB Location";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(212, 30);
            label7.Name = "label7";
            label7.Size = new Size(87, 15);
            label7.TabIndex = 32;
            label7.Text = "DBNameOrigin";
            // 
            // txt_db_destination
            // 
            txt_db_destination.Location = new Point(6, 48);
            txt_db_destination.Name = "txt_db_destination";
            txt_db_destination.Size = new Size(200, 23);
            txt_db_destination.TabIndex = 14;
            txt_db_destination.Text = "localhost";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(212, 48);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(200, 23);
            textBox3.TabIndex = 31;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dsd);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txt_db_origin);
            groupBox1.Controls.Add(txt_db_name);
            groupBox1.Location = new Point(7, 26);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(420, 93);
            groupBox1.TabIndex = 33;
            groupBox1.TabStop = false;
            groupBox1.Text = "Origin";
            // 
            // dsd
            // 
            dsd.AutoSize = true;
            dsd.Location = new Point(6, 30);
            dsd.Name = "dsd";
            dsd.Size = new Size(71, 15);
            dsd.TabIndex = 15;
            dsd.Text = "DB Location";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(212, 30);
            label5.Name = "label5";
            label5.Size = new Size(87, 15);
            label5.TabIndex = 32;
            label5.Text = "DBNameOrigin";
            // 
            // txt_db_origin
            // 
            txt_db_origin.Location = new Point(6, 48);
            txt_db_origin.Name = "txt_db_origin";
            txt_db_origin.Size = new Size(200, 23);
            txt_db_origin.TabIndex = 14;
            // 
            // txt_db_name
            // 
            txt_db_name.Location = new Point(212, 48);
            txt_db_name.Name = "txt_db_name";
            txt_db_name.Size = new Size(200, 23);
            txt_db_name.TabIndex = 31;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(118, 296);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(46, 19);
            radioButton3.TabIndex = 30;
            radioButton3.TabStop = true;
            radioButton3.Text = "CSV";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(59, 296);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(53, 19);
            radioButton2.TabIndex = 29;
            radioButton2.TabStop = true;
            radioButton2.Text = "JSON";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // SQL
            // 
            SQL.AutoSize = true;
            SQL.Location = new Point(7, 296);
            SQL.Name = "SQL";
            SQL.Size = new Size(46, 19);
            SQL.TabIndex = 28;
            SQL.TabStop = true;
            SQL.Text = "SQL";
            SQL.UseVisualStyleBackColor = true;
            SQL.CheckedChanged += SQL_CheckedChanged;
            // 
            // button5
            // 
            button5.Location = new Point(769, 453);
            button5.Name = "button5";
            button5.Size = new Size(90, 23);
            button5.TabIndex = 27;
            button5.Text = "Import";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(664, 453);
            button6.Name = "button6";
            button6.Size = new Size(97, 23);
            button6.TabIndex = 26;
            button6.Text = "Load file";
            button6.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 435);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 25;
            label3.Text = "Load file animals";
            // 
            // txt_json_animals
            // 
            txt_json_animals.Location = new Point(6, 453);
            txt_json_animals.Name = "txt_json_animals";
            txt_json_animals.ReadOnly = true;
            txt_json_animals.Size = new Size(652, 23);
            txt_json_animals.TabIndex = 24;
            // 
            // button3
            // 
            button3.Location = new Point(769, 395);
            button3.Name = "button3";
            button3.Size = new Size(90, 23);
            button3.TabIndex = 23;
            button3.Text = "Import";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(664, 395);
            button4.Name = "button4";
            button4.Size = new Size(97, 23);
            button4.TabIndex = 22;
            button4.Text = "Load file";
            button4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 377);
            label2.Name = "label2";
            label2.Size = new Size(97, 15);
            label2.TabIndex = 21;
            label2.Text = "Load file product";
            // 
            // txt_json_product
            // 
            txt_json_product.Location = new Point(6, 395);
            txt_json_product.Name = "txt_json_product";
            txt_json_product.ReadOnly = true;
            txt_json_product.Size = new Size(652, 23);
            txt_json_product.TabIndex = 20;
            // 
            // button2
            // 
            button2.Location = new Point(769, 346);
            button2.Name = "button2";
            button2.Size = new Size(90, 23);
            button2.TabIndex = 19;
            button2.Text = "Import";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // button1
            // 
            button1.Location = new Point(664, 346);
            button1.Name = "button1";
            button1.Size = new Size(97, 23);
            button1.TabIndex = 18;
            button1.Text = "Load file";
            button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 328);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 17;
            label1.Text = "Load file Person";
            // 
            // txt_json_person
            // 
            txt_json_person.Location = new Point(6, 346);
            txt_json_person.Name = "txt_json_person";
            txt_json_person.ReadOnly = true;
            txt_json_person.Size = new Size(652, 23);
            txt_json_person.TabIndex = 16;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(txt_sql);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(868, 528);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "SQL";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 10);
            label4.Name = "label4";
            label4.Size = new Size(28, 15);
            label4.TabIndex = 16;
            label4.Text = "SQL";
            // 
            // txt_sql
            // 
            txt_sql.Location = new Point(3, 28);
            txt_sql.Name = "txt_sql";
            txt_sql.Size = new Size(862, 500);
            txt_sql.TabIndex = 0;
            txt_sql.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(873, 560);
            Controls.Add(tabControl1);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            Config.ResumeLayout(false);
            Config.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage Config;
        private Button button5;
        private Button button6;
        private Label label3;
        private TextBox txt_json_animals;
        private Button button3;
        private Button button4;
        private Label label2;
        private TextBox txt_json_product;
        private Button button2;
        private Button button1;
        private Label label1;
        private TextBox txt_json_person;
        private Label dsd;
        private TextBox txt_db_origin;
        private TabPage tabPage2;
        private RichTextBox txt_sql;
        private Label label4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton SQL;
        private Label label5;
        private TextBox txt_db_name;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label6;
        private Label label7;
        private TextBox txt_db_destination;
        private TextBox textBox3;
    }
}
