﻿// <auto-generated />
using System;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatabaseContext.Migrations
{
    [DbContext(typeof(BankContext))]
    [Migration("20230109024029_AddedFiscalAccounts")]
    partial class AddedFiscalAccounts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("ClientSequence");

            modelBuilder.Entity("Model.BankAccount", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Client")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("Client")
                        .IsUnique();

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("Model.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [ClientSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Model.Currency", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("Model.ExchangeRate", b =>
                {
                    b.Property<int>("FromCurrencyID")
                        .HasColumnType("int");

                    b.Property<int>("ToCurrencyID")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.HasKey("FromCurrencyID", "ToCurrencyID");

                    b.HasIndex("ToCurrencyID");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("Model.FiscalAccount", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("BankAccountID")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyID")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BankAccountID");

                    b.HasIndex("CurrencyID");

                    b.ToTable("FiscalAccounts");
                });

            modelBuilder.Entity("Model.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("FromAccountID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("ToAccountID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromAccountID");

                    b.HasIndex("ToAccountID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Model.LegalEntity", b =>
                {
                    b.HasBaseType("Model.Client");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("LegalEntity");
                });

            modelBuilder.Entity("Model.NaturalEntity", b =>
                {
                    b.HasBaseType("Model.Client");

                    b.ToTable("NaturalEntity");
                });

            modelBuilder.Entity("Model.BankAccount", b =>
                {
                    b.HasOne("Model.Client", "Owner")
                        .WithOne("BankAccount")
                        .HasForeignKey("Model.BankAccount", "Client")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Model.ExchangeRate", b =>
                {
                    b.HasOne("Model.Currency", "FromCurrency")
                        .WithMany()
                        .HasForeignKey("FromCurrencyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Currency", "ToCurrency")
                        .WithMany()
                        .HasForeignKey("ToCurrencyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromCurrency");

                    b.Navigation("ToCurrency");
                });

            modelBuilder.Entity("Model.FiscalAccount", b =>
                {
                    b.HasOne("Model.BankAccount", null)
                        .WithMany("FiscalAccounts")
                        .HasForeignKey("BankAccountID");

                    b.HasOne("Model.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("Model.Transaction", b =>
                {
                    b.HasOne("Model.FiscalAccount", "FromAccount")
                        .WithMany()
                        .HasForeignKey("FromAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.FiscalAccount", "ToAccount")
                        .WithMany()
                        .HasForeignKey("ToAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromAccount");

                    b.Navigation("ToAccount");
                });

            modelBuilder.Entity("Model.BankAccount", b =>
                {
                    b.Navigation("FiscalAccounts");
                });

            modelBuilder.Entity("Model.Client", b =>
                {
                    b.Navigation("BankAccount")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
