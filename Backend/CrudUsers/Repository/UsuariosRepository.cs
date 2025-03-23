using CrudUsers.Context;
using CrudUsers.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudUsers.Repository;
public interface IUsuariosRepository : IBaseRepository<Usuarios> { }
public class UsuariosRepository(IDbContextFactory<DatabaseContext> dbContextFactory, ILogger<UsuariosRepository> logger) : BaseRepository<Usuarios>(dbContextFactory, logger), IUsuariosRepository
{
}
