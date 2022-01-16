using System;

namespace Company.Application.Company.Dtos;

public class CompanyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Website { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string MobilePhone { get; set; }
    public string FaxNumber { get; set; }
    public int EmployeesCount { get; set; }
    public short EstablishedYear { get; set; }
    public TaxDto TaxInfo { get; set; }
    public IdNameRefDto Sector { get; set; }
}