using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MD.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardModels_NameModels_NameId",
                table: "CardModels");

            migrationBuilder.DropIndex(
                name: "IX_CardModels_NameId",
                table: "CardModels");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "StudentModel");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "CardModels");

            migrationBuilder.DropColumn(
                name: "SexType",
                table: "CardModels");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "StudentModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NameId",
                table: "StudentModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SexType",
                table: "StudentModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Academic",
                table: "CardModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RowId",
                table: "CardModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolId",
                table: "CardModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stage",
                table: "CardModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "CardModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentModel_NameId",
                table: "StudentModel",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentModel_NameModels_NameId",
                table: "StudentModel",
                column: "NameId",
                principalTable: "NameModels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentModel_NameModels_NameId",
                table: "StudentModel");

            migrationBuilder.DropIndex(
                name: "IX_StudentModel_NameId",
                table: "StudentModel");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "StudentModel");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "StudentModel");

            migrationBuilder.DropColumn(
                name: "SexType",
                table: "StudentModel");

            migrationBuilder.DropColumn(
                name: "Academic",
                table: "CardModels");

            migrationBuilder.DropColumn(
                name: "RowId",
                table: "CardModels");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "CardModels");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "CardModels");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "CardModels");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StudentModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameId",
                table: "CardModels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SexType",
                table: "CardModels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardModels_NameId",
                table: "CardModels",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardModels_NameModels_NameId",
                table: "CardModels",
                column: "NameId",
                principalTable: "NameModels",
                principalColumn: "Id");
        }
    }
}
