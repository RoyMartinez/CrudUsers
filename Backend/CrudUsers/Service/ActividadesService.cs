using CrudUsers.Dtos;
using CrudUsers.Models;
using CrudUsers.Repository;
using CrudUsers.Shared;

namespace CrudUsers.Service;

public interface IActividadesService : IBaseService<Actividades>
{
    Task<PagedResult<ActividadesResponse>> GetAllAsync(int? pageNumber, int? pageSize);
}
public class ActividadesService(
IActividadesRepository actividadesRepository,
IUsuariosRepository usuariosRepository
) : BaseService<Actividades>,IActividadesService
{
    private readonly IActividadesRepository _actividadesRepository = actividadesRepository;

    public async Task<PagedResult<ActividadesResponse>> GetAllAsync(int? pageNumber, int? pageSize)
    {
        PagedResult<Actividades> repositoryResponse =  await _actividadesRepository.GetAllAsync(pageNumber, pageSize);


        PagedResult<ActividadesResponse> serviceResponse = new PagedResult<ActividadesResponse>()
        {
            Items = (await Task.WhenAll(
                repositoryResponse.Items.Select(async x => new ActividadesResponse
                {
                    Id = x.Id,
                    FechaCreacion = x.FechaCreacion,
                    UsuarioId = x.UsuarioId,
                    NombreUsuario = await GetNombre(x.UsuarioId),
                    Actividad = ((ListaActividad)x.Actividad).ToString()
                })
            )).ToList(),
            TotalCount = repositoryResponse.TotalCount,
            PageNumber = repositoryResponse.PageNumber,
            PageSize = repositoryResponse.PageSize
        };
        return serviceResponse;
    }
    private async Task<string> GetNombre(int UsuarioId) 
    {
        var usuario = await usuariosRepository.GetByIdAsync(UsuarioId);
        return $"{usuario?.Nombre} {usuario?.Apellido}";
    }
}
