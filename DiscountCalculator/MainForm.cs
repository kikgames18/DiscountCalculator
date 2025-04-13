using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // ��� ������ � ���������
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

                // �������� �� ��������� ������� � ComboBox
                if (currencyComboBox.SelectedIndex == -1 || baseCurrencyComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("����������, �������� ������.");
                    return;
                }

                // �������� ��������� ������, ��������� �� null
                string currency = currencyComboBox.SelectedItem?.ToString();
                string baseCurrency = baseCurrencyComboBox.SelectedItem?.ToString();

                // �������� �� null ��� �����
                if (string.IsNullOrEmpty(currency) || string.IsNullOrEmpty(baseCurrency))
                {
                    MessageBox.Show("������: ���� �� ����� �� �������.");
                    return;
                }

                // ��������� ������
                decimal discountedPrice = originalPrice - (originalPrice * discount / 100);

                // �������� ���� ����� ����� API
                decimal conversionRate = await GetConversionRate(baseCurrency, currency);

                // ��������� � ��������� ������
                decimal finalPrice = discountedPrice * conversionRate;

                // ��������� ���������
                string result = $"������: {discount}%\n" +
                                $"����� ���������: {discountedPrice.ToString("0.00")} {baseCurrency}\n" +
                                $"���������� � {currency}: {finalPrice.ToString("0.00")} {currency}";

                // ���������� ���������
                resultLabel.Text = result;

                // ������ ������ ��������� ��������� � ������ �������
                DrawDiscountChart(originalPrice);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� ������: {ex.Message}");
            }
        }



        private async Task<decimal> GetConversionRate(string baseCurrency, string targetCurrency)
        {
            // �������� �� null ��� baseCurrency � targetCurrency
            if (string.IsNullOrEmpty(baseCurrency) || string.IsNullOrEmpty(targetCurrency))
            {
                MessageBox.Show("������: ���� �� ����� �� �������.");
                return 0; // ��� ��������� ����������
            }

            string apiKey = "e728d31c7e90361a27fca9f4"; // ��� API ����
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
            // ������� ������ ����� ����������� ����� ������
            discountChart.Series.Clear();
            var series = new Series("Discounts")
            {
                ChartType = SeriesChartType.Line // ����� ��� �������
            };

            // ������� ������ ��� ������� (������������ ��������� ��� ������ �� 0 �� 100)
            for (int i = 0; i <= 100; i++)
            {
                decimal discountedPrice = originalPrice - (originalPrice * i / 100);
                series.Points.AddXY(i, discountedPrice); // ��������� ����� �� ������� (������, ����)
            }

            // ��������� ����� �� ������
            discountChart.Series.Add(series);

            // �������� ���
            discountChart.ChartAreas[0].AxisX.Title = "������ (%)";
            discountChart.ChartAreas[0].AxisY.Title = "����� ���������";

            // �������������� ��������� �������
            discountChart.Titles.Add("������ ��������� ��������� � ������ ������");
        }
    }
}
