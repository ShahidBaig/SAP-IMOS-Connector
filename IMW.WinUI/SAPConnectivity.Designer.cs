namespace IMW.WinUI
{
	partial class SAPConnectivity
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtData = new System.Windows.Forms.TextBox();
            this.dgvQueryData = new System.Windows.Forms.DataGridView();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnGetGroups = new System.Windows.Forms.Button();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnForceSyncOpportunity = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueryData)).BeginInit();
            this.pnlFilters.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAdd.Location = new System.Drawing.Point(16, 105);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(163, 40);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Execute";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click_1);
            // 
            // txtData
            // 
            this.txtData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtData.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtData.Location = new System.Drawing.Point(0, 0);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(901, 179);
            this.txtData.TabIndex = 1;
            // 
            // dgvQueryData
            // 
            this.dgvQueryData.BackgroundColor = System.Drawing.Color.White;
            this.dgvQueryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueryData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQueryData.Location = new System.Drawing.Point(0, 179);
            this.dgvQueryData.Name = "dgvQueryData";
            this.dgvQueryData.RowTemplate.Height = 25;
            this.dgvQueryData.Size = new System.Drawing.Size(901, 463);
            this.dgvQueryData.TabIndex = 3;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCount.Location = new System.Drawing.Point(16, 148);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(90, 21);
            this.lblCount.TabIndex = 4;
            this.lblCount.Text = "Row Count:";
            // 
            // btnGetGroups
            // 
            this.btnGetGroups.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGetGroups.Location = new System.Drawing.Point(16, 4);
            this.btnGetGroups.Name = "btnGetGroups";
            this.btnGetGroups.Size = new System.Drawing.Size(163, 40);
            this.btnGetGroups.TabIndex = 5;
            this.btnGetGroups.Text = "Force Sync Items";
            this.btnGetGroups.UseVisualStyleBackColor = true;
            this.btnGetGroups.Click += new System.EventHandler(this.btnGetGroups_Click);
            // 
            // pnlFilters
            // 
            this.pnlFilters.Controls.Add(this.pnlButtons);
            this.pnlFilters.Controls.Add(this.txtData);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(0, 0);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Size = new System.Drawing.Size(901, 179);
            this.pnlFilters.TabIndex = 17;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnForceSyncOpportunity);
            this.pnlButtons.Controls.Add(this.btnGetGroups);
            this.pnlButtons.Controls.Add(this.lblCount);
            this.pnlButtons.Controls.Add(this.btnAdd);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButtons.Location = new System.Drawing.Point(710, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(191, 179);
            this.pnlButtons.TabIndex = 17;
            // 
            // btnForceSyncOpportunity
            // 
            this.btnForceSyncOpportunity.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnForceSyncOpportunity.Location = new System.Drawing.Point(16, 46);
            this.btnForceSyncOpportunity.Name = "btnForceSyncOpportunity";
            this.btnForceSyncOpportunity.Size = new System.Drawing.Size(163, 53);
            this.btnForceSyncOpportunity.TabIndex = 6;
            this.btnForceSyncOpportunity.Text = "Force Sync Opportunity";
            this.btnForceSyncOpportunity.UseVisualStyleBackColor = true;
            this.btnForceSyncOpportunity.Click += new System.EventHandler(this.btnForceSyncOpportunity_Click);
            // 
            // SAPConnectivity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 642);
            this.Controls.Add(this.dgvQueryData);
            this.Controls.Add(this.pnlFilters);
            this.MinimizeBox = false;
            this.Name = "SAPConnectivity";
            this.Text = "SAP Connectivity";
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueryData)).EndInit();
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private Button btnAdd;
		private TextBox txtData;
		private DataGridView dgvQueryData;
		private Label lblCount;
		private Button btnGetGroups;
        private Panel pnlFilters;
        private Panel pnlButtons;
        private Button btnForceSyncOpportunity;
    }
}