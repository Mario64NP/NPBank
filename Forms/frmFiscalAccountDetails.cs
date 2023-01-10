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
    public partial class frmFiscalAccountDetails : Form
    {
        private readonly BankContext _bankContext = new BankContext();

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
