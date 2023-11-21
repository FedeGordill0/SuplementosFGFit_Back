using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuplementosFGFitBack.Migrations
{
    /// <inheritdoc />
    public partial class dbNewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    idcategoria = table.Column<int>(name: "id_categoria", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categoria", x => x.idcategoria);
                });

            migrationBuilder.CreateTable(
                name: "Estado_OrdenCompra",
                columns: table => new
                {
                    idestadoorden = table.Column<int>(name: "id_estado_orden", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_estado_Orden_Compra", x => x.idestadoorden);
                });

            migrationBuilder.CreateTable(
                name: "Formas_Envio",
                columns: table => new
                {
                    idformaenvio = table.Column<int>(name: "id_forma_envio", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    precio = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_forma_envio", x => x.idformaenvio);
                });

            migrationBuilder.CreateTable(
                name: "Formas_Pago",
                columns: table => new
                {
                    idformapago = table.Column<int>(name: "id_forma_pago", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: true),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    porcentaje = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_forma_pago", x => x.idformapago);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    idproveedor = table.Column<int>(name: "id_proveedor", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cuit = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    direccion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proveedor", x => x.idproveedor);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    idrol = table.Column<int>(name: "id_rol", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    rol = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rol", x => x.idrol);
                });

            migrationBuilder.CreateTable(
                name: "Unidades_Medida",
                columns: table => new
                {
                    idunidadmedida = table.Column<int>(name: "id_unidad_medida", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: true),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unidad_medida", x => x.idunidadmedida);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes_Compra",
                columns: table => new
                {
                    idordencompra = table.Column<int>(name: "id_orden_compra", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecharegistro = table.Column<DateTime>(name: "fecha_registro", type: "datetime", nullable: true),
                    idestadoorden = table.Column<int>(name: "id_estado_orden", type: "int", nullable: true),
                    idformaenvio = table.Column<int>(name: "id_forma_envio", type: "int", nullable: true),
                    idformapago = table.Column<int>(name: "id_forma_pago", type: "int", nullable: true),
                    idproveedor = table.Column<int>(name: "id_proveedor", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orden_compra", x => x.idordencompra);
                    table.ForeignKey(
                        name: "fk_orden_compra_estado_orden_compra",
                        column: x => x.idestadoorden,
                        principalTable: "Estado_OrdenCompra",
                        principalColumn: "id_estado_orden");
                    table.ForeignKey(
                        name: "fk_orden_compra_forma_envio",
                        column: x => x.idformaenvio,
                        principalTable: "Formas_Envio",
                        principalColumn: "id_forma_envio");
                    table.ForeignKey(
                        name: "fk_orden_compra_forma_pago",
                        column: x => x.idformapago,
                        principalTable: "Formas_Pago",
                        principalColumn: "id_forma_pago");
                    table.ForeignKey(
                        name: "fk_orden_compra_proveedor",
                        column: x => x.idproveedor,
                        principalTable: "Proveedores",
                        principalColumn: "id_proveedor");
                });

            migrationBuilder.CreateTable(
                name: "ProveedoresXFormaEnvio",
                columns: table => new
                {
                    idproveedorformaenvio = table.Column<int>(name: "id_proveedor_forma_envio", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idproveedor = table.Column<int>(name: "id_proveedor", type: "int", nullable: true),
                    idformaenvio = table.Column<int>(name: "id_forma_envio", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proveedor_forma_envio", x => x.idproveedorformaenvio);
                    table.ForeignKey(
                        name: "fk_proveedor_forma_envio_forma_envio",
                        column: x => x.idformaenvio,
                        principalTable: "Formas_Envio",
                        principalColumn: "id_forma_envio");
                    table.ForeignKey(
                        name: "fk_proveedor_forma_envio_proveedor",
                        column: x => x.idproveedor,
                        principalTable: "Proveedores",
                        principalColumn: "id_proveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProveedoresXFormaPago",
                columns: table => new
                {
                    idproveedorformapago = table.Column<int>(name: "id_proveedor_forma_pago", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idproveedor = table.Column<int>(name: "id_proveedor", type: "int", nullable: true),
                    idformapago = table.Column<int>(name: "id_forma_pago", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proveedor_forma_pago", x => x.idproveedorformapago);
                    table.ForeignKey(
                        name: "fk_proveedor_forma_pago_forma_pago",
                        column: x => x.idformapago,
                        principalTable: "Formas_Pago",
                        principalColumn: "id_forma_pago");
                    table.ForeignKey(
                        name: "fk_proveedor_forma_pago_proveedor",
                        column: x => x.idproveedor,
                        principalTable: "Proveedores",
                        principalColumn: "id_proveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idusuario = table.Column<int>(name: "id_usuario", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    idrol = table.Column<int>(name: "id_rol", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.idusuario);
                    table.ForeignKey(
                        name: "fk_usuario_rol",
                        column: x => x.idrol,
                        principalTable: "Roles",
                        principalColumn: "id_rol");
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    idproducto = table.Column<int>(name: "id_producto", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: true),
                    imagen = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    marca = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    idcategoria = table.Column<int>(name: "id_categoria", type: "int", nullable: true),
                    idunidadmedida = table.Column<int>(name: "id_unidad_medida", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_producto", x => x.idproducto);
                    table.ForeignKey(
                        name: "fk_producto_categoria",
                        column: x => x.idcategoria,
                        principalTable: "Categorias",
                        principalColumn: "id_categoria");
                    table.ForeignKey(
                        name: "fk_producto_unidad_medida",
                        column: x => x.idunidadmedida,
                        principalTable: "Unidades_Medida",
                        principalColumn: "id_unidad_medida");
                });

            migrationBuilder.CreateTable(
                name: "HistorialRefreshToken",
                columns: table => new
                {
                    IdHistorialToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idusuario = table.Column<int>(name: "id_usuario", type: "int", nullable: true),
                    Token = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    RefreshToken = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime", nullable: true),
                    EsActivo = table.Column<bool>(type: "bit", nullable: true, computedColumnSql: "(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", stored: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historia__03DC48A5E214801A", x => x.IdHistorialToken);
                    table.ForeignKey(
                        name: "FK__Historial__id_us__44FF419A",
                        column: x => x.idusuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "Detalle_Orden",
                columns: table => new
                {
                    iddetalle = table.Column<int>(name: "id_detalle", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidad = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    precio = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    idordencompra = table.Column<int>(name: "id_orden_compra", type: "int", nullable: true),
                    idproducto = table.Column<int>(name: "id_producto", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_detalle", x => x.iddetalle);
                    table.ForeignKey(
                        name: "fk_detalle_orden_compra",
                        column: x => x.idordencompra,
                        principalTable: "Ordenes_Compra",
                        principalColumn: "id_orden_compra");
                    table.ForeignKey(
                        name: "fk_detalle_producto",
                        column: x => x.idproducto,
                        principalTable: "Productos",
                        principalColumn: "id_producto");
                });

            migrationBuilder.CreateTable(
                name: "ProductosXProveedores",
                columns: table => new
                {
                    idproductoproveedor = table.Column<int>(name: "id_producto_proveedor", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<bool>(type: "bit", nullable: true),
                    precio = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    idproducto = table.Column<int>(name: "id_producto", type: "int", nullable: true),
                    idproveedor = table.Column<int>(name: "id_proveedor", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_producto_proveedor", x => x.idproductoproveedor);
                    table.ForeignKey(
                        name: "fk_producto_proveedor_producto",
                        column: x => x.idproducto,
                        principalTable: "Productos",
                        principalColumn: "id_producto");
                    table.ForeignKey(
                        name: "fk_producto_proveedor_proveedor",
                        column: x => x.idproveedor,
                        principalTable: "Proveedores",
                        principalColumn: "id_proveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Orden_id_orden_compra",
                table: "Detalle_Orden",
                column: "id_orden_compra");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Orden_id_producto",
                table: "Detalle_Orden",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialRefreshToken_id_usuario",
                table: "HistorialRefreshToken",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_Compra_id_estado_orden",
                table: "Ordenes_Compra",
                column: "id_estado_orden");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_Compra_id_forma_envio",
                table: "Ordenes_Compra",
                column: "id_forma_envio");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_Compra_id_forma_pago",
                table: "Ordenes_Compra",
                column: "id_forma_pago");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_Compra_id_proveedor",
                table: "Ordenes_Compra",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_id_categoria",
                table: "Productos",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_id_unidad_medida",
                table: "Productos",
                column: "id_unidad_medida");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosXProveedores_id_producto",
                table: "ProductosXProveedores",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosXProveedores_id_proveedor",
                table: "ProductosXProveedores",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedoresXFormaEnvio_id_forma_envio",
                table: "ProveedoresXFormaEnvio",
                column: "id_forma_envio");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedoresXFormaEnvio_id_proveedor",
                table: "ProveedoresXFormaEnvio",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedoresXFormaPago_id_forma_pago",
                table: "ProveedoresXFormaPago",
                column: "id_forma_pago");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedoresXFormaPago_id_proveedor",
                table: "ProveedoresXFormaPago",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_id_rol",
                table: "Usuarios",
                column: "id_rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalle_Orden");

            migrationBuilder.DropTable(
                name: "HistorialRefreshToken");

            migrationBuilder.DropTable(
                name: "ProductosXProveedores");

            migrationBuilder.DropTable(
                name: "ProveedoresXFormaEnvio");

            migrationBuilder.DropTable(
                name: "ProveedoresXFormaPago");

            migrationBuilder.DropTable(
                name: "Ordenes_Compra");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Estado_OrdenCompra");

            migrationBuilder.DropTable(
                name: "Formas_Envio");

            migrationBuilder.DropTable(
                name: "Formas_Pago");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Unidades_Medida");
        }
    }
}
