using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KyrgyzTest.DAL.Migrations
{
    /// <inheritdoc />
    public partial class certmigrationsecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CertificateRecord",
                table: "CertificateRecord");

            migrationBuilder.RenameTable(
                name: "CertificateRecord",
                newName: "CertificateRecords");

            migrationBuilder.AlterColumn<string>(
                name: "CertificateNumber",
                table: "CertificateRecords",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CertificateRecords",
                table: "CertificateRecords",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CertificateRecords",
                table: "CertificateRecords");

            migrationBuilder.RenameTable(
                name: "CertificateRecords",
                newName: "CertificateRecord");

            migrationBuilder.AlterColumn<long>(
                name: "CertificateNumber",
                table: "CertificateRecord",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CertificateRecord",
                table: "CertificateRecord",
                column: "Id");
        }
    }
}
