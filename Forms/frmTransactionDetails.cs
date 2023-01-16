using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Forms
{
    public partial class frmTransactionDetails : Form
    {
        private readonly BankContext _bankContext = new();

        public FiscalAccount FromAccount { get { return (FiscalAccount)cmbAccountFrom.SelectedItem; } set { cmbAccountFrom.SelectedItem = value; } }
        public FiscalAccount ToAccount { get { return (FiscalAccount)cmbAccountTo.SelectedItem; } set { cmbAccountTo.SelectedItem = value; } }
        public double Amount { get { return double.Parse(txtAmount.Text); } set { txtAmount.Text = value.ToString(); } }
        public DateTime Timestamp { get { return dtpCreated.Value; } set { dtpCreated.Value = value; } }
        public frmTransactionDetails()
        {
            InitializeComponent();

            _bankContext.FiscalAccounts.Load();
            cmbAccountFrom.DataSource = _bankContext.FiscalAccounts.Local.ToObservableCollection();
        }

        private void cmbAccountFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAccountTo.DataSource = OtherAccounts((FiscalAccount)cmbAccountFrom.SelectedItem);
        }

        /// <summary>
        /// Provides a list of all fiscal accounts except the one specified
        /// </summary>
        /// <param name="accountToRemove">The fiscal account you want to exclude</param>
        /// <returns>A list of fiscal accounts excluding the one sepcified</returns>
        private List<FiscalAccount> OtherAccounts(FiscalAccount accountToRemove)
        {
            List<FiscalAccount> accounts = _bankContext.FiscalAccounts.Local.ToList();
            accounts.Remove(accountToRemove); 
            return accounts;
        }
    }
}