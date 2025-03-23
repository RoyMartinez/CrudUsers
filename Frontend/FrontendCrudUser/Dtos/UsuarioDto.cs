namespace FrontendCrudUser.Dtos;
public class UsuarioDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty; 
    public long Telefono { get; set; }
    public string PaisResidencia { get; set; } = string.Empty;
}