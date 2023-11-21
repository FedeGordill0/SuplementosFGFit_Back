﻿//// <auto-generated />
//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
//using SuplementosFGFit_Back.Models;

//#nullable disable

//namespace SuplementosFGFitBack.Migrations
//{
//    [DbContext(typeof(SuplementosFgfitContext))]
//    [Migration("20231023231246_dbNewMigration")]
//    partial class dbNewMigration
//    {
//        /// <inheritdoc />
//        protected override void BuildTargetModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("ProductVersion", "7.0.0")
//                .HasAnnotation("Relational:MaxIdentifierLength", 128);

//            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.Categoria", b =>
//                {
//                    b.Property<int>("IdCategoria")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_categoria");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategoria"));

//                    b.Property<string>("Descripcion")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(100)")
//                        .HasColumnName("descripcion");

//                    b.Property<bool>("Estado")
//                        .HasColumnType("bit")
//                        .HasColumnName("estado");

//                    b.Property<string>("Nombre")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(50)")
//                        .HasColumnName("nombre");

//                    b.HasKey("IdCategoria")
//                        .HasName("pk_categoria");

//                    b.ToTable("Categorias");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.DetalleOrden", b =>
//                {
//                    b.Property<int>("IdDetalle")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_detalle");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetalle"));

//                    b.Property<decimal?>("Cantidad")
//                        .HasColumnType("numeric(18, 0)")
//                        .HasColumnName("cantidad");

//                    b.Property<int?>("IdOrdenCompra")
//                        .HasColumnType("int")
//                        .HasColumnName("id_orden_compra");

//                    b.Property<int?>("IdProducto")
//                        .HasColumnType("int")
//                        .HasColumnName("id_producto");

//                    b.Property<decimal?>("Precio")
//                        .HasColumnType("numeric(18, 0)")
//                        .HasColumnName("precio");

//                    b.HasKey("IdDetalle")
//                        .HasName("pk_detalle");

//                    b.HasIndex("IdOrdenCompra");

//                    b.HasIndex("IdProducto");

//                    b.ToTable("Detalle_Orden", (string)null);
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.EstadoOrdenCompra", b =>
//                {
//                    b.Property<int>("IdEstadoOrden")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_estado_orden");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEstadoOrden"));

//                    b.Property<bool?>("Estado")
//                        .HasColumnType("bit")
//                        .HasColumnName("estado");

//                    b.HasKey("IdEstadoOrden")
//                        .HasName("pk_estado_Orden_Compra");

//                    b.ToTable("Estado_OrdenCompra", (string)null);
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.FormasEnvio", b =>
//                {
//                    b.Property<int>("IdFormaEnvio")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_forma_envio");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFormaEnvio"));

//                    b.Property<string>("Descripcion")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(100)")
//                        .HasColumnName("descripcion");

//                    b.Property<bool>("Estado")
//                        .HasColumnType("bit")
//                        .HasColumnName("estado");

//                    b.Property<string>("Nombre")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(100)")
//                        .HasColumnName("nombre");

//                    b.Property<decimal>("Precio")
//                        .HasColumnType("numeric(18, 0)")
//                        .HasColumnName("precio");

//                    b.HasKey("IdFormaEnvio")
//                        .HasName("pk_forma_envio");

//                    b.ToTable("Formas_Envio", (string)null);
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.FormasPago", b =>
//                {
//                    b.Property<int>("IdFormaPago")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_forma_pago");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFormaPago"));

//                    b.Property<string>("Descripcion")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(100)")
//                        .HasColumnName("descripcion");

//                    b.Property<bool?>("Estado")
//                        .HasColumnType("bit")
//                        .HasColumnName("estado");

//                    b.Property<string>("Nombre")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(100)")
//                        .HasColumnName("nombre");

//                    b.Property<decimal>("Porcentaje")
//                        .HasColumnType("numeric(18, 0)")
//                        .HasColumnName("porcentaje");

//                    b.HasKey("IdFormaPago")
//                        .HasName("pk_forma_pago");

