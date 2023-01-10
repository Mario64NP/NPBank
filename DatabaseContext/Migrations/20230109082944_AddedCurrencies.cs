using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseContext.Migrations
{
    /// <inheritdoc />
    public partial class AddedCurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currency_FromCurrencyID",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currency_ToCurrencyID",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_FiscalAccounts_Currency_CurrencyID",
                table: "FiscalAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currency",
                table: "Currency");

            migrationBuilder.RenameTable(
                name: "Currency",
                newName: "Currencies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currencies_FromCurrencyID",
                table: "ExchangeRates",
                column: "FromCurrencyID",
                principalTable: "Currencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currencies_ToCurrencyID",
                table: "ExchangeRates",
                column: "ToCurrencyID",
                principalTable: "Currencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalAccounts_Currencies_CurrencyID",
                table: "FiscalAccounts",
                column: "CurrencyID",
                principalTable: "Currencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currencies_FromCurrencyID",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currencies_ToCurrencyID",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_FiscalAccounts_Currencies_CurrencyID",
                table: "FiscalAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "Currency");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currency",
                table: "Currency",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currency_FromCurrencyID",
                table: "ExchangeRates",
                column: "FromCurrencyID",
                principalTable: "Currency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currency_ToCurrencyID",
                table: "ExchangeRates",
                column: "ToCurrencyID",
                principalTable: "Currency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalAccounts_Currency_CurrencyID",
                table: "FiscalAccounts",
                column: "CurrencyID",
                principalTable: "Currency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
