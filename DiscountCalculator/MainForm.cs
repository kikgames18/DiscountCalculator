using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Для работы с графиками
using Newtonsoft.Json.Linq;

namespace DiscountCalculator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void calculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация ввода данных
                if (!decimal.TryParse(originalPriceTextBox.Text, out decimal originalPrice) || originalPrice <= 0)
                {
                    MessageBox.Show("Пожалуйста, введите корректную исходную стоимость.");
                    return;
                }

                if (!decimal.TryParse(discountTextBox.Text, out decimal discount) || discount < 0 || discount > 100)
                {
                    MessageBox.Show("Пожалуйста, введите корректный процент скидки (от 0 до 100).");
                    return;
                }

                // Проверка на выбранный элемент в ComboBox
                if (currencyComboBox.SelectedIndex == -1 || baseCurrencyComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Пожалуйста, выберите валюты.");
                    return;
                }

                // Получаем выбранные валюты, проверяем на null
                string currency = currencyComboBox.SelectedItem?.ToString();
                string baseCurrency = baseCurrencyComboBox.SelectedItem?.ToString();

                // Проверка на null для валют
                if (string.IsNullOrEmpty(currency) || string.IsNullOrEmpty(baseCurrency))
                {
                    MessageBox.Show("Ошибка: одна из валют не выбрана.");
                    return;
                }

                // Вычисляем скидку
                decimal discountedPrice = originalPrice - (originalPrice * discount / 100);

                // Получаем курс валют через API
                decimal conversionRate = await GetConversionRate(baseCurrency, currency);

                // Переводим в выбранную валюту
                decimal finalPrice = discountedPrice * conversionRate;

                // Формируем результат
                string result = $"Скидка: {discount}%\n" +
                                $"Новая стоимость: {discountedPrice.ToString("0.00")} {baseCurrency}\n" +
                                $"Переведено в {currency}: {finalPrice.ToString("0.00")} {currency}";

                // Отображаем результат
                resultLabel.Text = result;

                // Строим график изменения стоимости с разной скидкой
                DrawDiscountChart(originalPrice);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }



        private async Task<decimal> GetConversionRate(string baseCurrency, string targetCurrency)
        {
            // Проверка на null для baseCurrency и targetCurrency
            if (string.IsNullOrEmpty(baseCurrency) || string.IsNullOrEmpty(targetCurrency))
            {
                MessageBox.Show("Ошибка: одна из валют не выбрана.");
                return 0; // Или выбросить исключение
            }

            string apiKey = "e728d31c7e90361a27fca9f4"; // Ваш API ключ
            string url = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{baseCurrency}";

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(url);
                JObject json = JObject.Parse(response);
                decimal conversionRate = json["conversion_rates"][targetCurrency]?.Value<decimal>() ?? 0;
                return conversionRate;
            }
        }


        private void DrawDiscountChart(decimal originalPrice)
        {
            // Очистим график перед добавлением новых данных
            discountChart.Series.Clear();
            var series = new Series("Discounts")
            {
                ChartType = SeriesChartType.Line // Линия для графика
            };

            // Добавим данные для графика (рассчитываем стоимость для скидок от 0 до 100)
            for (int i = 0; i <= 100; i++)
            {
                decimal discountedPrice = originalPrice - (originalPrice * i / 100);
                series.Points.AddXY(i, discountedPrice); // Добавляем точку на графике (скидка, цена)
            }

            // Добавляем серию на график
            discountChart.Series.Add(series);

            // Настроим оси
            discountChart.ChartAreas[0].AxisX.Title = "Скидка (%)";
            discountChart.ChartAreas[0].AxisY.Title = "Новая стоимость";

            // Дополнительные настройки графика
            discountChart.Titles.Add("График изменения стоимости с учетом скидки");
        }
    }
}
