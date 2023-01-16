using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Forms
{
    public partial class frmBankAccountDetails : Form
    {
        private readonly BankContext _bankContext = new();
        public new Client Owner { get { return (Client)cmbOwner.SelectedItem; } set { cmbOwner.SelectedItem = value; } }
        public DateTime DateCreated { get { return dtpCreated.Value; } set { dtpCreated.Value = value; } }
        public frmBankAccountDetails()
        {
            InitializeComponent();

            _bankContext.Clients.Load();
            cmbOwner.DataSource = _bankContext.Clients.Local.ToObservableCollection();
        }
    }
}