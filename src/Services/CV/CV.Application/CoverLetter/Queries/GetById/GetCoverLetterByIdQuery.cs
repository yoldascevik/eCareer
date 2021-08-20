using Career.MediatR.Query;
using CurriculumVitae.Application.CoverLetter.Dtos;

namespace CurriculumVitae.Application.CoverLetter.Queries.GetById
{
    public class GetCoverLetterByIdQuery : IQuery<CoverLetterDto>
    {
        public GetCoverLetterByIdQuery(string coverLetterId)
        {
            CoverLetterId = coverLetterId;
        }

        public string CoverLetterId { get; }
    }
}