namespace L_loans_Class;

public partial class Cliete
{

    public int Id { get; set; }

    public string? CNombre { get; set; }

    public string? CApellidos { get; set; }

    public int? CEdad { get; set; }

    public string? CCedula { get; set; }

    public string? CNumeroDeTelefono { get; set; }

    public DateTime? CFechaDeRegistro { get; set; }

    public string? C_Descripcion { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<HistorialDeCliente> HistorialDeClientes { get; set; } = new List<HistorialDeCliente>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