//                    b.ToTable("Formas_Pago", (string)null);
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.HistorialRefreshToken", b =>
//                {
//                    b.Property<int>("IdHistorialToken")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHistorialToken"));

//                    b.Property<bool?>("EsActivo")
//                        .ValueGeneratedOnAddOrUpdate()
//                        .HasColumnType("bit")
//                        .HasComputedColumnSql("(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);

//                    b.Property<DateTime?>("FechaCreacion")
//                        .HasColumnType("datetime");

//                    b.Property<DateTime?>("FechaExpiracion")
//                        .HasColumnType("datetime");

//                    b.Property<int?>("IdUsuario")
//                        .HasColumnType("int")
//                        .HasColumnName("id_usuario");

//                    b.Property<string>("RefreshToken")
//                        .HasMaxLength(200)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(200)");

//                    b.Property<string>("Token")
//                        .HasMaxLength(500)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(500)");

//                    b.HasKey("IdHistorialToken")
//                        .HasName("PK__Historia__03DC48A5E214801A");

//                    b.HasIndex("IdUsuario");

//                    b.ToTable("HistorialRefreshToken", (string)null);
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.OrdenesCompra", b =>
//                {
//                    b.Property<int>("IdOrdenCompra")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_orden_compra");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdOrdenCompra"));

//                    b.Property<DateTime?>("FechaRegistro")
//                        .HasColumnType("datetime")
//                        .HasColumnName("fecha_registro");

//                    b.Property<int?>("IdEstadoOrden")
//                        .HasColumnType("int")
//                        .HasColumnName("id_estado_orden");

//                    b.Property<int?>("IdFormaEnvio")
//                        .HasColumnType("int")
//                        .HasColumnName("id_forma_envio");

//                    b.Property<int?>("IdFormaPago")
//                        .HasColumnType("int")
//                        .HasColumnName("id_forma_pago");

//                    b.Property<int?>("IdProveedor")
//                        .HasColumnType("int")
//                        .HasColumnName("id_proveedor");

//                    b.HasKey("IdOrdenCompra")
//                        .HasName("pk_orden_compra");

//                    b.HasIndex("IdEstadoOrden");

//                    b.HasIndex("IdFormaEnvio");

//                    b.HasIndex("IdFormaPago");

//                    b.HasIndex("IdProveedor");

//                    b.ToTable("Ordenes_Compra", (string)null);
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.Producto", b =>
//                {
//                    b.Property<int>("IdProducto")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_producto");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProducto"));

//                    b.Property<string>("Descripcion")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(100)")
//                        .HasColumnName("descripcion");

//                    b.Property<bool?>("Estado")
//                        .HasColumnType("bit")
//                        .HasColumnName("estado");

//                    b.Property<int?>("IdCategoria")
//                        .HasColumnType("int")
//                        .HasColumnName("id_categoria");

//                    b.Property<int?>("IdUnidadMedida")
//                        .HasColumnType("int")
//                        .HasColumnName("id_unidad_medida");

//                    b.Property<string>("Imagen")
//                        .IsRequired()
//                        .HasMaxLength(1000)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(1000)")
//                        .HasColumnName("imagen");

//                    b.Property<string>("Marca")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(100)")
//                        .HasColumnName("marca");

//                    b.Property<string>("Nombre")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(100)")
//                        .HasColumnName("nombre");

//                    b.HasKey("IdProducto")
//                        .HasName("pk_producto");

//                    b.HasIndex("IdCategoria");

//                    b.HasIndex("IdUnidadMedida");

//                    b.ToTable("Productos");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.ProductosXproveedore", b =>
//                {
//                    b.Property<int>("IdProductoProveedor")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_producto_proveedor");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProductoProveedor"));

//                    b.Property<bool?>("Estado")
//                        .HasColumnType("bit")
//                        .HasColumnName("estado");

//                    b.Property<int?>("IdProducto")
//                        .HasColumnType("int")
//                        .HasColumnName("id_producto");

