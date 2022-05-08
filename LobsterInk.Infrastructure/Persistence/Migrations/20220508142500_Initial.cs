using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LobsterInk.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adventures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adventures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdventureQuestions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdventureId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentNavigationId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdventureQuestions_AdventureQuestions_ParentNavigationId",
                        column: x => x.ParentNavigationId,
                        principalTable: "AdventureQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdventureQuestions_Adventures_AdventureId",
                        column: x => x.AdventureId,
                        principalTable: "Adventures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAdventureQuestionsHistory",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdventureQuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdventureQuestionsHistory", x => new { x.UserId, x.AdventureQuestionId });
                    table.ForeignKey(
                        name: "FK_UserAdventureQuestionsHistory_AdventureQuestions_AdventureQuestionId",
                        column: x => x.AdventureQuestionId,
                        principalTable: "AdventureQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdventureQuestions_AdventureId",
                table: "AdventureQuestions",
                column: "AdventureId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureQuestions_ParentNavigationId",
                table: "AdventureQuestions",
                column: "ParentNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdventureQuestionsHistory_AdventureQuestionId",
                table: "UserAdventureQuestionsHistory",
                column: "AdventureQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAdventureQuestionsHistory");

            migrationBuilder.DropTable(
                name: "AdventureQuestions");

            migrationBuilder.DropTable(
                name: "Adventures");
        }
    }
}
