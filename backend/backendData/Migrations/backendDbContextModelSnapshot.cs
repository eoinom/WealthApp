﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backendData;

namespace backendData.Migrations
{
    [DbContext(typeof(backendDbContext))]
    partial class backendDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("backendData.Models.AccountValue", b =>
                {
                    b.Property<int>("AccountValueId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BankAccountId");

                    b.Property<int?>("CashAccountId");

                    b.Property<int?>("CreditCardId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("LoanAccountId");

                    b.Property<int?>("MortgageAccountId");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AccountValueId");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("CashAccountId");

                    b.HasIndex("CreditCardId");

                    b.HasIndex("LoanAccountId");

                    b.HasIndex("MortgageAccountId");

                    b.ToTable("AccountValues");
                });

            modelBuilder.Entity("backendData.Models.BankAccount", b =>
                {
                    b.Property<int>("BankAccountId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired();

                    b.Property<string>("AccountType")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("Institution")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("QuotedCurrencyCode")
                        .IsRequired();

                    b.Property<string>("Type");

                    b.Property<int>("UserId");

                    b.HasKey("BankAccountId");

                    b.HasIndex("QuotedCurrencyCode");

                    b.HasIndex("UserId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("backendData.Models.CashAccount", b =>
                {
                    b.Property<int>("CashAccountId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("QuotedCurrencyCode")
                        .IsRequired();

                    b.Property<string>("Type");

                    b.Property<int>("UserId");

                    b.HasKey("CashAccountId");

                    b.HasIndex("QuotedCurrencyCode");

                    b.HasIndex("UserId");

                    b.ToTable("CashAccounts");
                });

            modelBuilder.Entity("backendData.Models.Country", b =>
                {
                    b.Property<string>("Iso2Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Continent");

                    b.Property<string>("Iso3Code");

                    b.Property<string>("MainCurrencyCode");

                    b.Property<string>("Name");

                    b.HasKey("Iso2Code");

                    b.HasIndex("MainCurrencyCode");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("backendData.Models.CreditCard", b =>
                {
                    b.Property<int>("CreditCardId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreditCardName")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("QuotedCurrencyCode")
                        .IsRequired();

                    b.Property<string>("Type");

                    b.Property<int>("UserId");

                    b.HasKey("CreditCardId");

                    b.HasIndex("QuotedCurrencyCode");

                    b.HasIndex("UserId");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("backendData.Models.CryptoAccount", b =>
                {
                    b.Property<int>("CryptoAccountId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("QuotedCurrencyCode")
                        .IsRequired();

                    b.Property<string>("Type");

                    b.Property<int>("UserId");

                    b.HasKey("CryptoAccountId");

                    b.HasIndex("QuotedCurrencyCode");

                    b.HasIndex("UserId");

                    b.ToTable("CryptoAccounts");
                });

            modelBuilder.Entity("backendData.Models.CryptoAccountValue", b =>
                {
                    b.Property<int>("CryptoAccountValueId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CryptoAccountId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CryptoAccountValueId");

                    b.HasIndex("CryptoAccountId");

                    b.ToTable("CryptoAccountValues");
                });

            modelBuilder.Entity("backendData.Models.CryptoValue", b =>
                {
                    b.Property<int>("CryptoValueId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CryptoAccountValueId");

                    b.Property<string>("CryptocurrencyCode");

                    b.Property<DateTime>("Date");

                    b.Property<double>("NoCoins");

                    b.Property<double>("Price");

                    b.Property<string>("QuotedCurrency");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CryptoValueId");

                    b.HasIndex("CryptoAccountValueId");

                    b.HasIndex("CryptocurrencyCode");

                    b.ToTable("CryptoValues");
                });

            modelBuilder.Entity("backendData.Models.Cryptocurrency", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LogoUrl");

                    b.Property<string>("Name");

                    b.HasKey("Code");

                    b.ToTable("Cryptocurrencies");
                });

            modelBuilder.Entity("backendData.Models.Currency", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NameLong");

                    b.Property<string>("NameShort");

                    b.HasKey("Code");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("backendData.Models.LoanAccount", b =>
                {
                    b.Property<int>("LoanAccountId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired();

                    b.Property<double>("AprRate");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("QuotedCurrencyCode")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TermInMonths");

                    b.Property<string>("Type");

                    b.Property<int>("UserId");

                    b.HasKey("LoanAccountId");

                    b.HasIndex("QuotedCurrencyCode");

                    b.HasIndex("UserId");

                    b.ToTable("LoanAccounts");
                });

            modelBuilder.Entity("backendData.Models.MortgageAccount", b =>
                {
                    b.Property<int>("MortgageAccountId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired();

                    b.Property<double>("AprRate");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("MortgagedPropertyPropertyId");

                    b.Property<string>("QuotedCurrencyCode")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TermInMonths");

                    b.Property<string>("Type");

                    b.Property<int>("UserId");

                    b.HasKey("MortgageAccountId");

                    b.HasIndex("MortgagedPropertyPropertyId");

                    b.HasIndex("QuotedCurrencyCode");

                    b.HasIndex("UserId");

                    b.ToTable("MortgageAccounts");
                });

            modelBuilder.Entity("backendData.Models.Property", b =>
                {
                    b.Property<int>("PropertyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("PropertyName")
                        .IsRequired();

                    b.Property<string>("PropertyType");

                    b.Property<string>("QuotedCurrencyCode")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("PropertyId");

                    b.HasIndex("QuotedCurrencyCode");

                    b.HasIndex("UserId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("backendData.Models.PropertyValue", b =>
                {
                    b.Property<int>("PropertyValueId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int?>("PropertyId");

                    b.Property<string>("Source");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PropertyValueId");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyValues");
                });

            modelBuilder.Entity("backendData.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryIso2Code");

                    b.Property<string>("DisplayCurrencyCode");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("NewsletterSub");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.HasIndex("CountryIso2Code");

                    b.HasIndex("DisplayCurrencyCode");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("backendData.Models.AccountValue", b =>
                {
                    b.HasOne("backendData.Models.BankAccount", "BankAccount")
                        .WithMany("AccountValues")
                        .HasForeignKey("BankAccountId");

                    b.HasOne("backendData.Models.CashAccount", "CashAccount")
                        .WithMany("AccountValues")
                        .HasForeignKey("CashAccountId");

                    b.HasOne("backendData.Models.CreditCard")
                        .WithMany("BalancesOwing")
                        .HasForeignKey("CreditCardId");

                    b.HasOne("backendData.Models.LoanAccount", "LoanAccount")
                        .WithMany()
                        .HasForeignKey("LoanAccountId");

                    b.HasOne("backendData.Models.MortgageAccount", "MortgageAccount")
                        .WithMany()
                        .HasForeignKey("MortgageAccountId");
                });

            modelBuilder.Entity("backendData.Models.BankAccount", b =>
                {
                    b.HasOne("backendData.Models.Currency", "QuotedCurrency")
                        .WithMany()
                        .HasForeignKey("QuotedCurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backendData.Models.User", "User")
                        .WithMany("BankAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backendData.Models.CashAccount", b =>
                {
                    b.HasOne("backendData.Models.Currency", "QuotedCurrency")
                        .WithMany()
                        .HasForeignKey("QuotedCurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backendData.Models.User", "User")
                        .WithMany("CashAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backendData.Models.Country", b =>
                {
                    b.HasOne("backendData.Models.Currency", "MainCurrency")
                        .WithMany()
                        .HasForeignKey("MainCurrencyCode");
                });

            modelBuilder.Entity("backendData.Models.CreditCard", b =>
                {
                    b.HasOne("backendData.Models.Currency", "QuotedCurrency")
                        .WithMany()
                        .HasForeignKey("QuotedCurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backendData.Models.User", "User")
                        .WithMany("CreditCards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backendData.Models.CryptoAccount", b =>
                {
                    b.HasOne("backendData.Models.Currency", "QuotedCurrency")
                        .WithMany()
                        .HasForeignKey("QuotedCurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backendData.Models.User", "User")
                        .WithMany("CryptoAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backendData.Models.CryptoAccountValue", b =>
                {
                    b.HasOne("backendData.Models.CryptoAccount", "CryptoAccount")
                        .WithMany("CryptoAccountValues")
                        .HasForeignKey("CryptoAccountId");
                });

            modelBuilder.Entity("backendData.Models.CryptoValue", b =>
                {
                    b.HasOne("backendData.Models.CryptoAccountValue", "CryptoAccountValue")
                        .WithMany("CryptoValues")
                        .HasForeignKey("CryptoAccountValueId");

                    b.HasOne("backendData.Models.Cryptocurrency", "Cryptocurrency")
                        .WithMany()
                        .HasForeignKey("CryptocurrencyCode");
                });

            modelBuilder.Entity("backendData.Models.LoanAccount", b =>
                {
                    b.HasOne("backendData.Models.Currency", "QuotedCurrency")
                        .WithMany()
                        .HasForeignKey("QuotedCurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backendData.Models.User", "User")
                        .WithMany("LoanAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backendData.Models.MortgageAccount", b =>
                {
                    b.HasOne("backendData.Models.Property", "MortgagedProperty")
                        .WithMany()
                        .HasForeignKey("MortgagedPropertyPropertyId");

                    b.HasOne("backendData.Models.Currency", "QuotedCurrency")
                        .WithMany()
                        .HasForeignKey("QuotedCurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backendData.Models.User", "User")
                        .WithMany("MortgageAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backendData.Models.Property", b =>
                {
                    b.HasOne("backendData.Models.Currency", "QuotedCurrency")
                        .WithMany()
                        .HasForeignKey("QuotedCurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backendData.Models.User", "User")
                        .WithMany("Properties")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backendData.Models.PropertyValue", b =>
                {
                    b.HasOne("backendData.Models.Property", "Property")
                        .WithMany("PropertyValues")
                        .HasForeignKey("PropertyId");
                });

            modelBuilder.Entity("backendData.Models.User", b =>
                {
                    b.HasOne("backendData.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryIso2Code");

                    b.HasOne("backendData.Models.Currency", "DisplayCurrency")
                        .WithMany()
                        .HasForeignKey("DisplayCurrencyCode");
                });
#pragma warning restore 612, 618
        }
    }
}
