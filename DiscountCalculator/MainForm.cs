using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
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

                string currency = currencyComboBox.SelectedItem.ToString();
                string baseCurrency = baseCurrencyComboBox.SelectedItem.ToString();

                // Вычисляем скидку
                decimal discountedPrice = originalPrice - (originalPrice * discount / 100);

                // Получаем курс валют через API
                decimal conversionRate = await GetConversionRate(baseCurrency, currency);

                // Переводим в выбранную валюту
                decimal finalPrice = discountedPrice * conversionRate;

                // Отображаем результат
                resultLabel.Text = $"Скидка: {discount}%\n" +
                                   $"Новая стоимость: {discountedPrice.ToString("0.00")} {baseCurrency}\n" +
                                   $"Переведено в {currency}: {finalPrice.ToString("0.00")} {currency}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private async Task<decimal> GetConversionRate(string baseCurrency, string targetCurrency)
        {
            string apiKey = "e728d31c7e90361a27fca9f4"; // Ваш API ключ
            string url = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{baseCurrency}";

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(url);
                JObject json = JObject.Parse(response);
                decimal conversionRate = json["conversion_rates"][targetCurrency].Value<decimal>();
                return conversionRate;
            }
        }
    }
}
