using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParaTipi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DövizTipi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParaTipi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bakiye",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ParaTipiId = table.Column<int>(type: "int", nullable: false),
                    ParaMiktarı = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bakiye", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bakiye_ParaTipi_ParaTipiId",
                        column: x => x.ParaTipiId,
                        principalTable: "ParaTipi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bakiye_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinList_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isBuy = table.Column<bool>(type: "bit", nullable: false),
                    isSell = table.Column<bool>(type: "bit", nullable: false),
                    Count = table.Column<double>(type: "float", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaitingTrades = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trades_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ParaTipi",
                columns: new[] { "Id", "DövizTipi" },
                values: new object[,]
                {
                    { 1, "TL" },
                    { 2, "USDT" },
                    { 3, "BTC" },
                    { 4, "ETH" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 1L, "eray@mail.com", "Eray", "Türemiş", "1" },
                    { 2L, "erdal@mail.com", "Erdal", "Türemiş", "2" }
                });

            migrationBuilder.InsertData(
                table: "Bakiye",
                columns: new[] { "Id", "ParaMiktarı", "ParaTipiId", "UserId" },
                values: new object[,]
                {
                    { 1, 10000000m, 1, 1L },
                    { 2, 10000000m, 2, 1L },
                    { 3, 10000000m, 1, 2L },
                    { 4, 10000000m, 2, 2L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bakiye_ParaTipiId",
                table: "Bakiye",
                column: "ParaTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_Bakiye_UserId",
                table: "Bakiye",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinList_UserId",
                table: "CoinList",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_UserId",
                table: "Trades",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bakiye");

            migrationBuilder.DropTable(
                name: "CoinList");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "ParaTipi");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
