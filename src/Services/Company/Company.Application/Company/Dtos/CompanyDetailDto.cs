namespace Company.Application.Company.Dtos
{
    public class CompanyDetailDto
    {
        public string SectorId { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string FaxNumber { get; set; }
        public int EmployeesCount { get; set; }
        public short EstablishedYear { get; set; }
    }
}