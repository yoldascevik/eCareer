using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.DisabilityType.Commands.Create
{
    public class CreateDisabilityTypeCommandHandler : ICommandHandler<CreateDisabilityTypeCommand, DisabilityTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly IDisabilityTypeRepository _disabilityTypeRepository;
        private readonly ILogger<CreateDisabilityTypeCommandHandler> _logger;

        public CreateDisabilityTypeCommandHandler(
            IDisabilityTypeRepository disabilityTypeRepository,
            IMapper mapper,
            ILogger<CreateDisabilityTypeCommandHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _disabilityTypeRepository = disabilityTypeRepository;
        }

        public async Task<DisabilityTypeDto> Handle(CreateDisabilityTypeCommand request, CancellationToken cancellationToken)
        {
            if (await _disabilityTypeRepository.ExistsByNameAsync(request.Name))
                throw new ItemAlreadyExistsException(request.Name);

            var disabilityType = await _disabilityTypeRepository.AddAsync(new Core.Entities.DisabilityType()
            {
                Name = request.Name
            });

            _logger.LogInformation("Disability type {Name} ({Id}) created", disabilityType.Name, disabilityType.Id);

            return _mapper.Map<DisabilityTypeDto>(disabilityType);
        }
    }
}