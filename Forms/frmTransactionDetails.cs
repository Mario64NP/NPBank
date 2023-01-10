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
    public partial class frmTransactionDetails : Form
    {
        private readonly BankContext _bankContext = new BankContext();

        public FiscalAccount FromAccount { get { return (FiscalAccount)cmbAccountFrom.SelectedItem; } set { cmbAccountFrom.SelectedItem = value; } }
        public FiscalAccount ToAccount { get { return (FiscalAccount)cmbAccountTo.SelectedItem; } set { cmbAccountTo.SelectedItem = value; } }
        public double Amount { get { return double.Parse(txtAmount.Text); } set { txtAmount.Text = value.ToString(); } }
        public DateTime Timestamp { get { return dtpCreated.Value; } set { dtpCreated.Value = value; } }
        public frmTransactionDetails()
        {
            InitializeComponent();

            _bankContext.FiscalAccounts.Load();
            cmbAccountFrom.DataSource = _bankContext.FiscalAccounts.Local.ToObservableCollection();
            cmbAccountTo.DataSource = _bankContext.FiscalAccounts.Local.ToObservableCollection();
        }
    }
}
