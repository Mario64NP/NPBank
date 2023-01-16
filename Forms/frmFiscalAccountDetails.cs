using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Forms
{
    public partial class frmFiscalAccountDetails : Form
    {
        private readonly BankContext _bankContext = new();

        public string AccountNumber { get { return txtAccountNumber.Text; } set { txtAccountNumber.Text = value; } }
        public Currency Currency { get { return (Currency)cmbCurrency.SelectedItem; } set { cmbCurrency.SelectedItem = value; } }
        public double Balance { get { return double.Parse(txtBalance.Text); } set { txtBalance.Text = value.ToString(); } }
        public BankAccount BankAccount { get { return (BankAccount)cmbBAccount.SelectedItem; } set { cmbBAccount.SelectedItem = value; } }

        public frmFiscalAccountDetails()
        {
            InitializeComponent();

            _bankContext.Clients.Load();
            _bankContext.Currencies.Load();
            _bankContext.BankAccounts.Load();
            cmbCurrency.DataSource = _bankContext.Currencies.Local.ToObservableCollection();
            cmbBAccount.DataSource = _bankContext.BankAccounts.Local.ToObservableCollection();
        }
    }
}