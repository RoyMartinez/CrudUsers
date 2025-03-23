using CrudUsers.Service;
using Microsoft.AspNetCore.Mvc;

namespace CrudUsers.Controllers;

[Route("api/Actividades")]
public class ActividadesController(IActividadesService _service) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetActividades(
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
}