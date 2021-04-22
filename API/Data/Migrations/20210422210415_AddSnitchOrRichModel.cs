using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddSnitchOrRichModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FamilyId",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SnitchDeadlineHours = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PublicId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RichEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetId = table.Column<int>(type: "INTEGER", nullable: true),
                    PhotoId = table.Column<int>(type: "INTEGER", nullable: false),
                    FamilyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: false),
                    Completed = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RichEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RichEntries_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RichEntries_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RichEntries_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RichEntries_Users_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Snitches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetId = table.Column<int>(type: "INTEGER", nullable: true),
                    PhotoId = table.Column<int>(type: "INTEGER", nullable: false),
                    FamilyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RichId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snitches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Snitches_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Snitches_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Snitches_RichEntries_RichId",
                        column: x => x.RichId,
                        principalTable: "RichEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Snitches_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Snitches_Users_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SnitchPolls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SnitchId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnitchPolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnitchPolls_Snitches_SnitchId",
                        column: x => x.SnitchId,
                        principalTable: "Snitches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SnitchPollVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Points = table.Column<int>(type: "INTEGER", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    SnitchPollId = table.Column<int>(type: "INTEGER", nullable: false),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnitchPollVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnitchPollVotes_SnitchPolls_SnitchPollId",
                        column: x => x.SnitchPollId,
                        principalTable: "SnitchPolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SnitchPollVotes_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_FamilyId",
                table: "Users",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RichEntries_CreatorId",
                table: "RichEntries",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RichEntries_FamilyId",
                table: "RichEntries",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_RichEntries_PhotoId",
                table: "RichEntries",
                column: "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RichEntries_TargetId",
                table: "RichEntries",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Snitches_CreatorId",
                table: "Snitches",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Snitches_FamilyId",
                table: "Snitches",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Snitches_PhotoId",
                table: "Snitches",
                column: "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Snitches_RichId",
                table: "Snitches",
                column: "RichId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Snitches_TargetId",
                table: "Snitches",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_SnitchPolls_SnitchId",
                table: "SnitchPolls",
                column: "SnitchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SnitchPollVotes_AppUserId",
                table: "SnitchPollVotes",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SnitchPollVotes_SnitchPollId",
                table: "SnitchPollVotes",
                column: "SnitchPollId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Families_FamilyId",
                table: "Users",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Families_FamilyId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "SnitchPollVotes");

            migrationBuilder.DropTable(
                name: "SnitchPolls");

            migrationBuilder.DropTable(
                name: "Snitches");

            migrationBuilder.DropTable(
                name: "RichEntries");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Users_FamilyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "Users");
        }
    }
}
