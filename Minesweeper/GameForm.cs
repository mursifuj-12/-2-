using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Minesweeper
{
    public partial class GameForm : Form
    {
        Button[,] cells;
        bool[,] mines;
        int sizeX, sizeY, chance;

        public GameForm(int x, int y, int c)
        {
            InitializeComponent();
            sizeX = x; 
            sizeY = y; 
            chance = c;
            CreateGameField();
        }

        private void CreateGameField()
        {
            cells = new Button[sizeY, sizeX];
            mines = new bool[sizeY, sizeX];
            Random rnd = new Random();

            for (int i = 0; i < sizeY; i++) 
            {
                for (int j = 0; j < sizeX; j++) 
                {
                    mines[i, j] = rnd.Next(100) < chance;

                    cells[i, j] = new Button();
                    cells[i, j].FlatStyle = FlatStyle.Standard;
                    cells[i, j].Size = new Size(30, 30);
                    cells[i, j].Location = new Point(j * 30, i * 30);
                    cells[i, j].Tag = i + "," + j;
                    cells[i, j].MouseUp += Cell_MouseUp;
                    this.Controls.Add(cells[i, j]);
                }
            }

            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (!mines[i, j])
                    {
                        OpenCell(i, j);
                        return;
                    }
                }
            }
        }

        private void Cell_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            string[] coords = btn.Tag.ToString().Split(',');
            int i = int.Parse(coords[0]);
            int j = int.Parse(coords[1]);

            if (e.Button == MouseButtons.Right)
            {
                btn.Text = btn.Text == "🚩" ? "" : "🚩";
                return;
            }

            if (mines[i, j])
            {
                MessageBox.Show("Вы проиграли!");
                File.AppendAllText("stats.txt", "Поражение " + Form1.playerName + " " + sizeX + " " + sizeY + " " + chance + "\n");
                this.Close();
            }
            else
            {
                OpenCell(i, j);
                CheckWin();
            }
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }

        private void OpenCell(int i, int j)
        {
            if (cells[i, j].Enabled == false) return;
            int count = 0;

            for (int r = i - 1; r <= i + 1; r++)
            {
                for (int c = j - 1; c <= j + 1; c++)
                {
                    if (r >= 0 && r < sizeY && c >= 0 && c < sizeX && mines[r, c])
                        count++;
                }
            }

            cells[i, j].Text = count.ToString();
            cells[i, j].BackColor = Color.LightGray;
            cells[i, j].Enabled = false;
        }

        private void CheckWin()
        {
            for (int i = 0; i < sizeY; i++)
                for (int j = 0; j < sizeX; j++)
                    if (!mines[i, j] && cells[i, j].Enabled) return;

            MessageBox.Show("Победа!");
            File.AppendAllText("stats.txt", "Победа " + Form1.playerName + " " + sizeX + " " + sizeY + " " + chance + "\n");
            this.Close();
        }
    }
}