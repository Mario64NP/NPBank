using Controller;
using Model;

namespace Forms
{
    public partial class frmMain : Form
    {
        private readonly Coordinator _coordinator = new();

        public frmMain()
        {
            InitializeComponent();

            RefreshAllDgvs();
        }

        /// <summary>
        /// Re-links the clients data grid view with the data source and formats the columns.
        /// </summary>
        private void RefreshDgvClients()
        {
            dgvClients.DataSource = null;
            dgvClients.DataSource = _coordinator.BankContext.Clients.Local.ToObservableCollection();

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
            dgvBAccounts.DataSource = _coordinator.BankContext.BankAccounts.Local.ToObservableCollection();

            dgvBAccounts.Columns[0].Width = 30;
        }

        /// <summary>
        /// Re-links the fiscal accounts data grid view with the data source and formats the columns.
        /// </summary>
        private void RefreshDgvFAccounts()
        {
            dgvFAccounts.DataSource = null;
            dgvFAccounts.DataSource = _coordinator.BankContext.FiscalAccounts.Local.ToObservableCollection();

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
            dgvExchangeRates.DataSource = _coordinator.BankContext.ExchangeRates.Local.ToObservableCollection();

            dgvExchangeRates.Columns["FromCurrencyID"].Visible = false;
            dgvExchangeRates.Columns["ToCurrencyID"].Visible   = false;
        }

        /// <summary>
        /// Re-links the transactions data grid view with the data source and formats the columns.
        /// </summary>
        private void RefreshDgvTransactions()
        {
            dgvTransactions.DataSource = null;
            dgvTransactions.DataSource = _coordinator.BankContext.Transactions.Local.ToObservableCollection();

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
                        _coordinator.BankContext.Add(le);
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
                        _coordinator.BankContext.Add(ne);
                    else
                        MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                _coordinator.BankContext.SaveChanges();

                RefreshDgvClients();
            }
        }

        private void btnClientSearch_Click(object sender, EventArgs e)
        {
            frmClientDetails frm = new();
            frm.Text = "Search clients";

            if (frm.ShowDialog() == DialogResult.OK)
                dgvClients.DataSource = _coordinator.BankContext.Clients.Local.Where(x => 
                x.Name == (string.IsNullOrEmpty(frm.ClientName) ? x.Name : frm.ClientName) && 
                x.PhoneNumber == (string.IsNullOrEmpty(frm.PhoneNumber) ? x.PhoneNumber : frm.PhoneNumber) &&
                x.Email == (string.IsNullOrEmpty(frm.Email) ? x.Email : frm.Email)).ToList();
        }

