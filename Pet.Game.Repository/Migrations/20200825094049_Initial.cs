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
                    UserId = table.Column<Guid>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PetTypes",
                columns: new[] { "Id", "Created", "HappinessInterval", "HungrinessInterval", "LastModified", "Name" },
                values: new object[,]
                {
                    { new Guid("6493657c-81cb-4a85-b71a-3ce8a7d2d312"), new DateTime(2020, 8, 25, 9, 40, 48, 186, DateTimeKind.Utc).AddTicks(51), 1, 1, new DateTime(2020, 8, 25, 9, 40, 48, 186, DateTimeKind.Utc).AddTicks(1409), "Cats" },
                    { new Guid("dc3eb6fc-a47c-41a2-8367-1de4834574a1"), new DateTime(2020, 8, 25, 9, 40, 48, 190, DateTimeKind.Utc).AddTicks(4408), 1, 1, new DateTime(2020, 8, 25, 9, 40, 48, 190, DateTimeKind.Utc).AddTicks(4451), "Dogs" },
                    { new Guid("5d676e75-af7d-4925-9991-cca8973afac7"), new DateTime(2020, 8, 25, 9, 40, 48, 190, DateTimeKind.Utc).AddTicks(5217), 1, 1, new DateTime(2020, 8, 25, 9, 40, 48, 190, DateTimeKind.Utc).AddTicks(5223), "Birds" },
                    { new Guid("d5426c61-0bb7-47cd-85a2-df74dca3ddc3"), new DateTime(2020, 8, 25, 9, 40, 48, 190, DateTimeKind.Utc).AddTicks(5295), 1, 1, new DateTime(2020, 8, 25, 9, 40, 48, 190, DateTimeKind.Utc).AddTicks(5299), "Reptiles" }
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