//                    b.Property<int?>("IdProveedor")
//                        .HasColumnType("int")
//                        .HasColumnName("id_proveedor");

//                    b.Property<decimal?>("Precio")
//                        .HasColumnType("numeric(18, 0)")
//                        .HasColumnName("precio");

//                    b.HasKey("IdProductoProveedor")
//                        .HasName("pk_producto_proveedor");

//                    b.HasIndex("IdProducto");

//                    b.HasIndex("IdProveedor");

//                    b.ToTable("ProductosXProveedores", (string)null);
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.Proveedore", b =>
//                {
//                    b.Property<int>("IdProveedor")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_proveedor");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProveedor"));

//                    b.Property<string>("Cuit")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(50)")
//                        .HasColumnName("cuit");

//                    b.Property<string>("Direccion")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(50)")
//                        .HasColumnName("direccion");

//                    b.Property<string>("Email")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(100)")
//                        .HasColumnName("email");

//                    b.Property<bool?>("Estado")
//                        .HasColumnType("bit")
//                        .HasColumnName("estado");

//                    b.Property<string>("Nombre")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(50)")
//                        .HasColumnName("nombre");

//                    b.Property<string>("Telefono")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(50)")
//                        .HasColumnName("telefono");

//                    b.HasKey("IdProveedor")
//                        .HasName("pk_proveedor");

//                    b.ToTable("Proveedores");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.ProveedoresXformaEnvio", b =>
//                {
//                    b.Property<int>("IdProveedorFormaEnvio")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_proveedor_forma_envio");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProveedorFormaEnvio"));

//                    b.Property<int?>("IdFormaEnvio")
//                        .HasColumnType("int")
//                        .HasColumnName("id_forma_envio");

//                    b.Property<int?>("IdProveedor")
//                        .HasColumnType("int")
//                        .HasColumnName("id_proveedor");

//                    b.HasKey("IdProveedorFormaEnvio")
//                        .HasName("pk_proveedor_forma_envio");

//                    b.HasIndex("IdFormaEnvio");

//                    b.HasIndex("IdProveedor");

//                    b.ToTable("ProveedoresXFormaEnvio", (string)null);
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.ProveedoresXformaPago", b =>
//                {
//                    b.Property<int>("IdProveedorFormaPago")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_proveedor_forma_pago");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProveedorFormaPago"));

//                    b.Property<int?>("IdFormaPago")
//                        .HasColumnType("int")
//                        .HasColumnName("id_forma_pago");

//                    b.Property<int?>("IdProveedor")
//                        .HasColumnType("int")
//                        .HasColumnName("id_proveedor");

//                    b.HasKey("IdProveedorFormaPago")
//                        .HasName("pk_proveedor_forma_pago");

//                    b.HasIndex("IdFormaPago");

//                    b.HasIndex("IdProveedor");

//                    b.ToTable("ProveedoresXFormaPago", (string)null);
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.Role", b =>
//                {
//                    b.Property<int>("IdRol")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_rol");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRol"));

//                    b.Property<string>("Descripcion")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(50)")
//                        .HasColumnName("descripcion");

//                    b.Property<string>("Rol")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(50)")
//                        .HasColumnName("rol");

//                    b.HasKey("IdRol")
//                        .HasName("pk_rol");

//                    b.ToTable("Roles");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.UnidadesMedidum", b =>
//                {
//                    b.Property<int>("IdUnidadMedida")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_unidad_medida");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUnidadMedida"));

//                    b.Property<string>("Descripcion")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(50)")
//                        .HasColumnName("descripcion");

//                    b.Property<bool?>("Estado")
//                        .HasColumnType("bit")
//                        .HasColumnName("estado");

//                    b.Property<string>("Nombre")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(100)")
//                        .HasColumnName("nombre");

//                    b.HasKey("IdUnidadMedida")
//                        .HasName("pk_unidad_medida");

//                    b.ToTable("Unidades_Medida", (string)null);
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.Usuario", b =>
//                {
//                    b.Property<int>("IdUsuario")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasColumnName("id_usuario");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

