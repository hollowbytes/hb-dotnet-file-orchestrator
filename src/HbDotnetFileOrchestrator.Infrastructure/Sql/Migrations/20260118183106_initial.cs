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
            migrationBuilder.CreateTable(
                name: "StorageType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StorageTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageRule_StorageType_StorageTypeId",
                        column: x => x.StorageTypeId,
                        principalTable: "StorageType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorageRule_Name",
                table: "StorageRule",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageRule_StorageTypeId",
                table: "StorageRule",
                column: "StorageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageType_Name",
                table: "StorageType",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorageRule");

            migrationBuilder.DropTable(
                name: "StorageType");
        }
    }
}
