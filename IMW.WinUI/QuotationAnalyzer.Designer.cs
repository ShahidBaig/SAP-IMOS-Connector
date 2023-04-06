namespace IMW.WinUI
{
    partial class QuotationAnalyzer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_DocNo = new System.Windows.Forms.Label();
            this.l_frmDate_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.lbl_FromDate = new System.Windows.Forms.Label();
            this.l_chkboxFromDate = new System.Windows.Forms.CheckBox();
            this.lbl_ToDate = new System.Windows.Forms.Label();
            this.l_ToDate_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.l_chkboxToDate = new System.Windows.Forms.CheckBox();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.l_dataGridView = new System.Windows.Forms.DataGridView();
            this.l_StatusCombobox = new System.Windows.Forms.ComboBox();
            this.l_SearchData = new System.Windows.Forms.Button();
            this.l_DocNoUpDown = new System.Windows.Forms.NumericUpDown();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.l_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.l_DocNoUpDown)).BeginInit();
            this.pnlFilters.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_DocNo
            // 
            this.lbl_DocNo.AutoSize = true;
            this.lbl_DocNo.Location = new System.Drawing.Point(50, 20);
            this.lbl_DocNo.Name = "lbl_DocNo";
            this.lbl_DocNo.Size = new System.Drawing.Size(45, 15);
            this.lbl_DocNo.TabIndex = 0;
            this.lbl_DocNo.Text = "Doc no";
            // 
            // l_frmDate_dateTimePicker
            // 
            this.l_frmDate_dateTimePicker.Location = new System.Drawing.Point(397, 12);
            this.l_frmDate_dateTimePicker.Name = "l_frmDate_dateTimePicker";
            this.l_frmDate_dateTimePicker.Size = new System.Drawing.Size(200, 23);
            this.l_frmDate_dateTimePicker.TabIndex = 2;
            // 
            // lbl_FromDate
            // 
            this.lbl_FromDate.AutoSize = true;
            this.lbl_FromDate.Location = new System.Drawing.Point(329, 20);
            this.lbl_FromDate.Name = "lbl_FromDate";
            this.lbl_FromDate.Size = new System.Drawing.Size(62, 15);
            this.lbl_FromDate.TabIndex = 4;
            this.lbl_FromDate.Text = "From Date";
            // 
            // l_chkboxFromDate
            // 
            this.l_chkboxFromDate.AutoSize = true;
            this.l_chkboxFromDate.Location = new System.Drawing.Point(310, 21);
            this.l_chkboxFromDate.Name = "l_chkboxFromDate";
            this.l_chkboxFromDate.Size = new System.Drawing.Size(15, 14);
            this.l_chkboxFromDate.TabIndex = 5;
            this.l_chkboxFromDate.UseVisualStyleBackColor = true;
            // 
            // lbl_ToDate
            // 
            this.lbl_ToDate.AutoSize = true;
            this.lbl_ToDate.Location = new System.Drawing.Point(345, 62);
            this.lbl_ToDate.Name = "lbl_ToDate";
            this.lbl_ToDate.Size = new System.Drawing.Size(46, 15);
            this.lbl_ToDate.TabIndex = 6;
            this.lbl_ToDate.Text = "To Date";
            // 
            // l_ToDate_dateTimePicker
            // 
            this.l_ToDate_dateTimePicker.Location = new System.Drawing.Point(397, 54);
            this.l_ToDate_dateTimePicker.Name = "l_ToDate_dateTimePicker";
            this.l_ToDate_dateTimePicker.Size = new System.Drawing.Size(200, 23);
            this.l_ToDate_dateTimePicker.TabIndex = 7;
            // 
            // l_chkboxToDate
            // 
            this.l_chkboxToDate.AutoSize = true;
            this.l_chkboxToDate.Location = new System.Drawing.Point(324, 63);
            this.l_chkboxToDate.Name = "l_chkboxToDate";
            this.l_chkboxToDate.Size = new System.Drawing.Size(15, 14);
            this.l_chkboxToDate.TabIndex = 8;
            this.l_chkboxToDate.UseVisualStyleBackColor = true;
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Location = new System.Drawing.Point(56, 62);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(39, 15);
            this.lbl_Status.TabIndex = 9;
            this.lbl_Status.Text = "Status";
            // 
            // l_dataGridView
            // 
            this.l_dataGridView.AllowUserToAddRows = false;
            this.l_dataGridView.AllowUserToDeleteRows = false;
            this.l_dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.l_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.l_dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.l_dataGridView.Location = new System.Drawing.Point(0, 96);
            this.l_dataGridView.Name = "l_dataGridView";
            this.l_dataGridView.ReadOnly = true;
            this.l_dataGridView.RowTemplate.Height = 25;
            this.l_dataGridView.Size = new System.Drawing.Size(892, 464);
            this.l_dataGridView.TabIndex = 11;
            // 
            // l_StatusCombobox
            // 
            this.l_StatusCombobox.FormattingEnabled = true;
            this.l_StatusCombobox.Items.AddRange(new object[] {
            "PostedToISC",
            "PostedToIMOS",
            "ReceivedFromIMOS",
            "PostedTOSAP"});
            this.l_StatusCombobox.Location = new System.Drawing.Point(101, 54);
            this.l_StatusCombobox.Name = "l_StatusCombobox";
            this.l_StatusCombobox.Size = new System.Drawing.Size(165, 23);
            this.l_StatusCombobox.TabIndex = 12;
            // 
            // l_SearchData
            // 
            this.l_SearchData.Location = new System.Drawing.Point(16, 50);
            this.l_SearchData.Name = "l_SearchData";
            this.l_SearchData.Size = new System.Drawing.Size(122, 27);
            this.l_SearchData.TabIndex = 13;
            this.l_SearchData.Text = "Search";
            this.l_SearchData.UseVisualStyleBackColor = true;
            // 
            // l_DocNoUpDown
            // 
            this.l_DocNoUpDown.Location = new System.Drawing.Point(101, 12);
            this.l_DocNoUpDown.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.l_DocNoUpDown.Name = "l_DocNoUpDown";
            this.l_DocNoUpDown.Size = new System.Drawing.Size(165, 23);
            this.l_DocNoUpDown.TabIndex = 14;
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(16, 12);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(122, 27);
            this.btn_Clear.TabIndex = 15;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            // 
            // pnlFilters
            // 
            this.pnlFilters.Controls.Add(this.l_ToDate_dateTimePicker);
            this.pnlFilters.Controls.Add(this.l_DocNoUpDown);
            this.pnlFilters.Controls.Add(this.lbl_DocNo);
            this.pnlFilters.Controls.Add(this.l_frmDate_dateTimePicker);
            this.pnlFilters.Controls.Add(this.l_StatusCombobox);
            this.pnlFilters.Controls.Add(this.lbl_FromDate);
            this.pnlFilters.Controls.Add(this.l_chkboxFromDate);
            this.pnlFilters.Controls.Add(this.lbl_Status);
            this.pnlFilters.Controls.Add(this.lbl_ToDate);
            this.pnlFilters.Controls.Add(this.l_chkboxToDate);
            this.pnlFilters.Controls.Add(this.pnlButtons);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(0, 0);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Size = new System.Drawing.Size(892, 96);
            this.pnlFilters.TabIndex = 16;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btn_Clear);
            this.pnlButtons.Controls.Add(this.l_SearchData);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButtons.Location = new System.Drawing.Point(742, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(150, 96);
            this.pnlButtons.TabIndex = 17;
            // 
            // QuotationAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 560);
            this.Controls.Add(this.l_dataGridView);
            this.Controls.Add(this.pnlFilters);
            this.MinimizeBox = false;
            this.Name = "QuotationAnalyzer";
            this.ShowInTaskbar = false;
            this.Text = "Quotation Analyzer";
            ((System.ComponentModel.ISupportInitialize)(this.l_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.l_DocNoUpDown)).EndInit();
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void L_chkboxToDate_CheckedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label lbl_DocNo;
        private DateTimePicker l_frmDate_dateTimePicker;
        private Label lbl_FromDate;
        private CheckBox l_chkboxFromDate;
        private Label lbl_ToDate;
        private DateTimePicker l_ToDate_dateTimePicker;
        private CheckBox l_chkboxToDate;
        private Label lbl_Status;
        private DataGridView l_dataGridView;
        private ComboBox l_StatusCombobox;
        private Button l_SearchData;
        private NumericUpDown l_DocNoUpDown;
        private Button btn_Clear;
        private Panel pnlFilters;
        private Panel pnlButtons;
    }
}