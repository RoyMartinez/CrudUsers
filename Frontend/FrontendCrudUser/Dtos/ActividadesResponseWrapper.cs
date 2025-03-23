namespace FrontendCrudUser.Dtos;

public class ActividadesResponseWrapper
{
    public List<ActividadesResponse> Items { get; set; } = new();
    public MetadataDto Metadata { get; set; } = new();
}
