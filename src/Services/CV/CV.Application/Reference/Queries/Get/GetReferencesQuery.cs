using System.Collections.Generic;
using Career.MediatR.Query;
using CurriculumVitae.Application.Reference.Dtos;

namespace CurriculumVitae.Application.Reference.Queries.Get
{
    public class GetReferencesQuery : IQuery<List<ReferenceDto>>
    {
        public GetReferencesQuery(string cvId)
        {
            CvId = cvId;
        }

        public string CvId { get; }
    }
}