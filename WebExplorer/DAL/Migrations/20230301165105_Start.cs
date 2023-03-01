using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Start : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogsInCatalogs",
                columns: table => new
                {
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogsInCatalogs", x => new { x.ParentId, x.ChildId });
                    table.ForeignKey(
                        name: "FK_CatalogsInCatalogs_Catalogs_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogsInCatalogs_Catalogs_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Catalogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogsInCatalogs_ChildId",
                table: "CatalogsInCatalogs",
                column: "ChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogsInCatalogs");

            migrationBuilder.DropTable(
                name: "Catalogs");
        }
    }
}
