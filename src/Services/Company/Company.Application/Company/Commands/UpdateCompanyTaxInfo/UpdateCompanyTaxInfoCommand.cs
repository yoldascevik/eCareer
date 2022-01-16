using Career.MediatR.Command;
using Company.Application.Company.Dtos;

namespace Company.Application.Company.Commands.UpdateCompanyTaxInfo;

public class UpdateCompanyTaxInfoCommand : ICommand<TaxDto>
{
    public UpdateCompanyTaxInfoCommand(Guid companyId, TaxDto taxInfo)
    {
        CompanyId = companyId;
        TaxInfo = taxInfo;
    }

    public Guid CompanyId { get; }
    public TaxDto TaxInfo { get; set; }
}