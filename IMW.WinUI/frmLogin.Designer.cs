namespace IMW.WinUI
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            lblComments = new Label();
            btnLogin = new Button();
            btnCancel = new Button();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblUserName = new Label();
            txtUserName = new TextBox();
            SuspendLayout();
            // 
            // lblComments
            // 
            lblComments.AutoSize = true;
            lblComments.Location = new Point(6, 13);
            lblComments.Name = "lblComments";
            lblComments.Size = new Size(10, 15);
            lblComments.TabIndex = 21;
            lblComments.Text = ".";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(132, 145);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(83, 32);
            btnLogin.TabIndex = 19;
            btnLogin.Text = "&Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(240, 145);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(83, 32);
            btnCancel.TabIndex = 20;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(42, 104);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 17;
            lblPassword.Text = "&Password";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(116, 100);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(245, 23);
            txtPassword.TabIndex = 18;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(55, 59);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(44, 15);
            lblUserName.TabIndex = 15;
            lblUserName.Text = "&User ID";
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(116, 55);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(245, 23);
            txtUserName.TabIndex = 16;
            // 
            // frmLogin
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(435, 201);
            Controls.Add(lblComments);
            Controls.Add(btnLogin);
            Controls.Add(btnCancel);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblUserName);
            Controls.Add(txtUserName);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Login ISC";
            Load += frmLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblComments;
        private Button btnLogin;
        private Button btnCancel;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblUserName;
        private TextBox txtUserName;
    }
}