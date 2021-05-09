using Company.Domain.Refs;

namespace Company.Application.Company.Dtos
{
    public class AddressInputDto
    {
        public string Title { get; set; }
        public CountryRef Country { get; set; }
        public CityRef City { get; set; }
        public DistrictRef District { get; set; }
        public string Details { get; set; }
        public string ZipCode { get; set; }
        public bool IsPrimary { get; set; }
    }
}