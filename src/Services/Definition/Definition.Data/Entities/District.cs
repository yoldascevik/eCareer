namespace Definition.Data.Entities;

public sealed class District : LookupDocument
{
    public string Name { get; set; }
    public string CityId { get; set; }
    public string CityCode { get; set; }
    public string CountryId { get; set; }
    public string CountryCode { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}