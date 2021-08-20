using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.Attachment
{
    public class AttachmentNotFoundException : NotFoundException
    {
        public AttachmentNotFoundException(string id)
            :base($"Attachment \"{id}\" is not found!")
        { }
    }
}