namespace L_loans_Class;

public partial class HistorialDePrestamo
{
    public int Id { get; set; }

    public int? IdPrestamos { get; set; }

    public DateTime? FechaDeCreacion { get; set; }

    public string? Descripcion { get; set; }

    public virtual Prestamo? IdPrestamosNavigation { get; set; }
}
