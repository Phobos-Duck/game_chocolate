using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_course
{
    public partial class Form2 : Form
    {
        int players = 0;
        Color color1, color2;
        OrderedDictionary colors = new OrderedDictionary()
        {
            {"Зеленый", "Green"},
            {"Желтый", "Yellow"},
            {"Розовый", "Pink"},
            {"Синий", "Blue"},
            {"Голубой", "Aqua" },
            {"Фиолетовый", "Purple"}
        };

        public Form2()
        {
            InitializeComponent();
            groupBox2.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex != -1)
            {
                // Получаем текст выбранного элемента
                string selectedText = checkedListBox1.GetItemText(checkedListBox1.SelectedItem);
                players = Convert.ToInt32(selectedText);
            }
            else
            {
                MessageBox.Show("Ни один элемент не выбран.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (players <= 0)
            {
                MessageBox.Show("Пожалуйста, выберите количество игроков!");
                groupBox2.Hide();
                groupBox1.Show();
            }
            else
            {
                Form4 form4 = new Form4(players, color1, color2);
                form4.Show();
                this.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Hide();
            groupBox2.Show();
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox2.SelectedIndex != -1)
            {
                string selectedKey = checkedListBox2.SelectedItem.ToString();
                color1 = Color.FromName(colors[selectedKey].ToString());
            }
            else
            {
                MessageBox.Show("Не выбран ни один или больше двух!");
            }
        }

        private void checkedListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox3.SelectedIndex != -1)
            {
                string selectedKey = checkedListBox3.SelectedItem.ToString();
                color2 = Color.FromName(colors[selectedKey].ToString());
            }
            else
            {
                MessageBox.Show("Не выбран ни один или больше двух!");
            }
        }

        private void checkedListBox3_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                checkedListBox3.SetItemChecked(e.Index, true);
            }
            else
            {
                for (int i = 0; i < checkedListBox3.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        checkedListBox3.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                checkedListBox2.SetItemChecked(e.Index, true);
            }
            else
            {
                for (int i = 0; i < checkedListBox2.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        checkedListBox2.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                checkedListBox1.SetItemChecked(e.Index, true);
            }
            else {
                for(int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if(i != e.Index)
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }
                }
            }
        }
    }
}
