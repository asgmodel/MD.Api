using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MD.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RowModels_SchoolModel_SchoolId",
                table: "RowModels");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolModelTeacherModel_SchoolModel_SchoolsId",
                table: "SchoolModelTeacherModel");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherModel_RowModels_RowId",
                table: "TeacherModel");

            migrationBuilder.DropIndex(
                name: "IX_TeacherModel_RowId",
                table: "TeacherModel");

            migrationBuilder.RenameColumn(
                name: "SchoolsId",
                table: "SchoolModelTeacherModel",
                newName: "SchoolModelsId");

            migrationBuilder.AlterColumn<string>(
                name: "RowId",
                table: "TeacherModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RowModelId",
                table: "TeacherModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RowName",
                table: "TeacherModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SchoolId",
                table: "RowModels",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "RowId",
                table: "RowModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RowName",
                table: "RowModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherModel_RowModelId",
                table: "TeacherModel",
                column: "RowModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RowModels_SchoolModel_SchoolId",
                table: "RowModels",
                column: "SchoolId",
                principalTable: "SchoolModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolModelTeacherModel_SchoolModel_SchoolModelsId",
                table: "SchoolModelTeacherModel",
                column: "SchoolModelsId",
                principalTable: "SchoolModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherModel_RowModels_RowModelId",
                table: "TeacherModel",
                column: "RowModelId",
                principalTable: "RowModels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RowModels_SchoolModel_SchoolId",
                table: "RowModels");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolModelTeacherModel_SchoolModel_SchoolModelsId",
                table: "SchoolModelTeacherModel");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherModel_RowModels_RowModelId",
                table: "TeacherModel");

            migrationBuilder.DropIndex(
                name: "IX_TeacherModel_RowModelId",
                table: "TeacherModel");

            migrationBuilder.DropColumn(
                name: "RowModelId",
                table: "TeacherModel");

            migrationBuilder.DropColumn(
                name: "RowName",
                table: "TeacherModel");

            migrationBuilder.DropColumn(
                name: "RowId",
                table: "RowModels");

            migrationBuilder.DropColumn(
                name: "RowName",
                table: "RowModels");

            migrationBuilder.RenameColumn(
                name: "SchoolModelsId",
                table: "SchoolModelTeacherModel",
                newName: "SchoolsId");

            migrationBuilder.AlterColumn<string>(
                name: "RowId",
                table: "TeacherModel",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SchoolId",
                table: "RowModels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherModel_RowId",
                table: "TeacherModel",
                column: "RowId");

            migrationBuilder.AddForeignKey(
                name: "FK_RowModels_SchoolModel_SchoolId",
                table: "RowModels",
                column: "SchoolId",
                principalTable: "SchoolModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolModelTeacherModel_SchoolModel_SchoolsId",
                table: "SchoolModelTeacherModel",
                column: "SchoolsId",
                principalTable: "SchoolModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherModel_RowModels_RowId",
                table: "TeacherModel",
                column: "RowId",
                principalTable: "RowModels",
                principalColumn: "Id");
        }
    }
}
