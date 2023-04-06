namespace IMW.WinUI
{
    partial class UserManager
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
            this.UserID = new System.Windows.Forms.TextBox();
            this.UID = new System.Windows.Forms.Label();
            this.FirstName = new System.Windows.Forms.TextBox();
            this.FName = new System.Windows.Forms.Label();
            this.LName = new System.Windows.Forms.Label();
            this.LastName = new System.Windows.Forms.TextBox();
            this.Pass = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.IsAdmin = new System.Windows.Forms.CheckBox();
            this.Blocked = new System.Windows.Forms.CheckBox();
            this.menuDetailGrid = new System.Windows.Forms.DataGridView();
            this.Modify = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.New = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.menuDetailGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // UserID
            // 
            this.UserID.Location = new System.Drawing.Point(77, 21);
            this.UserID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UserID.MaxLength = 20;
            this.UserID.Name = "UserID";
            this.UserID.Size = new System.Drawing.Size(173, 23);
            this.UserID.TabIndex = 0;
            this.UserID.KeyDown += UserIDKeyPress;  
            // 
            // UID
            // 
            this.UID.AutoSize = true;
            this.UID.Location = new System.Drawing.Point(10, 26);
            this.UID.Name = "UID";
            this.UID.Size = new System.Drawing.Size(44, 15);
            this.UID.TabIndex = 1;
            this.UID.Text = "User ID";
            // 
            // FirstName
            // 
            this.FirstName.Location = new System.Drawing.Point(336, 21);
            this.FirstName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FirstName.MaxLength = 50;
            this.FirstName.Name = "FirstName";
            this.FirstName.Size = new System.Drawing.Size(220, 23);
            this.FirstName.TabIndex = 2;
            // 
            // FName
            // 
            this.FName.AutoSize = true;
            this.FName.Location = new System.Drawing.Point(261, 26);
            this.FName.Name = "FName";
            this.FName.Size = new System.Drawing.Size(64, 15);
            this.FName.TabIndex = 3;
            this.FName.Text = "First Name";
            // 
            // LName
            // 
            this.LName.AutoSize = true;
            this.LName.Location = new System.Drawing.Point(570, 26);
            this.LName.Name = "LName";
            this.LName.Size = new System.Drawing.Size(63, 15);
            this.LName.TabIndex = 4;
            this.LName.Text = "Last Name";
            // 
            // LastName
            // 
            this.LastName.Location = new System.Drawing.Point(644, 21);
            this.LastName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LastName.MaxLength = 50;
            this.LastName.Name = "LastName";
            this.LastName.Size = new System.Drawing.Size(162, 23);
            this.LastName.TabIndex = 5;
            // 
            // Pass
            // 
            this.Pass.AutoSize = true;
            this.Pass.Location = new System.Drawing.Point(10, 83);
            this.Pass.Name = "Pass";
            this.Pass.Size = new System.Drawing.Size(57, 15);
            this.Pass.TabIndex = 6;
            this.Pass.Text = "Password";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(77, 80);
            this.Password.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Password.MaxLength = 50;
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(173, 23);
            this.Password.TabIndex = 7;
            this.Password.Text = "Password";
            // 
            // IsAdmin
            // 
            this.IsAdmin.AutoSize = true;
            this.IsAdmin.Location = new System.Drawing.Point(336, 82);
            this.IsAdmin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IsAdmin.Name = "IsAdmin";
            this.IsAdmin.Size = new System.Drawing.Size(88, 19);
            this.IsAdmin.TabIndex = 8;
            this.IsAdmin.Text = "Admin User";
            this.IsAdmin.UseVisualStyleBackColor = true;
            this.IsAdmin.CheckedChanged += IsAdmin_CheckedChanged;
            // 
            // Blocked
            // 
            this.Blocked.AutoSize = true;
            this.Blocked.Location = new System.Drawing.Point(570, 82);
            this.Blocked.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Blocked.Name = "Blocked";
            this.Blocked.Size = new System.Drawing.Size(68, 19);
            this.Blocked.TabIndex = 9;
            this.Blocked.Text = "Blocked";
            this.Blocked.UseVisualStyleBackColor = true;
            // 
            // menuDetailGrid
            // 
            this.menuDetailGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.menuDetailGrid.Location = new System.Drawing.Point(77, 145);
            this.menuDetailGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.menuDetailGrid.Name = "menuDetailGrid";
            this.menuDetailGrid.RowHeadersWidth = 51;
            this.menuDetailGrid.RowTemplate.Height = 29;
            this.menuDetailGrid.Size = new System.Drawing.Size(640, 208);
            this.menuDetailGrid.TabIndex = 10;
            this.menuDetailGrid.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Modify
            // 
            this.Modify.Location = new System.Drawing.Point(173, 393);
            this.Modify.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Modify.Name = "Modify";
            this.Modify.Size = new System.Drawing.Size(82, 22);
            this.Modify.TabIndex = 11;
            this.Modify.Text = "Modify";
            this.Modify.UseVisualStyleBackColor = true;
            this.Modify.Click += Modify_Click;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(348, 393);
            this.Save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(82, 22);
            this.Save.TabIndex = 12;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += Save_Click;
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(433, 393);
            this.Delete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(82, 22);
            this.Delete.TabIndex = 13;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += Delete_Click;
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(521, 393);
            this.Cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 22);
            this.Cancel.TabIndex = 14;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += Cancel_Click;
            // 
            // New
            // 
            this.New.Location = new System.Drawing.Point(261, 393);
            this.New.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(82, 22);
            this.New.TabIndex = 15;
            this.New.Text = "New";
            this.New.UseVisualStyleBackColor = true;
            this.New.Click += New_Click;
            // 
            // UserManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 434);
            this.Controls.Add(this.New);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Modify);
            this.Controls.Add(this.menuDetailGrid);
            this.Controls.Add(this.Blocked);
            this.Controls.Add(this.IsAdmin);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Pass);
            this.Controls.Add(this.LastName);
            this.Controls.Add(this.LName);
            this.Controls.Add(this.FName);
            this.Controls.Add(this.FirstName);
            this.Controls.Add(this.UID);
            this.Controls.Add(this.UserID);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UserManager";
            this.ShowInTaskbar = false;
            this.Text = "User Manager";
            ((System.ComponentModel.ISupportInitialize)(this.menuDetailGrid)).EndInit();
            this.Load += new System.EventHandler(this.UserManager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox UserID;
        private Label UID;
        private TextBox FirstName;
        private Label FName;
        private Label LName;
        private TextBox LastName;
        private Label Pass;
        private TextBox Password;
        private CheckBox IsAdmin;
        private CheckBox Blocked;
        private DataGridView menuDetailGrid;
        private Button Modify;
        private Button Save;
        private Button Delete;
        private Button Cancel;
        private Button New;
    }
}