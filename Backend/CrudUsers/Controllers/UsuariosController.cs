using Microsoft.AspNetCore.Mvc;
using CrudUsers.Dtos;
using CrudUsers.Models;
using CrudUsers.Service;

namespace CrudUsers.Controllers;

[Route("api/Usuarios")]
public class UsuariosController(IUsuariosService _service) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetUsuarios(
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber)
    {
        var pagedResult = await _service.GetAllAsync(pageNumber, pageSize);

        if (!pagedResult.Items.Any())
            return NotFound();

        return Ok(new
        {
            pagedResult.Items,
            Metadata = new
            {
                pagedResult.TotalCount,
                pagedResult.PageSize,
                pagedResult.PageNumber,
                pagedResult.TotalPages
            }
        });
    }

    [HttpGet("{UserId}")]
    public async Task<IActionResult> GetUsuarioById(
        [FromRoute] int UserId)
    {
        Usuarios? Result = await _service.GetByIdAsync(UserId);

        if (Result is null)
            return NotFound();

        return Ok(Result);
    }

    [HttpPost()]
    public async Task<IActionResult> CreateUsuario(
        [FromBody] UsuarioRequest Request)
    {

        var mapedRequest = Map(Request);
        Usuarios? Result = await _service.CreateAsync(mapedRequest);

        if (Result is null)
            return NotFound();

        return Ok(Result);
    }

    [HttpPut("{UserId}")]
    public async Task<IActionResult> UpdateUsuario(
        [FromRoute] int UserId,
        [FromBody] UsuarioRequest Request)
    {
        var mapedRequest = MapWithId(UserId, Request);
        Usuarios? Result = await _service.UpdateAsync(mapedRequest);

        if (Result is null)
            return NotFound();

        return Ok(Result);
    }

    [HttpDelete("{UserId}")]
    public async Task<IActionResult> DeleteUsuario(
        [FromRoute] int UserId
    )
    {
        bool Result = await _service.DeleteAsync(UserId);

        if (!Result)
            return BadRequest("El usuario no pudo ser Eliminado");

        return Ok(Result);
    }

    private static Usuarios Map(UsuarioRequest Request) => new()
    {
        Nombre = Request.Nombre,
        Apellido = Request.Apellido,
        Correo = Request.Correo,
        PaisResidencia = Request.PaisResidencia,
        DeseaRecibirInformacion = Request.DeseaRecibirInformacion,
        Telefono = Request.Telefono == null ? 0: (long)Request.Telefono,
    };

    private static Usuarios MapWithId(int UserId, UsuarioRequest Request) => new()
    {
        Id = UserId,
        Nombre = Request.Nombre,
        Apellido = Request.Apellido,
        Correo = Request.Correo,
        PaisResidencia = Request.PaisResidencia,
        DeseaRecibirInformacion = Request.DeseaRecibirInformacion,
        Telefono = Request.Telefono == null ? 0 : (long)Request.Telefono,
    };
}
