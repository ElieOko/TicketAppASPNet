using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBranches",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchZone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBranches", x => x.BranchId);
                });

            migrationBuilder.CreateTable(
                name: "TCards",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCards", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "TCurrencies",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCurrencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "TTitles",
                columns: table => new
                {
                    TitleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTitles", x => x.TitleId);
                });

            migrationBuilder.CreateTable(
                name: "TTransfertStatus",
                columns: table => new
                {
                    TransferStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTransfertStatus", x => x.TransferStatusId);
                });

            migrationBuilder.CreateTable(
                name: "TTransferTypes",
                columns: table => new
                {
                    TransferTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTransferTypes", x => x.TransferTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TCounters",
                columns: table => new
                {
                    CounterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CounterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchFId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCounters", x => x.CounterId);
                    table.ForeignKey(
                        name: "FK_TCounters_TBranches_BranchFId",
                        column: x => x.BranchFId,
                        principalTable: "TBranches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    AccessLevel = table.Column<byte>(type: "tinyint", nullable: true),
                    MaxAttempt = table.Column<int>(type: "int", nullable: true),
                    BranchFId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_TUsers_TBranches_BranchFId",
                        column: x => x.BranchFId,
                        principalTable: "TBranches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TCustomers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleFId = table.Column<int>(type: "int", nullable: false),
                    CardTypeFID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whatsappNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    township = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idCardNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idCardExpiryDate1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    signature = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCustomers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_TCustomers_TTitles_TitleFId",
                        column: x => x.TitleFId,
                        principalTable: "TTitles",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TIntervals",
                columns: table => new
                {
                    IntervalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferTypeFId = table.Column<int>(type: "int", nullable: true),
                    CurrencyFId = table.Column<int>(type: "int", nullable: true),
                    Min = table.Column<int>(type: "int", nullable: true),
                    Max = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIntervals", x => x.IntervalId);
                    table.ForeignKey(
                        name: "FK_TIntervals_TCurrencies_CurrencyFId",
                        column: x => x.CurrencyFId,
                        principalTable: "TCurrencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TIntervals_TTransferTypes_TransferTypeFId",
                        column: x => x.TransferTypeFId,
                        principalTable: "TTransferTypes",
                        principalColumn: "TransferTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TOrderNumbers",
                columns: table => new
                {
                    OrderNumberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    TransferTypeFId = table.Column<int>(type: "int", nullable: false),
                    BranchFId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOrderNumbers", x => x.OrderNumberId);
                    table.ForeignKey(
                        name: "FK_TOrderNumbers_TBranches_BranchFId",
                        column: x => x.BranchFId,
                        principalTable: "TBranches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TOrderNumbers_TTransferTypes_TransferTypeFId",
                        column: x => x.TransferTypeFId,
                        principalTable: "TTransferTypes",
                        principalColumn: "TransferTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TCalls",
                columns: table => new
                {
                    CallId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket = table.Column<int>(type: "int", nullable: true),
                    CounterFId = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserFId = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCalls", x => x.CallId);
                    table.ForeignKey(
                        name: "FK_TCalls_TCounters_CounterFId",
                        column: x => x.CounterFId,
                        principalTable: "TCounters",
                        principalColumn: "CounterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TCalls_TUsers_UserFId",
                        column: x => x.UserFId,
                        principalTable: "TUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TTransferts",
                columns: table => new
                {
                    TransfertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferStatusFId = table.Column<int>(type: "int", nullable: false),
                    CallUserFId = table.Column<int>(type: "int", nullable: true),
                    BranchFId = table.Column<int>(type: "int", nullable: false),
                    UserFId = table.Column<int>(type: "int", nullable: false),
                    CurrencyFId = table.Column<int>(type: "int", nullable: false),
                    IntervalFId = table.Column<int>(type: "int", nullable: false),
                    CardFId = table.Column<int>(type: "int", nullable: false),
                    FromBranchId = table.Column<int>(type: "int", nullable: true),
                    ToBranchId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: true),
                    CompleteNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardExpiryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCalled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UniqueString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTransferts", x => x.TransfertId);
                    table.ForeignKey(
                        name: "FK_TTransferts_TBranches_BranchFId",
                        column: x => x.BranchFId,
                        principalTable: "TBranches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TTransferts_TCards_CardFId",
                        column: x => x.CardFId,
                        principalTable: "TCards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TTransferts_TCurrencies_CurrencyFId",
                        column: x => x.CurrencyFId,
                        principalTable: "TCurrencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TTransferts_TIntervals_IntervalFId",
                        column: x => x.IntervalFId,
                        principalTable: "TIntervals",
                        principalColumn: "IntervalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TTransferts_TTransfertStatus_TransferStatusFId",
                        column: x => x.TransferStatusFId,
                        principalTable: "TTransfertStatus",
                        principalColumn: "TransferStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TTransferts_TUsers_UserFId",
                        column: x => x.UserFId,
                        principalTable: "TUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TCalls_CounterFId",
                table: "TCalls",
                column: "CounterFId");

            migrationBuilder.CreateIndex(
                name: "IX_TCalls_UserFId",
                table: "TCalls",
                column: "UserFId");

            migrationBuilder.CreateIndex(
                name: "IX_TCounters_BranchFId",
                table: "TCounters",
                column: "BranchFId");

            migrationBuilder.CreateIndex(
                name: "IX_TCustomers_TitleFId",
                table: "TCustomers",
                column: "TitleFId");

            migrationBuilder.CreateIndex(
                name: "IX_TIntervals_CurrencyFId",
                table: "TIntervals",
                column: "CurrencyFId");

            migrationBuilder.CreateIndex(
                name: "IX_TIntervals_TransferTypeFId",
                table: "TIntervals",
                column: "TransferTypeFId");

            migrationBuilder.CreateIndex(
                name: "IX_TOrderNumbers_BranchFId",
                table: "TOrderNumbers",
                column: "BranchFId");

            migrationBuilder.CreateIndex(
                name: "IX_TOrderNumbers_TransferTypeFId",
                table: "TOrderNumbers",
                column: "TransferTypeFId");

            migrationBuilder.CreateIndex(
                name: "IX_TTransferts_BranchFId",
                table: "TTransferts",
                column: "BranchFId");

            migrationBuilder.CreateIndex(
                name: "IX_TTransferts_CardFId",
                table: "TTransferts",
                column: "CardFId");

            migrationBuilder.CreateIndex(
                name: "IX_TTransferts_CurrencyFId",
                table: "TTransferts",
                column: "CurrencyFId");

            migrationBuilder.CreateIndex(
                name: "IX_TTransferts_IntervalFId",
                table: "TTransferts",
                column: "IntervalFId");

            migrationBuilder.CreateIndex(
                name: "IX_TTransferts_TransferStatusFId",
                table: "TTransferts",
                column: "TransferStatusFId");

            migrationBuilder.CreateIndex(
                name: "IX_TTransferts_UserFId",
                table: "TTransferts",
                column: "UserFId");

            migrationBuilder.CreateIndex(
                name: "IX_TUsers_BranchFId",
                table: "TUsers",
                column: "BranchFId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TCalls");

            migrationBuilder.DropTable(
                name: "TCustomers");

            migrationBuilder.DropTable(
                name: "TOrderNumbers");

            migrationBuilder.DropTable(
                name: "TTransferts");

            migrationBuilder.DropTable(
                name: "TCounters");

            migrationBuilder.DropTable(
                name: "TTitles");

            migrationBuilder.DropTable(
                name: "TCards");

            migrationBuilder.DropTable(
                name: "TIntervals");

            migrationBuilder.DropTable(
                name: "TTransfertStatus");

            migrationBuilder.DropTable(
                name: "TUsers");

            migrationBuilder.DropTable(
                name: "TCurrencies");

            migrationBuilder.DropTable(
                name: "TTransferTypes");

            migrationBuilder.DropTable(
                name: "TBranches");
        }
    }
}
