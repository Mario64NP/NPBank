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
    public partial class frmBankAccountDetails : Form
    {
        private readonly BankContext _bankContext = new BankContext();
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
