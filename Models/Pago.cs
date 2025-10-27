namespace ApiInmoZarate.Models;

public class Pago
{
    public long Id { get; set; }
    public long ContratoId { get; set; }
    public int NumeroPago { get; set; }
    public DateOnly FechaPago { get; set; }
    public decimal Importe { get; set; }

    public Contrato Contrato { get; set; } = null!;
}
