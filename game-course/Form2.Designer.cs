namespace game_course
{
    partial class Form2
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
            checkedListBox1 = new CheckedListBox();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            groupBox1 = new GroupBox();
            button3 = new Button();
            groupBox2 = new GroupBox();
            checkedListBox3 = new CheckedListBox();
            checkedListBox2 = new CheckedListBox();
            label2 = new Label();
            label3 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Items.AddRange(new object[] { "1", "2" });
            checkedListBox1.Location = new Point(6, 26);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(51, 48);
            checkedListBox1.TabIndex = 0;
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 9);
            label1.Name = "label1";
            label1.Size = new Size(325, 20);
            label1.TabIndex = 1;
            label1.Text = "Выберите настройки для дальнейшей игры";
            // 
            // button1
            // 
            button1.Location = new Point(123, 210);
            button1.Name = "button1";
            button1.Size = new Size(94, 28);
            button1.TabIndex = 2;
            button1.Text = "Начать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(86, 287);
            button2.Name = "button2";
            button2.Size = new Size(185, 29);
            button2.TabIndex = 3;
            button2.Text = "В главное меню";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(checkedListBox1);
            groupBox1.Location = new Point(100, 43);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(180, 112);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Количество Игроков";
            // 
            // button3
            // 
            button3.Location = new Point(0, 77);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 1;
            button3.Text = "Дальше";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(checkedListBox3);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(checkedListBox2);
            groupBox2.Location = new Point(4, 43);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(362, 244);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Цвета игроков";
            // 
            // checkedListBox3
            // 
            checkedListBox3.FormattingEnabled = true;
            checkedListBox3.Items.AddRange(new object[] { "Зеленый", "Желтый", "Розовый", "Синий", "Голубой", "Фиолетовый" });
            checkedListBox3.Location = new Point(196, 68);
            checkedListBox3.Name = "checkedListBox3";
            checkedListBox3.Size = new Size(150, 136);
            checkedListBox3.TabIndex = 6;
            checkedListBox3.ItemCheck += checkedListBox3_ItemCheck;
            checkedListBox3.SelectedIndexChanged += checkedListBox3_SelectedIndexChanged;
            // 
            // checkedListBox2
            // 
            checkedListBox2.FormattingEnabled = true;
            checkedListBox2.Items.AddRange(new object[] { "Зеленый", "Желтый", "Розовый", "Синий", "Голубой", "Фиолетовый" });
            checkedListBox2.Location = new Point(8, 68);
            checkedListBox2.Name = "checkedListBox2";
            checkedListBox2.Size = new Size(150, 136);
            checkedListBox2.TabIndex = 5;
            checkedListBox2.ItemCheck += checkedListBox2_ItemCheck;
            checkedListBox2.SelectedIndexChanged += checkedListBox2_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(37, 45);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 7;
            label2.Text = "Игрок 1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(233, 45);
            label3.Name = "label3";
            label3.Size = new Size(66, 20);
            label3.TabIndex = 8;
            label3.Text = "Игрок 2";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(386, 347);
            Controls.Add(groupBox2);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Name = "Form2";
            Text = "Form2";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckedListBox checkedListBox1;
        private Label label1;
        private Button button1;
        private Button button2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private CheckedListBox checkedListBox2;
        private Button button3;
        private CheckedListBox checkedListBox3;
        private Label label3;
        private Label label2;
    }
}