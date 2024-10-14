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
            button8 = new Button();
            label10 = new Label();
            password_destination = new TextBox();
            label11 = new Label();
            user_destination = new TextBox();
            label6 = new Label();
            label7 = new Label();
            host_destination = new TextBox();
            db_destination = new TextBox();
            groupBox1 = new GroupBox();
            label12 = new Label();
            port_origin = new TextBox();
            cmb_db = new ComboBox();
            button7 = new Button();
            label9 = new Label();
            password_origin = new TextBox();
            label8 = new Label();
            user_origin = new TextBox();
            dsd = new Label();
            label5 = new Label();
            host_origin = new TextBox();
            db_origin = new TextBox();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            che_sql = new RadioButton();
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
            tabPage1 = new TabPage();
            txt_logs = new RichTextBox();
            tabControl1.SuspendLayout();
            Config.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Config);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage1);
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
            Config.Controls.Add(che_sql);
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
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button8);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(password_destination);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(user_destination);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(host_destination);
            groupBox2.Controls.Add(db_destination);
            groupBox2.Location = new Point(439, 26);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(420, 157);
            groupBox2.TabIndex = 34;
            groupBox2.TabStop = false;
            groupBox2.Text = "Destination";
            // 
            // button8
            // 
            button8.Location = new Point(324, 126);
            button8.Name = "button8";
            button8.Size = new Size(90, 23);
            button8.TabIndex = 37;
            button8.Text = "Test connect";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(212, 79);
            label10.Name = "label10";
            label10.Size = new Size(57, 15);
            label10.TabIndex = 40;
            label10.Text = "Password";
            // 
            // password_destination
            // 
            password_destination.Location = new Point(212, 97);
            password_destination.Name = "password_destination";
            password_destination.Size = new Size(200, 23);
            password_destination.TabIndex = 39;
            password_destination.Text = "Atmus@#4080";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 79);
            label11.Name = "label11";
            label11.Size = new Size(30, 15);
            label11.TabIndex = 38;
            label11.Text = "User";
            // 
            // user_destination
            // 
            user_destination.Location = new Point(6, 97);
            user_destination.Name = "user_destination";
            user_destination.Size = new Size(200, 23);
            user_destination.TabIndex = 37;
            user_destination.Text = "atmusinf";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 30);
            label6.Name = "label6";
            label6.Size = new Size(32, 15);
            label6.TabIndex = 15;
            label6.Text = "Host";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(212, 30);
            label7.Name = "label7";
            label7.Size = new Size(57, 15);
            label7.TabIndex = 32;
            label7.Text = "DB Name";
            // 
            // host_destination
            // 
            host_destination.Location = new Point(6, 48);
            host_destination.Name = "host_destination";
            host_destination.Size = new Size(200, 23);
            host_destination.TabIndex = 14;
            host_destination.Text = "localhost\\MSSQLSERVER2022";
            // 
            // db_destination
            // 
            db_destination.Location = new Point(212, 48);
            db_destination.Name = "db_destination";
            db_destination.Size = new Size(200, 23);
            db_destination.TabIndex = 31;
            db_destination.Text = "atmusinf_Control-3344";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(port_origin);
            groupBox1.Controls.Add(cmb_db);
            groupBox1.Controls.Add(button7);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(password_origin);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(user_origin);
            groupBox1.Controls.Add(dsd);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(host_origin);
            groupBox1.Controls.Add(db_origin);
            groupBox1.Location = new Point(7, 26);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(420, 157);
            groupBox1.TabIndex = 33;
            groupBox1.TabStop = false;
            groupBox1.Text = "Origin";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(156, 30);
            label12.Name = "label12";
            label12.Size = new Size(29, 15);
            label12.TabIndex = 37;
            label12.Text = "Port";
            // 
            // port_origin
            // 
            port_origin.Location = new Point(156, 48);
            port_origin.Name = "port_origin";
            port_origin.Size = new Size(50, 23);
            port_origin.TabIndex = 35;
            // 
            // cmb_db
            // 
            cmb_db.FormattingEnabled = true;
            cmb_db.Items.AddRange(new object[] { "SQLSERVER", "POSTGRESSQL", "MYSQL" });
            cmb_db.Location = new Point(6, 127);
            cmb_db.Name = "cmb_db";
            cmb_db.Size = new Size(200, 23);
            cmb_db.TabIndex = 35;
            cmb_db.Text = "SQLSERVER";
            cmb_db.SelectedIndexChanged += cmb_db_SelectedIndexChanged;
            // 
            // button7
            // 
            button7.Location = new Point(322, 126);
            button7.Name = "button7";
            button7.Size = new Size(90, 23);
            button7.TabIndex = 35;
            button7.Text = "Test connect";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(212, 79);
            label9.Name = "label9";
            label9.Size = new Size(57, 15);
            label9.TabIndex = 36;
            label9.Text = "Password";
            // 
            // password_origin
            // 
            password_origin.Location = new Point(212, 97);
            password_origin.Name = "password_origin";
            password_origin.Size = new Size(200, 23);
            password_origin.TabIndex = 35;
            password_origin.Text = "Atmus@#4080";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 79);
            label8.Name = "label8";
            label8.Size = new Size(30, 15);
            label8.TabIndex = 34;
            label8.Text = "User";
            // 
            // user_origin
            // 
            user_origin.Location = new Point(6, 97);
            user_origin.Name = "user_origin";
            user_origin.Size = new Size(200, 23);
            user_origin.TabIndex = 33;
            user_origin.Text = "sa";
            // 
            // dsd
            // 
            dsd.AutoSize = true;
            dsd.Location = new Point(6, 30);
            dsd.Name = "dsd";
            dsd.Size = new Size(32, 15);
            dsd.TabIndex = 15;
            dsd.Text = "Host";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(212, 30);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 32;
            label5.Text = "DB Name";
            // 
            // host_origin
            // 
            host_origin.Location = new Point(6, 48);
            host_origin.Name = "host_origin";
            host_origin.Size = new Size(144, 23);
            host_origin.TabIndex = 14;
            host_origin.Text = "vm.geovane-linux.com";
            // 
            // db_origin
            // 
            db_origin.Location = new Point(212, 48);
            db_origin.Name = "db_origin";
            db_origin.Size = new Size(200, 23);
            db_origin.TabIndex = 31;
            db_origin.Text = "sismoura";
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(118, 189);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(46, 19);
            radioButton3.TabIndex = 30;
            radioButton3.Text = "CSV";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(59, 189);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(53, 19);
            radioButton2.TabIndex = 29;
            radioButton2.Text = "JSON";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // che_sql
            // 
            che_sql.AutoSize = true;
            che_sql.Checked = true;
            che_sql.Location = new Point(7, 189);
            che_sql.Name = "che_sql";
            che_sql.Size = new Size(46, 19);
            che_sql.TabIndex = 28;
            che_sql.TabStop = true;
            che_sql.Text = "SQL";
            che_sql.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(769, 346);
            button5.Name = "button5";
            button5.Size = new Size(90, 23);
            button5.TabIndex = 27;
            button5.Text = "Import";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(664, 346);
            button6.Name = "button6";
            button6.Size = new Size(97, 23);
            button6.TabIndex = 26;
            button6.Text = "Load file";
            button6.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 328);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 25;
            label3.Text = "Load file animals";
            // 
            // txt_json_animals
            // 
            txt_json_animals.Location = new Point(6, 346);
            txt_json_animals.Name = "txt_json_animals";
            txt_json_animals.ReadOnly = true;
            txt_json_animals.Size = new Size(652, 23);
            txt_json_animals.TabIndex = 24;
            // 
            // button3
            // 
            button3.Location = new Point(769, 288);
            button3.Name = "button3";
            button3.Size = new Size(90, 23);
            button3.TabIndex = 23;
            button3.Text = "Import";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(664, 288);
            button4.Name = "button4";
            button4.Size = new Size(97, 23);
            button4.TabIndex = 22;
            button4.Text = "Load file";
            button4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 270);
            label2.Name = "label2";
            label2.Size = new Size(97, 15);
            label2.TabIndex = 21;
            label2.Text = "Load file product";
            // 
            // txt_json_product
            // 
            txt_json_product.Location = new Point(6, 288);
            txt_json_product.Name = "txt_json_product";
            txt_json_product.ReadOnly = true;
            txt_json_product.Size = new Size(652, 23);
            txt_json_product.TabIndex = 20;
            // 
            // button2
            // 
            button2.Location = new Point(769, 239);
            button2.Name = "button2";
            button2.Size = new Size(90, 23);
            button2.TabIndex = 19;
            button2.Text = "Import";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // button1
            // 
            button1.Location = new Point(664, 239);
            button1.Name = "button1";
            button1.Size = new Size(97, 23);
            button1.TabIndex = 18;
            button1.Text = "Load file";
            button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 221);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 17;
            label1.Text = "Load file Person";
            // 
            // txt_json_person
            // 
            txt_json_person.Location = new Point(6, 239);
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
            // tabPage1
            // 
            tabPage1.Controls.Add(txt_logs);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(868, 528);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Logs";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // txt_logs
            // 
            txt_logs.Location = new Point(3, 28);
            txt_logs.Name = "txt_logs";
            txt_logs.Size = new Size(862, 500);
            txt_logs.TabIndex = 1;
            txt_logs.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(873, 560);
            Controls.Add(tabControl1);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Importador DataOn";
            tabControl1.ResumeLayout(false);
            Config.ResumeLayout(false);
            Config.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage1.ResumeLayout(false);
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
        private TextBox host_origin;
        private TabPage tabPage2;
        private RichTextBox txt_sql;
        private Label label4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton che_sql;
        private Label label5;
        private TextBox db_origin;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label6;
        private Label label7;
        private TextBox host_destination;
        private TextBox db_destination;
        private Label label10;
        private TextBox password_destination;
        private Label label11;
        private TextBox user_destination;
        private Label label9;
        private TextBox password_origin;
        private Label label8;
        private TextBox user_origin;
        private Button button8;
        private Button button7;
        private TabPage tabPage1;
        private RichTextBox txt_logs;
        private ComboBox cmb_db;
        private Label label12;
        private TextBox port_origin;
    }
}
