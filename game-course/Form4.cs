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
        int rows = 12, cols = 6, j0 = 0, i0 = 0, c = 1, m1, m2, play = 1, total_1 = 0, total_2 = 0;
        bool gameover = false;

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
            if (c == 1)
            {
                random_cells();
                c = 0;
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
        private void poisen_cells(int x1, int y1, int x2, int y2)
        {
            int startX = Math.Min(x1, x2);
            int endX = Math.Max(x1, x2);
            int startY = Math.Min(y1, y2);
            int endY = Math.Max(y1, y2);

            DataGridViewCell selectedCell = dataGridView1.Rows[j0].Cells[i0];
            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    if (i == i0 && j == j0)
                    {
                        gameover = true;
                        MessageBox.Show("Game Over! Вы отравились((");
                        return;
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

        private void machine_game()
        {
            int minX, maxX, minY, maxY;

            // Получаем координаты первой пустой клетки на границе
            if (!TryGetNextEmptyEdge(out minX, out maxX, out minY, out maxY))
            {
                return; // Если не найдено пустых клеток на границе, просто завершаем функцию
            }

            // Если координаты отравленной дольки заданы
            if (i0 != -1 && j0 != -1)
            {
                // Определяем, в каком направлении находится отравленная долька
                bool isHorizontal = (i0 == minY || i0 == maxY);
                bool isVertical = (j0 == minX || j0 == maxX);

                // Если отравленная долька лежит на краю поля, выбираем направление от нее
                if ((isHorizontal && minY == 0) || (isVertical && minX == 0))
                {
                    // Если отравленная долька находится на верхней или левой границе,
                    // закрашиваем вниз или вправо соответственно
                    if (isHorizontal)
                    {
                        colorful_cells(minX, minY, maxX, maxY, 1); // Закрасить вниз
                    }
                    else if (isVertical)
                    {
                        colorful_cells(minX, minY, maxX, maxY, 1); // Закрасить вправо
                    }
                }
                else
                {
                    // Иначе закрашиваем в противоположном направлении от отравленной дольки
                    if (isHorizontal)
                    {
                        // Если верхняя часть свободна, закрасить вверх, иначе закрасить вниз
                        int topY = Math.Max(0, minY - 1);
                        int bottomY = Math.Min(dataGridView1.RowCount - 1, maxY + 1);
                        colorful_cells(minX, topY, maxX, topY, 1);
                        colorful_cells(minX, bottomY, maxX, bottomY, 1);
                    }
                    else if (isVertical)
                    {
                        // Если левая часть свободна, закрасить влево, иначе закрасить вправо
                        int leftX = Math.Max(0, minX - 1);
                        int rightX = Math.Min(dataGridView1.ColumnCount - 1, maxX + 1);
                        colorful_cells(leftX, minY, leftX, maxY, 1);
                        colorful_cells(rightX, minY, rightX, maxY, 1);
                    }
                }
            }
            else
            {
                // Если координаты отравленной дольки не заданы, просто закрашиваем по стандартной стратегии
                if (maxY - minY > maxX - minX)
                {
                    // Если высота больше ширины, закрасить всю строку
                    colorful_cells(minX, minY, maxX, minY, 1);
                }
                else
                {
                    // Если ширина больше или равна высоте, закрасить весь столбец
                    colorful_cells(minX, minY, minX, maxY, 1);
                }
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
                        if (dataGridView1[x, y].Style.BackColor == Color.Empty || dataGridView1[x, y].Style.BackColor != pallete[1])
                        {
                            dataGridView1[x, y].Style.BackColor = pallete[step];
                        }
                    }
                    else
                    {
                        if (dataGridView1[x, y].Style.BackColor == Color.Empty || dataGridView1[x, y].Style.BackColor != pallete[0])
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
                        machine_game();
                        poisen_cells(xCol[0], yRow[0], xCol[1], yRow[1]);
                    }
                    else
                    {
                        if (play == 1)
                        {
                            play = 2;
                            colorful_cells(xCol[0], yRow[0], xCol[1], yRow[1], 0);
                            poisen_cells(xCol[0], yRow[0], xCol[1], yRow[1]);
                            panel1.BackColor = pallete[1];
                        }
                        else
                        {
                            play = 1;
                            colorful_cells(xCol[0], yRow[0], xCol[1], yRow[1], 1);
                            poisen_cells(xCol[0], yRow[0], xCol[1], yRow[1]);
                            panel1.BackColor = pallete[0];
                        }
                    }
                }

                xCol.Clear();
                yRow.Clear();

                total_paintCells();
                course_game();
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
