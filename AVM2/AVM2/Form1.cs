using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVM2
{
    public partial class Form1 : Form
    {
        // Переменные для хранения введенных данных
        private int selectedFunction = 1;
        private double a = 0;
        private double b = 1;
        private double eps = 0.001;
        private int n = 2;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LeftGran = new System.Windows.Forms.TextBox();
            this.RightGran = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Eps = new System.Windows.Forms.TextBox();
            this.calculateButton = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.methodComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(25, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите функцию: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(28, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "1) x^2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(28, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "2) sin(x)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(28, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "3) exp(x)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(26, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "4) 1/(1+x^2)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(32, 199);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(290, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(221, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Введите левую границу: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(290, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(230, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Введите правую границу: ";
            // 
            // LeftGran
            // 
            this.LeftGran.Location = new System.Drawing.Point(558, 62);
            this.LeftGran.Name = "LeftGran";
            this.LeftGran.Size = new System.Drawing.Size(100, 20);
            this.LeftGran.TabIndex = 8;
            this.LeftGran.Text = "0";
            // 
            // RightGran
            // 
            this.RightGran.Location = new System.Drawing.Point(558, 132);
            this.RightGran.Name = "RightGran";
            this.RightGran.Size = new System.Drawing.Size(100, 20);
            this.RightGran.TabIndex = 9;
            this.RightGran.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(290, 217);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(211, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "Введите точность eps: ";
            // 
            // Eps
            // 
            this.Eps.Location = new System.Drawing.Point(558, 217);
            this.Eps.Name = "Eps";
            this.Eps.Size = new System.Drawing.Size(100, 20);
            this.Eps.TabIndex = 11;
            this.Eps.Text = "0.001";
            // 
            // calculateButton
            // 
            this.calculateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.calculateButton.Location = new System.Drawing.Point(294, 300);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(200, 40);
            this.calculateButton.TabIndex = 12;
            this.calculateButton.Text = "Вычислить интеграл";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.calculateButton_Click);
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultLabel.Location = new System.Drawing.Point(29, 350);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(143, 17);
            this.resultLabel.TabIndex = 13;
            this.resultLabel.Text = "Результат появится здесь";
            this.resultLabel.MaximumSize = new System.Drawing.Size(650, 0);
            // 
            // methodComboBox
            // 
            this.methodComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.methodComboBox.FormattingEnabled = true;
            this.methodComboBox.Items.AddRange(new object[] {
            "Метод правых прямоугольников",
            "Метод трапеций"});
            this.methodComboBox.Location = new System.Drawing.Point(32, 260);
            this.methodComboBox.Name = "methodComboBox";
            this.methodComboBox.Size = new System.Drawing.Size(200, 21);
            this.methodComboBox.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(29, 240);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 17);
            this.label9.TabIndex = 15;
            this.label9.Text = "Выберите метод:";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(709, 500);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.methodComboBox);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.Eps);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.RightGran);
            this.Controls.Add(this.LeftGran);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Вычисление интегралов";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.ComboBox methodComboBox;
        private System.Windows.Forms.Label label9;

        static double Func(double x, int fnum)
        {
            if (fnum == 1) return x * x;
            else if (fnum == 2) return Math.Sin(x);
            else if (fnum == 3) return Math.Exp(x);
            else if (fnum == 4) return 1.0 / (1.0 + x * x);
            else return x * x;
        }

        static double RightRectangles(double a, double b, int n, int fnum)
        {
            double h = (b - a) / n;
            double sum = 0;
            for (int i = 1; i <= n; i++)
            {
                double xi = a + i * h;
                sum += Func(xi, fnum);
            }
            return sum * h;
        }

        static double Trapezoid(double a, double b, int n, int fnum)
        {
            double h = (b - a) / n;
            double sum = 0.5 * (Func(a, fnum) + Func(b, fnum));
            for (int i = 1; i < n; i++)
            {
                double xi = a + i * h;
                sum += Func(xi, fnum);
            }
            return sum * h;
        }

        private void ComputeWithRunge(string method)
        {
            try
            {
                // Получаем данные из текстовых полей
                if (!int.TryParse(textBox1.Text, out selectedFunction) || selectedFunction < 1 || selectedFunction > 4)
                {
                    resultLabel.Text = "Ошибка: выберите функцию от 1 до 4";
                    return;
                }

                if (!double.TryParse(LeftGran.Text, out a))
                {
                    resultLabel.Text = "Ошибка: некорректная левая граница";
                    return;
                }

                if (!double.TryParse(RightGran.Text, out b))
                {
                    resultLabel.Text = "Ошибка: некорректная правая граница";
                    return;
                }

                if (!double.TryParse(Eps.Text, out eps) || eps <= 0)
                {
                    resultLabel.Text = "Ошибка: некорректная точность (должна быть > 0)";
                    return;
                }

                if (a >= b)
                {
                    resultLabel.Text = "Ошибка: левая граница должна быть меньше правой";
                    return;
                }

                int n = 2;
                int p = (method == "rp") ? 1 : 2;

                // Вычисляем первый интеграл при n разбиениях
                double I1;
                if (method == "rp")
                    I1 = RightRectangles(a, b, n, selectedFunction);
                else
                    I1 = Trapezoid(a, b, n, selectedFunction);

                // Цикл уточнения по правилу Рунге
                while (true)
                {
                    int n2 = n * 2;

                    // Для удвоенного числа разбиений
                    double I2;
                    if (method == "rp")
                        I2 = RightRectangles(a, b, n2, selectedFunction);
                    else
                        I2 = Trapezoid(a, b, n2, selectedFunction);

                    // Правило Рунге
                    double denominator = Math.Pow(2, p) - 1;
                    double pogreshnost = Math.Abs((I2 - I1) / denominator);
                    double I_runge = I2 + (I2 - I1) / denominator;

                    // Проверяем, достигнута ли требуемая точность
                    if (pogreshnost < eps)
                    {
                        double h = (b - a) / n2;
                        string methodName = (method == "rp") ? "правых прямоугольников" : "трапеций";

                        resultLabel.Text = $"Метод {methodName}:\n" +
                                         $"  Приближённое значение интеграла = {I_runge:F6}\n" +
                                         $"  Оценка погрешности (Рунге) = {pogreshnost:E2}\n" +
                                         $"  Шаг h = {h:E2}\n" +
                                         $"  Количество разбиений n = {n2}";
                        break;
                    }

                    // Если точность ещё не достигнута — повторяем с большим n
                    n = n2;
                    I1 = I2;

                    if (n > 1_000_000)
                    {
                        resultLabel.Text = "Слишком много разбиений, остановка. Требуемая точность не достигнута.";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                resultLabel.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            string method = (methodComboBox.SelectedIndex == 0) ? "rp" : "trapezoid";
            ComputeWithRunge(method);
        }
    }
}