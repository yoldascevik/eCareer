using Career.Mongo.Document;

namespace Definition.Data.Entities
{
    public class City : Document
    {
        public string Name { get; set; }
        public string CountryId { get; set; }
        public string CountryCode { get; set; }
        public string CityCode { get; set; }
    }
}
