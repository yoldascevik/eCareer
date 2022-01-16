namespace Company.Application.Company.Dtos;

public class IdNameRefDto
{
    public IdNameRefDto()
    {
            
    }

    public IdNameRefDto(string refId, string name): this()
    {
        RefId = refId;
        Name = name;
    }
        
    public string RefId { get; set; }
    public string Name { get; set; }
}