using FrontendCrudUser.Dtos;

namespace FrontendCrudUser.Dtos;
public class UsuariosResponseWrapper
{
    public List<UsuarioDto> Items { get; set; } = new();
    public MetadataDto Metadata { get; set; } = new();
}

public class MetadataDto
{
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
}