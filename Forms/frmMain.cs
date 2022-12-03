using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Forms
{
    public partial class frmMain : Form
    {
        private BankContext bankContext = new BankContext();

        public frmMain()
        {
            InitializeComponent();

            bankContext.Clients.Load();
            bankContext.BankAccounts.Load();
            bankContext.ExchangeRates.Load();
            bankContext.Transactions.Load();

            dgvClients.DataSource = bankContext.Clients.Local.ToObservableCollection();
            dgvBAccounts.DataSource = bankContext.BankAccounts.Local.ToObservableCollection();
            dgvFAccounts.DataSource = null;
            dgvExchangeRates.DataSource = bankContext.ExchangeRates.Local.ToObservableCollection();
            dgvTransactions.DataSource = bankContext.Transactions.Local.ToObservableCollection();
        }
    }
}