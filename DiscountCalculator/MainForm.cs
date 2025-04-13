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
                // ��������� ����� ������
                if (!decimal.TryParse(originalPriceTextBox.Text, out decimal originalPrice) || originalPrice <= 0)
                {
                    MessageBox.Show("����������, ������� ���������� �������� ���������.");
                    return;
                }

                if (!decimal.TryParse(discountTextBox.Text, out decimal discount) || discount < 0 || discount > 100)
                {
                    MessageBox.Show("����������, ������� ���������� ������� ������ (�� 0 �� 100).");
                    return;
                }

                string currency = currencyComboBox.SelectedItem.ToString();
                string baseCurrency = baseCurrencyComboBox.SelectedItem.ToString();

                // ��������� ������
                decimal discountedPrice = originalPrice - (originalPrice * discount / 100);

                // �������� ���� ����� ����� API
                decimal conversionRate = await GetConversionRate(baseCurrency, currency);

                // ��������� � ��������� ������
                decimal finalPrice = discountedPrice * conversionRate;

                // ���������� ���������
                resultLabel.Text = $"������: {discount}%\n" +
                                   $"����� ���������: {discountedPrice.ToString("0.00")} {baseCurrency}\n" +
                                   $"���������� � {currency}: {finalPrice.ToString("0.00")} {currency}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� ������: {ex.Message}");
            }
        }

        private async Task<decimal> GetConversionRate(string baseCurrency, string targetCurrency)
        {
            string apiKey = "e728d31c7e90361a27fca9f4"; // ��� API ����
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
