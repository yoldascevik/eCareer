using System;
using Career.Data.Pagination;
using Career.MediatR.Query;
using CurriculumVitae.Application.CoverLetter.Dtos;

namespace CurriculumVitae.Application.CoverLetter.Queries.GetByUserId
{
    public class GetCoverLettersByUserIdQuery : PaginationFilter, IQuery<PagedList<CoverLetterDto>>
    {
        public Guid UserId { get; set; }
    }
}