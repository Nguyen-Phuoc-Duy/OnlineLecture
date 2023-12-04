using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLecture.Migrations
{
    /// <inheritdoc />
    public partial class usersubjectmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSubjectModel",
                columns: table => new
                {
                    IdUserSubject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSubject = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubjectModel", x => x.IdUserSubject);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSubjectModel");
        }
    }
}
