using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseContext.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "ClientSequence");

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LegalEntity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ClientSequence]"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntity", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NaturalEntity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ClientSequence]"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalEntity", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    FromCurrencyID = table.Column<int>(type: "int", nullable: false),
                    ToCurrencyID = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => new { x.FromCurrencyID, x.ToCurrencyID });
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currency_FromCurrencyID",
                        column: x => x.FromCurrencyID,
                        principalTable: "Currency",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currency_ToCurrencyID",
                        column: x => x.ToCurrencyID,
                        principalTable: "Currency",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FiscalAccount",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BankAccountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FiscalAccount_BankAccounts_BankAccountID",
                        column: x => x.BankAccountID,
                        principalTable: "BankAccounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FiscalAccount_Currency_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "Currency",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromAccountID = table.Column<int>(type: "int", nullable: false),
                    ToAccountID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_FiscalAccount_FromAccountID",
                        column: x => x.FromAccountID,
                        principalTable: "FiscalAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_FiscalAccount_ToAccountID",
                        column: x => x.ToAccountID,
                        principalTable: "FiscalAccount",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_Client",
                table: "BankAccounts",
                column: "Client",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_ToCurrencyID",
                table: "ExchangeRates",
                column: "ToCurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalAccount_BankAccountID",
                table: "FiscalAccount",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalAccount_CurrencyID",
                table: "FiscalAccount",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromAccountID",
                table: "Transactions",
                column: "FromAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToAccountID",
                table: "Transactions",
                column: "ToAccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "LegalEntity");

            migrationBuilder.DropTable(
                name: "NaturalEntity");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "FiscalAccount");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropSequence(
                name: "ClientSequence");
        }
    }
}
