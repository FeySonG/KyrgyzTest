using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KyrgyzTest.OldDbRegion.Migrations
{
    public partial class AddStudentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    CertificateNumber = table.Column<long>(
                        type: "bigint", nullable: false),

                    FirstName = table.Column<string>(
                        type: "nvarchar(200)", maxLength: 200, nullable: false),

                    LastName = table.Column<string>(
                        type: "nvarchar(200)", maxLength: 200, nullable: false),

                    MiddleName = table.Column<string>(
                        type: "nvarchar(200)", maxLength: 200, nullable: true),

                    Pin = table.Column<long>(
                        type: "bigint", nullable: false),

                    StudentId = table.Column<int>(
                        type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Students");
        }
    }
}