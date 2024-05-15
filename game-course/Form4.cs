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
using static System.Windows.Forms.AxHost;

namespace game_course
{
    public partial class Form4 : Form
    {
        Random r = new Random();
        List<int> xCol = new List<int>();
        List<int> yRow = new List<int>();
        Color[] pallete = new Color[2];
        int rows = 12, cols = 6, j0 = -1, i0 = -1, c = 1, play = 1, total_1 = 0, total_2 = 0;
        bool gameover = false, turn = false;

        int countPlayers;


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
                    listBox1.Items.Add("Ваш ход: " + total_2 + "\r\n");
                    listBox1.Items.Add("Ход компьютера: " + total_1 + "\r\n");
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
                    if (cell.Style.BackColor == pallete[0])
                    {
                        total_1++;
                    }
                    else if (cell.Style.BackColor == pallete[1])
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
        private void poisen_cells(int x1, int y1, int x2, int y2)
        {
            int startX = Math.Min(x1, x2);
            int endX = Math.Max(x1, x2);
            int startY = Math.Min(y1, y2);
            int endY = Math.Max(y1, y2);

            // Проверяем, содержит ли закрашенная область отравленную дольку
            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    if (i == i0 && j == j0)
                    {
                        gameover = true;
                        if (countPlayers == 1)
                        {
                            MessageBox.Show($"Game Over! Выиграл {(turn ? "игрок" : "компьютер")}");
                            return;
                        }
                        else
                        {
                            if (play == 1)
                            {
                                MessageBox.Show("Game Over! Выиграл второй игрок");
                            }
                            else if (play == 2)
                            {
                                MessageBox.Show("Game Over! Выиграл первый игрок");
                            }
                        }
                    }
                }
            }
        }
        private void random_cells()
        {

            i0 = r.Next(cols);
            j0 = r.Next(rows);
            dataGridView1[i0, j0].Style.BackColor = Color.Red;


        }
        private List<Tuple<int, int, int, int>> FindAvailableCombinations(int minX, int maxX, int minY, int maxY)
        {
            List<Tuple<int, int, int, int>> availableCombinations = new List<Tuple<int, int, int, int>>();

            // Проверяем, что текущая область содержит несколько строк
            if (maxY - minY > 0)
            {
                availableCombinations.Add(Tuple.Create(minX, minY, maxX, maxY));
            }

            // Проверяем, что текущая область содержит несколько столбцов
            if (maxX - minX > 0)
            {
                availableCombinations.Add(Tuple.Create(minX, minY, maxX, maxY));
            }

            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    if ((i != i0 || j != j0) && (i == minX || i == maxX || j == minY || j == maxY))
                    {
                        bool validRow = true;
                        bool validColumn = true;

                        // Проверяем, что включенные клетки образуют целую строку или столбец
                        for (int k = minX; k <= maxX; k++)
                        {
                            if (dataGridView1[k, j].Style.BackColor != Color.Empty)
                            {
                                validRow = false;
                                break;
                            }
                        }

                        for (int k = minY; k <= maxY; k++)
                        {
                            if (dataGridView1[i, k].Style.BackColor != Color.Empty)
                            {
                                validColumn = false;
                                break;
                            }
                        }

                        if (validRow || validColumn)
                        {
                            // Проверяем, что обе координаты выбранной клетки затрагивают текущие края
                            if ((i == minX || i == maxX) && (j == minY || j == maxY))
                            {
                                // Если выбрана строка, то добавляем все столбцы до конца
                                if (validRow)
                                {
                                    availableCombinations.Add(Tuple.Create(minX, j, maxX, j));
                                }

                                // Если выбран столбец, то добавляем все строки до конца
                                if (validColumn)
                                {
                                    availableCombinations.Add(Tuple.Create(i, minY, i, maxY));
                                }
                            }
                        }
                    }
                }
            }

            return availableCombinations;
        }

        private int GetCombinationCellCount(Tuple<int, int, int, int> combination)
        {
            int startX = Math.Min(combination.Item1, combination.Item3);
            int endX = Math.Max(combination.Item1, combination.Item3);
            int startY = Math.Min(combination.Item2, combination.Item4);
            int endY = Math.Max(combination.Item2, combination.Item4);

            return (endX - startX + 1) * (endY - startY + 1);
        }

        private void machine_game()
        {
            int minX, maxX, minY, maxY;

            // Получаем координаты крайних пустых клеток
            if (!TryGetNextEmptyEdge(out minX, out maxX, out minY, out maxY))
            {
                return;
            }

            // Проверяем, остается ли хотя бы одна пустая клетка вне текущей области
            bool hasEmptyCell = false;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    if (dataGridView1[i, j].Style.BackColor == Color.Empty)
                    {
                        hasEmptyCell = true;
                        break;
                    }
                }
                if (hasEmptyCell)
                {
                    break;
                }
            }

            if (!hasEmptyCell)
            {
                turn = true;
                return;
            }

            // Проверяем положение отравленной дольки относительно выбранной области
            bool poisonInside = (i0 >= minX && i0 <= maxX && j0 >= minY && j0 <= maxY);
            bool poisonOnEdge = (i0 == minX || i0 == maxX || j0 == minY || j0 == maxY);

            // Создаем список доступных комбинаций клеток
            List<Tuple<int, int, int, int>> availableCombinations = FindAvailableCombinations(minX, maxX, minY, maxY);

            // Если список доступных комбинаций не пустой, выбираем случайную комбинацию и закрашиваем ее
            if (availableCombinations.Count > 0)
            {
                Random rnd = new Random();
                var filteredCombinations = availableCombinations.Where(combination => GetCombinationCellCount(combination) <= 24).ToList();
                if (filteredCombinations.Count > 0)
                {
                    int randomIndex = rnd.Next(filteredCombinations.Count);
                    var combination = filteredCombinations[randomIndex];
                    colorful_cells(combination.Item1, combination.Item2, combination.Item3, combination.Item4, 1);
                    poisen_cells(combination.Item1, combination.Item2, combination.Item3, combination.Item4);
                    turn = false;
                }
            }
            else
            {
                // Если не удалось найти доступные комбинации, закрашиваем всю область, кроме отравленной дольки
                colorful_cells(minX, minY, maxX, maxY, 1);
                poisen_cells(minX, minY, maxX, maxY);
                turn = false;
            }



        }


        private void colorful_cells(int j1, int i1, int j2, int i2, int step)
        {
            int startX = Math.Min(j1, j2);
            int endX = Math.Max(j1, j2);
            int startY = Math.Min(i1, i2);
            int endY = Math.Max(i1, i2);

            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    if (step == 0)
                    {
                        if (dataGridView1[x, y].Style.BackColor == Color.Empty || dataGridView1[x, y].Style.BackColor != pallete[1] || dataGridView1[x, y].Style.BackColor == Color.Red)
                        {
                            dataGridView1[x, y].Style.BackColor = pallete[step];
                        }
                    }
                    else
                    {
                        if (dataGridView1[x, y].Style.BackColor == Color.Empty || dataGridView1[x, y].Style.BackColor != pallete[0] || dataGridView1[x, y].Style.BackColor == Color.Red)
                        {
                            dataGridView1[x, y].Style.BackColor = pallete[step];
                        }
                    }
                }
            }
        }

        private bool TryGetNextEmptyEdge(out int minX, out int maxX, out int minY, out int maxY)
        {
            minX = minY = int.MaxValue;
            maxX = maxY = int.MinValue;

            // Поиск крайних не закрашенных строк
            for (int j = 0; j < dataGridView1.RowCount; j++)
            {
                bool isEmpty = false;
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (dataGridView1[i, j].Style.BackColor == Color.Empty)
                    {
                        isEmpty = true;
                        break;
                    }
                }

                if (isEmpty)
                {
                    minY = Math.Min(minY, j);
                    maxY = Math.Max(maxY, j);
                }
            }

            // Поиск крайних не закрашенных столбцов
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                bool isEmpty = false;
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    if (dataGridView1[i, j].Style.BackColor == Color.Empty)
                    {
                        isEmpty = true;
                        break;
                    }
                }

                if (isEmpty)
                {
                    minX = Math.Min(minX, i);
                    maxX = Math.Max(maxX, i);
                }
            }

            return minX != int.MaxValue && minY != int.MaxValue && maxX != int.MinValue && maxY != int.MinValue;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (c == 1)
            {
                random_cells();
                c = 0;
            }
            groupBox1.Hide();
            if (gameover)
            {
                return;
            }

            int x = e.ColumnIndex;
            int y = e.RowIndex;

            // Добавляем координаты выбранной клетки в список
            xCol.Add(x);
            yRow.Add(y);

            // Проверяем, что выбраны хотя бы две клетки
            if (xCol.Count == 2 && yRow.Count == 2)
            {
                int minX, maxX, minY, maxY;
                if (!TryGetNextEmptyEdge(out minX, out maxX, out minY, out maxY))
                {
                    xCol.Clear();
                    yRow.Clear();
                    return;
                }
                else
                {
                    bool isValidSelection = ((xCol.Min() == minX || xCol.Max() == maxX) && (yRow.Min() == minY && yRow.Max() == maxY)) ||
                        ((yRow.Min() == minY || yRow.Max() == maxY) && (xCol.Min() == minX && xCol.Max() == maxX));

                    if (!isValidSelection)
                    {
                        MessageBox.Show("Выберите строки или столбцы, начинающиеся и заканчивающиеся на краях шоколадки и еще не закрашенные!");
                        xCol.Clear();
                        yRow.Clear();
                        return;
                    }

                    if (countPlayers == 1)
                    {
                        colorful_cells(xCol[0], yRow[0], xCol[1], yRow[1], 0);
                        turn = true;
                        machine_game();
                        poisen_cells(xCol[0], yRow[0], xCol[1], yRow[1]);
                        total_paintCells();
                        course_game();
                    }
                    else
                    {
                        if (play == 1)
                        {
                            colorful_cells(xCol[0], yRow[0], xCol[1], yRow[1], 0);
                            poisen_cells(xCol[0], yRow[0], xCol[1], yRow[1]);
                            total_paintCells();
                            course_game();
                            panel1.BackColor = pallete[1];
                            play = 2;
                        }
                        else
                        {
                            colorful_cells(xCol[0], yRow[0], xCol[1], yRow[1], 1);
                            poisen_cells(xCol[0], yRow[0], xCol[1], yRow[1]);
                            total_paintCells();
                            course_game();
                            panel1.BackColor = pallete[0];
                            play = 1;
                        }
                    }
                }

                xCol.Clear();
                yRow.Clear();
                panel1.BackColor = pallete[(countPlayers == 1) ? 0 : 1];

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
