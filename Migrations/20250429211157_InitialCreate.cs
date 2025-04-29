using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MD.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NameModels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardModels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SexType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardModels_NameModels_NameId",
                        column: x => x.NameId,
                        principalTable: "NameModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RowModels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RowModels_SchoolModel_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "SchoolModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModulModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowModelId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SchoolModelId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModulModel_RowModels_RowModelId",
                        column: x => x.RowModelId,
                        principalTable: "RowModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModulModel_SchoolModel_SchoolModelId",
                        column: x => x.SchoolModelId,
                        principalTable: "SchoolModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SchoolId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CardId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentModel_CardModels_CardId",
                        column: x => x.CardId,
                        principalTable: "CardModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentModel_RowModels_RowId",
                        column: x => x.RowId,
                        principalTable: "RowModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentModel_SchoolModel_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "SchoolModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeacherModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherModel_RowModels_RowId",
                        column: x => x.RowId,
                        principalTable: "RowModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModulModelStudentModel",
                columns: table => new
                {
                    ModulsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulModelStudentModel", x => new { x.ModulsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_ModulModelStudentModel_ModulModel_ModulsId",
                        column: x => x.ModulsId,
                        principalTable: "ModulModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModulModelStudentModel_StudentModel_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "StudentModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModulModelTeacherModel",
                columns: table => new
                {
                    ModulsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeachersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulModelTeacherModel", x => new { x.ModulsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_ModulModelTeacherModel_ModulModel_ModulsId",
                        column: x => x.ModulsId,
                        principalTable: "ModulModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModulModelTeacherModel_TeacherModel_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "TeacherModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolModelTeacherModel",
                columns: table => new
                {
                    SchoolsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeachersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolModelTeacherModel", x => new { x.SchoolsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_SchoolModelTeacherModel_SchoolModel_SchoolsId",
                        column: x => x.SchoolsId,
                        principalTable: "SchoolModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolModelTeacherModel_TeacherModel_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "TeacherModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentModelTeacherModel",
                columns: table => new
                {
                    StudentsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeachersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentModelTeacherModel", x => new { x.StudentsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_StudentModelTeacherModel_StudentModel_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "StudentModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentModelTeacherModel_TeacherModel_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "TeacherModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardModels_NameId",
                table: "CardModels",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_ModulModel_RowModelId",
                table: "ModulModel",
                column: "RowModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ModulModel_SchoolModelId",
                table: "ModulModel",
                column: "SchoolModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ModulModelStudentModel_StudentsId",
                table: "ModulModelStudentModel",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_ModulModelTeacherModel_TeachersId",
                table: "ModulModelTeacherModel",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_RowModels_SchoolId",
                table: "RowModels",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolModelTeacherModel_TeachersId",
                table: "SchoolModelTeacherModel",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModel_CardId",
                table: "StudentModel",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModel_RowId",
                table: "StudentModel",
                column: "RowId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModel_SchoolId",
                table: "StudentModel",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModelTeacherModel_TeachersId",
                table: "StudentModelTeacherModel",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherModel_RowId",
                table: "TeacherModel",
                column: "RowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModulModelStudentModel");

            migrationBuilder.DropTable(
                name: "ModulModelTeacherModel");

            migrationBuilder.DropTable(
                name: "SchoolModelTeacherModel");

            migrationBuilder.DropTable(
                name: "StudentModelTeacherModel");

            migrationBuilder.DropTable(
                name: "ModulModel");

            migrationBuilder.DropTable(
                name: "StudentModel");

            migrationBuilder.DropTable(
                name: "TeacherModel");

            migrationBuilder.DropTable(
                name: "CardModels");

            migrationBuilder.DropTable(
                name: "RowModels");

            migrationBuilder.DropTable(
                name: "NameModels");

            migrationBuilder.DropTable(
                name: "SchoolModel");
        }
    }
}
