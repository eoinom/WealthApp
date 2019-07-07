﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backendData;

namespace backendData.Migrations
{
    [DbContext(typeof(backendDbContext))]
    [Migration("20190707205441_Rationalised_and_Altered_Models")]
    partial class Rationalised_and_Altered_Models
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("backendData.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("Institution");

                    b.Property<bool>("IsActive");

                    b.Property<string>("QuotedCurrencyCode")
                        .IsRequired();

                    b.Property<string>("Type");

                    b.Property<int>("UserId");

                    b.HasKey("AccountId");

                    b.HasIndex("QuotedCurrencyCode");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("backendData.Models.AccountValue", b =>
                {
                    b.Property<int>("AccountValueId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<int?>("CreditCardId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("LoanId");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AccountValueId");

                    b.HasIndex("AccountId");

                    b.HasIndex("CreditCardId");

                    b.HasIndex("LoanId");

                    b.ToTable("AccountValues");
                });

            modelBuilder.Entity("backendData.Models.Asset", b =>
                {
                    b.Property<int>("AssetId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssetName")
                        .IsRequired();

                    b.Property<string>("AssetType");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("QuotedCurrencyCode")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("AssetId");

                    b.HasIndex("QuotedCurrencyCode");

                    b.HasIndex("UserId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("backendData.Models.AssetValue", b =>
                {
                    b.Property<int>("AssetValueId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssetId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Source");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AssetValueId");

                    b.HasIndex("AssetId");

                    b.ToTable("AssetValues");
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

                    b.Property<string>("Institution");

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

            modelBuilder.Entity("backendData.Models.Loan", b =>
                {
                    b.Property<int>("LoanId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AprRate");

                    b.Property<string>("Description");

                    b.Property<int>("FixedTermInMonths");

                    b.Property<string>("Institution");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LoanName")
                        .IsRequired();

                    b.Property<string>("QuotedCurrencyCode")
                        .IsRequired();

                    b.Property<string>("RateType");

                    b.Property<decimal>("RepaymentAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RepaymentFrequency");

                    b.Property<int?>("SecuredAssetAssetId");

                    b.Property<DateTime>("StartDate");

                    b.Property<decimal>("StartPrincipal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TermInMonths");

                    b.Property<string>("Type");

                    b.Property<int>("UserId");

                    b.HasKey("LoanId");

                    b.HasIndex("QuotedCurrencyCode");

                    b.HasIndex("SecuredAssetAssetId");

                    b.HasIndex("UserId");

                    b.ToTable("Loans");
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

            modelBuilder.Entity("backendData.Models.Account", b =>
                {
                    b.HasOne("backendData.Models.Currency", "QuotedCurrency")
                        .WithMany()
                        .HasForeignKey("QuotedCurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backendData.Models.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backendData.Models.AccountValue", b =>
                {
                    b.HasOne("backendData.Models.Account", "Account")
                        .WithMany("AccountValues")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backendData.Models.CreditCard")
                        .WithMany("BalancesOwing")
                        .HasForeignKey("CreditCardId");

                    b.HasOne("backendData.Models.Loan")
                        .WithMany("BalancesOwing")
                        .HasForeignKey("LoanId");
                });

            modelBuilder.Entity("backendData.Models.Asset", b =>
                {
                    b.HasOne("backendData.Models.Currency", "QuotedCurrency")
                        .WithMany()
                        .HasForeignKey("QuotedCurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backendData.Models.User", "User")
                        .WithMany("Assets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backendData.Models.AssetValue", b =>
                {
                    b.HasOne("backendData.Models.Asset", "Asset")
                        .WithMany("AssetValues")
                        .HasForeignKey("AssetId");
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

            modelBuilder.Entity("backendData.Models.Loan", b =>
                {
                    b.HasOne("backendData.Models.Currency", "QuotedCurrency")
                        .WithMany()
                        .HasForeignKey("QuotedCurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backendData.Models.Asset", "SecuredAsset")
                        .WithMany()
                        .HasForeignKey("SecuredAssetAssetId");

                    b.HasOne("backendData.Models.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
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
