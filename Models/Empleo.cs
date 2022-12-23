using System.ComponentModel.DataAnnotations;

namespace rrhhApi.Models;

public class Empleo
{
    public int Id { get; set; }
    public string? NombreEmpresa { get; set; }

    [DataType(DataType.Date)]
    public DateTime Desde { get; set; }

    [DataType(DataType.Date)]
    public DateTime Hasta { get; set; }

    public int CandidatoId { get; set; }
}