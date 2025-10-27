namespace ApiInmoZarate.Models;

public class Contrato
{
    public long Id { get; set; }
    public long InmuebleId { get; set; }
    public long InquilinoId { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
    public decimal MontoAlquiler { get; set; }

    public Inmueble Inmueble { get; set; } = null!;
    public Inquilino Inquilino { get; set; } = null!;
    public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
