using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mocan_Melisa_MariaMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Breed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnimalType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeMonths = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vaccinated = table.Column<bool>(type: "bit", nullable: false),
                    HealthCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeInShelterDays = table.Column<int>(type: "int", nullable: false),
                    BreedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pet_Breed_BreedId",
                        column: x => x.BreedId,
                        principalTable: "Breed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdoptionRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesiredPetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinAgeMonths = table.Column<int>(type: "int", nullable: true),
                    MaxAgeMonths = table.Column<int>(type: "int", nullable: true),
                    DesiredSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdoptionRequest_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdoptionRequest_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donation_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequest_PetId",
                table: "AdoptionRequest",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequest_UserId",
                table: "AdoptionRequest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_PetId",
                table: "Donation",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_UserId",
                table: "Donation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_BreedId",
                table: "Pet",
                column: "BreedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdoptionRequest");

            migrationBuilder.DropTable(
                name: "Donation");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Breed");
        }
    }
}
