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
            txt_db = new TextBox();
            dsd = new Label();
            txt_json_person = new TextBox();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            label2 = new Label();
            txt_json_product = new TextBox();
            SuspendLayout();
            // 
            // txt_db
            // 
            txt_db.Location = new Point(12, 31);
            txt_db.Name = "txt_db";
            txt_db.Size = new Size(200, 23);
            txt_db.TabIndex = 0;
            // 
            // dsd
            // 
            dsd.AutoSize = true;
            dsd.Location = new Point(12, 13);
            dsd.Name = "dsd";
            dsd.Size = new Size(71, 15);
            dsd.TabIndex = 1;
            dsd.Text = "DB Location";
            // 
            // txt_json_person
            // 
            txt_json_person.Location = new Point(12, 98);
            txt_json_person.Name = "txt_json_person";
            txt_json_person.Size = new Size(432, 23);
            txt_json_person.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 80);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 3;
            label1.Text = "Load file Person";
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(450, 98);
            button1.Name = "button1";
            button1.Size = new Size(99, 23);
            button1.TabIndex = 4;
            button1.Text = "Load file";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(555, 98);
            button2.Name = "button2";
            button2.Size = new Size(92, 23);
            button2.TabIndex = 5;
            button2.Text = "Import";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(555, 147);
            button3.Name = "button3";
            button3.Size = new Size(92, 23);
            button3.TabIndex = 9;
            button3.Text = "Import";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(450, 147);
            button4.Name = "button4";
            button4.Size = new Size(99, 23);
            button4.TabIndex = 8;
            button4.Text = "Load file";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 129);
            label2.Name = "label2";
            label2.Size = new Size(97, 15);
            label2.TabIndex = 7;
            label2.Text = "Load file product";
            label2.Click += label2_Click;
            // 
            // txt_json_product
            // 
            txt_json_product.Location = new Point(12, 147);
            txt_json_product.Name = "txt_json_product";
            txt_json_product.Size = new Size(432, 23);
            txt_json_product.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(659, 450);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(label2);
            Controls.Add(txt_json_product);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(txt_json_person);
            Controls.Add(dsd);
            Controls.Add(txt_db);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_db;
        private Label dsd;
        private TextBox txt_json_person;
        private Label label1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Label label2;
        private TextBox txt_json_product;
    }
}
