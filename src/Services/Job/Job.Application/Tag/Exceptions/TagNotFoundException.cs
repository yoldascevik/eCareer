using System;
using Career.Exceptions.Exceptions;

namespace Job.Application.Tag.Exceptions;

public class TagNotFoundException: NotFoundException
{
    public TagNotFoundException(Guid tagId) : base($"Tag is not found by id: {tagId}")
    {
    }
}