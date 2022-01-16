using System;
using System.Linq;
using Career.Repositories.Repository;
using CurriculumVitae.Core.Entities;

namespace CurriculumVitae.Core.Repositories;

public interface ICoverLetterRepository : IRepository<CoverLetter>
{
    IQueryable<CoverLetter> GetByUserId(Guid userId);
}