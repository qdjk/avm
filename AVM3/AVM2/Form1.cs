using System;
using System.Drawing;
using System.Windows.Forms;

namespace LinearEquationSolver
{
    public partial class MainForm : Form
    {
        private TextBox[,] matrixA;
        private TextBox[] vectorF;
        private TextBox[] solutionX;
        private TextBox[] residualR;

        // Все контролы объявлены явно
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private ComboBox methodComboBox;
        private Button calculateButton;
        private Button clearButton;
        private Label resultLabel;
        private GroupBox examplesGroup;
        private RadioButton example1Radio;
        private RadioButton example2Radio;
        private RadioButton example3Radio;
        private Button loadExampleButton;

        private int n = 3; // Размерность системы

        public MainForm()
        {
            InitializeComponent();
            SetExampleValues(1); // Загружаем первый пример по умолчанию
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Настройка формы
            this.Text = "Решение СЛАУ - Методы Гаусса и Холецкого";
            this.Size = new Size(950, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = SystemColors.Control;

            // Создание контролов
            CreateControls();

            this.ResumeLayout(false);
        }

        private void CreateControls()
        {
            // Заголовок
            this.label1 = new Label();
            this.label1.Text = "Решение системы линейных уравнений Ax = f";
            this.label1.Font = new Font("Arial", 12, FontStyle.Bold);
            this.label1.Location = new Point(20, 20);
            this.label1.AutoSize = true;
            this.Controls.Add(this.label1);

            // Метод решения
            this.label2 = new Label();
            this.label2.Text = "Выберите метод решения:";
            this.label2.Location = new Point(20, 60);
            this.label2.AutoSize = true;
            this.Controls.Add(this.label2);

            this.methodComboBox = new ComboBox();
            this.methodComboBox.Location = new Point(180, 57);
            this.methodComboBox.Size = new Size(150, 25);
            this.methodComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.methodComboBox.Items.AddRange(new string[] { "Метод Гаусса", "Метод Холецкого" });
            this.methodComboBox.SelectedIndex = 0;
            this.Controls.Add(this.methodComboBox);

            // Заголовок матрицы A
            this.label3 = new Label();
            this.label3.Text = "Матрица A (3x3):";
            this.label3.Location = new Point(20, 100);
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Arial", 9, FontStyle.Bold);
            this.Controls.Add(this.label3);

            // Создание матрицы A
            matrixA = new TextBox[n, n];
            int startX = 20, startY = 130;
            int cellSize = 50;

            // Подписи строк и столбцов
            this.label4 = new Label();
            this.label4.Text = "i\\j";
            this.label4.Location = new Point(startX, startY - 20);
            this.label4.AutoSize = true;
            this.Controls.Add(this.label4);

            for (int i = 0; i < n; i++)
            {
                Label rowLabel = new Label();
                rowLabel.Text = (i + 1).ToString();
                rowLabel.Location = new Point(startX - 15, startY + i * 30 + 5);
                rowLabel.AutoSize = true;
                this.Controls.Add(rowLabel);

                Label colLabel = new Label();
                colLabel.Text = (i + 1).ToString();
                colLabel.Location = new Point(startX + i * (cellSize + 5) + 20, startY - 20);
                colLabel.AutoSize = true;
                this.Controls.Add(colLabel);

                for (int j = 0; j < n; j++)
                {
                    matrixA[i, j] = new TextBox();
                    matrixA[i, j].Location = new Point(startX + j * (cellSize + 5), startY + i * 30);
                    matrixA[i, j].Size = new Size(cellSize, 25);
                    matrixA[i, j].Text = "0";
                    matrixA[i, j].TextAlign = HorizontalAlignment.Center;
                    this.Controls.Add(matrixA[i, j]);
                }
            }

            // Вектор f
            this.label5 = new Label();
            this.label5.Text = "Вектор f:";
            this.label5.Location = new Point(200, 100);
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Arial", 9, FontStyle.Bold);
            this.Controls.Add(this.label5);

            vectorF = new TextBox[n];
            for (int i = 0; i < n; i++)
            {
                Label vecLabel = new Label();
                vecLabel.Text = $"f[{i + 1}] =";
                vecLabel.Location = new Point(200, startY + i * 30 + 5);
                vecLabel.AutoSize = true;
                this.Controls.Add(vecLabel);

                vectorF[i] = new TextBox();
                vectorF[i].Location = new Point(250, startY + i * 30);
                vectorF[i].Size = new Size(cellSize, 25);
                vectorF[i].Text = "0";
                vectorF[i].TextAlign = HorizontalAlignment.Center;
                this.Controls.Add(vectorF[i]);
            }

            // Кнопки
            this.calculateButton = new Button();
            this.calculateButton.Text = "Решить";
            this.calculateButton.Location = new Point(20, 230);
            this.calculateButton.Size = new Size(100, 30);
            this.calculateButton.BackColor = Color.LightGreen;
            this.calculateButton.Click += CalculateButton_Click;
            this.Controls.Add(this.calculateButton);

            this.clearButton = new Button();
            this.clearButton.Text = "Очистить";
            this.clearButton.Location = new Point(130, 230);
            this.clearButton.Size = new Size(100, 30);
            this.clearButton.BackColor = Color.LightCoral;
            this.clearButton.Click += ClearButton_Click;
            this.Controls.Add(this.clearButton);

            // Группа примеров справа
            this.examplesGroup = new GroupBox();
            this.examplesGroup.Text = "Примеры систем";
            this.examplesGroup.Location = new Point(400, 60);
            this.examplesGroup.Size = new Size(300, 200);
            this.examplesGroup.Font = new Font("Arial", 9, FontStyle.Bold);
            this.Controls.Add(this.examplesGroup);

            // Пример 1 - Симметричная положительно определенная (для Холецкого)
            this.example1Radio = new RadioButton();
            this.example1Radio.Text = "Пример 1: Симметричная положительно определенная";
            this.example1Radio.Location = new Point(10, 30);
            this.example1Radio.Size = new Size(280, 20);
            this.example1Radio.Checked = true;
            this.examplesGroup.Controls.Add(this.example1Radio);

            this.label12 = new Label();
            this.label12.Text = "A = [[4,2,2],[2,5,3],[2,3,6]], f = [8,15,21]";
            this.label12.Location = new Point(25, 50);
            this.label12.Size = new Size(270, 30);
            this.label12.Font = new Font("Arial", 8);
            this.label12.ForeColor = Color.DarkGreen;
            this.examplesGroup.Controls.Add(this.label12);

            // Пример 2 - Общая матрица (для Гаусса)
            this.example2Radio = new RadioButton();
            this.example2Radio.Text = "Пример 2: Общая матрица";
            this.example2Radio.Location = new Point(10, 85);
            this.example2Radio.Size = new Size(280, 20);
            this.examplesGroup.Controls.Add(this.example2Radio);

            this.label13 = new Label();
            this.label13.Text = "A = [[1,2,3],[4,5,6],[7,8,10]], f = [14,32,53]";
            this.label13.Location = new Point(25, 105);
            this.label13.Size = new Size(270, 30);
            this.label13.Font = new Font("Arial", 8);
            this.label13.ForeColor = Color.DarkGreen;
            this.examplesGroup.Controls.Add(this.label13);

            // Пример 3 - Вырожденная система
            this.example3Radio = new RadioButton();
            this.example3Radio.Text = "Пример 3: Вырожденная система";
            this.example3Radio.Location = new Point(10, 140);
            this.example3Radio.Size = new Size(280, 20);
            this.examplesGroup.Controls.Add(this.example3Radio);

            this.label14 = new Label();
            this.label14.Text = "A = [[1,2,3],[2,4,6],[3,6,9]], f = [6,12,18]";
            this.label14.Location = new Point(25, 160);
            this.label14.Size = new Size(270, 30);
            this.label14.Font = new Font("Arial", 8);
            this.label14.ForeColor = Color.DarkGreen;
            this.examplesGroup.Controls.Add(this.label14);

            // Кнопка загрузки примера
            this.loadExampleButton = new Button();
            this.loadExampleButton.Text = "Загрузить пример";
            this.loadExampleButton.Location = new Point(450, 270);
            this.loadExampleButton.Size = new Size(200, 30);
            this.loadExampleButton.BackColor = Color.LightBlue;
            this.loadExampleButton.Click += LoadExampleButton_Click;
            this.Controls.Add(this.loadExampleButton);

            // Разделительная линия
            this.label6 = new Label();
            this.label6.BorderStyle = BorderStyle.Fixed3D;
            this.label6.Location = new Point(20, 320);
            this.label6.Size = new Size(800, 2);
            this.Controls.Add(this.label6);

            // Результаты
            this.resultLabel = new Label();
            this.resultLabel.Text = "Результат:";
            this.resultLabel.Location = new Point(20, 340);
            this.resultLabel.AutoSize = true;
            this.resultLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(this.resultLabel);

            // Решение x
            this.label7 = new Label();
            this.label7.Text = "Решение x:";
            this.label7.Location = new Point(20, 370);
            this.label7.AutoSize = true;
            this.label7.Font = new Font("Arial", 9, FontStyle.Bold);
            this.Controls.Add(this.label7);

            solutionX = new TextBox[n];
            for (int i = 0; i < n; i++)
            {
                Label solLabel = new Label();
                solLabel.Text = $"x[{i + 1}] =";
                solLabel.Location = new Point(20, 400 + i * 30);
                solLabel.AutoSize = true;
                this.Controls.Add(solLabel);

                solutionX[i] = new TextBox();
                solutionX[i].Location = new Point(70, 400 + i * 30);
                solutionX[i].Size = new Size(100, 25);
                solutionX[i].ReadOnly = true;
                solutionX[i].BackColor = Color.LightYellow;
                solutionX[i].TextAlign = HorizontalAlignment.Center;
                this.Controls.Add(solutionX[i]);
            }

            // Невязка r
            this.label8 = new Label();
            this.label8.Text = "Невязка r = Ax - f:";
            this.label8.Location = new Point(200, 370);
            this.label8.AutoSize = true;
            this.label8.Font = new Font("Arial", 9, FontStyle.Bold);
            this.Controls.Add(this.label8);

            residualR = new TextBox[n];
            for (int i = 0; i < n; i++)
            {
                Label resLabel = new Label();
                resLabel.Text = $"r[{i + 1}] =";
                resLabel.Location = new Point(200, 400 + i * 30);
                resLabel.AutoSize = true;
                this.Controls.Add(resLabel);

                residualR[i] = new TextBox();
                residualR[i].Location = new Point(250, 400 + i * 30);
                residualR[i].Size = new Size(120, 25);
                residualR[i].ReadOnly = true;
                residualR[i].BackColor = Color.LightCyan;
                residualR[i].TextAlign = HorizontalAlignment.Center;
                this.Controls.Add(residualR[i]);
            }

            // Информационные labels
            this.label9 = new Label();
            this.label9.Text = "Метод Гаусса: для любых матриц";
            this.label9.Location = new Point(20, 500);
            this.label9.AutoSize = true;
            this.label9.ForeColor = Color.DarkBlue;
            this.Controls.Add(this.label9);

            this.label10 = new Label();
            this.label10.Text = "Метод Холецкого: для симметричных положительно определенных матриц";
            this.label10.Location = new Point(20, 520);
            this.label10.AutoSize = true;
            this.label10.ForeColor = Color.DarkBlue;
            this.Controls.Add(this.label10);

            this.label11 = new Label();
            this.label11.Text = "Пример 1 подходит для обоих методов, Пример 2 - только для Гаусса";
            this.label11.Location = new Point(20, 540);
            this.label11.AutoSize = true;
            this.label11.ForeColor = Color.DarkGreen;
            this.Controls.Add(this.label11);
        }

        private void SetExampleValues(int exampleNumber)
        {
            switch (exampleNumber)
            {
                case 1:
                    // Пример 1: Симметричная положительно определенная матрица
                    double[,] exampleA1 = { { 4, 2, 2 }, { 2, 5, 3 }, { 2, 3, 6 } };
                    double[] exampleF1 = { 8, 15, 21 };
                    LoadMatrix(exampleA1, exampleF1);
                    break;

                case 2:
                    // Пример 2: Общая матрица
                    double[,] exampleA2 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 10 } };
                    double[] exampleF2 = { 14, 32, 53 };
                    LoadMatrix(exampleA2, exampleF2);
                    break;

                case 3:
                    // Пример 3: Вырожденная система
                    double[,] exampleA3 = { { 1, 2, 3 }, { 2, 4, 6 }, { 3, 6, 9 } };
                    double[] exampleF3 = { 6, 12, 18 };
                    LoadMatrix(exampleA3, exampleF3);
                    break;
            }
        }

