using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        public static string playerName;

        public Form1() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
        }

        private void CheckFields(object sender, EventArgs e)
        {
            btnStart.Enabled = txtName.Text != "" && txtW.Text != "" && txtH.Text != "" && txtMines.Text != "";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Введите имя!");
                return;
            }

            int w = int.Parse(txtW.Text);
            int h = int.Parse(txtH.Text);
            int m = int.Parse(txtMines.Text);

            if (w <= 0 || h <= 0 || m < 0 || m > 100)
            {
                MessageBox.Show("Некорректные значения!");
                return;
            }

            playerName = txtName.Text;
            GameForm game = new GameForm(w, h, m);
            game.Owner = this;
            game.Show();
            this.Hide();
        }
    }
}