using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_list_back.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermisosRoles_Permisos_PermisosId",
                table: "PermisosRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_PermisosRoles_Roles_RolesId",
                table: "PermisosRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_TareasUsuarios_Tareas_TareasId",
                table: "TareasUsuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_TareasUsuarios_Usuarios_UsuariosId",
                table: "TareasUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_TareasUsuarios_TareasId",
                table: "TareasUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_TareasUsuarios_UsuariosId",
                table: "TareasUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_PermisosRoles_PermisosId",
                table: "PermisosRoles");

            migrationBuilder.DropIndex(
                name: "IX_PermisosRoles_RolesId",
                table: "PermisosRoles");

            migrationBuilder.DropColumn(
                name: "TareasId",
                table: "TareasUsuarios");

            migrationBuilder.DropColumn(
                name: "UsuariosId",
                table: "TareasUsuarios");

            migrationBuilder.DropColumn(
                name: "PermisosId",
                table: "PermisosRoles");

            migrationBuilder.DropColumn(
                name: "RolesId",
                table: "PermisosRoles");

            migrationBuilder.CreateIndex(
                name: "IX_TareasUsuarios_Id_Tareas_FK",
                table: "TareasUsuarios",
                column: "Id_Tareas_FK");

            migrationBuilder.CreateIndex(
                name: "IX_TareasUsuarios_Id_Usuarios_FK",
                table: "TareasUsuarios",
                column: "Id_Usuarios_FK");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosRoles_Id_Permisos_FK",
                table: "PermisosRoles",
                column: "Id_Permisos_FK");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosRoles_Id_Roles_FK",
                table: "PermisosRoles",
                column: "Id_Roles_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_PermisosRoles_Permisos_Id_Permisos_FK",
                table: "PermisosRoles",
                column: "Id_Permisos_FK",
                principalTable: "Permisos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermisosRoles_Roles_Id_Roles_FK",
                table: "PermisosRoles",
                column: "Id_Roles_FK",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TareasUsuarios_Tareas_Id_Tareas_FK",
                table: "TareasUsuarios",
                column: "Id_Tareas_FK",
                principalTable: "Tareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TareasUsuarios_Usuarios_Id_Usuarios_FK",
                table: "TareasUsuarios",
                column: "Id_Usuarios_FK",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermisosRoles_Permisos_Id_Permisos_FK",
                table: "PermisosRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_PermisosRoles_Roles_Id_Roles_FK",
                table: "PermisosRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_TareasUsuarios_Tareas_Id_Tareas_FK",
                table: "TareasUsuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_TareasUsuarios_Usuarios_Id_Usuarios_FK",
                table: "TareasUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_TareasUsuarios_Id_Tareas_FK",
                table: "TareasUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_TareasUsuarios_Id_Usuarios_FK",
                table: "TareasUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_PermisosRoles_Id_Permisos_FK",
                table: "PermisosRoles");

            migrationBuilder.DropIndex(
                name: "IX_PermisosRoles_Id_Roles_FK",
                table: "PermisosRoles");

            migrationBuilder.AddColumn<int>(
                name: "TareasId",
                table: "TareasUsuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuariosId",
                table: "TareasUsuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PermisosId",
                table: "PermisosRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RolesId",
                table: "PermisosRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TareasUsuarios_TareasId",
                table: "TareasUsuarios",
                column: "TareasId");

            migrationBuilder.CreateIndex(
                name: "IX_TareasUsuarios_UsuariosId",
                table: "TareasUsuarios",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosRoles_PermisosId",
                table: "PermisosRoles",
                column: "PermisosId");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosRoles_RolesId",
                table: "PermisosRoles",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermisosRoles_Permisos_PermisosId",
                table: "PermisosRoles",
                column: "PermisosId",
                principalTable: "Permisos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermisosRoles_Roles_RolesId",
                table: "PermisosRoles",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TareasUsuarios_Tareas_TareasId",
                table: "TareasUsuarios",
                column: "TareasId",
                principalTable: "Tareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TareasUsuarios_Usuarios_UsuariosId",
                table: "TareasUsuarios",
                column: "UsuariosId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
