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

            RefreshAllDgvs();
        }

        /// <summary>
        /// Re-links the clients data grid view with the data source and formats the columns.
        /// </summary>
        private void RefreshDgvClients()
        {
            dgvClients.DataSource = null;
            dgvClients.DataSource = _bankContext.Clients.Local.ToObservableCollection();

            dgvClients.Columns[0].Width = 30;
            dgvClients.Columns[3].Width = 200;
            dgvClients.Columns["BankAccount"].Visible = false;
        }

        /// <summary>
        /// Re-links the bank accounts data grid view with the data source and formats the columns.
        /// </summary>
        private void RefreshDgvBAccounts()
        {
            dgvBAccounts.DataSource = null;
            dgvBAccounts.DataSource = _bankContext.BankAccounts.Local.ToObservableCollection();

            dgvBAccounts.Columns[0].Width = 30;
        }

        /// <summary>
        /// Re-links the fiscal accounts data grid view with the data source and formats the columns.
        /// </summary>
        private void RefreshDgvFAccounts()
        {
            dgvFAccounts.DataSource = null;
            dgvFAccounts.DataSource = _bankContext.FiscalAccounts.Local.ToObservableCollection();

            dgvFAccounts.Columns[0].Width = 30;
            dgvFAccounts.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvFAccounts.Columns[4].Width = 150;
        }

        /// <summary>
        /// Re-links the exchange rates data grid view with the data source and formats the columns.
        /// </summary>
        private void RefreshDgvExchangeRates()
        {
            dgvExchangeRates.DataSource = null;
            dgvExchangeRates.DataSource = _bankContext.ExchangeRates.Local.ToObservableCollection();

            dgvExchangeRates.Columns["FromCurrencyID"].Visible = false;
            dgvExchangeRates.Columns["ToCurrencyID"].Visible   = false;
        }

        /// <summary>
        /// Re-links the transactions data grid view with the data source and formats the columns.
        /// </summary>
        private void RefreshDgvTransactions()
        {
            dgvTransactions.DataSource = null;
            dgvTransactions.DataSource = _bankContext.Transactions.Local.ToObservableCollection();

            dgvTransactions.Columns[0].Width = 30;
            dgvTransactions.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        /// <summary>
        /// Refreshes all data grid views.
        /// </summary>
        private void RefreshAllDgvs()
        {
            RefreshDgvClients();
            RefreshDgvBAccounts();
            RefreshDgvFAccounts();
            RefreshDgvExchangeRates();
            RefreshDgvTransactions();
        }

        /// <summary>
        /// Executes the transaction. Decreases the balance of the source account by the amount of the transaction,
        /// and increases the balance of the destination account by the amount of the transaction multiplied by the exchange rate.
        /// </summary>
        /// <param name="t">The transaction</param>
        private void ExecuteTransaction(Transaction t)
        {
            double rate = _bankContext.ExchangeRates.Single(r => r.FromCurrencyID == t.FromAccount.Currency.ID && r.ToCurrencyID == t.ToAccount.Currency.ID).Rate;
            t.FromAccount.Balance -= t.Amount;
            t.ToAccount.Balance   += t.Amount * rate;
            _bankContext.SaveChanges();
        }

        private void btnClientAdd_Click(object sender, EventArgs e)
        {
            frmClientDetails frm = new();
            frm.Text = "Add a client";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.LegalEntity)
                {
                    LegalEntity le = new()
                    {
                        Name        = frm.ClientName,
                        PhoneNumber = frm.PhoneNumber,
                        Email       = frm.Email,
                        Owner       = frm.Owner
                    };
                    if (Client.IsValidClient(le))
                        _bankContext.Add(le);
                    else
                        MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    NaturalEntity ne = new()
                    {
                        Name        = frm.ClientName,
                        PhoneNumber = frm.PhoneNumber,
                        Email       = frm.Email
                    };
                    if (Client.IsValidClient(ne))
                        _bankContext.Add(ne);
                    else
                        MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

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

            if (selectedClient is LegalEntity entity)
                frm.Owner = entity.Owner;

            frm.DisableCmbType();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (Client.IsValidClient(new NaturalEntity() { 
                    Name        = frm.ClientName,
                    PhoneNumber = frm.PhoneNumber, 
                    Email       = frm.Email
                }))
                {
                    selectedClient.Name        = frm.ClientName;
                    selectedClient.PhoneNumber = frm.PhoneNumber;
                    selectedClient.Email       = frm.Email;
                
                    if (selectedClient is LegalEntity lEntity)
                        lEntity.Owner = frm.Owner;
                
                    _bankContext.SaveChanges();

                    RefreshDgvClients();
                }
                else
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClientDelete_Click(object sender, EventArgs e)
        {
            _bankContext.Remove((Client)dgvClients.SelectedRows[0].DataBoundItem);
            _bankContext.SaveChanges();

            RefreshAllDgvs();
        }

        private void btnBAccountAdd_Click(object sender, EventArgs e)
        {
            frmBankAccountDetails frm = new();
            frm.Text = "Add a bank account";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                BankAccount b = new();
                try
                {
                    b.Owner       = _bankContext.Clients.Single(c => c.ID == frm.Owner.ID);
                    b.DateCreated = frm.DateCreated;
                }
                catch (Exception)
                {
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (BankAccount.IsValidBankAccount(b))
                {
                    _bankContext.Add(b);
                    _bankContext.SaveChanges();

                    RefreshDgvBAccounts();
                }
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
                BankAccount b = new();
                try
                {
                    b.Owner       = _bankContext.Clients.Local.Single(c => c.ID == frm.Owner.ID);
                    b.DateCreated = frm.DateCreated;
                }
                catch (Exception)
                {
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (BankAccount.IsValidBankAccount(b))
                {
                    selectedBankAccount.Owner       = b.Owner;
                    selectedBankAccount.DateCreated = b.DateCreated;

                    _bankContext.SaveChanges();

                    RefreshDgvBAccounts();
                }
                else
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBAccountDelete_Click(object sender, EventArgs e)
        {
            _bankContext.Remove((BankAccount)dgvBAccounts.SelectedRows[0].DataBoundItem);
            _bankContext.SaveChanges();

            RefreshAllDgvs();
        }

        private void btnFAccountAdd_Click(object sender, EventArgs e)
        {
            frmFiscalAccountDetails frm = new();
            frm.Text = "Add a fiscal account";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                FiscalAccount f = new();

                try
                {
                    f.Number      = frm.AccountNumber;
                    f.Balance     = frm.Balance;
                    f.Currency    = _bankContext.Currencies.Single(c => c.ID == frm.Currency.ID);
                    f.BankAccount = _bankContext.BankAccounts.Single(b => b.ID == frm.BankAccount.ID);
                }
                catch (Exception)
                {
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (FiscalAccount.IsValidFiscalAccount(f))
                {
                    _bankContext.Add(f);
                    _bankContext.SaveChanges();

                    RefreshDgvFAccounts();
                }
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
                FiscalAccount f = new();
                try
                {
                    f.Number      = frm.AccountNumber;
                    f.Balance     = frm.Balance;
                    f.Currency    = _bankContext.Currencies.Local.Single(c => c.ID == frm.Currency.ID);
                    f.BankAccount = _bankContext.BankAccounts.Local.Single(b => b.ID == frm.BankAccount.ID);
                }
                catch (Exception)
                {
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (FiscalAccount.IsValidFiscalAccount(f))
                {
                    selectedFiscalAccount.Number      = f.Number;
                    selectedFiscalAccount.Currency    = f.Currency;
                    selectedFiscalAccount.Balance     = f.Balance;
                    selectedFiscalAccount.BankAccount = f.BankAccount;

                    _bankContext.SaveChanges();
                
                    RefreshDgvFAccounts();
                }
                else
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFAccountDelete_Click(object sender, EventArgs e)
        {
            _bankContext.Remove((FiscalAccount)dgvFAccounts.SelectedRows[0].DataBoundItem);
            _bankContext.SaveChanges();

            RefreshAllDgvs();
        }

        private void btnExchangeRateAdd_Click(object sender, EventArgs e)
        {
            frmExchangeRateDetails frm = new();
            frm.Text = "Add a new exchange rate";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                ExchangeRate er = new();
                try
                {
                    er.FromCurrency   = _bankContext.Currencies.Single(c => c.ID == frm.FromCurrency.ID);
                    er.ToCurrency     = _bankContext.Currencies.Single(c => c.ID == frm.ToCurrency.ID);
                    er.FromCurrencyID = frm.FromCurrency.ID;
                    er.ToCurrencyID   = frm.ToCurrency.ID;
                    er.Rate           = frm.Rate;
                }
                catch (Exception)
                {
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (ExchangeRate.IsValidExchangeRate(er))
                {
                    _bankContext.Add(er);
                    _bankContext.SaveChanges();

                    RefreshDgvExchangeRates();
                }
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
                ExchangeRate er = new();
                try
                {
                    er.FromCurrency   = _bankContext.Currencies.Single(c => c.ID == frm.FromCurrency.ID);
                    er.ToCurrency     = _bankContext.Currencies.Single(c => c.ID == frm.ToCurrency.ID);
                    er.FromCurrencyID = frm.FromCurrency.ID;
                    er.ToCurrencyID   = frm.ToCurrency.ID;
                    er.Rate           = frm.Rate;
                }
                catch (Exception)
                {
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (ExchangeRate.IsValidExchangeRate(er))
                {
                    selectedExchange.Rate = frm.Rate;
                    _bankContext.SaveChanges();

                    RefreshDgvExchangeRates();
                }
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
                Transaction t = new();
                try
                {
                    t.FromAccount = _bankContext.FiscalAccounts.Single(a => a.ID == frm.FromAccount.ID);
                    t.ToAccount   = _bankContext.FiscalAccounts.Single(a => a.ID == frm.ToAccount.ID);
                    t.Amount      = frm.Amount;
                    t.Timestamp   = frm.Timestamp;
                }
                catch (Exception)
                {
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Transaction.IsValidTransaction(t))
                {
                    _bankContext.Add(t);
                    _bankContext.SaveChanges();

                    ExecuteTransaction(t);

                    RefreshDgvTransactions();
                }
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