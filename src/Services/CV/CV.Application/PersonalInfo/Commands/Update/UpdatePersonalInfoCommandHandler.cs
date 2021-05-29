using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.PersonalInfo.Commands.Update
{
    public class UpdatePersonalInfoCommandHandler: ICommandHandler<UpdatePersonalInfoCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<UpdatePersonalInfoCommandHandler> _logger;

        public UpdatePersonalInfoCommandHandler(
            ICVRepository cvRepository, 
            IMapper mapper,
            ILogger<UpdatePersonalInfoCommandHandler> logger) 
        {
            _cvRepository = cvRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePersonalInfoCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }

            _mapper.Map(request.PersonalInfo, cv.PersonalInfo);
            await _cvRepository.UpdateAsync(cv.Id, cv); 
                
            _logger.LogInformation("CV {CvId} personal info updated", cv.Id);
            
            return Unit.Value;
        }
    }
}