using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Iks_Oks_RG
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.game;
            pictureBox.Image = Properties.Resources.game1;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtBoxPlayer1.Text != "" && txtBoxPlayer2.Text != "")
            {
                // Pozivam konstruktor za Formu u kojoj je igra kome prosledjujem ime igraca 1 i ime igraca 2
                mainForm mf = new mainForm(txtBoxPlayer1.Text, txtBoxPlayer2.Text);
                this.Hide();
                mf.ShowDialog();
            }
            else
            {
                MessageBox.Show("Unesite imena igača!");
            }
        }
    }
}
