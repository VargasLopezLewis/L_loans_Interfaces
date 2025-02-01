namespace L_loans_Class;

public partial class Garantium
{
    public int Id { get; set; }

    public int? PId { get; set; }

    public string? TipoDeGarantia { get; set; }

    public string? Descripcion { get; set; }

    public decimal? ValorEstimado { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<HistorialDeGarantium> HistorialDeGarantia { get; set; } = new List<HistorialDeGarantium>();

    public virtual Prestamo? PIdNavigation { get; set; }
}
