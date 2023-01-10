using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Forms
{
    public partial class frmMain : Form
    {
        private readonly BankContext _bankContext = new BankContext();

        public frmMain()
        {
            InitializeComponent();

            _bankContext.Clients.Load();
            _bankContext.BankAccounts.Load();
            _bankContext.Currencies.Load();
            _bankContext.FiscalAccounts.Load();
            _bankContext.ExchangeRates.Load();
            _bankContext.Transactions.Load();

            dgvClients.DataSource = _bankContext.Clients.Local.ToObservableCollection();
            dgvBAccounts.DataSource = _bankContext.BankAccounts.Local.ToObservableCollection();
            dgvFAccounts.DataSource = _bankContext.FiscalAccounts.Local.ToObservableCollection();
            dgvExchangeRates.DataSource = _bankContext.ExchangeRates.Local.ToObservableCollection();
            dgvTransactions.DataSource = _bankContext.Transactions.Local.ToObservableCollection();

            dgvClients.Columns["BankAccount"].Visible = false;
            dgvExchangeRates.Columns["FromCurrencyID"].Visible = false;
            dgvExchangeRates.Columns["ToCurrencyID"].Visible = false;
        }

        private void btnClientAdd_Click(object sender, EventArgs e)
        {
            frmClientDetails frm = new frmClientDetails();
            frm.Text = "Add a client";

            if (frm.ShowDialog() == DialogResult.OK)
                if (frm.LegalEntity)
                    _bankContext.Add(new LegalEntity()
                    {
                        Name = frm.ClientName,
                        PhoneNumber = frm.PhoneNumber,
                        Email = frm.Email,
                        Owner = frm.Owner
                    });
                else
                    _bankContext.Add(new NaturalEntity()
                    {
                        Name = frm.ClientName,
                        PhoneNumber = frm.PhoneNumber,
                        Email = frm.Email
                    });
            _bankContext.SaveChanges();

            dgvClients.DataSource = null;
            dgvClients.DataSource = _bankContext.Clients.Local.ToObservableCollection();
        }

        private void btnClientSearch_Click(object sender, EventArgs e)
        {
            frmClientDetails frm = new frmClientDetails();
            frm.Text = "Search clients";

            if (frm.ShowDialog() == DialogResult.OK)
                dgvClients.DataSource = _bankContext.Clients.Local.Where(x => 
                x.Name == (string.IsNullOrEmpty(frm.ClientName) ? x.Name : frm.ClientName) && 
                x.PhoneNumber == (string.IsNullOrEmpty(frm.PhoneNumber) ? x.PhoneNumber : frm.PhoneNumber) &&
                x.Email == (string.IsNullOrEmpty(frm.Email) ? x.Email : frm.Email)).ToList();
        }

        private void btnClientEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnClientDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnBAccountAdd_Click(object sender, EventArgs e)
        {
            frmBankAccountDetails frm = new frmBankAccountDetails();
            frm.Text = "Add a bank account";

            if (frm.ShowDialog() == DialogResult.OK)
                _bankContext.Add(new BankAccount()
                {
                    Owner = frm.Owner,
                    DateCreated = frm.DateCreated
                });
            _bankContext.SaveChanges();

            dgvBAccounts.DataSource = null;
            dgvBAccounts.DataSource = _bankContext.BankAccounts.Local.ToObservableCollection();
        }

        private void btnBAccountSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnBAccountEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnBAccountDelete_Click(object sender, EventArgs e)
        {
            
        }

        private void btnFAccountAdd_Click(object sender, EventArgs e)
        {
            frmFiscalAccountDetails frm = new frmFiscalAccountDetails();
            frm.Text = "Add a fiscal account";

            if (frm.ShowDialog() == DialogResult.OK)
                _bankContext.Add(new FiscalAccount()
                {
                    Number = frm.AccountNumber,
                    Currency = frm.Currency,
                    Balance = frm.Balance
                });
            _bankContext.SaveChanges();

            dgvFAccounts.DataSource = null;
            dgvFAccounts.DataSource = _bankContext.FiscalAccounts.Local.ToObservableCollection();
        }

        private void btnFAccountSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnFAccountEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnFAccountDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnExchangeRateAdd_Click(object sender, EventArgs e)
        {
            frmExchangeRateDetails frm = new frmExchangeRateDetails();
            frm.Text = "Add a new exchange rate";

            if (frm.ShowDialog() == DialogResult.OK)
                _bankContext.Add(new ExchangeRate()
                {
                    FromCurrency = frm.FromCurrency,
                    FromCurrencyID = frm.FromCurrency.ID,
                    ToCurrency = frm.ToCurrency,
                    ToCurrencyID = frm.ToCurrency.ID,
                    Rate = frm.Rate
                });
            _bankContext.SaveChanges();

            dgvExchangeRates.DataSource = null;
            dgvExchangeRates.DataSource = _bankContext.ExchangeRates.Local.ToObservableCollection();
        }

        private void btnExchangeRateSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnExchangeRateEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnExchangeRateDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnTransactionAdd_Click(object sender, EventArgs e)
        {
            frmTransactionDetails frm = new frmTransactionDetails();
            frm.Text = "Add a new transaction";

            if (frm.ShowDialog() == DialogResult.OK)
                _bankContext.Add(new Transaction()
                {
                    FromAccount = frm.FromAccount,
                    ToAccount = frm.ToAccount,
                    Amount = frm.Amount,
                    Timestamp = frm.Timestamp
                });
            _bankContext.SaveChanges();

            dgvTransactions.DataSource = null;
            dgvTransactions.DataSource = _bankContext.Transactions.Local.ToObservableCollection();
        }

        private void btnTransactionSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnTransactionEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnTransactionDelete_Click(object sender, EventArgs e)
        {

        }
    }
}