namespace FortniteCosmeticsApi.Dtos;

public class PagedCosmeticResponse
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public IList<CosmeticResponse> Data { get; set; } = new List<CosmeticResponse>();
}