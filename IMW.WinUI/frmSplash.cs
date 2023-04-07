using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMW.WinUI
{
    public partial class frmSplash : Form
    {
        int counter = 1;

        public frmSplash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblLoading.Text = this.lblLoading.Text + ".";
            counter++;

            if (counter == 5)
            {
                this.Hide();
            }
        }
    }
}
