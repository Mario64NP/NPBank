using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseContext.Migrations
{
    /// <inheritdoc />
    public partial class AddedFiscalAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FiscalAccount_BankAccounts_BankAccountID",
                table: "FiscalAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_FiscalAccount_Currency_CurrencyID",
                table: "FiscalAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FiscalAccount_FromAccountID",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FiscalAccount_ToAccountID",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FiscalAccount",
                table: "FiscalAccount");

            migrationBuilder.RenameTable(
                name: "FiscalAccount",
                newName: "FiscalAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_FiscalAccount_CurrencyID",
                table: "FiscalAccounts",
                newName: "IX_FiscalAccounts_CurrencyID");

            migrationBuilder.RenameIndex(
                name: "IX_FiscalAccount_BankAccountID",
                table: "FiscalAccounts",
                newName: "IX_FiscalAccounts_BankAccountID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FiscalAccounts",
                table: "FiscalAccounts",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalAccounts_BankAccounts_BankAccountID",
                table: "FiscalAccounts",
                column: "BankAccountID",
                principalTable: "BankAccounts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalAccounts_Currency_CurrencyID",
                table: "FiscalAccounts",
                column: "CurrencyID",
                principalTable: "Currency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FiscalAccounts_FromAccountID",
                table: "Transactions",
                column: "FromAccountID",
                principalTable: "FiscalAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FiscalAccounts_ToAccountID",
                table: "Transactions",
                column: "ToAccountID",
                principalTable: "FiscalAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FiscalAccounts_BankAccounts_BankAccountID",
                table: "FiscalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_FiscalAccounts_Currency_CurrencyID",
                table: "FiscalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FiscalAccounts_FromAccountID",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FiscalAccounts_ToAccountID",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FiscalAccounts",
                table: "FiscalAccounts");

            migrationBuilder.RenameTable(
                name: "FiscalAccounts",
                newName: "FiscalAccount");

            migrationBuilder.RenameIndex(
                name: "IX_FiscalAccounts_CurrencyID",
                table: "FiscalAccount",
                newName: "IX_FiscalAccount_CurrencyID");

            migrationBuilder.RenameIndex(
                name: "IX_FiscalAccounts_BankAccountID",
                table: "FiscalAccount",
                newName: "IX_FiscalAccount_BankAccountID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FiscalAccount",
                table: "FiscalAccount",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalAccount_BankAccounts_BankAccountID",
                table: "FiscalAccount",
                column: "BankAccountID",
                principalTable: "BankAccounts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalAccount_Currency_CurrencyID",
                table: "FiscalAccount",
                column: "CurrencyID",
                principalTable: "Currency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FiscalAccount_FromAccountID",
                table: "Transactions",
                column: "FromAccountID",
                principalTable: "FiscalAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FiscalAccount_ToAccountID",
                table: "Transactions",
                column: "ToAccountID",
                principalTable: "FiscalAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
