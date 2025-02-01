namespace L_loans_Class;

public partial class HistorialDeGarantium
{
    public int Id { get; set; }

    public int? IdGarantia { get; set; }

    public DateTime? FechaDeCreacion { get; set; }

    public string? Descripcion { get; set; }

    public virtual Garantium? IdGarantiaNavigation { get; set; }
}
