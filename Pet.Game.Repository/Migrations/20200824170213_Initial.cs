using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pet.Game.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    HappinessInterval = table.Column<int>(nullable: false),
                    HungrinessInterval = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    HungrinessStatus = table.Column<int>(nullable: false),
                    HappinessStatus = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    TypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_PetTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PetTypes",
                columns: new[] { "Id", "Created", "HappinessInterval", "HungrinessInterval", "LastModified", "Name" },
                values: new object[,]
                {
                    { new Guid("93d124c5-7669-45ec-ba23-10d125ad8f1a"), new DateTime(2020, 8, 24, 17, 2, 13, 483, DateTimeKind.Utc).AddTicks(3631), 1, 1, new DateTime(2020, 8, 24, 17, 2, 13, 483, DateTimeKind.Utc).AddTicks(4062), "Cats" },
                    { new Guid("07fbf68d-0823-45a3-8e39-3b10dfa8984d"), new DateTime(2020, 8, 24, 17, 2, 13, 484, DateTimeKind.Utc).AddTicks(6170), 1, 1, new DateTime(2020, 8, 24, 17, 2, 13, 484, DateTimeKind.Utc).AddTicks(6173), "Dogs" },
                    { new Guid("630660ca-4af6-49bc-bbc5-b425475f4326"), new DateTime(2020, 8, 24, 17, 2, 13, 484, DateTimeKind.Utc).AddTicks(6442), 1, 1, new DateTime(2020, 8, 24, 17, 2, 13, 484, DateTimeKind.Utc).AddTicks(6442), "Birds" },
                    { new Guid("37804ed4-d0d3-4058-82fc-ae9c37b19dba"), new DateTime(2020, 8, 24, 17, 2, 13, 484, DateTimeKind.Utc).AddTicks(6466), 1, 1, new DateTime(2020, 8, 24, 17, 2, 13, 484, DateTimeKind.Utc).AddTicks(6466), "Reptiles" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_TypeId",
                table: "Pets",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_UserId",
                table: "Pets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "PetTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
