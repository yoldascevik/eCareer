using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Company.Domain.Repository;
using MediatR;

namespace Company.Application.Commands.DeleteCompany
{
    public class DeleteCompanyCommandHandler: IRequestHandler<DeleteCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByKeyAsync(request.Id);
            if (company == null)
                throw new ItemNotFoundException($"Company is not found by id: {request.Id}");

            await _companyRepository.DeleteAsync(company);
            return Unit.Value;
        }
    }
}