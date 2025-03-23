namespace FrontendCrudUser.Dtos;
public class ActividadesResponse
{
    public int Id { get; set; }
    public DateTime FechaCreacion { get; set; }
    public int UsuarioId { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Actividad { get; set; } = string.Empty;
}
