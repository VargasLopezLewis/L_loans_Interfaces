namespace L_loans_Class;

public partial class HistorialDePago
{
    public int Id { get; set; }

    public int? IdPago { get; set; }

    public DateTime? FechaDeCreacion { get; set; }

    public string? Descripcion { get; set; }

    public virtual Pago? IdPagoNavigation { get; set; }
}
