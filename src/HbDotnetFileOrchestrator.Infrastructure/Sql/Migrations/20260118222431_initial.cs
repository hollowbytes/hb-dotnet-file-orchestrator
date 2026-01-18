using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "FileDestinationBaseSequence");

            migrationBuilder.CreateTable(
                name: "StorageRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Expression = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageRule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileSystemStorage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [FileDestinationBaseSequence]"),
                    RuleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSystemStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileSystemStorage_StorageRule_RuleId",
                        column: x => x.RuleId,
                        principalTable: "StorageRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileSystemStorage_Name",
                table: "FileSystemStorage",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileSystemStorage_RuleId",
                table: "FileSystemStorage",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageRule_Name",
                table: "StorageRule",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileSystemStorage");

            migrationBuilder.DropTable(
                name: "StorageRule");

            migrationBuilder.DropSequence(
                name: "FileDestinationBaseSequence");
        }
    }
}
