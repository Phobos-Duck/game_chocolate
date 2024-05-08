namespace game_course
{
    partial class Form3
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
            listBox1 = new ListBox();
            label1 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Items.AddRange(new object[] { "В чем суть игры? Дана шоколадка. ", "По очереди вы отламываете от неё кусочки, начиная с верхней её части.", " Как только вам попадается кусочек, где есть отравленная долька - проигрываете(возможно насмерть).", "Теперь об ключевых моментах:", "1. Нельзя достать один только кусочек от середины, нужно отломить либо большую часть, либо какую-то часть.", "2. Начинать следует с верхней части шоколдаки, иначе придется отламывать большую её часть.", "3. Игроки ходят по очереди. Игрок первый обозначен зеленым цветом, второй - розовым.", "4. В случае обнаружения отравленной дольки (она загорается красным) игра останавливается.", "5. Проигрывает тот, на чью долю выпала отравленная долька." });
            listBox1.Location = new Point(12, 32);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(839, 204);
            listBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(96, 20);
            label1.TabIndex = 1;
            label1.Text = "О чем игра?";
            // 
            // button1
            // 
            button1.Location = new Point(12, 242);
            button1.Name = "button1";
            button1.Size = new Size(144, 29);
            button1.TabIndex = 2;
            button1.Text = "Назад в меню";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 274);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(listBox1);
            Name = "Form3";
            Text = "Form3";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private Label label1;
        private Button button1;
    }
}