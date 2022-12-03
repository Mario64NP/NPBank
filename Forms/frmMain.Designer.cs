namespace Forms
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpClient = new System.Windows.Forms.TabPage();
            this.dgvClients = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tpBAccount = new System.Windows.Forms.TabPage();
            this.dgvBAccounts = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tpFAccount = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.dgvFAccounts = new System.Windows.Forms.DataGridView();
            this.tpExchange = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.dgvExchangeRates = new System.Windows.Forms.DataGridView();
            this.tpTransaction = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.tabControl.SuspendLayout();
            this.tpClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).BeginInit();
            this.panel1.SuspendLayout();
            this.tpBAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBAccounts)).BeginInit();
            this.panel2.SuspendLayout();
            this.tpFAccount.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFAccounts)).BeginInit();
            this.tpExchange.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExchangeRates)).BeginInit();
            this.tpTransaction.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpClient);
            this.tabControl.Controls.Add(this.tpBAccount);
            this.tabControl.Controls.Add(this.tpFAccount);
            this.tabControl.Controls.Add(this.tpExchange);
            this.tabControl.Controls.Add(this.tpTransaction);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 450);
            this.tabControl.TabIndex = 0;
            // 
            // tpClient
            // 
            this.tpClient.Controls.Add(this.dgvClients);
            this.tpClient.Controls.Add(this.panel1);
            this.tpClient.Location = new System.Drawing.Point(4, 25);
            this.tpClient.Name = "tpClient";
            this.tpClient.Padding = new System.Windows.Forms.Padding(3);
            this.tpClient.Size = new System.Drawing.Size(792, 421);
            this.tpClient.TabIndex = 0;
            this.tpClient.Text = "Clients";
            this.tpClient.UseVisualStyleBackColor = true;
            // 
            // dgvClients
            // 
            this.dgvClients.AllowUserToAddRows = false;
            this.dgvClients.AllowUserToDeleteRows = false;
            this.dgvClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClients.Location = new System.Drawing.Point(3, 3);
            this.dgvClients.Name = "dgvClients";
            this.dgvClients.ReadOnly = true;
            this.dgvClients.RowTemplate.Height = 25;
            this.dgvClients.Size = new System.Drawing.Size(786, 380);
            this.dgvClients.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 383);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 35);
            this.panel1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(463, 7);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(544, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(625, 7);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(706, 7);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // tpBAccount
            // 
            this.tpBAccount.Controls.Add(this.dgvBAccounts);
            this.tpBAccount.Controls.Add(this.panel2);
            this.tpBAccount.Location = new System.Drawing.Point(4, 25);
            this.tpBAccount.Name = "tpBAccount";
            this.tpBAccount.Padding = new System.Windows.Forms.Padding(3);
            this.tpBAccount.Size = new System.Drawing.Size(792, 421);
            this.tpBAccount.TabIndex = 1;
            this.tpBAccount.Text = "Bank Accounts";
            this.tpBAccount.UseVisualStyleBackColor = true;
            // 
            // dgvBAccounts
            // 
            this.dgvBAccounts.AllowUserToAddRows = false;
            this.dgvBAccounts.AllowUserToDeleteRows = false;
            this.dgvBAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBAccounts.Location = new System.Drawing.Point(3, 3);
            this.dgvBAccounts.Name = "dgvBAccounts";
            this.dgvBAccounts.ReadOnly = true;
            this.dgvBAccounts.RowTemplate.Height = 25;
            this.dgvBAccounts.Size = new System.Drawing.Size(786, 380);
            this.dgvBAccounts.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 383);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(786, 35);
            this.panel2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(463, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(544, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Search";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(625, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Edit";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(706, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "Delete";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // tpFAccount
            // 
            this.tpFAccount.Controls.Add(this.dgvFAccounts);
            this.tpFAccount.Controls.Add(this.panel3);
            this.tpFAccount.Location = new System.Drawing.Point(4, 25);
            this.tpFAccount.Name = "tpFAccount";
            this.tpFAccount.Padding = new System.Windows.Forms.Padding(3);
            this.tpFAccount.Size = new System.Drawing.Size(792, 421);
            this.tpFAccount.TabIndex = 2;
            this.tpFAccount.Text = "Fiscal Accounts";
            this.tpFAccount.UseVisualStyleBackColor = true;
            // 
            // dgvFAccounts
            // 
            this.dgvFAccounts.AllowUserToAddRows = false;
            this.dgvFAccounts.AllowUserToDeleteRows = false;
            this.dgvFAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFAccounts.Location = new System.Drawing.Point(3, 3);
            this.dgvFAccounts.Name = "dgvFAccounts";
            this.dgvFAccounts.ReadOnly = true;
            this.dgvFAccounts.RowTemplate.Height = 25;
            this.dgvFAccounts.Size = new System.Drawing.Size(786, 380);
            this.dgvFAccounts.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.button6);
            this.panel3.Controls.Add(this.button7);
            this.panel3.Controls.Add(this.button8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 383);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(786, 35);
            this.panel3.TabIndex = 1;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(463, 7);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 3;
            this.button5.Text = "Add";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(544, 7);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 2;
            this.button6.Text = "Search";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(625, 7);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 1;
            this.button7.Text = "Edit";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(706, 7);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 0;
            this.button8.Text = "Delete";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // tpExchange
            // 
            this.tpExchange.Controls.Add(this.dgvExchangeRates);
            this.tpExchange.Controls.Add(this.panel4);
            this.tpExchange.Location = new System.Drawing.Point(4, 25);
            this.tpExchange.Name = "tpExchange";
            this.tpExchange.Padding = new System.Windows.Forms.Padding(3);
            this.tpExchange.Size = new System.Drawing.Size(792, 421);
            this.tpExchange.TabIndex = 3;
            this.tpExchange.Text = "Exchange Rates";
            this.tpExchange.UseVisualStyleBackColor = true;
            // 
            // dgvExchangeRates
            // 
            this.dgvExchangeRates.AllowUserToAddRows = false;
            this.dgvExchangeRates.AllowUserToDeleteRows = false;
            this.dgvExchangeRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExchangeRates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExchangeRates.Location = new System.Drawing.Point(3, 3);
            this.dgvExchangeRates.Name = "dgvExchangeRates";
            this.dgvExchangeRates.ReadOnly = true;
            this.dgvExchangeRates.RowTemplate.Height = 25;
            this.dgvExchangeRates.Size = new System.Drawing.Size(786, 380);
            this.dgvExchangeRates.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button9);
            this.panel4.Controls.Add(this.button10);
            this.panel4.Controls.Add(this.button11);
            this.panel4.Controls.Add(this.button12);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 383);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(786, 35);
            this.panel4.TabIndex = 1;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(463, 7);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 3;
            this.button9.Text = "Add";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(544, 7);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 2;
            this.button10.Text = "Search";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(625, 7);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 1;
            this.button11.Text = "Edit";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(706, 7);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 23);
            this.button12.TabIndex = 0;
            this.button12.Text = "Delete";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // tpTransaction
            // 
            this.tpTransaction.Controls.Add(this.dgvTransactions);
            this.tpTransaction.Controls.Add(this.panel5);
            this.tpTransaction.Location = new System.Drawing.Point(4, 25);
            this.tpTransaction.Name = "tpTransaction";
            this.tpTransaction.Padding = new System.Windows.Forms.Padding(3);
            this.tpTransaction.Size = new System.Drawing.Size(792, 421);
            this.tpTransaction.TabIndex = 4;
            this.tpTransaction.Text = "Transactions";
            this.tpTransaction.UseVisualStyleBackColor = true;
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransactions.Location = new System.Drawing.Point(3, 3);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.RowTemplate.Height = 25;
            this.dgvTransactions.Size = new System.Drawing.Size(786, 380);
            this.dgvTransactions.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button13);
            this.panel5.Controls.Add(this.button14);
            this.panel5.Controls.Add(this.button15);
            this.panel5.Controls.Add(this.button16);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(3, 383);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(786, 35);
            this.panel5.TabIndex = 1;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(463, 7);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 3;
            this.button13.Text = "Add";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(544, 7);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 2;
            this.button14.Text = "Search";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(625, 7);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 23);
            this.button15.TabIndex = 1;
            this.button15.Text = "Edit";
            this.button15.UseVisualStyleBackColor = true;
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(706, 7);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(75, 23);
            this.button16.TabIndex = 0;
            this.button16.Text = "Delete";
            this.button16.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.tabControl.ResumeLayout(false);
            this.tpClient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tpBAccount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBAccounts)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tpFAccount.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFAccounts)).EndInit();
            this.tpExchange.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExchangeRates)).EndInit();
            this.tpTransaction.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl;
        private TabPage tpClient;
        private TabPage tpBAccount;
        private TabPage tpFAccount;
        private TabPage tpExchange;
        private TabPage tpTransaction;
        private DataGridView dgvClients;
        private DataGridView dgvBAccounts;
        private DataGridView dgvFAccounts;
        private DataGridView dgvExchangeRates;
        private DataGridView dgvTransactions;
        private Panel panel1;
        private Button btnAdd;
        private Button btnSearch;
        private Button btnEdit;
        private Button btnDelete;
        private Panel panel2;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Panel panel3;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Panel panel4;
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button12;
        private Panel panel5;
        private Button button13;
        private Button button14;
        private Button button15;
        private Button button16;
    }
}