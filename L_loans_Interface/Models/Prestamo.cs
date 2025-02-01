namespace L_loans_Class;

public partial class Prestamo
{
    public int Id { get; set; }

    public int? CId { get; set; }

    public decimal? Monto { get; set; }

    public decimal? Interes { get; set; }

    public DateTime? FechaDeInicio { get; set; }

    public DateTime? DuracionDelPrestamo { get; set; }

    public string? Estado { get; set; }

    public decimal? Deuda_Restante { get; set; }

    public decimal? Mora { get; set; }

    public string? Tipo_de_cuota { get; set; }

    public virtual Cliete? CIdNavigation { get; set; }

    public virtual ICollection<Garantium> Garantia { get; set; } = new List<Garantium>();

    public virtual ICollection<HistorialDePrestamo> HistorialDePrestamos { get; set; } = new List<HistorialDePrestamo>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
