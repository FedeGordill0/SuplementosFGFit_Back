using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuplementosFGFit_Back.Models;

public partial class SuplementosFgfitContext : DbContext
{
    public SuplementosFgfitContext()
    {
    }

    public SuplementosFgfitContext(DbContextOptions<SuplementosFgfitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<DetalleOrden> DetalleOrdens { get; set; }

    public virtual DbSet<EstadoOrdenCompra> EstadoOrdenCompras { get; set; }

    public virtual DbSet<FormasEnvio> FormasEnvios { get; set; }

    public virtual DbSet<FormasPago> FormasPagos { get; set; }

    public virtual DbSet<HistorialRefreshToken> HistorialRefreshTokens { get; set; }

    public virtual DbSet<OrdenesCompra> OrdenesCompras { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosXproveedore> ProductosXproveedores { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<ProveedoresXformaEnvio> ProveedoresXformaEnvios { get; set; }

    public virtual DbSet<ProveedoresXformaPago> ProveedoresXformaPagos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UnidadesMedidum> UnidadesMedida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuariosXrole> UsuariosXroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6FOBPHQ\\SQLEXPRESS;Database=SuplementosFGFit;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("pk_categoria");

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<DetalleOrden>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("pk_detalle");

            entity.ToTable("Detalle_Orden");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Cantidad)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("cantidad");
            entity.Property(e => e.IdOrdenCompra).HasColumnName("id_orden_compra");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Precio)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdOrdenCompraNavigation).WithMany(p => p.DetalleOrdens)
                .HasForeignKey(d => d.IdOrdenCompra)
                .HasConstraintName("fk_detalle_orden_compra");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleOrdens)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("fk_detalle_producto");
        });

        modelBuilder.Entity<EstadoOrdenCompra>(entity =>
        {
            entity.HasKey(e => e.IdEstadoOrden).HasName("pk_estado_Orden_Compra");

            entity.ToTable("Estado_OrdenCompra");

            entity.Property(e => e.IdEstadoOrden).HasColumnName("id_estado_orden");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<FormasEnvio>(entity =>
        {
            entity.HasKey(e => e.IdFormaEnvio).HasName("pk_forma_envio");

            entity.ToTable("Formas_Envio");

            entity.Property(e => e.IdFormaEnvio).HasColumnName("id_forma_envio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("precio");
        });

        modelBuilder.Entity<FormasPago>(entity =>
        {
            entity.HasKey(e => e.IdFormaPago).HasName("pk_forma_pago");

            entity.ToTable("Formas_Pago");

            entity.Property(e => e.IdFormaPago).HasColumnName("id_forma_pago");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Porcentaje)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("porcentaje");
        });

        modelBuilder.Entity<HistorialRefreshToken>(entity =>
        {
            entity.HasKey(e => e.IdHistorialToken).HasName("PK__Historia__03DC48A5E53D6697");

            entity.ToTable("HistorialRefreshToken");

            entity.Property(e => e.EsActivo).HasComputedColumnSql("(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialRefreshTokens)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Historial__id_us__48CFD27E");
        });

        modelBuilder.Entity<OrdenesCompra>(entity =>
        {
            entity.HasKey(e => e.IdOrdenCompra).HasName("pk_orden_compra");

            entity.ToTable("Ordenes_Compra");

            entity.Property(e => e.IdOrdenCompra).HasColumnName("id_orden_compra");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdEstadoOrden).HasColumnName("id_estado_orden");
            entity.Property(e => e.IdFormaEnvio).HasColumnName("id_forma_envio");
            entity.Property(e => e.IdFormaPago).HasColumnName("id_forma_pago");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

            entity.HasOne(d => d.IdEstadoOrdenNavigation).WithMany(p => p.OrdenesCompras)
                .HasForeignKey(d => d.IdEstadoOrden)
                .HasConstraintName("fk_orden_compra_estado_orden_compra");

            entity.HasOne(d => d.IdFormaEnvioNavigation).WithMany(p => p.OrdenesCompras)
                .HasForeignKey(d => d.IdFormaEnvio)
                .HasConstraintName("fk_orden_compra_forma_envio");

            entity.HasOne(d => d.IdFormaPagoNavigation).WithMany(p => p.OrdenesCompras)
                .HasForeignKey(d => d.IdFormaPago)
                .HasConstraintName("fk_orden_compra_forma_pago");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.OrdenesCompras)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("fk_orden_compra_proveedor");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("pk_producto");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("datetime")
                .HasColumnName("fechaVencimiento");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdUnidadMedida).HasColumnName("id_unidad_medida");
            entity.Property(e => e.Imagen)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("imagen");
            entity.Property(e => e.Marca)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("marca");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("fk_producto_categoria");

            entity.HasOne(d => d.IdUnidadMedidaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdUnidadMedida)
                .HasConstraintName("fk_producto_unidad_medida");
        });

        modelBuilder.Entity<ProductosXproveedore>(entity =>
        {
            entity.HasKey(e => e.IdProductoProveedor).HasName("pk_producto_proveedor");

            entity.ToTable("ProductosXProveedores");

            entity.Property(e => e.IdProductoProveedor).HasColumnName("id_producto_proveedor");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Precio)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductosXproveedores)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("fk_producto_proveedor_producto");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProductosXproveedores)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("fk_producto_proveedor_proveedor");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("pk_proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Cuit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cuit");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<ProveedoresXformaEnvio>(entity =>
        {
            entity.HasKey(e => e.IdProveedorFormaEnvio).HasName("pk_proveedor_forma_envio");

            entity.ToTable("ProveedoresXFormaEnvio");

            entity.Property(e => e.IdProveedorFormaEnvio).HasColumnName("id_proveedor_forma_envio");
            entity.Property(e => e.IdFormaEnvio).HasColumnName("id_forma_envio");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

            entity.HasOne(d => d.IdFormaEnvioNavigation).WithMany(p => p.ProveedoresXformaEnvios)
                .HasForeignKey(d => d.IdFormaEnvio)
                .HasConstraintName("fk_proveedor_forma_envio_forma_envio");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProveedoresXformaEnvios)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("fk_proveedor_forma_envio_proveedor");
        });

        modelBuilder.Entity<ProveedoresXformaPago>(entity =>
        {
            entity.HasKey(e => e.IdProveedorFormaPago).HasName("pk_proveedor_forma_pago");

            entity.ToTable("ProveedoresXFormaPago");

            entity.Property(e => e.IdProveedorFormaPago).HasColumnName("id_proveedor_forma_pago");
            entity.Property(e => e.IdFormaPago).HasColumnName("id_forma_pago");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

            entity.HasOne(d => d.IdFormaPagoNavigation).WithMany(p => p.ProveedoresXformaPagos)
                .HasForeignKey(d => d.IdFormaPago)
                .HasConstraintName("fk_proveedor_forma_pago_forma_pago");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProveedoresXformaPagos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("fk_proveedor_forma_pago_proveedor");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("pk_rol");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rol");
        });

        modelBuilder.Entity<UnidadesMedidum>(entity =>
        {
            entity.HasKey(e => e.IdUnidadMedida).HasName("pk_unidad_medida");

            entity.ToTable("Unidades_Medida");

            entity.Property(e => e.IdUnidadMedida).HasColumnName("id_unidad_medida");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("pk_usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("fk_usuario_rol");
        });

        modelBuilder.Entity<UsuariosXrole>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioRol).HasName("pk_usuario_rol");

            entity.ToTable("UsuariosXRoles");

            entity.Property(e => e.IdUsuarioRol).HasColumnName("id_usuario_rol");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.UsuariosXroles)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("fk_usuario_rol_rol");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuariosXroles)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_usuario_rol_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
