using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Text.Json;

namespace Forms
{
    public partial class frmExchangeRateDetails : Form
    {
        private readonly BankContext _bankContext = new();

        public Currency FromCurrency { get { return (Currency)cmbFrom.SelectedItem; } set { cmbFrom.SelectedItem = value; } }
        public Currency ToCurrency { get { return (Currency)cmbTo.SelectedItem; } set { cmbTo.SelectedItem = value; } }
        public double Rate { get { return double.Parse(txtRate.Text); } set { txtRate.Text = value.ToString(); } }
        public frmExchangeRateDetails()
        {
            InitializeComponent();

            _bankContext.Currencies.Load();
            cmbFrom.DataSource = _bankContext.Currencies.Local.ToObservableCollection();
        }

        private void cmbFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTo.DataSource = OtherCurrencies((Currency)cmbFrom.SelectedItem);
        }

        /// <summary>
        /// Provides a list of all the currencies except the one specified
        /// </summary>
        /// <param name="currencyToRemove">The currency you want to exclude</param>
        /// <returns>A list of currencies excluding the one specified</returns>
        private List<Currency> OtherCurrencies(Currency currencyToRemove)
        {
            List<Currency> currencies = _bankContext.Currencies.Local.ToList();
            currencies.Remove(currencyToRemove);
            return currencies;
        }

        internal void DisableCurrencyChanging()
        {
            cmbFrom.Enabled = false;
            cmbTo.Enabled = false;
        }

        private void btnGetOnline_Click(object sender, EventArgs e)
        {
            try
            {
                txtRate.Text = GetRateOnline(txtRate.Text).ToString();
            }
            catch (Exception exc)
            {
                txtRate.Text = exc.Message;
            }
        }

        /// <summary>
        /// Gets the current exchange rate for the selected currencies online
        /// </summary>
        /// <param name="apiKey">Your API key for <see href="https://apilayer.com">apilayer.com</see></param>
        /// <returns>The current exchange rate</returns>
        private double GetRateOnline(string apiKey)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
            string response = httpClient.GetStringAsync($"https://api.apilayer.com/exchangerates_data/convert?to={((Currency)(cmbTo.SelectedItem)).Code}&from={((Currency)(cmbFrom.SelectedItem)).Code}&amount=1").Result;

            using JsonDocument document = JsonDocument.Parse(response);
            JsonElement rootElement = document.RootElement;
            return rootElement.GetProperty("result").GetDouble();
        }
    }
}
