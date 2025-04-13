namespace DiscountCalculator
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataVisualization.Charting.Chart discountChart; // График

        private void InitializeComponent()
        {
            this.originalPriceTextBox = new System.Windows.Forms.TextBox();
            this.discountTextBox = new System.Windows.Forms.TextBox();
            this.currencyComboBox = new System.Windows.Forms.ComboBox();
            this.baseCurrencyComboBox = new System.Windows.Forms.ComboBox();
            this.calculateButton = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.originalPriceLabel = new System.Windows.Forms.Label();
            this.discountLabel = new System.Windows.Forms.Label();
            this.currencyLabel = new System.Windows.Forms.Label();
            this.baseCurrencyLabel = new System.Windows.Forms.Label();
            this.discountChart = new System.Windows.Forms.DataVisualization.Charting.Chart(); // Инициализация графика

            this.SuspendLayout();

            // 
            // discountChart
            // 
            this.discountChart.Location = new System.Drawing.Point(12, 250); // Размещение графика
            this.discountChart.Name = "discountChart";
            this.discountChart.Size = new System.Drawing.Size(260, 150); // Размер графика
            this.discountChart.TabIndex = 12;
            this.discountChart.Text = "discountChart";

            // 
            // originalPriceLabel
            // 
            this.originalPriceLabel.AutoSize = true;
            this.originalPriceLabel.Location = new System.Drawing.Point(12, 9);
            this.originalPriceLabel.Name = "originalPriceLabel";
            this.originalPriceLabel.Size = new System.Drawing.Size(128, 13);
            this.originalPriceLabel.TabIndex = 0;
            this.originalPriceLabel.Text = "Изначальная стоимость (USD):";

            // 
            // originalPriceTextBox
            // 
            this.originalPriceTextBox.Location = new System.Drawing.Point(12, 25);
            this.originalPriceTextBox.Name = "originalPriceTextBox";
            this.originalPriceTextBox.Size = new System.Drawing.Size(260, 20);
            this.originalPriceTextBox.TabIndex = 1;

            // 
            // discountLabel
            // 
            this.discountLabel.AutoSize = true;
            this.discountLabel.Location = new System.Drawing.Point(12, 49);
            this.discountLabel.Name = "discountLabel";
            this.discountLabel.Size = new System.Drawing.Size(83, 13);
            this.discountLabel.TabIndex = 2;
            this.discountLabel.Text = "Скидка (%) :";

            // 
            // discountTextBox
            // 
            this.discountTextBox.Location = new System.Drawing.Point(12, 65);
            this.discountTextBox.Name = "discountTextBox";
            this.discountTextBox.Size = new System.Drawing.Size(260, 20);
            this.discountTextBox.TabIndex = 3;

            // 
            // currencyLabel
            // 
            this.currencyLabel.AutoSize = true;
            this.currencyLabel.Location = new System.Drawing.Point(12, 89);
            this.currencyLabel.Name = "currencyLabel";
            this.currencyLabel.Size = new System.Drawing.Size(113, 13);
            this.currencyLabel.TabIndex = 4;
            this.currencyLabel.Text = "Выберите валюту:";

            // 
            // currencyComboBox
            // 
            this.currencyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currencyComboBox.FormattingEnabled = true;
            this.currencyComboBox.Items.AddRange(new object[] {
            "USD",
            "EUR",
            "RUB",
            "UAH"});
            this.currencyComboBox.Location = new System.Drawing.Point(12, 105);
            this.currencyComboBox.Name = "currencyComboBox";
            this.currencyComboBox.Size = new System.Drawing.Size(121, 21);
            this.currencyComboBox.TabIndex = 5;

            // 
            // baseCurrencyLabel
            // 
            this.baseCurrencyLabel.AutoSize = true;
            this.baseCurrencyLabel.Location = new System.Drawing.Point(12, 130);
            this.baseCurrencyLabel.Name = "baseCurrencyLabel";
            this.baseCurrencyLabel.Size = new System.Drawing.Size(124, 13);
            this.baseCurrencyLabel.TabIndex = 6;
            this.baseCurrencyLabel.Text = "Выберите базовую валюту:";

            // 
            // baseCurrencyComboBox
            // 
            this.baseCurrencyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baseCurrencyComboBox.FormattingEnabled = true;
            this.baseCurrencyComboBox.Items.AddRange(new object[] {
            "USD",
            "EUR",
            "RUB",
            "UAH"});
            this.baseCurrencyComboBox.Location = new System.Drawing.Point(12, 146);
            this.baseCurrencyComboBox.Name = "baseCurrencyComboBox";
            this.baseCurrencyComboBox.Size = new System.Drawing.Size(121, 21);
            this.baseCurrencyComboBox.TabIndex = 7;

            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(12, 185);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(260, 23);
            this.calculateButton.TabIndex = 8;
            this.calculateButton.Text = "Рассчитать";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.calculateButton_Click);

            // 
            // resultLabel
            // 
            this.resultLabel.Location = new System.Drawing.Point(12, 225);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(260, 100);
            this.resultLabel.TabIndex = 9;
            this.resultLabel.Text = "Результат";

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 411); // Размер формы
            this.Controls.Add(this.discountChart); // Добавление графика в форму
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.baseCurrencyComboBox);
            this.Controls.Add(this.baseCurrencyLabel);
            this.Controls.Add(this.currencyComboBox);
            this.Controls.Add(this.currencyLabel);
            this.Controls.Add(this.discountTextBox);
            this.Controls.Add(this.discountLabel);
            this.Controls.Add(this.originalPriceTextBox);
            this.Controls.Add(this.originalPriceLabel);
            this.Name = "MainForm";
            this.Text = "Калькулятор скидок";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox originalPriceTextBox;
        private System.Windows.Forms.TextBox discountTextBox;
        private System.Windows.Forms.ComboBox currencyComboBox;
        private System.Windows.Forms.ComboBox baseCurrencyComboBox;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Label originalPriceLabel;
        private System.Windows.Forms.Label discountLabel;
        private System.Windows.Forms.Label currencyLabel;
        private System.Windows.Forms.Label baseCurrencyLabel;
    }
}
