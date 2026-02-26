using System;
using System.Windows.Forms;
using System.Drawing;

namespace MinMaxFinder
{
    public partial class Form1 : Form
    {
        private TextBox sizeTextBox;
        private Button createButton;
        private Button findButton;
        private Button clearButton;
        private ListBox arrayListBox;
        private Label resultLabel;
        private Label sizeLabel;
        private Panel mainPanel;

        private int[] numbers;
        private Random random = new Random();

        public Form1()
        {
            this.Text = "Поиск минимума и максимума";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            CreateInterface();
        }

        private void CreateInterface()
        {
            Label titleLabel = new Label
            {
                Text = "Поиск минимального и максимального элемента",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(50, 20),
                Size = new Size(450, 30),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(titleLabel);

            Panel inputPanel = new Panel
            {
                Location = new Point(30, 60),
                Size = new Size(430, 60),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(inputPanel);

            sizeLabel = new Label
            {
                Text = "Размер массива N:",
                Location = new Point(10, 20),
                Size = new Size(120, 23),
                Font = new Font("Arial", 10),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.Black
            };
            inputPanel.Controls.Add(sizeLabel);

            sizeTextBox = new TextBox
            {
                Location = new Point(140, 20),
                Size = new Size(80, 23),
                Font = new Font("Arial", 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            inputPanel.Controls.Add(sizeTextBox);

            createButton = new Button
            {
                Text = "Создать массив",
                Location = new Point(240, 18),
                Size = new Size(120, 28),
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                Font = new Font("Arial", 9),
                FlatStyle = FlatStyle.Standard
            };
            createButton.Click += CreateButton_Click;
            inputPanel.Controls.Add(createButton);

            mainPanel = new Panel
            {
                Location = new Point(30, 130),
                Size = new Size(430, 180),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(mainPanel);

            Label arrayLabel = new Label
            {
                Text = "Сгенерированный массив:",
                Location = new Point(10, 10),
                Size = new Size(300, 20),
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.Black
            };
            mainPanel.Controls.Add(arrayLabel);

            arrayListBox = new ListBox
            {
                Location = new Point(10, 35),
                Size = new Size(410, 130),
                Font = new Font("Consolas", 10),
                BorderStyle = BorderStyle.FixedSingle,
                HorizontalScrollbar = true,
                BackColor = Color.White
            };
            mainPanel.Controls.Add(arrayListBox);

            Panel buttonPanel = new Panel
            {
                Location = new Point(30, 320),
                Size = new Size(430, 50),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(buttonPanel);

            findButton = new Button
            {
                Text = "Найти минимум и максимум",
                Location = new Point(80, 10),
                Size = new Size(180, 30),
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                Font = new Font("Arial", 9),
                FlatStyle = FlatStyle.Standard,
                Enabled = false
            };
            findButton.Click += FindButton_Click;
            buttonPanel.Controls.Add(findButton);

            clearButton = new Button
            {
                Text = "Очистить",
                Location = new Point(270, 10),
                Size = new Size(80, 30),
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                Font = new Font("Arial", 9),
                FlatStyle = FlatStyle.Standard
            };
            clearButton.Click += ClearButton_Click;
            buttonPanel.Controls.Add(clearButton);

            resultLabel = new Label
            {
                Text = "Результат: ",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(30, 380),
                Size = new Size(430, 30),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(resultLabel);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sizeTextBox.Text))
                {
                    MessageBox.Show("Введите размер массива!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(sizeTextBox.Text, out int size) || size <= 0)
                {
                    MessageBox.Show("Размер массива должен быть положительным целым числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (size > 100)
                {
                    MessageBox.Show("Размер массива не должен превышать 100!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                numbers = new int[size];

                arrayListBox.Items.Clear();
                for (int i = 0; i < size; i++)
                {
                    numbers[i] = random.Next(1, 101);

                    arrayListBox.Items.Add($"[{i}] = {numbers[i]}");
                }

                findButton.Enabled = true;

                resultLabel.Text = "Результат: ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (numbers == null || numbers.Length == 0)
                {
                    MessageBox.Show("Сначала создайте массив!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int min = numbers[0];
                int max = numbers[0];
                int minIndex = 0;
                int maxIndex = 0;

                for (int i = 1; i < numbers.Length; i++)
                {
                    if (numbers[i] < min)
                    {
                        min = numbers[i];
                        minIndex = i;
                    }

                    if (numbers[i] > max)
                    {
                        max = numbers[i];
                        maxIndex = i;
                    }
                }

                resultLabel.Text = $"Результат: Минимум = {min} [индекс {minIndex}], " +
                                  $"Максимум = {max} [индекс {maxIndex}]";

                string message = $"Массив размером {numbers.Length} элементов\n\n" +
                                $"Минимальный элемент: {min} (индекс {minIndex})\n" +
                                $"Максимальный элемент: {max} (индекс {maxIndex})";

                MessageBox.Show(message, "Результаты поиска",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            sizeTextBox.Text = "";

            arrayListBox.Items.Clear();

            numbers = null;

            findButton.Enabled = false;

            resultLabel.Text = "Результат: ";
        }
    }
}