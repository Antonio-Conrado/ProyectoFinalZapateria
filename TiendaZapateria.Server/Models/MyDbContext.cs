using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TiendaZapateria.Server.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatCategoria> CatCategoria { get; set; }

    public virtual DbSet<CatColor> CatColors { get; set; }

    public virtual DbSet<CatDepartamento> CatDepartamentos { get; set; }

    public virtual DbSet<CatMarca> CatMarcas { get; set; }

    public virtual DbSet<CatMaterial> CatMaterials { get; set; }

    public virtual DbSet<CatMunicipio> CatMunicipios { get; set; }

    public virtual DbSet<CatProducto> CatProductos { get; set; }

    public virtual DbSet<CatProveedor> CatProveedors { get; set; }

    public virtual DbSet<CatTalla> CatTallas { get; set; }

    public virtual DbSet<TblCompra> TblCompras { get; set; }

    public virtual DbSet<TblDetalleCompra> TblDetalleCompras { get; set; }

    public virtual DbSet<TblDetalleInventario> TblDetalleInventarios { get; set; }

    public virtual DbSet<TblDetalleVenta> TblDetalleVenta { get; set; }

    public virtual DbSet<TblHistorialRefreshToken> TblHistorialRefreshTokens { get; set; }

    public virtual DbSet<TblInventario> TblInventarios { get; set; }

    public virtual DbSet<TblMovimientoInterno> TblMovimientoInternos { get; set; }

    public virtual DbSet<TblPermiso> TblPermisos { get; set; }

    public virtual DbSet<TblRol> TblRols { get; set; }

    public virtual DbSet<TblRolPermiso> TblRolPermisos { get; set; }

    public virtual DbSet<TblUsuario> TblUsuarios { get; set; }

    public virtual DbSet<TblVenta> TblVenta { get; set; }

    public virtual DbSet<TblVista> TblVista { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatCategoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__CatCateg__A3C02A10165356D6");

            entity.Property(e => e.NombreCategoria).HasMaxLength(80);
        });

        modelBuilder.Entity<CatColor>(entity =>
        {
            entity.HasKey(e => e.IdColor).HasName("PK__CatColor__E83D55CBE1426020");

            entity.ToTable("CatColor");

            entity.Property(e => e.NombreColor).HasMaxLength(50);
        });

        modelBuilder.Entity<CatDepartamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__CatDepar__787A433D23A11597");

            entity.ToTable("CatDepartamento");

            entity.Property(e => e.NombreDepartamento).HasMaxLength(80);
        });

        modelBuilder.Entity<CatMarca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__CatMarca__4076A887B4BD86F0");

            entity.ToTable("CatMarca");

            entity.Property(e => e.NombreMarca).HasMaxLength(60);
        });

        modelBuilder.Entity<CatMaterial>(entity =>
        {
            entity.HasKey(e => e.IdMaterial).HasName("PK__CatMater__94356E58A6CD497B");

            entity.ToTable("CatMaterial");

            entity.Property(e => e.NombreMaterial).HasMaxLength(80);
        });

        modelBuilder.Entity<CatMunicipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PK__CatMunic__61005978EB074DBE");

            entity.ToTable("CatMunicipio");

            entity.Property(e => e.NombreMunicipio).HasMaxLength(80);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.CatMunicipios)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CatMunici__IdDep__2A4B4B5E");
        });

        modelBuilder.Entity<CatProducto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__CatProdu__098892107DB17B11");

            entity.ToTable("CatProducto");

            entity.Property(e => e.NombreProducto).HasMaxLength(100);
        });

        modelBuilder.Entity<CatProveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__CatProve__E8B631AFA77747CE");

            entity.ToTable("CatProveedor");

            entity.Property(e => e.Correo).HasMaxLength(150);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.NombreProveedor).HasMaxLength(150);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.CatProveedors)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CatProvee__IdDep__30F848ED");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.CatProveedors)
                .HasForeignKey(d => d.IdMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CatProvee__IdMun__31EC6D26");
        });

        modelBuilder.Entity<CatTalla>(entity =>
        {
            entity.HasKey(e => e.IdTalla).HasName("PK__CatTalla__E95BE9437CB9740C");

            entity.ToTable("CatTalla");

            entity.Property(e => e.TallaUs).HasColumnName("TallaUS");
        });

        modelBuilder.Entity<TblCompra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__tblCompr__0A5CDB5CA85FC3E1");

            entity.ToTable("tblCompra");

            entity.Property(e => e.FechaCompra).HasColumnType("date");
            entity.Property(e => e.Iva).HasColumnName("IVA");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TblCompras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblCompra__IdPro__4D94879B");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TblCompras)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblCompra__IdUsu__4E88ABD4");
        });

        modelBuilder.Entity<TblDetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra).HasName("PK__tblDetal__E046CCBB30BE81AD");

            entity.ToTable("tblDetalleCompra");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.TblDetalleCompras)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblDetall__IdCom__5165187F");

            entity.HasOne(d => d.IdInventarioNavigation).WithMany(p => p.TblDetalleCompras)
                .HasForeignKey(d => d.IdInventario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblDetall__IdInv__52593CB8");
        });

        modelBuilder.Entity<TblDetalleInventario>(entity =>
        {
            entity.HasKey(e => e.IdDetalleInventario).HasName("PK__tblDetal__5F17CC530DA9718A");

            entity.ToTable("tblDetalleInventario");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.TblDetalleInventarios)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblDetall__IdCat__440B1D61");

            entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.TblDetalleInventarios)
                .HasForeignKey(d => d.IdColor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblDetall__IdCol__45F365D3");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.TblDetalleInventarios)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblDetall__IdMar__44FF419A");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.TblDetalleInventarios)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblDetall__IdMat__47DBAE45");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.TblDetalleInventarios)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblDetall__IdPro__4316F928");

            entity.HasOne(d => d.IdTallaNavigation).WithMany(p => p.TblDetalleInventarios)
                .HasForeignKey(d => d.IdTalla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblDetall__IdTal__46E78A0C");
        });

        modelBuilder.Entity<TblDetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__tblDetal__AAA5CEC2C21EDA66");

            entity.ToTable("tblDetalleVenta");

            entity.HasOne(d => d.IdInventarioNavigation).WithMany(p => p.TblDetalleVenta)
                .HasForeignKey(d => d.IdInventario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblDetall__IdInv__59063A47");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.TblDetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__tblDetall__IdVen__5812160E");
        });

        modelBuilder.Entity<TblHistorialRefreshToken>(entity =>
        {
            entity.HasKey(e => e.IdHistorialToken).HasName("PK__tblHisto__03DC48A5F316370B");

            entity.ToTable("tblHistorialRefreshToken");

            entity.Property(e => e.EsActivo).HasComputedColumnSql("(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TblHistorialRefreshTokens)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__tblHistor__IdUsu__5BE2A6F2");
        });

        modelBuilder.Entity<TblInventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK__tblInven__1927B20C622DE744");

            entity.ToTable("tblInventario");

            entity.Property(e => e.FechaIngreso).HasColumnType("date");

            entity.HasOne(d => d.IdDetalleInventarioNavigation).WithMany(p => p.TblInventarios)
                .HasForeignKey(d => d.IdDetalleInventario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblInvent__IdDet__4AB81AF0");
        });

        modelBuilder.Entity<TblMovimientoInterno>(entity =>
        {
            entity.HasKey(e => e.IdMovimientoInterno).HasName("PK__tblMovim__2B198A814573363E");

            entity.ToTable("tblMovimientoInterno");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Fecha).HasColumnType("date");

            entity.HasOne(d => d.IdInventarioNavigation).WithMany(p => p.TblMovimientoInternos)
                .HasForeignKey(d => d.IdInventario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblMovimi__IdInv__5EBF139D");
        });

        modelBuilder.Entity<TblPermiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__tblPermi__0D626EC83CD5493C");

            entity.ToTable("tblPermiso");
        });

        modelBuilder.Entity<TblRol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__tblRol__2A49584CCEE4F981");

            entity.ToTable("tblRol");
        });

        modelBuilder.Entity<TblRolPermiso>(entity =>
        {
            entity.HasKey(e => e.IdRolPermiso).HasName("PK__tblRolPe__0CC73B1B35DC3A11");

            entity.ToTable("tblRolPermiso");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.TblRolPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblRolPer__IdPer__3B75D760");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TblRolPermisos)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblRolPer__IdRol__3A81B327");
        });

        modelBuilder.Entity<TblUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__tblUsuar__5B65BF973C6ABD4C");

            entity.ToTable("tblUsuario");

            entity.Property(e => e.ClaveAcceso).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.NombreUsuario).HasMaxLength(100);
            entity.Property(e => e.PrimerApellido).HasMaxLength(40);
            entity.Property(e => e.PrimerNombre).HasMaxLength(40);
            entity.Property(e => e.SegundoApellido).HasMaxLength(40);
            entity.Property(e => e.SegundoNombre).HasMaxLength(40);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TblUsuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblUsuari__IdRol__3E52440B");
        });

        modelBuilder.Entity<TblVenta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__tblVenta__BC1240BD32EB2720");

            entity.ToTable("tblVenta");

            entity.Property(e => e.FechaVenta).HasColumnType("date");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TblVenta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblVenta__IdUsua__5535A963");
        });

        modelBuilder.Entity<TblVista>(entity =>
        {
            entity.HasKey(e => e.IdVista).HasName("PK__tblVista__58A5C1FFE6C15C27");

            entity.ToTable("tblVista");

            entity.Property(e => e.UrlName).HasColumnName("urlName");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TblVista)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblVista__IdRol__619B8048");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
