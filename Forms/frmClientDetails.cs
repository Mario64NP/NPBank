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
    public partial class frmClientDetails : Form
    {
        public string ClientName { get { return txtName.Text; } set { txtName.Text = value; } }
        public string PhoneNumber { get { return txtPhone.Text; } set { txtPhone.Text = value; } }
        public string Email { get { return txtEmail.Text; } set { txtEmail.Text = value; } }
        public new string Owner { get { return txtOwner.Text; } set { txtOwner.Text = value; } }
        public bool LegalEntity { get { return cmbType.SelectedIndex == 1; } set { cmbType.SelectedIndex = value ? 1 : 0; } }
        public frmClientDetails()
        {
            InitializeComponent();

            lblOwner.Visible = false;
            txtOwner.Visible = false;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex == 0)
            {
                lblOwner.Visible = false;
                txtOwner.Visible = false;
            }
            else
            {
                lblOwner.Visible = true;
                txtOwner.Visible = true;
            }
        }
    }
}