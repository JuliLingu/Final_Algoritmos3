using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_list_back.Migrations
{
    /// <inheritdoc />
    public partial class DropTableTareaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TareasUsuarios");

            migrationBuilder.AddColumn<int>(
                name: "Id_Usuarios_FK",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_Id_Usuarios_FK",
                table: "Tareas",
                column: "Id_Usuarios_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuarios_Id_Usuarios_FK",
                table: "Tareas",
                column: "Id_Usuarios_FK",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuarios_Id_Usuarios_FK",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_Id_Usuarios_FK",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Id_Usuarios_FK",
                table: "Tareas");

            migrationBuilder.CreateTable(
                name: "TareasUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Tareas_FK = table.Column<int>(type: "int", nullable: false),
                    Id_Usuarios_FK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TareasUsuarios_Tareas_Id_Tareas_FK",
                        column: x => x.Id_Tareas_FK,
                        principalTable: "Tareas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareasUsuarios_Usuarios_Id_Usuarios_FK",
                        column: x => x.Id_Usuarios_FK,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TareasUsuarios_Id_Tareas_FK",
                table: "TareasUsuarios",
                column: "Id_Tareas_FK");

            migrationBuilder.CreateIndex(
                name: "IX_TareasUsuarios_Id_Usuarios_FK",
                table: "TareasUsuarios",
                column: "Id_Usuarios_FK");
        }
    }
}
