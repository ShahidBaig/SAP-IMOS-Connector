namespace IMW.WinUI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmBillOfMaterial : Form
    {
        private IContainer components = null;
        private Button button2;
        private Button button1;
        private Label label13;
        private TextBox textBox8;
        private Label label12;
        private TextBox textBox7;
        private Label label11;
        private ComboBox comboBox3;
        private DataGridView dataGridView1;
        private DateTimePicker dateTimePicker3;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private Label label6;
        private ComboBox comboBox2;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private TextBox textBox5;
        private TextBox textBox6;
        private Label label5;
        private ComboBox comboBox1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private DataGridViewTextBoxColumn dgcSr;
        private DataGridViewTextBoxColumn dgcItemNo;
        private DataGridViewTextBoxColumn dgcQuantity;

        public frmBillOfMaterial()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            button2 = new Button();
            button1 = new Button();
            label13 = new Label();
            textBox8 = new TextBox();
            label12 = new Label();
            textBox7 = new TextBox();
            label11 = new Label();
            comboBox3 = new ComboBox();
            dataGridView1 = new DataGridView();
            dgcSr = new DataGridViewTextBoxColumn();
            dgcItemNo = new DataGridViewTextBoxColumn();
            dgcQuantity = new DataGridViewTextBoxColumn();
            dateTimePicker3 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            dateTimePicker1 = new DateTimePicker();
            label6 = new Label();
            comboBox2 = new ComboBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            label5 = new Label();
            comboBox1 = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            ((ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(90, 652);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(88, 47);
            button2.TabIndex = 86;
            button2.Text = "Add";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(197, 652);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(88, 47);
            button1.TabIndex = 85;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(56, 543);
            label13.Name = "label13";
            label13.Size = new Size(65, 20);
            label13.TabIndex = 72;
            label13.Text = "Remarks";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(171, 543);
            textBox8.Margin = new Padding(3, 4, 3, 4);
            textBox8.Multiline = true;
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(589, 75);
            textBox8.TabIndex = 71;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(56, 508);
            label12.Name = "label12";
            label12.Size = new Size(52, 20);
            label12.TabIndex = 70;
            label12.Text = "Owner";
            // 
            // textBox7
            // 
            textBox7.Location = new Point(171, 508);
            textBox7.Margin = new Padding(3, 4, 3, 4);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(217, 27);
            textBox7.TabIndex = 69;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(56, 479);
            label11.Name = "label11";
            label11.Size = new Size(75, 20);
            label11.TabIndex = 68;
            label11.Text = "Employee";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(171, 469);
            comboBox3.Margin = new Padding(3, 4, 3, 4);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(217, 28);
            comboBox3.TabIndex = 67;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dgcSr, dgcItemNo, dgcQuantity });
            dataGridView1.Location = new Point(149, 228);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new Size(510, 235);
            dataGridView1.TabIndex = 66;
            // 
            // dgcSr
            // 
            dgcSr.HeaderText = "#";
            dgcSr.MinimumWidth = 6;
            dgcSr.Name = "dgcSr";
            dgcSr.Width = 125;
            // 
            // dgcItemNo
            // 
            dgcItemNo.HeaderText = "Item No";
            dgcItemNo.MinimumWidth = 6;
            dgcItemNo.Name = "dgcItemNo";
            dgcItemNo.Width = 125;
            // 
            // dgcQuantity
            // 
            dgcQuantity.HeaderText = "Quantity";
            dgcQuantity.MinimumWidth = 6;
            dgcQuantity.Name = "dgcQuantity";
            dgcQuantity.Width = 125;
            // 
            // dateTimePicker3
            // 
            dateTimePicker3.CustomFormat = "";
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.Location = new Point(584, 117);
            dateTimePicker3.Margin = new Padding(3, 4, 3, 4);
            dateTimePicker3.Name = "dateTimePicker3";
            dateTimePicker3.Size = new Size(157, 27);
            dateTimePicker3.TabIndex = 65;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CustomFormat = "";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Location = new Point(584, 152);
            dateTimePicker2.Margin = new Padding(3, 4, 3, 4);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(157, 27);
            dateTimePicker2.TabIndex = 64;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(584, 192);
            dateTimePicker1.Margin = new Padding(3, 4, 3, 4);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(157, 27);
            dateTimePicker1.TabIndex = 63;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(443, 199);
            label6.Name = "label6";
            label6.Size = new Size(114, 20);
            label6.TabIndex = 62;
            label6.Text = "Document Date";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(494, 47);
            comboBox2.Margin = new Padding(3, 4, 3, 4);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(77, 28);
            comboBox2.TabIndex = 61;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(443, 161);
            label7.Name = "label7";
            label7.Size = new Size(99, 20);
            label7.TabIndex = 60;
            label7.Text = "Delivery Date";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(443, 127);
            label8.Name = "label8";
            label8.Size = new Size(93, 20);
            label8.TabIndex = 59;
            label8.Text = "Posting Date";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(443, 91);
            label9.Name = "label9";
            label9.Size = new Size(49, 20);
            label9.TabIndex = 58;
            label9.Text = "Status";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(443, 51);
            label10.Name = "label10";
            label10.Size = new Size(32, 20);
            label10.TabIndex = 57;
            label10.Text = "No.";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(584, 51);
            textBox5.Margin = new Padding(3, 4, 3, 4);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(175, 27);
            textBox5.TabIndex = 56;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(584, 85);
            textBox6.Margin = new Padding(3, 4, 3, 4);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(175, 27);
            textBox6.TabIndex = 55;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(56, 199);
            label5.Name = "label5";
            label5.Size = new Size(105, 20);
            label5.TabIndex = 54;
            label5.Text = "Local Currency";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(197, 189);
            comboBox1.Margin = new Padding(3, 4, 3, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(217, 28);
            comboBox1.TabIndex = 53;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(56, 161);
            label4.Name = "label4";
            label4.Size = new Size(131, 20);
            label4.TabIndex = 52;
            label4.Text = "Customer. Ref. No.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(56, 127);
            label3.Name = "label3";
            label3.Size = new Size(107, 20);
            label3.TabIndex = 51;
            label3.Text = "Contact Person";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(56, 91);
            label2.Name = "label2";
            label2.Size = new Size(49, 20);
            label2.TabIndex = 50;
            label2.Text = "Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(56, 51);
            label1.Name = "label1";
            label1.Size = new Size(72, 20);
            label1.TabIndex = 49;
            label1.Text = "Customer";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(197, 51);
            textBox4.Margin = new Padding(3, 4, 3, 4);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(217, 27);
            textBox4.TabIndex = 48;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(197, 85);
            textBox3.Margin = new Padding(3, 4, 3, 4);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(217, 27);
            textBox3.TabIndex = 47;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(197, 120);
            textBox2.Margin = new Padding(3, 4, 3, 4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(217, 27);
            textBox2.TabIndex = 46;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(197, 155);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(217, 27);
            textBox1.TabIndex = 45;
            // 
            // frmBillOfMaterial
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(854, 745);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label13);
            Controls.Add(textBox8);
            Controls.Add(label12);
            Controls.Add(textBox7);
            Controls.Add(label11);
            Controls.Add(comboBox3);
            Controls.Add(dataGridView1);
            Controls.Add(dateTimePicker3);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Controls.Add(label6);
            Controls.Add(comboBox2);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(label10);
            Controls.Add(textBox5);
            Controls.Add(textBox6);
            Controls.Add(label5);
            Controls.Add(comboBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmBillOfMaterial";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bill Of Material";
            ((ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

