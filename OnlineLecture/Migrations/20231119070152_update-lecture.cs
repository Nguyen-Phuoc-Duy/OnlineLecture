using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLecture.Migrations
{
    /// <inheritdoc />
    public partial class updatelecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectLectureModel");

            migrationBuilder.AddColumn<int>(
                name: "IdSubject",
                table: "LectureModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdSubject",
                table: "LectureModel");

            migrationBuilder.CreateTable(
                name: "SubjectLectureModel",
                columns: table => new
                {
                    IdSubjectLecture = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLecture = table.Column<int>(type: "int", nullable: false),
                    IdSubject = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectLectureModel", x => x.IdSubjectLecture);
                });
        }
    }
}
