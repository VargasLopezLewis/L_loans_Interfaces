namespace L_loans_Class;

public partial class Pago
{
    public int Id { get; set; }

    public int? PId { get; set; }

    public int? Id_de_pago { get; set; }

    public decimal? MontoPagado { get; set; }

    public DateTime? FechaDePago { get; set; }

    public string? MetodoDePago { get; set; }

    public virtual ICollection<HistorialDePago> HistorialDePagos { get; set; } = new List<HistorialDePago>();

    public virtual Prestamo? PIdNavigation { get; set; }
}