//                    b.Property<string>("Email")
//                        .IsRequired()
//                        .HasMaxLength(100)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(100)")
//                        .HasColumnName("email");

//                    b.Property<int?>("IdRol")
//                        .HasColumnType("int")
//                        .HasColumnName("id_rol");

//                    b.Property<string>("Password")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .IsUnicode(false)
//                        .HasColumnType("varchar(50)")
//                        .HasColumnName("password");

//                    b.HasKey("IdUsuario")
//                        .HasName("pk_usuario");

//                    b.HasIndex("IdRol");

//                    b.ToTable("Usuarios");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.DetalleOrden", b =>
//                {
//                    b.HasOne("SuplementosFGFit_Back.Models.OrdenesCompra", "IdOrdenCompraNavigation")
//                        .WithMany("DetalleOrdens")
//                        .HasForeignKey("IdOrdenCompra")
//                        .HasConstraintName("fk_detalle_orden_compra");

//                    b.HasOne("SuplementosFGFit_Back.Models.Producto", "IdProductoNavigation")
//                        .WithMany("DetalleOrdens")
//                        .HasForeignKey("IdProducto")
//                        .HasConstraintName("fk_detalle_producto");

//                    b.Navigation("IdOrdenCompraNavigation");

//                    b.Navigation("IdProductoNavigation");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.HistorialRefreshToken", b =>
//                {
//                    b.HasOne("SuplementosFGFit_Back.Models.Usuario", "IdUsuarioNavigation")
//                        .WithMany("HistorialRefreshTokens")
//                        .HasForeignKey("IdUsuario")
//                        .HasConstraintName("FK__Historial__id_us__44FF419A");

//                    b.Navigation("IdUsuarioNavigation");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.OrdenesCompra", b =>
//                {
//                    b.HasOne("SuplementosFGFit_Back.Models.EstadoOrdenCompra", "IdEstadoOrdenNavigation")
//                        .WithMany("OrdenesCompras")
//                        .HasForeignKey("IdEstadoOrden")
//                        .HasConstraintName("fk_orden_compra_estado_orden_compra");

//                    b.HasOne("SuplementosFGFit_Back.Models.FormasEnvio", "IdFormaEnvioNavigation")
//                        .WithMany("OrdenesCompras")
//                        .HasForeignKey("IdFormaEnvio")
//                        .HasConstraintName("fk_orden_compra_forma_envio");

//                    b.HasOne("SuplementosFGFit_Back.Models.FormasPago", "IdFormaPagoNavigation")
//                        .WithMany("OrdenesCompras")
//                        .HasForeignKey("IdFormaPago")
//                        .HasConstraintName("fk_orden_compra_forma_pago");

//                    b.HasOne("SuplementosFGFit_Back.Models.Proveedore", "IdProveedorNavigation")
//                        .WithMany("OrdenesCompras")
//                        .HasForeignKey("IdProveedor")
//                        .HasConstraintName("fk_orden_compra_proveedor");

//                    b.Navigation("IdEstadoOrdenNavigation");

//                    b.Navigation("IdFormaEnvioNavigation");

//                    b.Navigation("IdFormaPagoNavigation");

//                    b.Navigation("IdProveedorNavigation");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.Producto", b =>
//                {
//                    b.HasOne("SuplementosFGFit_Back.Models.Categoria", "IdCategoriaNavigation")
//                        .WithMany("Productos")
//                        .HasForeignKey("IdCategoria")
//                        .HasConstraintName("fk_producto_categoria");

//                    b.HasOne("SuplementosFGFit_Back.Models.UnidadesMedidum", "IdUnidadMedidaNavigation")
//                        .WithMany("Productos")
//                        .HasForeignKey("IdUnidadMedida")
//                        .HasConstraintName("fk_producto_unidad_medida");

//                    b.Navigation("IdCategoriaNavigation");

