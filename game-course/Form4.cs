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
        int rows = 12, cols = 6, j0 = -1, i0 = -1, play = 1, total_1 = 0, total_2 = 0;
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
            panel1.BackColor = pallete[0];
            random_cells();

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

            bool hasPoisonedCell = false;

            // Проверяем, содержит ли закрашенная область отравленную дольку
            if ((i0 >= startX && i0 <= endX && j0 >= startY && j0 <= endY) ||
    (i0 == x1 && j0 == y1) || (i0 == x2 && j0 == y2))
            {
                hasPoisonedCell = true;
            }

            if (hasPoisonedCell)
            {
                gameover = true;
                if (countPlayers == 1)
                {
                    MessageBox.Show($"Game Over! Выиграл {(panel1.BackColor == pallete[1] ? "компьютер" : "игрок")}");
                }
                else
                {
                    if (play == 1)
                    {
                        MessageBox.Show($"Game Over! Выиграл {(play == 1 ? "первый" : "второй")} игрок");
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

            // Проверяем, что текущая область содержит несколько строк или столбцов

            if ((maxX - minX > 0) || (maxY - minY > 0))
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

        private void machine_game()
        {
            int minX, maxX, minY, maxY;

            if (gameover)
            {
                return;
            }

            if (!TryGetNextEmptyEdge(out minX, out maxX, out minY, out maxY))
            {
                return;
            }


            // Создаем список доступных комбинаций клеток
            List<Tuple<int, int, int, int>> availableCombinations = FindAvailableCombinations(minX, maxX, minY, maxY);

            // Если список доступных комбинаций не пустой, выбираем комбинацию с наименьшим количеством вариантов хода игрока
            if (availableCombinations.Count > 0)
            {
                Tuple<int, int, int, int> bestCombination = availableCombinations[0];
                int minPlayerOptions = int.MaxValue;

                foreach (var combination in availableCombinations)
                {
                    int playerOptions = CountPlayerOptions(combination.Item1, combination.Item2, combination.Item3, combination.Item4);
                    if (playerOptions < minPlayerOptions)
                    {
                        minPlayerOptions = playerOptions;
                        bestCombination = combination;
                    }
                }

                colorful_cells(bestCombination.Item1, bestCombination.Item2, bestCombination.Item3, bestCombination.Item4, 1);
                poisen_cells(bestCombination.Item1, bestCombination.Item2, bestCombination.Item3, bestCombination.Item4);
            }
            else
            {
                // Если не удалось найти доступные комбинации, закрашиваем первую подходящую область
                if (minX == maxX)
                {
                    // Закрашиваем столбец
                    colorful_cells(minX, minY, minX, maxY, 1);
                    poisen_cells(minX, minY, minX, maxY);
                }
                else if (minY == maxY)
                {
                    // Закрашиваем строку
                    colorful_cells(minX, minY, maxX, minY, 1);
                    poisen_cells(minX, minY, maxX, minY);
                }
                else
                {
                    // Закрашиваем минимальную область
                    colorful_cells(minX, minY, minX + 1, minY + 1, 1);
                    poisen_cells(minX, minY, minX + 1, minY + 1);
                }
            }

            turn = false;

        }

        private int CountPlayerOptions(int minX, int minY, int maxX, int maxY)
        {
            int count = 0;

            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    if ((i != i0 || j != j0) && (i == minX || i == maxX || j == minY || j == maxY))
                    {
                        if (IsPlayerMoveOption(i, j))
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        private bool IsPlayerMoveOption(int x, int y)
        {
            return x >= 0 && x < cols && y >= 0 && y < rows &&
                   (dataGridView1[x, y].Style.BackColor == Color.Empty || dataGridView1[x, y].Style.BackColor == pallete[0]);
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
                            panel1.BackColor = pallete[1];
                            turn = true;
                            dataGridView1[x, y].Style.BackColor = pallete[step];
                            
                        }
                    }
                    else
                    {
                        if (dataGridView1[x, y].Style.BackColor == Color.Empty || dataGridView1[x, y].Style.BackColor != pallete[0] || dataGridView1[x, y].Style.BackColor == Color.Red)
                        {
                            panel1.BackColor = pallete[0];
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
            for (int j = 0; j < rows; j++)
            {
                bool isEmpty = false;
                for (int i = 0; i < cols; i++)
                {
                    if (dataGridView1[i, j].Style.BackColor == Color.Empty || dataGridView1[i, j].Style.BackColor == Color.Red)
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
            for (int i = 0; i < cols; i++)
            {
                bool isEmpty = false;
                for (int j = 0; j < rows; j++)
                {
                    if (dataGridView1[i, j].Style.BackColor == Color.Empty || dataGridView1[i, j].Style.BackColor == Color.Red)
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
            groupBox1.Hide();
            if (gameover)
            {
                return;
            }

            int x = e.ColumnIndex;
            int y = e.RowIndex;

            // Добавляем координаты только в случае, если выбрана пустая клетка
                xCol.Add(x);
                yRow.Add(y);

                // Проверяем, выбраны ли уже две клетки
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
                         ((yRow.Min() == minY || yRow.Max() == maxY) && (xCol.Min() == minX && xCol.Max() == maxX)) ||
                         ((maxX - minX <= 2 && maxX - minX >= 0) && (maxY - minY <= 2 && maxY - minY >= 0));

                        if (!isValidSelection)
                        {
                            // Если выбор не корректный, выводим сообщение и отменяем его
                            MessageBox.Show("Выберите строки или столбцы, начинающиеся и заканчивающиеся на краях шоколадки и еще не закрашенные!");
                            xCol.Clear();
                            yRow.Clear();
                            return;
                        }

                        if (countPlayers == 1)
                        {
                            colorful_cells(xCol[0], yRow[0], xCol[1], yRow[1], 0);
                            poisen_cells(xCol[0], yRow[0], xCol[1], yRow[1]);


                            if (!gameover)
                            {
                                machine_game();

                            }
                            total_paintCells();
                            course_game();
                        }
                        else
                        {
                            if (play == 1)
                            {
                                panel1.BackColor = pallete[1];
                                colorful_cells(xCol[0], yRow[0], xCol[1], yRow[1], 0);
                                total_paintCells();
                                course_game();

                                play = 2;
                            }
                            else
                            {
                                panel1.BackColor = pallete[0];
                                colorful_cells(xCol[0], yRow[0], xCol[1], yRow[1], 1);
                                total_paintCells();
                                course_game();
                                play = 1;
                            }
                            poisen_cells(xCol[0], yRow[0], xCol[1], yRow[1]);
                        }
                        xCol.Clear();
                        yRow.Clear();
                    }
                }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Empty;
            gameover = false;
            groupBox1.Show();
            clean_cells();
            random_cells();
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
