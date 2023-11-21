using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuplementosFGFitBack.Migrations
{
    /// <inheritdoc />
    public partial class newEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_producto_proveedor_proveedor",
                table: "ProductosXProveedores");

            migrationBuilder.DropForeignKey(
                name: "fk_proveedor_forma_envio_proveedor",
                table: "ProveedoresXFormaEnvio");

            migrationBuilder.DropForeignKey(
                name: "fk_proveedor_forma_pago_proveedor",
                table: "ProveedoresXFormaPago");

            migrationBuilder.CreateTable(
                name: "UsuariosXRoles",
                columns: table => new
                {
                    idusuariorol = table.Column<int>(name: "id_usuario_rol", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idusuario = table.Column<int>(name: "id_usuario", type: "int", nullable: true),
                    idrol = table.Column<int>(name: "id_rol", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario_rol", x => x.idusuariorol);
                    table.ForeignKey(
                        name: "fk_usuario_rol_rol",
                        column: x => x.idrol,
                        principalTable: "Roles",
                        principalColumn: "id_rol");
                    table.ForeignKey(
                        name: "fk_usuario_rol_usuario",
                        column: x => x.idusuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosXRoles_id_rol",
                table: "UsuariosXRoles",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosXRoles_id_usuario",
                table: "UsuariosXRoles",
                column: "id_usuario");

            migrationBuilder.AddForeignKey(
                name: "fk_producto_proveedor_proveedor",
                table: "ProductosXProveedores",
                column: "id_proveedor",
                principalTable: "Proveedores",
                principalColumn: "id_proveedor");

            migrationBuilder.AddForeignKey(
                name: "fk_proveedor_forma_envio_proveedor",
                table: "ProveedoresXFormaEnvio",
                column: "id_proveedor",
                principalTable: "Proveedores",
                principalColumn: "id_proveedor");

            migrationBuilder.AddForeignKey(
                name: "fk_proveedor_forma_pago_proveedor",
                table: "ProveedoresXFormaPago",
                column: "id_proveedor",
                principalTable: "Proveedores",
                principalColumn: "id_proveedor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_producto_proveedor_proveedor",
                table: "ProductosXProveedores");

            migrationBuilder.DropForeignKey(
                name: "fk_proveedor_forma_envio_proveedor",
                table: "ProveedoresXFormaEnvio");

            migrationBuilder.DropForeignKey(
                name: "fk_proveedor_forma_pago_proveedor",
                table: "ProveedoresXFormaPago");

            migrationBuilder.DropTable(
                name: "UsuariosXRoles");

            migrationBuilder.AddForeignKey(
                name: "fk_producto_proveedor_proveedor",
                table: "ProductosXProveedores",
                column: "id_proveedor",
                principalTable: "Proveedores",
                principalColumn: "id_proveedor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_proveedor_forma_envio_proveedor",
                table: "ProveedoresXFormaEnvio",
                column: "id_proveedor",
                principalTable: "Proveedores",
                principalColumn: "id_proveedor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_proveedor_forma_pago_proveedor",
                table: "ProveedoresXFormaPago",
                column: "id_proveedor",
                principalTable: "Proveedores",
                principalColumn: "id_proveedor",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
