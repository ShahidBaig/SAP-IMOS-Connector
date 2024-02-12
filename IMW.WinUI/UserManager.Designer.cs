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
            UserID = new TextBox();
            UID = new Label();
            FirstName = new TextBox();
            FName = new Label();
            LName = new Label();
            LastName = new TextBox();
            Pass = new Label();
            Password = new TextBox();
            IsAdmin = new CheckBox();
            Blocked = new CheckBox();
            menuDetailGrid = new DataGridView();
            Modify = new Button();
            Save = new Button();
            Delete = new Button();
            Cancel = new Button();
            New = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            ((System.ComponentModel.ISupportInitialize)menuDetailGrid).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // UserID
            // 
            UserID.Location = new Point(94, 17);
            UserID.MaxLength = 20;
            UserID.Name = "UserID";
            UserID.Size = new Size(197, 27);
            UserID.TabIndex = 0;
            UserID.KeyDown += UserIDKeyPress;
            // 
            // UID
            // 
            UID.AutoSize = true;
            UID.Location = new Point(17, 24);
            UID.Name = "UID";
            UID.Size = new Size(57, 20);
            UID.TabIndex = 1;
            UID.Text = "User ID";
            // 
            // FirstName
            // 
            FirstName.Location = new Point(390, 17);
            FirstName.MaxLength = 50;
            FirstName.Name = "FirstName";
            FirstName.Size = new Size(251, 27);
            FirstName.TabIndex = 2;
            // 
            // FName
            // 
            FName.AutoSize = true;
            FName.Location = new Point(304, 24);
            FName.Name = "FName";
            FName.Size = new Size(80, 20);
            FName.TabIndex = 3;
            FName.Text = "First Name";
            // 
            // LName
            // 
            LName.AutoSize = true;
            LName.Location = new Point(657, 24);
            LName.Name = "LName";
            LName.Size = new Size(79, 20);
            LName.TabIndex = 4;
            LName.Text = "Last Name";
            // 
            // LastName
            // 
            LastName.Location = new Point(742, 17);
            LastName.MaxLength = 50;
            LastName.Name = "LastName";
            LastName.Size = new Size(185, 27);
            LastName.TabIndex = 5;
            // 
            // Pass
            // 
            Pass.AutoSize = true;
            Pass.Location = new Point(17, 68);
            Pass.Name = "Pass";
            Pass.Size = new Size(70, 20);
            Pass.TabIndex = 6;
            Pass.Text = "Password";
            // 
            // Password
            // 
            Password.Location = new Point(94, 64);
            Password.MaxLength = 50;
            Password.Name = "Password";
            Password.Size = new Size(197, 27);
            Password.TabIndex = 7;
            Password.Text = "Password";
            // 
            // IsAdmin
            // 
            IsAdmin.AutoSize = true;
            IsAdmin.Location = new Point(390, 66);
            IsAdmin.Name = "IsAdmin";
            IsAdmin.Size = new Size(108, 24);
            IsAdmin.TabIndex = 8;
            IsAdmin.Text = "Admin User";
            IsAdmin.UseVisualStyleBackColor = true;
            IsAdmin.CheckedChanged += IsAdmin_CheckedChanged;
            // 
            // Blocked
            // 
            Blocked.AutoSize = true;
            Blocked.Location = new Point(657, 66);
            Blocked.Name = "Blocked";
            Blocked.Size = new Size(84, 24);
            Blocked.TabIndex = 9;
            Blocked.Text = "Blocked";
            Blocked.UseVisualStyleBackColor = true;
            // 
            // menuDetailGrid
            // 
            menuDetailGrid.BackgroundColor = Color.White;
            menuDetailGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            menuDetailGrid.Dock = DockStyle.Fill;
            menuDetailGrid.Location = new Point(0, 117);
            menuDetailGrid.Name = "menuDetailGrid";
            menuDetailGrid.RowHeadersWidth = 51;
            menuDetailGrid.RowTemplate.Height = 29;
            menuDetailGrid.Size = new Size(942, 404);
            menuDetailGrid.TabIndex = 10;
            menuDetailGrid.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Modify
            // 
            Modify.Location = new Point(16, 17);
            Modify.Name = "Modify";
            Modify.Size = new Size(94, 29);
            Modify.TabIndex = 11;
            Modify.Text = "Modify";
            Modify.UseVisualStyleBackColor = true;
            Modify.Click += Modify_Click;
            // 
            // Save
            // 
            Save.Location = new Point(216, 17);
            Save.Name = "Save";
            Save.Size = new Size(94, 29);
            Save.TabIndex = 12;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // Delete
            // 
            Delete.Location = new Point(313, 17);
            Delete.Name = "Delete";
            Delete.Size = new Size(94, 29);
            Delete.TabIndex = 13;
            Delete.Text = "Delete";
            Delete.UseVisualStyleBackColor = true;
            Delete.Click += Delete_Click;
            // 
            // Cancel
            // 
            Cancel.Location = new Point(413, 17);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(94, 29);
            Cancel.TabIndex = 14;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // New
            // 
            New.Location = new Point(116, 17);
            New.Name = "New";
            New.Size = new Size(94, 29);
            New.TabIndex = 15;
            New.Text = "New";
            New.UseVisualStyleBackColor = true;
            New.Click += New_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 521);
            panel1.Name = "panel1";
            panel1.Size = new Size(942, 58);
            panel1.TabIndex = 16;
            // 
            // panel2
            // 
            panel2.Controls.Add(New);
            panel2.Controls.Add(Cancel);
            panel2.Controls.Add(Delete);
            panel2.Controls.Add(Save);
            panel2.Controls.Add(Modify);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(198, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(524, 58);
            panel2.TabIndex = 16;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(722, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(220, 58);
            panel3.TabIndex = 17;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(198, 58);
            panel4.TabIndex = 18;
            // 
            // panel5
            // 
            panel5.Controls.Add(Blocked);
            panel5.Controls.Add(IsAdmin);
            panel5.Controls.Add(Password);
            panel5.Controls.Add(Pass);
            panel5.Controls.Add(LastName);
            panel5.Controls.Add(LName);
            panel5.Controls.Add(FName);
            panel5.Controls.Add(FirstName);
            panel5.Controls.Add(UID);
            panel5.Controls.Add(UserID);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(942, 117);
            panel5.TabIndex = 17;
            // 
            // UserManager
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 579);
            Controls.Add(menuDetailGrid);
            Controls.Add(panel5);
            Controls.Add(panel1);
            Name = "UserManager";
            ShowInTaskbar = false;
            Text = "User Manager";
            Load += UserManager_Load;
            ((System.ComponentModel.ISupportInitialize)menuDetailGrid).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ResumeLayout(false);
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
        private Panel panel1;
        private Panel panel4;
        private Panel panel3;
        private Panel panel2;
        private Panel panel5;
    }
}