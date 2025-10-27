namespace ApiInmoZarate.Models;

public class Inmueble
{
    public long Id { get; set; }
    public long PropietarioId { get; set; }
    public string Direccion { get; set; } = null!;
    public string? Uso { get; set; }         // comercial / residencial
    public string? Tipo { get; set; }        // casa, dpto, local, depósito, etc.
    public int? Ambientes { get; set; }
    public decimal? Precio { get; set; }
    public string? Estado { get; set; }      // disponible / no disponible

    public Propietario Propietario { get; set; } = null!;
    public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
