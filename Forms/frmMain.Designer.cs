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
            this.btnClientAdd = new System.Windows.Forms.Button();
            this.btnClientSearch = new System.Windows.Forms.Button();
            this.btnClientEdit = new System.Windows.Forms.Button();
            this.btnClientDelete = new System.Windows.Forms.Button();
            this.tpBAccount = new System.Windows.Forms.TabPage();
            this.dgvBAccounts = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBAccountAdd = new System.Windows.Forms.Button();
            this.btnBAccountSearch = new System.Windows.Forms.Button();
            this.btnBAccountEdit = new System.Windows.Forms.Button();
            this.btnBAccountDelete = new System.Windows.Forms.Button();
            this.tpFAccount = new System.Windows.Forms.TabPage();
            this.dgvFAccounts = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnFAccountAdd = new System.Windows.Forms.Button();
            this.btnFAccountSearch = new System.Windows.Forms.Button();
            this.btnFAccountEdit = new System.Windows.Forms.Button();
            this.btnFAccountDelete = new System.Windows.Forms.Button();
            this.tpExchange = new System.Windows.Forms.TabPage();
            this.dgvExchangeRates = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnExchangeRateAdd = new System.Windows.Forms.Button();
            this.btnExchangeRateSearch = new System.Windows.Forms.Button();
            this.btnExchangeRateEdit = new System.Windows.Forms.Button();
            this.btnExchangeRateDelete = new System.Windows.Forms.Button();
            this.tpTransaction = new System.Windows.Forms.TabPage();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnTransactionAdd = new System.Windows.Forms.Button();
            this.btnTransactionSearch = new System.Windows.Forms.Button();
            this.btnTransactionEdit = new System.Windows.Forms.Button();
            this.btnTransactionDelete = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tpClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).BeginInit();
            this.panel1.SuspendLayout();
            this.tpBAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBAccounts)).BeginInit();
            this.panel2.SuspendLayout();
            this.tpFAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFAccounts)).BeginInit();
            this.panel3.SuspendLayout();
            this.tpExchange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExchangeRates)).BeginInit();
            this.panel4.SuspendLayout();
            this.tpTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.panel5.SuspendLayout();
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
            this.panel1.Controls.Add(this.btnClientAdd);
            this.panel1.Controls.Add(this.btnClientSearch);
            this.panel1.Controls.Add(this.btnClientEdit);
            this.panel1.Controls.Add(this.btnClientDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 383);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 35);
            this.panel1.TabIndex = 0;
            // 
            // btnClientAdd
            // 
            this.btnClientAdd.Location = new System.Drawing.Point(463, 7);
            this.btnClientAdd.Name = "btnClientAdd";
            this.btnClientAdd.Size = new System.Drawing.Size(75, 23);
            this.btnClientAdd.TabIndex = 3;
            this.btnClientAdd.Text = "Add";
            this.btnClientAdd.UseVisualStyleBackColor = true;
            this.btnClientAdd.Click += new System.EventHandler(this.btnClientAdd_Click);
            // 
            // btnClientSearch
            // 
            this.btnClientSearch.Location = new System.Drawing.Point(544, 7);
            this.btnClientSearch.Name = "btnClientSearch";
            this.btnClientSearch.Size = new System.Drawing.Size(75, 23);
            this.btnClientSearch.TabIndex = 2;
            this.btnClientSearch.Text = "Search";
            this.btnClientSearch.UseVisualStyleBackColor = true;
            this.btnClientSearch.Click += new System.EventHandler(this.btnClientSearch_Click);
            // 
            // btnClientEdit
            // 
            this.btnClientEdit.Location = new System.Drawing.Point(625, 7);
            this.btnClientEdit.Name = "btnClientEdit";
            this.btnClientEdit.Size = new System.Drawing.Size(75, 23);
            this.btnClientEdit.TabIndex = 1;
            this.btnClientEdit.Text = "Edit";
            this.btnClientEdit.UseVisualStyleBackColor = true;
            this.btnClientEdit.Click += new System.EventHandler(this.btnClientEdit_Click);
            // 
            // btnClientDelete
            // 
            this.btnClientDelete.Location = new System.Drawing.Point(706, 7);
            this.btnClientDelete.Name = "btnClientDelete";
            this.btnClientDelete.Size = new System.Drawing.Size(75, 23);
            this.btnClientDelete.TabIndex = 0;
            this.btnClientDelete.Text = "Delete";
            this.btnClientDelete.UseVisualStyleBackColor = true;
            this.btnClientDelete.Click += new System.EventHandler(this.btnClientDelete_Click);
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
            this.panel2.Controls.Add(this.btnBAccountAdd);
            this.panel2.Controls.Add(this.btnBAccountSearch);
            this.panel2.Controls.Add(this.btnBAccountEdit);
            this.panel2.Controls.Add(this.btnBAccountDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 383);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(786, 35);
            this.panel2.TabIndex = 1;
            // 
            // btnBAccountAdd
            // 
            this.btnBAccountAdd.Location = new System.Drawing.Point(463, 7);
            this.btnBAccountAdd.Name = "btnBAccountAdd";
            this.btnBAccountAdd.Size = new System.Drawing.Size(75, 23);
            this.btnBAccountAdd.TabIndex = 3;
            this.btnBAccountAdd.Text = "Add";
            this.btnBAccountAdd.UseVisualStyleBackColor = true;
            this.btnBAccountAdd.Click += new System.EventHandler(this.btnBAccountAdd_Click);
            // 
            // btnBAccountSearch
            // 
            this.btnBAccountSearch.Location = new System.Drawing.Point(544, 7);
            this.btnBAccountSearch.Name = "btnBAccountSearch";
            this.btnBAccountSearch.Size = new System.Drawing.Size(75, 23);
            this.btnBAccountSearch.TabIndex = 2;
            this.btnBAccountSearch.Text = "Search";
            this.btnBAccountSearch.UseVisualStyleBackColor = true;
            this.btnBAccountSearch.Click += new System.EventHandler(this.btnBAccountSearch_Click);
            // 
            // btnBAccountEdit
            // 
            this.btnBAccountEdit.Location = new System.Drawing.Point(625, 7);
            this.btnBAccountEdit.Name = "btnBAccountEdit";
            this.btnBAccountEdit.Size = new System.Drawing.Size(75, 23);
            this.btnBAccountEdit.TabIndex = 1;
            this.btnBAccountEdit.Text = "Edit";
            this.btnBAccountEdit.UseVisualStyleBackColor = true;
            this.btnBAccountEdit.Click += new System.EventHandler(this.btnBAccountEdit_Click);
            // 
            // btnBAccountDelete
            // 
            this.btnBAccountDelete.Location = new System.Drawing.Point(706, 7);
            this.btnBAccountDelete.Name = "btnBAccountDelete";
            this.btnBAccountDelete.Size = new System.Drawing.Size(75, 23);
            this.btnBAccountDelete.TabIndex = 0;
            this.btnBAccountDelete.Text = "Delete";
            this.btnBAccountDelete.UseVisualStyleBackColor = true;
            this.btnBAccountDelete.Click += new System.EventHandler(this.btnBAccountDelete_Click);
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
            this.panel3.Controls.Add(this.btnFAccountAdd);
            this.panel3.Controls.Add(this.btnFAccountSearch);
            this.panel3.Controls.Add(this.btnFAccountEdit);
            this.panel3.Controls.Add(this.btnFAccountDelete);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 383);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(786, 35);
            this.panel3.TabIndex = 1;
            // 
            // btnFAccountAdd
            // 
            this.btnFAccountAdd.Location = new System.Drawing.Point(463, 7);
            this.btnFAccountAdd.Name = "btnFAccountAdd";
            this.btnFAccountAdd.Size = new System.Drawing.Size(75, 23);
            this.btnFAccountAdd.TabIndex = 3;
            this.btnFAccountAdd.Text = "Add";
            this.btnFAccountAdd.UseVisualStyleBackColor = true;
            this.btnFAccountAdd.Click += new System.EventHandler(this.btnFAccountAdd_Click);
            // 
            // btnFAccountSearch
            // 
            this.btnFAccountSearch.Location = new System.Drawing.Point(544, 7);
            this.btnFAccountSearch.Name = "btnFAccountSearch";
            this.btnFAccountSearch.Size = new System.Drawing.Size(75, 23);
            this.btnFAccountSearch.TabIndex = 2;
            this.btnFAccountSearch.Text = "Search";
            this.btnFAccountSearch.UseVisualStyleBackColor = true;
            this.btnFAccountSearch.Click += new System.EventHandler(this.btnFAccountSearch_Click);
            // 
            // btnFAccountEdit
            // 
            this.btnFAccountEdit.Location = new System.Drawing.Point(625, 7);
            this.btnFAccountEdit.Name = "btnFAccountEdit";
            this.btnFAccountEdit.Size = new System.Drawing.Size(75, 23);
            this.btnFAccountEdit.TabIndex = 1;
            this.btnFAccountEdit.Text = "Edit";
            this.btnFAccountEdit.UseVisualStyleBackColor = true;
            this.btnFAccountEdit.Click += new System.EventHandler(this.btnFAccountEdit_Click);
            // 
            // btnFAccountDelete
            // 
            this.btnFAccountDelete.Location = new System.Drawing.Point(706, 7);
            this.btnFAccountDelete.Name = "btnFAccountDelete";
            this.btnFAccountDelete.Size = new System.Drawing.Size(75, 23);
            this.btnFAccountDelete.TabIndex = 0;
            this.btnFAccountDelete.Text = "Delete";
            this.btnFAccountDelete.UseVisualStyleBackColor = true;
            this.btnFAccountDelete.Click += new System.EventHandler(this.btnFAccountDelete_Click);
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
            this.panel4.Controls.Add(this.btnExchangeRateAdd);
            this.panel4.Controls.Add(this.btnExchangeRateSearch);
            this.panel4.Controls.Add(this.btnExchangeRateEdit);
            this.panel4.Controls.Add(this.btnExchangeRateDelete);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 383);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(786, 35);
            this.panel4.TabIndex = 1;
            // 
            // btnExchangeRateAdd
            // 
            this.btnExchangeRateAdd.Location = new System.Drawing.Point(463, 7);
            this.btnExchangeRateAdd.Name = "btnExchangeRateAdd";
            this.btnExchangeRateAdd.Size = new System.Drawing.Size(75, 23);
            this.btnExchangeRateAdd.TabIndex = 3;
            this.btnExchangeRateAdd.Text = "Add";
            this.btnExchangeRateAdd.UseVisualStyleBackColor = true;
            this.btnExchangeRateAdd.Click += new System.EventHandler(this.btnExchangeRateAdd_Click);
            // 
            // btnExchangeRateSearch
            // 
            this.btnExchangeRateSearch.Location = new System.Drawing.Point(544, 7);
            this.btnExchangeRateSearch.Name = "btnExchangeRateSearch";
            this.btnExchangeRateSearch.Size = new System.Drawing.Size(75, 23);
            this.btnExchangeRateSearch.TabIndex = 2;
            this.btnExchangeRateSearch.Text = "Search";
            this.btnExchangeRateSearch.UseVisualStyleBackColor = true;
            this.btnExchangeRateSearch.Click += new System.EventHandler(this.btnExchangeRateSearch_Click);
            // 
            // btnExchangeRateEdit
            // 
            this.btnExchangeRateEdit.Location = new System.Drawing.Point(625, 7);
            this.btnExchangeRateEdit.Name = "btnExchangeRateEdit";
            this.btnExchangeRateEdit.Size = new System.Drawing.Size(75, 23);
            this.btnExchangeRateEdit.TabIndex = 1;
            this.btnExchangeRateEdit.Text = "Edit";
            this.btnExchangeRateEdit.UseVisualStyleBackColor = true;
            this.btnExchangeRateEdit.Click += new System.EventHandler(this.btnExchangeRateEdit_Click);
            // 
            // btnExchangeRateDelete
            // 
            this.btnExchangeRateDelete.Location = new System.Drawing.Point(706, 7);
            this.btnExchangeRateDelete.Name = "btnExchangeRateDelete";
            this.btnExchangeRateDelete.Size = new System.Drawing.Size(75, 23);
            this.btnExchangeRateDelete.TabIndex = 0;
            this.btnExchangeRateDelete.Text = "Delete";
            this.btnExchangeRateDelete.UseVisualStyleBackColor = true;
            this.btnExchangeRateDelete.Click += new System.EventHandler(this.btnExchangeRateDelete_Click);
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
            this.panel5.Controls.Add(this.btnTransactionAdd);
            this.panel5.Controls.Add(this.btnTransactionSearch);
            this.panel5.Controls.Add(this.btnTransactionEdit);
            this.panel5.Controls.Add(this.btnTransactionDelete);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(3, 383);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(786, 35);
            this.panel5.TabIndex = 1;
            // 
            // btnTransactionAdd
            // 
            this.btnTransactionAdd.Location = new System.Drawing.Point(463, 7);
            this.btnTransactionAdd.Name = "btnTransactionAdd";
            this.btnTransactionAdd.Size = new System.Drawing.Size(75, 23);
            this.btnTransactionAdd.TabIndex = 3;
            this.btnTransactionAdd.Text = "Add";
            this.btnTransactionAdd.UseVisualStyleBackColor = true;
            this.btnTransactionAdd.Click += new System.EventHandler(this.btnTransactionAdd_Click);
            // 
            // btnTransactionSearch
            // 
            this.btnTransactionSearch.Location = new System.Drawing.Point(544, 7);
            this.btnTransactionSearch.Name = "btnTransactionSearch";
            this.btnTransactionSearch.Size = new System.Drawing.Size(75, 23);
            this.btnTransactionSearch.TabIndex = 2;
            this.btnTransactionSearch.Text = "Search";
            this.btnTransactionSearch.UseVisualStyleBackColor = true;
            this.btnTransactionSearch.Click += new System.EventHandler(this.btnTransactionSearch_Click);
            // 
            // btnTransactionEdit
            // 
            this.btnTransactionEdit.Location = new System.Drawing.Point(625, 7);
            this.btnTransactionEdit.Name = "btnTransactionEdit";
            this.btnTransactionEdit.Size = new System.Drawing.Size(75, 23);
            this.btnTransactionEdit.TabIndex = 1;
            this.btnTransactionEdit.Text = "Edit";
            this.btnTransactionEdit.UseVisualStyleBackColor = true;
            this.btnTransactionEdit.Click += new System.EventHandler(this.btnTransactionEdit_Click);
            // 
            // btnTransactionDelete
            // 
            this.btnTransactionDelete.Location = new System.Drawing.Point(706, 7);
            this.btnTransactionDelete.Name = "btnTransactionDelete";
            this.btnTransactionDelete.Size = new System.Drawing.Size(75, 23);
            this.btnTransactionDelete.TabIndex = 0;
            this.btnTransactionDelete.Text = "Delete";
            this.btnTransactionDelete.UseVisualStyleBackColor = true;
            this.btnTransactionDelete.Click += new System.EventHandler(this.btnTransactionDelete_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.tabControl.ResumeLayout(false);
            this.tpClient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tpBAccount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBAccounts)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tpFAccount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFAccounts)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tpExchange.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExchangeRates)).EndInit();
            this.panel4.ResumeLayout(false);
            this.tpTransaction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.panel5.ResumeLayout(false);
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
        private Button btnClientAdd;
        private Button btnClientSearch;
        private Button btnClientEdit;
        private Button btnClientDelete;
        private Panel panel2;
        private Button btnBAccountAdd;
        private Button btnBAccountSearch;
        private Button btnBAccountEdit;
        private Button btnBAccountDelete;
        private Panel panel3;
        private Button btnFAccountAdd;
        private Button btnFAccountSearch;
        private Button btnFAccountEdit;
        private Button btnFAccountDelete;
        private Panel panel4;
        private Button btnExchangeRateAdd;
        private Button btnExchangeRateSearch;
        private Button btnExchangeRateEdit;
        private Button btnExchangeRateDelete;
        private Panel panel5;
        private Button btnTransactionAdd;
        private Button btnTransactionSearch;
        private Button btnTransactionEdit;
        private Button btnTransactionDelete;
    }
}