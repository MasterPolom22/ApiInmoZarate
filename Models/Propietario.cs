namespace ApiInmoZarate.Models;

public class Propietario
{
    public long Id { get; set; }
    public string? Dni { get; set; }
    public string? Apellido { get; set; }
    public string? Nombre { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? DireccionContacto { get; set; }

    public ICollection<Inmueble> Inmuebles { get; set; } = new List<Inmueble>();
}
