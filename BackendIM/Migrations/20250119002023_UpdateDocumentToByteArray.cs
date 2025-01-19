using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendIM.Migrations
{
    public partial class UpdateDocumentToByteArray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Add a temporary column to hold the converted data
            migrationBuilder.AddColumn<byte[]>(
                name: "DocumentTemp",
                table: "Documents",
                type: "varbinary(max)",
                nullable: true);

            // Step 2: Copy and convert data from the old column to the new temporary column
            migrationBuilder.Sql(@"
                UPDATE Documents
                SET DocumentTemp = CONVERT(varbinary(max), Document)
            ");

            // Step 3: Drop the old column
            migrationBuilder.DropColumn(
                name: "Document",
                table: "Documents");

            // Step 4: Rename the temporary column to the original column name
            migrationBuilder.RenameColumn(
                name: "DocumentTemp",
                table: "Documents",
                newName: "Document");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse process: Change varbinary back to nvarchar
            migrationBuilder.AddColumn<string>(
                name: "DocumentTemp",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE Documents
                SET DocumentTemp = CONVERT(nvarchar(max), Document)
            ");

            migrationBuilder.DropColumn(
                name: "Document",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "DocumentTemp",
                table: "Documents",
                newName: "Document");
        }
    }
}