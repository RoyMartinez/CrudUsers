using Netforemost_Todo_Api.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading;

namespace CrudUsers.Models;

/// <summary>
/// Representa un usuario en el sistema.
/// </summary>

public class Usuarios : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Apellido { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Correo { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string FechaNacimiento { get; set; } = string.Empty;

    [MaxLength(15)]
    public long Telefono { get; set; }
    [Required]
    [MaxLength(35)]
    public string PaisResidencia { get; set; } = string.Empty;

    [Required]
    [MaxLength(15)]
    public bool DeseaRecibirInformacion { get; set; }

    public virtual IEnumerable<Actividades> Actividades { get; set; } = new List<Actividades>();

}