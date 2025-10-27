namespace ApiInmoZarate.Models;

public class Inquilino
{
    public long Id { get; set; }
    public string? Dni { get; set; }
    public string? NombreCompleto { get; set; }
    public string? LugarTrabajo { get; set; }
    public string? GaranteNombre { get; set; }
    public string? GaranteDni { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }

    public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
