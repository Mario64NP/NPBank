using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class frmExchangeRateDetails : Form
    {
        private readonly BankContext _bankContext = new BankContext();

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

        private List<Currency> OtherCurrencies(Currency selectedItem)
        {
            List<Currency> currencies = _bankContext.Currencies.Local.ToList();
            currencies.Remove(selectedItem);
            return currencies;
        }

        internal void DisableCurrencyChanging()
        {
            cmbFrom.Enabled = false;
            cmbTo.Enabled = false;
        }
    }
}
