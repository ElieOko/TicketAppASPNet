﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketApp.Data;

#nullable disable

namespace TicketApp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TicketApp.Models.Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BranchId"));

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchZone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BranchId");

                    b.ToTable("TBranches");
                });

            modelBuilder.Entity("TicketApp.Models.Call", b =>
                {
                    b.Property<int>("CallId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CallId"));

                    b.Property<int?>("CounterFId")
                        .HasColumnType("int")
                        .HasColumnName("CounterFId");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Ticket")
                        .HasColumnType("int");

                    b.Property<int?>("UserFId")
                        .HasColumnType("int")
                        .HasColumnName("UserFId");

                    b.HasKey("CallId");

                    b.HasIndex("CounterFId");

                    b.HasIndex("UserFId");

                    b.ToTable("TCalls");
                });

            modelBuilder.Entity("TicketApp.Models.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CardId"));

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CardId");

                    b.ToTable("TCards");
                });

            modelBuilder.Entity("TicketApp.Models.Counter", b =>
                {
                    b.Property<int>("CounterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CounterId"));

                    b.Property<int>("BranchFId")
                        .HasColumnType("int");

                    b.Property<string>("CounterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CounterId");

                    b.HasIndex("BranchFId");

                    b.ToTable("TCounters");
                });

            modelBuilder.Entity("TicketApp.Models.Currency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CurrencyId"));

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrencyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CurrencyId");

                    b.ToTable("TCurrencies");
                });

            modelBuilder.Entity("TicketApp.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<int?>("CardTypeFID")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TitleFId")
                        .HasColumnType("int");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fatherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("idCardExpiryDate1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("idCardNumber1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("motherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("signature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("township")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("whatsappNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("TitleFId");

                    b.ToTable("TCustomers");
                });

            modelBuilder.Entity("TicketApp.Models.Interval", b =>
                {
                    b.Property<int>("IntervalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IntervalId"));

                    b.Property<int?>("CurrencyFId")
                        .HasColumnType("int")
                        .HasColumnName("CurrencyFId");

                    b.Property<int?>("Max")
                        .HasColumnType("int");

                    b.Property<int?>("Min")
                        .HasColumnType("int");

                    b.Property<int?>("TransferTypeFId")
                        .HasColumnType("int")
                        .HasColumnName("TransferTypeFId");

                    b.HasKey("IntervalId");

                    b.HasIndex("CurrencyFId");

                    b.HasIndex("TransferTypeFId");

                    b.ToTable("TIntervals");
                });

            modelBuilder.Entity("TicketApp.Models.OrderNumber", b =>
                {
                    b.Property<int>("OrderNumberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderNumberId"));

                    b.Property<int>("BranchFId")
                        .HasColumnType("int")
                        .HasColumnName("BranchFId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("TransferTypeFId")
                        .HasColumnType("int")
                        .HasColumnName("TransferTypeFId");

                    b.HasKey("OrderNumberId");

                    b.HasIndex("BranchFId");

                    b.HasIndex("TransferTypeFId");

                    b.ToTable("TOrderNumbers");
                });

            modelBuilder.Entity("TicketApp.Models.Title", b =>
                {
                    b.Property<int>("TitleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TitleId"));

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TitleId");

                    b.ToTable("TTitles");
                });

            modelBuilder.Entity("TicketApp.Models.TransferType", b =>
                {
                    b.Property<int>("TransferTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransferTypeId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransferTypeId");

                    b.ToTable("TTransferTypes");
                });

            modelBuilder.Entity("TicketApp.Models.Transfert", b =>
                {
                    b.Property<int>("TransfertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransfertId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BranchFId")
                        .HasColumnType("int");

                    b.Property<int?>("CallUserFId")
                        .HasColumnType("int");

                    b.Property<string>("CardExpiryDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CardFId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompleteNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Completed")
                        .HasColumnType("bit");

                    b.Property<int>("CurrencyFId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FromBranchId")
                        .HasColumnType("int");

                    b.Property<int>("IntervalFId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Signature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeCalled")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ToBranchId")
                        .HasColumnType("int");

                    b.Property<int>("TransferStatusFId")
                        .HasColumnType("int");

                    b.Property<string>("UniqueString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserFId")
                        .HasColumnType("int");

                    b.Property<string>("imagePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransfertId");

                    b.HasIndex("BranchFId");

                    b.HasIndex("CardFId");

                    b.HasIndex("CurrencyFId");

                    b.HasIndex("IntervalFId");

                    b.HasIndex("TransferStatusFId");

                    b.HasIndex("UserFId");

                    b.ToTable("TTransferts");
                });

            modelBuilder.Entity("TicketApp.Models.TransfertStatus", b =>
                {
                    b.Property<int>("TransferStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransferStatusId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransferStatusId");

                    b.ToTable("TTransfertStatus");
                });

            modelBuilder.Entity("TicketApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<byte?>("AccessLevel")
                        .HasColumnType("tinyint");

                    b.Property<int>("BranchFId")
                        .HasColumnType("int")
                        .HasColumnName("BranchFId");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Locked")
                        .HasColumnType("bit");

                    b.Property<int?>("MaxAttempt")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.HasIndex("BranchFId");

                    b.ToTable("TUsers");
                });

            modelBuilder.Entity("TicketApp.Models.Call", b =>
                {
                    b.HasOne("TicketApp.Models.Counter", "counters")
                        .WithMany("calls")
                        .HasForeignKey("CounterFId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TicketApp.Models.User", "users")
                        .WithMany("calls")
                        .HasForeignKey("UserFId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("counters");

                    b.Navigation("users");
                });

            modelBuilder.Entity("TicketApp.Models.Counter", b =>
                {
                    b.HasOne("TicketApp.Models.Branch", "branches")
                        .WithMany("counters")
                        .HasForeignKey("BranchFId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("branches");
                });

            modelBuilder.Entity("TicketApp.Models.Customer", b =>
                {
                    b.HasOne("TicketApp.Models.Title", "titles")
                        .WithMany("customers")
                        .HasForeignKey("TitleFId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("titles");
                });

            modelBuilder.Entity("TicketApp.Models.Interval", b =>
                {
                    b.HasOne("TicketApp.Models.Currency", "currencies")
                        .WithMany("intervals")
                        .HasForeignKey("CurrencyFId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TicketApp.Models.TransferType", "transferTypes")
                        .WithMany("intervals")
                        .HasForeignKey("TransferTypeFId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("currencies");

                    b.Navigation("transferTypes");
                });

            modelBuilder.Entity("TicketApp.Models.OrderNumber", b =>
                {
                    b.HasOne("TicketApp.Models.Branch", "branches")
                        .WithMany("orderNumbers")
                        .HasForeignKey("BranchFId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TicketApp.Models.TransferType", "transferTypes")
                        .WithMany("orderNumbers")
                        .HasForeignKey("TransferTypeFId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("branches");

                    b.Navigation("transferTypes");
                });

            modelBuilder.Entity("TicketApp.Models.Transfert", b =>
                {
                    b.HasOne("TicketApp.Models.Branch", "branches")
                        .WithMany("transferts")
                        .HasForeignKey("BranchFId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TicketApp.Models.Card", "cards")
                        .WithMany("transferts")
                        .HasForeignKey("CardFId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TicketApp.Models.Currency", "currencies")
                        .WithMany("transferts")
                        .HasForeignKey("CurrencyFId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TicketApp.Models.Interval", "intervals")
                        .WithMany("transferts")
                        .HasForeignKey("IntervalFId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TicketApp.Models.TransfertStatus", "transfertStatus")
                        .WithMany("transferts")
                        .HasForeignKey("TransferStatusFId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TicketApp.Models.User", "users")
                        .WithMany("transferts")
                        .HasForeignKey("UserFId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("branches");

                    b.Navigation("cards");

                    b.Navigation("currencies");

                    b.Navigation("intervals");

                    b.Navigation("transfertStatus");

                    b.Navigation("users");
                });

            modelBuilder.Entity("TicketApp.Models.User", b =>
                {
                    b.HasOne("TicketApp.Models.Branch", "branch")
                        .WithMany("Users")
                        .HasForeignKey("BranchFId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("branch");
                });

            modelBuilder.Entity("TicketApp.Models.Branch", b =>
                {
                    b.Navigation("Users");

                    b.Navigation("counters");

                    b.Navigation("orderNumbers");

                    b.Navigation("transferts");
                });

            modelBuilder.Entity("TicketApp.Models.Card", b =>
                {
                    b.Navigation("transferts");
                });

            modelBuilder.Entity("TicketApp.Models.Counter", b =>
                {
                    b.Navigation("calls");
                });

            modelBuilder.Entity("TicketApp.Models.Currency", b =>
                {
                    b.Navigation("intervals");

                    b.Navigation("transferts");
                });

            modelBuilder.Entity("TicketApp.Models.Interval", b =>
                {
                    b.Navigation("transferts");
                });

            modelBuilder.Entity("TicketApp.Models.Title", b =>
                {
                    b.Navigation("customers");
                });

            modelBuilder.Entity("TicketApp.Models.TransferType", b =>
                {
                    b.Navigation("intervals");

                    b.Navigation("orderNumbers");
                });

            modelBuilder.Entity("TicketApp.Models.TransfertStatus", b =>
                {
                    b.Navigation("transferts");
                });

            modelBuilder.Entity("TicketApp.Models.User", b =>
                {
                    b.Navigation("calls");

                    b.Navigation("transferts");
                });
#pragma warning restore 612, 618
        }
    }
}
