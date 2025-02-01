namespace L_loans_Class;

public partial class HistorialDeCliente
{
    public int Id { get; set; }

    public int? IdClientes { get; set; }

    public DateTime? FechaDeCreacion { get; set; }

    public string? Descripcion { get; set; }

    public virtual Cliete? IdClientesNavigation { get; set; }
}
