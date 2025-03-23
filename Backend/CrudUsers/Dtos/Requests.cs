using Netforemost_Todo_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CrudUsers.Dtos;

public record UsuarioRequest(string Nombre, string Apellido, string Correo, long? Telefono, DateTime FechaNacimiento, string PaisResidencia, bool DeseaRecibirInformacion);