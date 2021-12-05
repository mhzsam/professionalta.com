using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstZX.Datalayer.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RoleTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActiveCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentifireEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IdentifireKey = table.Column<int>(type: "int", nullable: false),
                    DigitalWalletId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ValidId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SelectedPlan = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BankDetailEarn",
                columns: table => new
                {
                    BankDetailEarnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RewardEarnmonth1 = table.Column<double>(type: "float", nullable: false),
                    RewardEarnmonth2 = table.Column<double>(type: "float", nullable: false),
                    RewardEarnmonth3 = table.Column<double>(type: "float", nullable: false),
                    RewardEarnmonth4 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetailEarn", x => x.BankDetailEarnId);
                    table.ForeignKey(
                        name: "FK_BankDetailEarn_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankDetailEarnWeek",
                columns: table => new
                {
                    BankDetailEarnWeekId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RewardEarnWeek1 = table.Column<double>(type: "float", nullable: false),
                    RewardEarnWeek2 = table.Column<double>(type: "float", nullable: false),
                    RewardEarnWeek3 = table.Column<double>(type: "float", nullable: false),
                    RewardEarnWeek4 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetailEarnWeek", x => x.BankDetailEarnWeekId);
                    table.ForeignKey(
                        name: "FK_BankDetailEarnWeek_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankMonths",
                columns: table => new
                {
                    BankMonthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Saving = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    InvestMonth = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    DirectSell = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    InDirect = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    UniLevel = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    Binary = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    Matrix = table.Column<double>(type: "float", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankMonths", x => x.BankMonthId);
                    table.ForeignKey(
                        name: "FK_BankMonths_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankRequests",
                columns: table => new
                {
                    BankRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CashOut = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmCashout = table.Column<bool>(type: "bit", nullable: false),
                    Reinvest = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmReinvest = table.Column<bool>(type: "bit", nullable: false),
                    IsDeActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankRequests", x => x.BankRequestId);
                    table.ForeignKey(
                        name: "FK_BankRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankWeeks",
                columns: table => new
                {
                    BankWeekId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Saving = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    InvestWeek = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    DirectSell = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    InDirect = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    UniLevel = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    Binary = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    Matrix = table.Column<double>(type: "float", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankWeeks", x => x.BankWeekId);
                    table.ForeignKey(
                        name: "FK_BankWeeks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswer",
                columns: table => new
                {
                    UserAnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswer", x => x.UserAnswerId);
                    table.ForeignKey(
                        name: "FK_UserAnswer_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UR_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UR_Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAnswerId = table.Column<int>(type: "int", nullable: false),
                    Cryptocurrency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RatioCryptocurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyOrSell = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<double>(type: "float", nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_UserAnswer_UserAnswerId",
                        column: x => x.UserAnswerId,
                        principalTable: "UserAnswer",
                        principalColumn: "UserAnswerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserAnswerId",
                table: "Answers",
                column: "UserAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDetailEarn_UserId",
                table: "BankDetailEarn",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankDetailEarnWeek_UserId",
                table: "BankDetailEarnWeek",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankMonths_UserId",
                table: "BankMonths",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankRequests_UserId",
                table: "BankRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankWeeks_UserId",
                table: "BankWeeks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_UserId",
                table: "UserAnswer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "BankDetailEarn");

            migrationBuilder.DropTable(
                name: "BankDetailEarnWeek");

            migrationBuilder.DropTable(
                name: "BankMonths");

            migrationBuilder.DropTable(
                name: "BankRequests");

            migrationBuilder.DropTable(
                name: "BankWeeks");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserAnswer");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