        private void LoadMatrix(double[,] A, double[] f)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrixA[i, j].Text = A[i, j].ToString("F2");
                }
                vectorF[i].Text = f[i].ToString("F2");
            }

            // Очищаем результаты
            for (int i = 0; i < n; i++)
            {
                solutionX[i].Text = "";
                residualR[i].Text = "";
            }
            resultLabel.Text = "Результат:";
        }

        private void LoadExampleButton_Click(object sender, EventArgs e)
        {
            int selectedExample = 1;
            if (example1Radio.Checked) selectedExample = 1;
            else if (example2Radio.Checked) selectedExample = 2;
            else if (example3Radio.Checked) selectedExample = 3;

            SetExampleValues(selectedExample);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrixA[i, j].Text = "0";
                }
                vectorF[i].Text = "0";
                solutionX[i].Text = "";
                residualR[i].Text = "";
            }
            resultLabel.Text = "Результат:";
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Чтение матрицы A и вектора f
                double[,] A = new double[n, n];
                double[] f = new double[n];

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (!double.TryParse(matrixA[i, j].Text, out A[i, j]))
                        {
                            MessageBox.Show($"Ошибка в элементе A[{i + 1},{j + 1}]", "Ошибка ввода",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (!double.TryParse(vectorF[i].Text, out f[i]))
                    {
                        MessageBox.Show($"Ошибка в элементе f[{i + 1}]", "Ошибка ввода",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                double[] x;
                string methodName = "";

                // Выбор метода решения
                if (methodComboBox.SelectedIndex == 0)
                {
                    x = SolveGauss(A, f);
                    methodName = "Метод Гаусса";
                }
                else
                {
                    // Проверка симметричности для метода Холецкого
                    if (!IsSymmetric(A))
                    {
                        MessageBox.Show("Для метода Холецкого матрица должна быть симметричной!", "Предупреждение",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    x = SolveCholesky(A, f);
                    methodName = "Метод Холецкого";
                }

                // Вывод решения
                for (int i = 0; i < n; i++)
                {
                    solutionX[i].Text = x[i].ToString("F6");
                }

                // Вычисление невязки
                double[] r = CalculateResidual(A, f, x);
                for (int i = 0; i < n; i++)
                {
                    // Улучшенное форматирование невязки
                    if (Math.Abs(r[i]) < 1e-15)
                    {
                        residualR[i].Text = "0";
                    }
                    else if (double.IsNaN(r[i]) || double.IsInfinity(r[i]))
                    {
                        residualR[i].Text = "Ошибка";
                        residualR[i].BackColor = Color.LightPink;
                    }
                    else
                    {
                        residualR[i].Text = r[i].ToString("E6");
                        residualR[i].BackColor = Color.LightCyan;
                    }
                }

                resultLabel.Text = $"Результат ({methodName}):";

                // Убрано всплывающее окно с сообщением об успехе
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при решении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsSymmetric(double[,] A)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(A[i, j] - A[j, i]) > 1e-10)
                        return false;
                }
            }
            return true;
        }

        private double MaxAbs(double[] vector)
        {
            double max = 0;
            foreach (double val in vector)
            {
                if (Math.Abs(val) > max)
                    max = Math.Abs(val);
            }
            return max;
        }

        private double[] SolveGauss(double[,] A, double[] f)
        {
            int n = f.Length;
            double[,] extendedMatrix = new double[n, n + 1];

            // Создаем расширенную матрицу
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    extendedMatrix[i, j] = A[i, j];
                }
                extendedMatrix[i, n] = f[i];
            }

            // Прямой ход
            for (int i = 0; i < n; i++)
            {
                // Поиск главного элемента
                int maxRow = i;
                for (int k = i + 1; k < n; k++)
                {
                    if (Math.Abs(extendedMatrix[k, i]) > Math.Abs(extendedMatrix[maxRow, i]))
                        maxRow = k;
                }

                // Перестановка строк
                if (maxRow != i)
                {
                    for (int k = 0; k <= n; k++)
                    {
                        double temp = extendedMatrix[i, k];
                        extendedMatrix[i, k] = extendedMatrix[maxRow, k];
                        extendedMatrix[maxRow, k] = temp;
                    }
                }

                // Проверка на вырожденность
                if (Math.Abs(extendedMatrix[i, i]) < 1e-15)
                {
                    throw new InvalidOperationException("Матрица вырождена или почти вырождена. Система не имеет единственного решения.");
                }

                // Приведение к треугольному виду
                for (int k = i + 1; k < n; k++)
                {
                    double factor = extendedMatrix[k, i] / extendedMatrix[i, i];
                    for (int j = i; j <= n; j++)
                    {
                        extendedMatrix[k, j] -= factor * extendedMatrix[i, j];
                    }
                }
            }

            // Обратный ход
            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = extendedMatrix[i, n];
                for (int j = i + 1; j < n; j++)
                {
                    x[i] -= extendedMatrix[i, j] * x[j];
                }
                x[i] /= extendedMatrix[i, i];
            }

            return x;
        }

        private double[] SolveCholesky(double[,] A, double[] f)
        {
            int n = f.Length;
            double[,] L = new double[n, n];

            // Разложение Холецкого: A = L * L^T
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    double sum = 0;
                    if (j == i)
                    {
                        for (int k = 0; k < j; k++)
                        {
                            sum += L[j, k] * L[j, k];
                        }
                        double diagonal = A[j, j] - sum;
                        if (diagonal <= 0)
                        {
                            throw new InvalidOperationException("Матрица не является положительно определенной.");
                        }
                        L[j, j] = Math.Sqrt(diagonal);
                    }
                    else
                    {
                        for (int k = 0; k < j; k++)
                        {
                            sum += L[i, k] * L[j, k];
                        }
                        L[i, j] = (A[i, j] - sum) / L[j, j];
                    }
                }
            }

            // Решение L * y = f
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++)
                {
                    sum += L[i, j] * y[j];
                }
                y[i] = (f[i] - sum) / L[i, i];
            }

            // Решение L^T * x = y
            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += L[j, i] * x[j];
                }
                x[i] = (y[i] - sum) / L[i, i];
            }

            return x;
        }

        private double[] CalculateResidual(double[,] A, double[] f, double[] x)
        {
            int n = f.Length;
            double[] r = new double[n];
            double[] Ax = new double[n];

            // Вычисление A * x
            for (int i = 0; i < n; i++)
            {
                Ax[i] = 0;
                for (int j = 0; j < n; j++)
                {
                    Ax[i] += A[i, j] * x[j];
                }
            }

            // Вычисление невязки r = Ax - f
            for (int i = 0; i < n; i++)
            {
                r[i] = Ax[i] - f[i];
            }

            return r;
        }
    }
}