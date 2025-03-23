using CrudUsers.Context;
using CrudUsers.Models;
using CrudUsers.Shared;
using Microsoft.EntityFrameworkCore;

namespace CrudUsers.Repository;

public interface IActividadesRepository : IBaseRepository<Actividades> { }
public class ActividadesRepository(IDbContextFactory<DatabaseContext> dbContextFactory, ILogger<ActividadesRepository> logger) : BaseRepository<Actividades>(dbContextFactory, logger), IActividadesRepository
{

    public override async Task<PagedResult<Actividades>> GetAllAsync(int? pageNumber, int? pageSize)
    {
        try
        {
            using var context = CreateDbContext();
            var query = context.Set<Actividades>().OrderByDescending(a => a.FechaCreacion);

            return await query.ToPagedResultAsync(pageNumber, pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el historial de actividades ordenado.");
            throw new FetchException("Error al obtener el historial de actividades ordenado.");
        }
    }
}
