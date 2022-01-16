using Career.MediatR.Command;
using Job.Application.Tag.Dtos;

namespace Job.Application.Job.Commands.UpdateJobTags;

public class UpdateJobTagsCommand: ICommand<List<TagDto>>
{
    public UpdateJobTagsCommand(Guid jobId, IEnumerable<string> tags)
    {
        JobId = jobId;
        Tags = tags ?? new string[0];
    }

    public Guid JobId { get; }
    public IEnumerable<string> Tags { get; }
}