//                    b.Navigation("IdUnidadMedidaNavigation");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.ProductosXproveedore", b =>
//                {
//                    b.HasOne("SuplementosFGFit_Back.Models.Producto", "IdProductoNavigation")
//                        .WithMany("ProductosXproveedores")
//                        .HasForeignKey("IdProducto")
//                        .HasConstraintName("fk_producto_proveedor_producto");

//                    b.HasOne("SuplementosFGFit_Back.Models.Proveedore", "IdProveedorNavigation")
//                        .WithMany("ProductosXproveedores")
//                        .HasForeignKey("IdProveedor")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .HasConstraintName("fk_producto_proveedor_proveedor");

//                    b.Navigation("IdProductoNavigation");

//                    b.Navigation("IdProveedorNavigation");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.ProveedoresXformaEnvio", b =>
//                {
//                    b.HasOne("SuplementosFGFit_Back.Models.FormasEnvio", "IdFormaEnvioNavigation")
//                        .WithMany("ProveedoresXformaEnvios")
//                        .HasForeignKey("IdFormaEnvio")
//                        .HasConstraintName("fk_proveedor_forma_envio_forma_envio");

//                    b.HasOne("SuplementosFGFit_Back.Models.Proveedore", "IdProveedorNavigation")
//                        .WithMany("ProveedoresXformaEnvios")
//                        .HasForeignKey("IdProveedor")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .HasConstraintName("fk_proveedor_forma_envio_proveedor");

//                    b.Navigation("IdFormaEnvioNavigation");

//                    b.Navigation("IdProveedorNavigation");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.ProveedoresXformaPago", b =>
//                {
//                    b.HasOne("SuplementosFGFit_Back.Models.FormasPago", "IdFormaPagoNavigation")
//                        .WithMany("ProveedoresXformaPagos")
//                        .HasForeignKey("IdFormaPago")
//                        .HasConstraintName("fk_proveedor_forma_pago_forma_pago");

//                    b.HasOne("SuplementosFGFit_Back.Models.Proveedore", "IdProveedorNavigation")
//                        .WithMany("ProveedoresXformaPagos")
//                        .HasForeignKey("IdProveedor")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .HasConstraintName("fk_proveedor_forma_pago_proveedor");

//                    b.Navigation("IdFormaPagoNavigation");

//                    b.Navigation("IdProveedorNavigation");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.Usuario", b =>
//                {
//                    b.HasOne("SuplementosFGFit_Back.Models.Role", "IdRolNavigation")
//                        .WithMany("Usuarios")
//                        .HasForeignKey("IdRol")
//                        .HasConstraintName("fk_usuario_rol");

//                    b.Navigation("IdRolNavigation");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.Categoria", b =>
//                {
//                    b.Navigation("Productos");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.EstadoOrdenCompra", b =>
//                {
//                    b.Navigation("OrdenesCompras");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.FormasEnvio", b =>
//                {
//                    b.Navigation("OrdenesCompras");

//                    b.Navigation("ProveedoresXformaEnvios");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.FormasPago", b =>
//                {
//                    b.Navigation("OrdenesCompras");

//                    b.Navigation("ProveedoresXformaPagos");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.OrdenesCompra", b =>
//                {
//                    b.Navigation("DetalleOrdens");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.Producto", b =>
//                {
//                    b.Navigation("DetalleOrdens");

//                    b.Navigation("ProductosXproveedores");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.Proveedore", b =>
//                {
//                    b.Navigation("OrdenesCompras");

//                    b.Navigation("ProductosXproveedores");

//                    b.Navigation("ProveedoresXformaEnvios");

//                    b.Navigation("ProveedoresXformaPagos");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.Role", b =>
//                {
//                    b.Navigation("Usuarios");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.UnidadesMedidum", b =>
//                {
//                    b.Navigation("Productos");
//                });

//            modelBuilder.Entity("SuplementosFGFit_Back.Models.Usuario", b =>
//                {
//                    b.Navigation("HistorialRefreshTokens");
//                });
//#pragma warning restore 612, 618
//        }
//    }
//}
