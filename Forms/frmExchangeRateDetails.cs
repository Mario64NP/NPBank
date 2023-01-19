using Controller;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Model;

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
                txtRate.Text = Coordinator.GetRateOnline(txtRate.Text, ((Currency)(cmbFrom.SelectedItem)).Code, ((Currency)(cmbTo.SelectedItem)).Code).ToString();
            }
            catch (Exception exc)
            {
                txtRate.Text = exc.Message;
            }
        }
    }
}