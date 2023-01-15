using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Forms
{
    public partial class frmMain : Form
    {
        private readonly BankContext _bankContext = new();

        public frmMain()
        {
            InitializeComponent();

            _bankContext.Clients.Load();
            _bankContext.BankAccounts.Load();
            _bankContext.Currencies.Load();
            _bankContext.FiscalAccounts.Load();
            _bankContext.ExchangeRates.Load();
            _bankContext.Transactions.Load();

            RefreshDgvClients();
            RefreshDgvBAccounts();
            RefreshDgvFAccounts();
            RefreshDgvExchangeRates();
            RefreshDgvTransactions();
        }

        private void RefreshDgvClients()
        {
            dgvClients.DataSource = null;
            dgvClients.DataSource = _bankContext.Clients.Local.ToObservableCollection();

            dgvClients.Columns[0].Width = 30;
            dgvClients.Columns[3].Width = 200;
            dgvClients.Columns["BankAccount"].Visible = false;
        }

        private void RefreshDgvBAccounts()
        {
            dgvBAccounts.DataSource = null;
            dgvBAccounts.DataSource = _bankContext.BankAccounts.Local.ToObservableCollection();

            dgvBAccounts.Columns[0].Width = 30;
        }

        private void RefreshDgvFAccounts()
        {
            dgvFAccounts.DataSource = null;
            dgvFAccounts.DataSource = _bankContext.FiscalAccounts.Local.ToObservableCollection();

            dgvFAccounts.Columns[0].Width = 30;
            dgvFAccounts.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvFAccounts.Columns[4].Width = 150;
        }

        private void RefreshDgvExchangeRates()
        {
            dgvExchangeRates.DataSource = null;
            dgvExchangeRates.DataSource = _bankContext.ExchangeRates.Local.ToObservableCollection();

            dgvExchangeRates.Columns["FromCurrencyID"].Visible = false;
            dgvExchangeRates.Columns["ToCurrencyID"].Visible   = false;
        }

        private void RefreshDgvTransactions()
        {
            dgvTransactions.DataSource = null;
            dgvTransactions.DataSource = _bankContext.Transactions.Local.ToObservableCollection();

            dgvTransactions.Columns[0].Width = 30;
            dgvTransactions.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnClientAdd_Click(object sender, EventArgs e)
        {
            frmClientDetails frm = new();
            frm.Text = "Add a client";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.LegalEntity)
                    _bankContext.Add(new LegalEntity()
                    {
                        Name        = frm.ClientName,
                        PhoneNumber = frm.PhoneNumber,
                        Email       = frm.Email,
                        Owner       = frm.Owner
                    });
                else
                    _bankContext.Add(new NaturalEntity()
                    {
                        Name        = frm.ClientName,
                        PhoneNumber = frm.PhoneNumber,
                        Email       = frm.Email
                    });

                _bankContext.SaveChanges();

                RefreshDgvClients();
            }
        }

        private void btnClientSearch_Click(object sender, EventArgs e)
        {
            frmClientDetails frm = new();
            frm.Text = "Search clients";

            if (frm.ShowDialog() == DialogResult.OK)
                dgvClients.DataSource = _bankContext.Clients.Local.Where(x => 
                x.Name == (string.IsNullOrEmpty(frm.ClientName) ? x.Name : frm.ClientName) && 
                x.PhoneNumber == (string.IsNullOrEmpty(frm.PhoneNumber) ? x.PhoneNumber : frm.PhoneNumber) &&
                x.Email == (string.IsNullOrEmpty(frm.Email) ? x.Email : frm.Email)).ToList();
        }

        private void btnClientEdit_Click(object sender, EventArgs e)
        {
            frmClientDetails frm = new();
            frm.Text = "Edit a client";

            Client selectedClient = (Client)dgvClients.SelectedRows[0].DataBoundItem;
            frm.ClientName  = selectedClient.Name;
            frm.PhoneNumber = selectedClient.PhoneNumber;
            frm.Email       = selectedClient.Email;
            frm.LegalEntity = selectedClient is LegalEntity;

            if (selectedClient is LegalEntity)
                frm.Owner = ((LegalEntity)selectedClient).Owner;

            frm.DisableCmbType();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                selectedClient.Name        = frm.ClientName;
                selectedClient.PhoneNumber = frm.PhoneNumber;
                selectedClient.Email       = frm.Email;
                
                if (selectedClient is LegalEntity)
                    ((LegalEntity)selectedClient).Owner = frm.Owner;
                
                _bankContext.SaveChanges();

                RefreshDgvClients();
            }
        }

        private void btnClientDelete_Click(object sender, EventArgs e)
        {
            _bankContext.Remove((Client)dgvClients.SelectedRows[0].DataBoundItem);
            _bankContext.SaveChanges();

            RefreshDgvClients();
        }

        private void btnBAccountAdd_Click(object sender, EventArgs e)
        {
            frmBankAccountDetails frm = new();
            frm.Text = "Add a bank account";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _bankContext.Add(new BankAccount()
                {
                    Owner       = _bankContext.Clients.Single(c => c.ID == frm.Owner.ID),
                    DateCreated = frm.DateCreated
                });

                _bankContext.SaveChanges();

                RefreshDgvBAccounts();
            }
        }

        private void btnBAccountSearch_Click(object sender, EventArgs e)
        {
            frmBankAccountDetails frm = new();
            frm.Text = "Search bank accounts";

            if (frm.ShowDialog() == DialogResult.OK)
                dgvBAccounts.DataSource = _bankContext.BankAccounts.Local.Where(b => b.Owner.ID == frm.Owner.ID).ToList();
            else
                dgvBAccounts.DataSource = _bankContext.BankAccounts.Local.ToObservableCollection();
        }

        private void btnBAccountEdit_Click(object sender, EventArgs e)
        {
            frmBankAccountDetails frm = new();
            frm.Text = "Edit a bank account";

            BankAccount selectedBankAccount = (BankAccount)dgvBAccounts.SelectedRows[0].DataBoundItem;
            frm.Owner       = selectedBankAccount.Owner;
            frm.DateCreated = selectedBankAccount.DateCreated;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                selectedBankAccount.Owner       = _bankContext.Clients.Local.Single(c => c.ID == frm.Owner.ID);
                selectedBankAccount.DateCreated = frm.DateCreated;
                _bankContext.SaveChanges();

                RefreshDgvBAccounts();
            }
        }

        private void btnBAccountDelete_Click(object sender, EventArgs e)
        {
            _bankContext.Remove((BankAccount)dgvBAccounts.SelectedRows[0].DataBoundItem);
            _bankContext.SaveChanges();

            RefreshDgvBAccounts();
        }

        private void btnFAccountAdd_Click(object sender, EventArgs e)
        {
            frmFiscalAccountDetails frm = new();
            frm.Text = "Add a fiscal account";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _bankContext.Add(new FiscalAccount()
                {
                    Number      = frm.AccountNumber,
                    Currency    = _bankContext.Currencies.Single(c => c.ID == frm.Currency.ID),
                    Balance     = frm.Balance,
                    BankAccount = _bankContext.BankAccounts.Single(b => b.ID == frm.BankAccount.ID),
                });

                _bankContext.SaveChanges();

                RefreshDgvFAccounts();
            }
        }

        private void btnFAccountSearch_Click(object sender, EventArgs e)
        {
            frmFiscalAccountDetails frm = new();
            frm.Text = "Search fiscal accounts";

            if (frm.ShowDialog() == DialogResult.OK)
                dgvFAccounts.DataSource = _bankContext.FiscalAccounts.Local.Where(f => 
                f.Number == (string.IsNullOrEmpty(frm.AccountNumber) ? f.Number : frm.AccountNumber) &&
                f.BankAccount.ID == frm.BankAccount.ID).ToList();
            else
                dgvFAccounts.DataSource = _bankContext.FiscalAccounts.Local.ToObservableCollection();
        }

        private void btnFAccountEdit_Click(object sender, EventArgs e)
        {
            frmFiscalAccountDetails frm = new();
            frm.Text = "Edit a fiscal account";

            FiscalAccount selectedFiscalAccount = (FiscalAccount)dgvFAccounts.SelectedRows[0].DataBoundItem;
            frm.AccountNumber = selectedFiscalAccount.Number;
            frm.Currency      = selectedFiscalAccount.Currency;
            frm.Balance       = selectedFiscalAccount.Balance;
            frm.BankAccount   = selectedFiscalAccount.BankAccount;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                selectedFiscalAccount.Number      = frm.AccountNumber;
                selectedFiscalAccount.Currency    = _bankContext.Currencies.Local.Single(c => c.ID == frm.Currency.ID);
                selectedFiscalAccount.Balance     = frm.Balance;
                selectedFiscalAccount.BankAccount = _bankContext.BankAccounts.Local.Single(b => b.ID == frm.BankAccount.ID);
                _bankContext.SaveChanges();

                RefreshDgvFAccounts();
            }
        }

        private void btnFAccountDelete_Click(object sender, EventArgs e)
        {
            _bankContext.Remove((FiscalAccount)dgvFAccounts.SelectedRows[0].DataBoundItem);
            _bankContext.SaveChanges();

            RefreshDgvFAccounts();
        }

        private void btnExchangeRateAdd_Click(object sender, EventArgs e)
        {
            frmExchangeRateDetails frm = new();
            frm.Text = "Add a new exchange rate";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _bankContext.Add(new ExchangeRate()
                {
                    FromCurrency   = _bankContext.Currencies.Single(c => c.ID == frm.FromCurrency.ID),
                    FromCurrencyID = frm.FromCurrency.ID,
                    ToCurrency     = _bankContext.Currencies.Single(c => c.ID == frm.ToCurrency.ID),
                    ToCurrencyID   = frm.ToCurrency.ID,
                    Rate           = frm.Rate
                });
                
                _bankContext.SaveChanges();

                RefreshDgvExchangeRates();
            }
        }

        private void btnExchangeRateSearch_Click(object sender, EventArgs e)
        {
            frmExchangeRateDetails frm = new();
            frm.Text = "Search exchange rates";

            if (frm.ShowDialog() == DialogResult.OK)
                dgvExchangeRates.DataSource = _bankContext.ExchangeRates.Local.Where(r =>
                r.FromCurrencyID == frm.FromCurrency.ID &&
                r.ToCurrencyID   == frm.ToCurrency.ID).ToList();
            else
                dgvExchangeRates.DataSource = _bankContext.ExchangeRates.Local.ToObservableCollection();
        }

        private void btnExchangeRateEdit_Click(object sender, EventArgs e)
        {
            frmExchangeRateDetails frm = new();
            frm.Text = "Edit an exchange rate";

            ExchangeRate selectedExchange = (ExchangeRate)dgvExchangeRates.SelectedRows[0].DataBoundItem;
            frm.FromCurrency = selectedExchange.FromCurrency;
            frm.ToCurrency   = selectedExchange.ToCurrency;
            frm.Rate         = selectedExchange.Rate;

            frm.DisableCurrencyChanging();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                selectedExchange.Rate           = frm.Rate;
                _bankContext.SaveChanges();

                RefreshDgvExchangeRates();
            }
        }

        private void btnExchangeRateDelete_Click(object sender, EventArgs e)
        {
            _bankContext.Remove((ExchangeRate)dgvExchangeRates.SelectedRows[0].DataBoundItem);
            _bankContext.SaveChanges();

            RefreshDgvExchangeRates();
        }

        private void btnTransactionAdd_Click(object sender, EventArgs e)
        {
            frmTransactionDetails frm = new();
            frm.Text = "Add a new transaction";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _bankContext.Add(new Transaction()
                {
                    FromAccount = _bankContext.FiscalAccounts.Single(a => a.ID == frm.FromAccount.ID),
                    ToAccount   = _bankContext.FiscalAccounts.Single(a => a.ID == frm.ToAccount.ID),
                    Amount      = frm.Amount,
                    Timestamp   = frm.Timestamp
                });
            
                _bankContext.SaveChanges();

                RefreshDgvTransactions();
            }
        }

        private void btnTransactionSearch_Click(object sender, EventArgs e)
        {
            frmTransactionDetails frm = new();
            frm.Text = "Search transactions";

            if (frm.ShowDialog() == DialogResult.OK)
                dgvTransactions.DataSource = _bankContext.Transactions.Local.Where(t =>
                t.FromAccount.ID == frm.FromAccount.ID &&
                t.ToAccount.ID   == frm.ToAccount.ID).ToList();
            else
                dgvTransactions.DataSource = _bankContext.Transactions.Local.ToObservableCollection();
        }

        private void btnTransactionEdit_Click(object sender, EventArgs e)
        {
            frmTransactionDetails frm = new();
            frm.Text = "Edit a transaction";

            Transaction selectedTransaction = (Transaction)dgvTransactions.SelectedRows[0].DataBoundItem;
            frm.FromAccount = selectedTransaction.FromAccount;
            frm.ToAccount   = selectedTransaction.ToAccount;
            frm.Amount      = selectedTransaction.Amount;
            frm.Timestamp   = selectedTransaction.Timestamp;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                selectedTransaction.FromAccount = _bankContext.FiscalAccounts.Local.Single(f => f.ID == frm.FromAccount.ID);
                selectedTransaction.ToAccount   = _bankContext.FiscalAccounts.Local.Single(f => f.ID == frm.ToAccount.ID);
                selectedTransaction.Amount      = frm.Amount;
                selectedTransaction.Timestamp   = frm.Timestamp;
                _bankContext.SaveChanges();

                RefreshDgvTransactions();
            }
        }

        private void btnTransactionDelete_Click(object sender, EventArgs e)
        {
            _bankContext.Remove((Transaction)dgvTransactions.SelectedRows[0].DataBoundItem);
            _bankContext.SaveChanges();

            RefreshDgvTransactions();
        }
    }
}