using System.Collections.Generic;
using Career.MediatR.Query;
using CurriculumVitae.Application.Disability.Dtos;

namespace CurriculumVitae.Application.Disability.Queries.Get
{
    public class GetDisabilitiesQuery : IQuery<List<DisabilityDto>>
    {
        public GetDisabilitiesQuery(string cvId)
        {
            CvId = cvId;
        }

        public string CvId { get; }
    }
}