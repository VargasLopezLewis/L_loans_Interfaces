using Microsoft.EntityFrameworkCore;

namespace L_loans_Class;

public partial class PInventoryContext : DbContext
{
    public PInventoryContext()
    {
    }

    public PInventoryContext(DbContextOptions<PInventoryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliete> Clietes { get; set; }

    public virtual DbSet<Garantium> Garantia { get; set; }

    public virtual DbSet<HistorialDeCliente> HistorialDeClientes { get; set; }

    public virtual DbSet<HistorialDeGarantium> HistorialDeGarantia { get; set; }

    public virtual DbSet<HistorialDePago> HistorialDePagos { get; set; }

    public virtual DbSet<HistorialDePrestamo> HistorialDePrestamos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<CalendarioPagos> CalendarioPagos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliete>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clietes__3214EC27CEC12C49");

            entity.ToTable(tb => tb.HasTrigger("trg_HistorialClientes"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CApellidos)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("C_Apellidos");
            entity.Property(e => e.CCedula)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("C_Cedula");
            entity.Property(e => e.CEdad)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("C_Edad");
            entity.Property(e => e.CFechaDeRegistro)
                .HasColumnType("datetime")
                .HasColumnName("C_Fecha_de_Registro");
            entity.Property(e => e.CNombre)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("C_Nombre");
            entity.Property(e => e.CNumeroDeTelefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("C_Numero_de_Telefono");
            entity.Property(e => e.C_Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Garantium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Garantia__3214EC270249D982");

            entity.ToTable(tb => tb.HasTrigger("trg_HistorialGarantia"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PId).HasColumnName("p_ID");
            entity.Property(e => e.TipoDeGarantia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tipo_de_garantia");
            entity.Property(e => e.ValorEstimado)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("Valor_Estimado");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Garantia)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Garantia__p_ID__5165187F");
        });

        modelBuilder.Entity<HistorialDeCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC27A4B00B60");

            entity.ToTable("Historial_de_Clientes");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaDeCreacion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_de_Creacion");
            entity.Property(e => e.IdClientes).HasColumnName("ID_Clientes");

            entity.HasOne(d => d.IdClientesNavigation).WithMany(p => p.HistorialDeClientes)
                .HasForeignKey(d => d.IdClientes)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Historial__ID_Cl__6A30C649");
        });

        modelBuilder.Entity<HistorialDeGarantium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC276A61611A");

            entity.ToTable("Historial_de_Garantia");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaDeCreacion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_de_Creacion");
            entity.Property(e => e.IdGarantia).HasColumnName("ID_Garantia");

            entity.HasOne(d => d.IdGarantiaNavigation).WithMany(p => p.HistorialDeGarantia)
                .HasForeignKey(d => d.IdGarantia)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Historial__ID_Ga__6FE99F9F");
        });

        modelBuilder.Entity<HistorialDePago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC27538204E8");

            entity.ToTable("Historial_de_Pagos");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaDeCreacion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_de_Creacion");
            entity.Property(e => e.IdPago).HasColumnName("ID_Pago");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.HistorialDePagos)
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Historial__ID_Pa__6754599E");
        });

        modelBuilder.Entity<HistorialDePrestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC2702F491E8");

            entity.ToTable("Historial_de_Prestamos");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaDeCreacion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_de_Creacion");
            entity.Property(e => e.IdPrestamos).HasColumnName("ID_Prestamos");

            entity.HasOne(d => d.IdPrestamosNavigation).WithMany(p => p.HistorialDePrestamos)
                .HasForeignKey(d => d.IdPrestamos)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Historial__ID_Pr__6D0D32F4");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagos__3214EC271E334557");

            entity.ToTable(tb => tb.HasTrigger("trg_HistorialPagos"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FechaDePago)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_de_Pago");
            entity.Property(e => e.MetodoDePago)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("Metodo_de_Pago");
            entity.Property(e => e.MontoPagado)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Monto_Pagado");
            entity.Property(e => e.PId).HasColumnName("p_ID");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Pagos__p_ID__4D94879B");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prestamo__3214EC27DC5AFF06");

            entity.ToTable(tb => tb.HasTrigger("trg_HistorialPrestamos"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CId).HasColumnName("C_ID");
            entity.Property(e => e.DuracionDelPrestamo)
                .HasColumnType("datetime")
                .HasColumnName("Duracion_Del_Prestamo");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FechaDeInicio)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_de_Inicio");
            entity.Property(e => e.Interes).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.CIdNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.CId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Prestamos__C_ID__45F365D3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
