using System.ComponentModel.DataAnnotations;

namespace rrhhApi.Models;

public class Candidato
{
    public int Id {get; set;}
    public string? Nombre { get; set; }

    public string? Apellido { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime FechaNacimiento { get; set; }
    public string? Email { get; set; }
    public long Telefono { get; set; }
    public List<Empleo> Empleos { get; set; }
}