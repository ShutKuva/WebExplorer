using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Start : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntryPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    EntryPointId = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalogs_Catalogs_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Catalogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Catalogs_EntryPoints_EntryPointId",
                        column: x => x.EntryPointId,
                        principalTable: "EntryPoints",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "EntryPoints",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "db" });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "EntryPointId", "IsProcessed", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, 1, true, "Creating Digital Images", null },
                    { 2, null, true, "Resources", 1 },
                    { 3, null, true, "Evidence", 1 },
                    { 4, null, true, "Graphic products", 1 },
                    { 5, null, true, "Primary Sources", 2 },
                    { 6, null, true, "Secondary Sources", 2 },
                    { 7, null, true, "Process", 4 },
                    { 8, null, true, "Final Product", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_EntryPointId",
                table: "Catalogs",
                column: "EntryPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_ParentId",
                table: "Catalogs",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.DropTable(
                name: "EntryPoints");
        }
    }
}
