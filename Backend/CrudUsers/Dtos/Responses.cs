using Netforemost_Todo_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CrudUsers.Models;

namespace CrudUsers.Dtos;

public class ActividadesResponse
{
    public int Id { get; set; }
    public DateTime FechaCreacion { get; set; }
    public int UsuarioId { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Actividad { get; set; } = string.Empty;
}

