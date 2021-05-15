namespace Company.Application.Company.Dtos
{
    public class AddressInputDto
    {
        public string Title { get; set; }
        public IdNameRefDto Country { get; set; }
        public IdNameRefDto City { get; set; }
        public IdNameRefDto District { get; set; }
        public string Details { get; set; }
        public string ZipCode { get; set; }
        public bool IsPrimary { get; set; }
    }
}