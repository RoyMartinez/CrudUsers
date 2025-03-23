using Netforemost_Todo_Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudUsers.Models;

public class Actividades:BaseEntity
{
    [Required]
    public DateTime FechaCreacion {  get; set; }
    [Required]
    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }
    public Usuarios? Usuario { get; set; }
    [Required]
    public int Actividad { get; set; } = (int)ListaActividad.Creacion;

}
public enum ListaActividad { 
    Creacion=1,
    Eliminacion=2,
    Actualizacion=3
}
