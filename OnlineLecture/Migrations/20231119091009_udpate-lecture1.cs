using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLecture.Migrations
{
    /// <inheritdoc />
    public partial class udpatelecture1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubjectLectureModel",
                columns: table => new
                {
                    IdSubjectLecture = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSubject = table.Column<int>(type: "int", nullable: false),
                    IdLecture = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectLectureModel", x => x.IdSubjectLecture);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectLectureModel");
        }
    }
}
