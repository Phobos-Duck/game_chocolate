using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_course
{
    public partial class Form4 : Form
    {
        Random r = new Random();
        Color[] pallete = new Color[2];
        int rows = 12, cols = 5, j0 = 0, i0 = 0, c = 1, m1, m2, play = 1, total_1 = 0, total_2 = 0;
        bool turn = false, gameover = false;

        private int countPlayers;

        public Form4(int number, Color color_1, Color color_2)
        {
            InitializeComponent();
            countPlayers = number;
            pallete[0] = color_1;
            pallete[1] = color_2;

            dataGridView1.RowCount = rows;
            dataGridView1.ColumnCount = cols;
            for (int j = 0; j < rows; j++)
            {
                dataGridView1.Rows[j].Height = 30;  // Устанавливаем меньшую высоту для строк
            }
            for (int i = 0; i < cols; i++)
            {
                dataGridView1.Columns[i].Width = 43;  // Ширина столбцов
            }
        }
        void course_game()
        {
            if (countPlayers == 2)
            {
                if (play == 1)
                    listBox1.Items.Add("Игрок 1: " + total_1 + "\r\n");
                else
                {
                    if (play == 2)
                    {
                        listBox1.Items.Add("Игрок 2: " + total_2 + "\r\n");
                    }
                }
            }
            else
            {
                if (countPlayers == 1)
                {
                    listBox1.Items.Add("Ваш ход: " + total_1 + "\r\n");
                    listBox1.Items.Add("Ход компьютера: " + total_2 + "\r\n");
                }
            }
        }
        void total_paintCells()
        {
            total_1 = 0; total_2 = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Style.BackColor == pallete[1])
                    {
                        total_1++;
                    }
                    else if (cell.Style.BackColor == pallete[0])
                    {
                        total_2++;
                    }
                }
            }
        }
        private void clean_cells()
        {
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    dataGridView1[i, j].Style.BackColor = Color.Empty;
                    c = 1;

                }
            }
            listBox1.Items.Clear();
        }
        private void poisen_cells(int x, int y)
        {
            if (gameover)
            {
                return;
            }
            DataGridViewCell selectedCell = dataGridView1.Rows[j0].Cells[i0];
            if (x == i0 && y == j0 || selectedCell.Style.BackColor == pallete[1] || selectedCell.Style.BackColor == pallete[0])
            {
                if (selectedCell.Style.BackColor == pallete[0])
                {
                    selectedCell.Style.BackColor = Color.Red;
                    gameover = true;
                    if (countPlayers == 1)
                    {
                        MessageBox.Show("Game Over! Я отравился((");
                    }
                    else
                    {
                        MessageBox.Show("Отравился второй игрок!");
                    }

                }
                else if (selectedCell.Style.BackColor == pallete[1])
                {
                    selectedCell.Style.BackColor = Color.Red;
                    gameover = true;
                    if (countPlayers == 1)
                    {
                        MessageBox.Show("Game Over! Вы отравились((");
                    }
                    else
                    {
                        MessageBox.Show("Отравился первый игрок!");
                    }

                }
            }
        }
        private void random_cells()
        {

            i0 = r.Next(cols);
            j0 = r.Next(rows);


        }
        private void machine_game()
        {
            if (!turn)
            {
                for (int i = 0; i < cols; i++)
                {
                    int sum = 0;
                    for (int j = 0; j < rows; j++)
                    {
                        if (dataGridView1[i, j].Style.BackColor == Color.Empty)
                            sum++;
                    }
                    if (sum > 0 && sum % 2 == 0)
                    {
                        for (int j = 0; j < rows; j++)
                        {
                            if (dataGridView1[i, j].Style.BackColor == Color.Empty)
                            {
                                panel1.BackColor = pallete[1];
                                colorful_cells(i, j, 0);
                                poisen_cells(i, j);
                                turn = true;
                                return;
                            }
                        }
                    }
                }

                int x, y;
                do
                {
                    x = r.Next(cols);
                    y = r.Next(rows);
                } while (dataGridView1[x, y].Style.BackColor != Color.Empty);

                colorful_cells(x, y, 0);
                poisen_cells(x, y);
                turn = true;
            }
        }
        private void colorful_cells(int j, int i, int step)
        {

            int distanceToLeft = j;
            int distanceToRight = dataGridView1.Columns.Count - 1 - j;

            // Определение, к какому краю ближе выбранная клетка
            if (distanceToLeft <= distanceToRight)
            {
                // Клетка ближе к левому краю, закрашиваем слева
                for (int y = 0; y <= i; y++)
                {
                    for (int x = 0; x <= j; x++)
                    {
                        if (step == 0)
                        {
                            if (dataGridView1[x, y].Style.BackColor == Color.Empty || dataGridView1[x, y].Style.BackColor != pallete[1])
                            {
                                dataGridView1[x, y].Style.BackColor = pallete[step];
                            }
                            else
                            {
                                continue;
                            }

                        }
                        else
                        {
                            if (dataGridView1[x, y].Style.BackColor == Color.Empty || dataGridView1[x, y].Style.BackColor != pallete[0])
                            {
                                dataGridView1[x, y].Style.BackColor = pallete[step];
                            }
                            else
                            {
                                continue;
                            }

                        }
                    }
                }
            }
            else
            {
                // Клетка ближе к правому краю, закрашиваем справа
                for (int y = 0; y <= i; y++)
                {
                    for (int x = j; x < dataGridView1.Columns.Count; x++)
                    {
                        if (step == 0)
                        {
                            if (dataGridView1[x, y].Style.BackColor == Color.Empty || dataGridView1[x, y].Style.BackColor != pallete[1])
                            {
                                dataGridView1[x, y].Style.BackColor = pallete[step];
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (dataGridView1[x, y].Style.BackColor == Color.Empty || dataGridView1[x, y].Style.BackColor != pallete[0])
                            {
                                dataGridView1[x, y].Style.BackColor = pallete[step];
                            }
                            else
                            {
                                continue;
                            }
                        }

                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int j = e.ColumnIndex, i = e.RowIndex;
            if (gameover)
            {
                return;
            }
            if (c == 1)
            {
                groupBox1.Hide();
                random_cells();
                c = 0;
            }
            if (countPlayers == 1)
            {
                panel1.BackColor = pallete[0];
                colorful_cells(j, i, 1);
                turn = false;
                machine_game();
                poisen_cells(j, i);
                total_paintCells();
                course_game();
            }
            else if (countPlayers == 2)
            {
                if (play == 1)
                {
                    panel1.BackColor = pallete[0];
                    colorful_cells(j, i, 1);
                    total_paintCells();
                    course_game();
                    poisen_cells(j, i);
                    play = 2;

                }

                else if (play == 2)
                {
                    panel1.BackColor = pallete[1];
                    colorful_cells(j, i, 0);
                    total_paintCells();
                    course_game();
                    poisen_cells(j, i);
                    play = 1;
                }


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Empty;
            gameover = false;
            groupBox1.Show();
            clean_cells();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
