using CrudUsers.Models;
using CrudUsers.Repository;
using CrudUsers.Shared;
using System.Runtime.CompilerServices;

namespace CrudUsers.Service;

public interface IUsuariosService : IBaseService<Usuarios>
{
    Task<Usuarios> CreateAsync(Usuarios entity);
    Task<bool> DeleteAsync(int id);
    Task<PagedResult<Usuarios>> GetAllAsync(int? pageNumber, int? pageSize);
    Task<Usuarios?> GetByIdAsync(int id);
    Task<Usuarios> UpdateAsync(Usuarios entity);
}
public class UsuariosService(
    IUsuariosRepository usuarioRepository,
    IActividadesRepository actividadesRepository
    ) : BaseService<Usuarios>, IUsuariosService
{
    private readonly IUsuariosRepository _usuariosRepository = usuarioRepository;
    private readonly IActividadesRepository _actividadesRepository = actividadesRepository;

    public async Task<PagedResult<Usuarios>> GetAllAsync(int? pageNumber, int? pageSize)
    {
        return await _usuariosRepository.GetAllAsync(pageNumber, pageSize);
    }

    public async Task<Usuarios?> GetByIdAsync(int id) 
    { 
        return await _usuariosRepository.GetByIdAsync(id);
    }

    public async Task<Usuarios> CreateAsync(Usuarios entity) 
    {
        Usuarios usuarioCreado = await _usuariosRepository.CreateAsync(entity);
        var actividadRealizada = new Actividades()
        {
            Actividad = (int)ListaActividad.Creacion,
            FechaCreacion = DateTime.Now,
            UsuarioId = usuarioCreado.Id
        };
        await _actividadesRepository.CreateAsync(actividadRealizada);
        return usuarioCreado;
    }

    public async  Task<Usuarios> UpdateAsync(Usuarios entity)
    {
        Usuarios usuarioCreado = await _usuariosRepository.UpdateAsync(entity);
        var actividadRealizada = new Actividades()
        {
            Actividad = (int)ListaActividad.Actualizacion,
            FechaCreacion = DateTime.Now,
            UsuarioId = usuarioCreado.Id
        };
        await _actividadesRepository.CreateAsync(actividadRealizada);
        return usuarioCreado;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        bool eliminado  = await _usuariosRepository.DeleteAsync(id);
        if (!eliminado)
            return false;
        var actividadRealizada = new Actividades()
        {
            Actividad = (int)ListaActividad.Eliminacion,
            FechaCreacion = DateTime.Now,
            UsuarioId = id
        };
        await _actividadesRepository.CreateAsync(actividadRealizada);
        return eliminado;
    }
}
