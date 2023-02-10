using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace markdown.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Markdowns",
                columns: table => new
                {
                    MarkdownId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markdowns", x => x.MarkdownId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Markdowns",
                columns: new[] { "MarkdownId", "Content", "DateTime", "UserId" },
                values: new object[] { new Guid("c2792f45-821e-4481-a129-fa6baf8a8b9e"), "# Hey, How are you doing?", new DateTime(2023, 1, 21, 19, 31, 35, 225, DateTimeKind.Local).AddTicks(8480), new Guid("7663a9bf-8b7b-46e2-806e-8802749ff2a0") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "UserName" },
                values: new object[] { new Guid("7663a9bf-8b7b-46e2-806e-8802749ff2a0"), "email@email.com", "password", "nassdaman" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Markdowns");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
