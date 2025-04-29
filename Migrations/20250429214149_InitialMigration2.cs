using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MD.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardModels_NameModels_NameId",
                table: "CardModels");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulModel_RowModels_RowModelId",
                table: "ModulModel");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherModel_RowModels_RowModelId",
                table: "TeacherModel");

            migrationBuilder.DropIndex(
                name: "IX_ModulModel_RowModelId",
                table: "ModulModel");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TeacherModel");

            migrationBuilder.DropColumn(
                name: "RowId",
                table: "TeacherModel");

            migrationBuilder.DropColumn(
                name: "RowName",
                table: "TeacherModel");

            migrationBuilder.DropColumn(
                name: "RowModelId",
                table: "ModulModel");

            migrationBuilder.DropColumn(
                name: "RowName",
                table: "ModulModel");

            migrationBuilder.RenameColumn(
                name: "RowModelId",
                table: "TeacherModel",
                newName: "NameId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherModel_RowModelId",
                table: "TeacherModel",
                newName: "IX_TeacherModel_NameId");

            migrationBuilder.AlterColumn<string>(
                name: "RowId",
                table: "ModulModel",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NameId",
                table: "CardModels",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "RowModelTeacherModel",
                columns: table => new
                {
                    RowsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeachersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowModelTeacherModel", x => new { x.RowsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_RowModelTeacherModel_RowModels_RowsId",
                        column: x => x.RowsId,
                        principalTable: "RowModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RowModelTeacherModel_TeacherModel_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "TeacherModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModulModel_RowId",
                table: "ModulModel",
                column: "RowId");

            migrationBuilder.CreateIndex(
                name: "IX_RowModelTeacherModel_TeachersId",
                table: "RowModelTeacherModel",
                column: "TeachersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardModels_NameModels_NameId",
                table: "CardModels",
                column: "NameId",
                principalTable: "NameModels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModulModel_RowModels_RowId",
                table: "ModulModel",
                column: "RowId",
                principalTable: "RowModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherModel_NameModels_NameId",
                table: "TeacherModel",
                column: "NameId",
                principalTable: "NameModels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardModels_NameModels_NameId",
                table: "CardModels");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulModel_RowModels_RowId",
                table: "ModulModel");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherModel_NameModels_NameId",
                table: "TeacherModel");

            migrationBuilder.DropTable(
                name: "RowModelTeacherModel");

            migrationBuilder.DropIndex(
                name: "IX_ModulModel_RowId",
                table: "ModulModel");

            migrationBuilder.RenameColumn(
                name: "NameId",
                table: "TeacherModel",
                newName: "RowModelId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherModel_NameId",
                table: "TeacherModel",
                newName: "IX_TeacherModel_RowModelId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TeacherModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RowId",
                table: "TeacherModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RowName",
                table: "TeacherModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RowId",
                table: "ModulModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "RowModelId",
                table: "ModulModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RowName",
                table: "ModulModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "NameId",
                table: "CardModels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModulModel_RowModelId",
                table: "ModulModel",
                column: "RowModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardModels_NameModels_NameId",
                table: "CardModels",
                column: "NameId",
                principalTable: "NameModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulModel_RowModels_RowModelId",
                table: "ModulModel",
                column: "RowModelId",
                principalTable: "RowModels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherModel_RowModels_RowModelId",
                table: "TeacherModel",
                column: "RowModelId",
                principalTable: "RowModels",
                principalColumn: "Id");
        }
    }
}