        private void btnClientEdit_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count == 0)
                return;

            Client selectedClient = (Client)dgvClients.SelectedRows[0].DataBoundItem;
            frmClientDetails frm = new()
            {
                Text = "Edit a client",
                ClientName  = selectedClient.Name,
                PhoneNumber = selectedClient.PhoneNumber,
                Email       = selectedClient.Email,
                LegalEntity = selectedClient is LegalEntity
            };

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
                
                    _coordinator.BankContext.SaveChanges();

                    RefreshDgvClients();
                }
                else
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClientDelete_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count == 0)
                return;

            _coordinator.BankContext.Remove((Client)dgvClients.SelectedRows[0].DataBoundItem);
            _coordinator.BankContext.SaveChanges();

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
                    b.Owner       = _coordinator.BankContext.Clients.Single(c => c.Equals(frm.Owner));
                    b.DateCreated = frm.DateCreated;
                }
                catch (Exception)
                {
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (BankAccount.IsValidBankAccount(b))
                {
                    _coordinator.BankContext.Add(b);
                    _coordinator.BankContext.SaveChanges();

                    RefreshDgvBAccounts();
                }
            }
        }

        private void btnBAccountSearch_Click(object sender, EventArgs e)
        {
            frmBankAccountDetails frm = new();
            frm.Text = "Search bank accounts";

            if (frm.ShowDialog() == DialogResult.OK)
                dgvBAccounts.DataSource = _coordinator.BankContext.BankAccounts.Local.Where(b => b.Owner.Equals(frm.Owner)).ToList();
            else
                dgvBAccounts.DataSource = _coordinator.BankContext.BankAccounts.Local.ToObservableCollection();
        }

        private void btnBAccountEdit_Click(object sender, EventArgs e)
        {
            if (dgvBAccounts.SelectedRows.Count == 0)
                return;

            BankAccount selectedBankAccount = (BankAccount)dgvBAccounts.SelectedRows[0].DataBoundItem;
            frmBankAccountDetails frm = new()
            {
                Text        = "Edit a bank account",
                Owner       = selectedBankAccount.Owner,
                DateCreated = selectedBankAccount.DateCreated
            };

            if (frm.ShowDialog() == DialogResult.OK)
            {
                BankAccount b = new();
                try
                {
                    b.Owner       = _coordinator.BankContext.Clients.Local.Single(c => c.Equals(frm.Owner));
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

                    _coordinator.BankContext.SaveChanges();

                    RefreshDgvBAccounts();
                }
                else
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBAccountDelete_Click(object sender, EventArgs e)
        {
            if (dgvBAccounts.SelectedRows.Count == 0)
                return;

            _coordinator.BankContext.Remove((BankAccount)dgvBAccounts.SelectedRows[0].DataBoundItem);
            _coordinator.BankContext.SaveChanges();

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
                    f.Currency    = _coordinator.BankContext.Currencies.Single(c => c.Equals(frm.Currency));
                    f.BankAccount = _coordinator.BankContext.BankAccounts.Single(b => b.Equals(frm.BankAccount));
                }
                catch (Exception)
                {
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (FiscalAccount.IsValidFiscalAccount(f))
                {
                    _coordinator.BankContext.Add(f);
                    _coordinator.BankContext.SaveChanges();

                    RefreshDgvFAccounts();
                }
            }
        }

        private void btnFAccountSearch_Click(object sender, EventArgs e)
        {
            frmFiscalAccountDetails frm = new();
            frm.Text = "Search fiscal accounts";

            if (frm.ShowDialog() == DialogResult.OK)
                dgvFAccounts.DataSource = _coordinator.BankContext.FiscalAccounts.Local.Where(f => 
                f.Number == (string.IsNullOrEmpty(frm.AccountNumber) ? f.Number : frm.AccountNumber) &&
                f.BankAccount.Equals(frm.BankAccount)).ToList();
            else
                dgvFAccounts.DataSource = _coordinator.BankContext.FiscalAccounts.Local.ToObservableCollection();
        }

        private void btnFAccountEdit_Click(object sender, EventArgs e)
        {
            if (dgvFAccounts.SelectedRows.Count == 0)
                return;

            FiscalAccount selectedFiscalAccount = (FiscalAccount)dgvFAccounts.SelectedRows[0].DataBoundItem;
            frmFiscalAccountDetails frm = new()
            {
                Text          = "Edit a fiscal account",
                AccountNumber = selectedFiscalAccount.Number,
                Currency      = selectedFiscalAccount.Currency,
                Balance       = selectedFiscalAccount.Balance,
                BankAccount   = selectedFiscalAccount.BankAccount
            };

            if (frm.ShowDialog() == DialogResult.OK)
            {
                FiscalAccount f = new();
                try
                {
                    f.Number      = frm.AccountNumber;
                    f.Balance     = frm.Balance;
                    f.Currency    = _coordinator.BankContext.Currencies.Local.Single(c => c.Equals(frm.Currency));
                    f.BankAccount = _coordinator.BankContext.BankAccounts.Local.Single(b => b.Equals(frm.BankAccount));
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

                    _coordinator.BankContext.SaveChanges();
                
                    RefreshDgvFAccounts();
                }
                else
                    MessageBox.Show("The details you've entered aren't valid.", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFAccountDelete_Click(object sender, EventArgs e)
        {
            if (dgvFAccounts.SelectedRows.Count == 0)
                return;

            _coordinator.BankContext.Remove((FiscalAccount)dgvFAccounts.SelectedRows[0].DataBoundItem);
            _coordinator.BankContext.SaveChanges();

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
                    er.FromCurrency   = _coordinator.BankContext.Currencies.Single(c => c.Equals(frm.FromCurrency));
                    er.ToCurrency     = _coordinator.BankContext.Currencies.Single(c => c.Equals(frm.ToCurrency));
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
                    _coordinator.BankContext.Add(er);
                    _coordinator.BankContext.SaveChanges();

                    RefreshDgvExchangeRates();
                }
            }
        }

        private void btnExchangeRateSearch_Click(object sender, EventArgs e)
        {
            frmExchangeRateDetails frm = new();
            frm.Text = "Search exchange rates";

            if (frm.ShowDialog() == DialogResult.OK)
                dgvExchangeRates.DataSource = _coordinator.BankContext.ExchangeRates.Local.Where(r =>
                r.FromCurrencyID == frm.FromCurrency.ID &&
                r.ToCurrencyID   == frm.ToCurrency.ID).ToList();
            else
                dgvExchangeRates.DataSource = _coordinator.BankContext.ExchangeRates.Local.ToObservableCollection();
        }

        private void btnExchangeRateEdit_Click(object sender, EventArgs e)
        {
            if (dgvExchangeRates.SelectedRows.Count == 0)
                return;

            ExchangeRate selectedExchange = (ExchangeRate)dgvExchangeRates.SelectedRows[0].DataBoundItem;
            frmExchangeRateDetails frm = new()
            {
                Text         = "Edit an exchange rate",
                FromCurrency = selectedExchange.FromCurrency,
                ToCurrency   = selectedExchange.ToCurrency,
                Rate         = selectedExchange.Rate
            };

            frm.DisableCurrencyChanging();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                ExchangeRate er = new();
                try
                {
                    er.FromCurrency   = _coordinator.BankContext.Currencies.Single(c => c.Equals(frm.FromCurrency));
                    er.ToCurrency     = _coordinator.BankContext.Currencies.Single(c => c.Equals(frm.ToCurrency));
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
                    _coordinator.BankContext.SaveChanges();

                    RefreshDgvExchangeRates();
                }
            }
        }

        private void btnExchangeRateDelete_Click(object sender, EventArgs e)
        {
            if (dgvExchangeRates.SelectedRows.Count == 0)
                return;

            _coordinator.BankContext.Remove((ExchangeRate)dgvExchangeRates.SelectedRows[0].DataBoundItem);
            _coordinator.BankContext.SaveChanges();

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
                    t.FromAccount = _coordinator.BankContext.FiscalAccounts.Single(a => a.Equals(frm.FromAccount));
                    t.ToAccount   = _coordinator.BankContext.FiscalAccounts.Single(a => a.Equals(frm.ToAccount));
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
                    _coordinator.BankContext.Add(t);
                    _coordinator.BankContext.SaveChanges();

                    _coordinator.ExecuteTransaction(t);

                    RefreshDgvTransactions();
                }
            }
        }

        private void btnTransactionSearch_Click(object sender, EventArgs e)
        {
            frmTransactionDetails frm = new();
            frm.Text = "Search transactions";

            if (frm.ShowDialog() == DialogResult.OK)
                dgvTransactions.DataSource = _coordinator.BankContext.Transactions.Local.Where(t =>
                t.FromAccount.Equals(frm.FromAccount) &&
                t.ToAccount.Equals(frm.ToAccount)).ToList();
            else
                dgvTransactions.DataSource = _coordinator.BankContext.Transactions.Local.ToObservableCollection();
        }

        private void btnTransactionEdit_Click(object sender, EventArgs e)
        {
            if (dgvTransactions.SelectedRows.Count == 0)
                return;

            Transaction selectedTransaction = (Transaction)dgvTransactions.SelectedRows[0].DataBoundItem;
            frmTransactionDetails frm = new()
            {
                Text        = "Edit a transaction",
                FromAccount = selectedTransaction.FromAccount,
                ToAccount   = selectedTransaction.ToAccount,
                Amount      = selectedTransaction.Amount,
                Timestamp   = selectedTransaction.Timestamp
            };

            if (frm.ShowDialog() == DialogResult.OK)
            {
                selectedTransaction.FromAccount = _coordinator.BankContext.FiscalAccounts.Local.Single(f => f.Equals(frm.FromAccount));
                selectedTransaction.ToAccount   = _coordinator.BankContext.FiscalAccounts.Local.Single(f => f.Equals(frm.ToAccount));
                selectedTransaction.Amount      = frm.Amount;
                selectedTransaction.Timestamp   = frm.Timestamp;
                _coordinator.BankContext.SaveChanges();

                RefreshDgvTransactions();
            }
        }

        private void btnTransactionDelete_Click(object sender, EventArgs e)
        {
            if (dgvTransactions.SelectedRows.Count == 0)
                return;

            _coordinator.BankContext.Remove((Transaction)dgvTransactions.SelectedRows[0].DataBoundItem);
            _coordinator.BankContext.SaveChanges();

            RefreshDgvTransactions();
        }
    }
}