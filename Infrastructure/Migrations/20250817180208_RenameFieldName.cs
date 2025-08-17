using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeleteAt",
                table: "Topics",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "DeleteAt",
                table: "Relationships",
                newName: "DeletedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Topics",
                newName: "DeleteAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Relationships",
                newName: "DeleteAt");
        }
    }
}
