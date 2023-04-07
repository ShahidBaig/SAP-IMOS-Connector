using Timer = System.Threading.Timer;

namespace IMW.WinUI
{
    partial class frmSplash
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
            components = new System.ComponentModel.Container();
            lblLoading = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // lblLoading
            // 
            lblLoading.BackColor = Color.Transparent;
            lblLoading.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblLoading.ForeColor = Color.Red;
            lblLoading.Location = new Point(-2, 411);
            lblLoading.Name = "lblLoading";
            lblLoading.Size = new Size(223, 18);
            lblLoading.TabIndex = 2;
            lblLoading.Text = "Loading Application ...";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // frmSplash
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.splash;
            ClientSize = new Size(492, 438);
            ControlBox = false;
            Controls.Add(lblLoading);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmSplash";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
        }

        #endregion

        private Label lblLoading;
        private System.Windows.Forms.Timer timer1;
    }
}