using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.DrivingLicence;

public class DrivingLicenceNotFoundException: NotFoundException
{
    public DrivingLicenceNotFoundException(string id)
        :base($"Driving licence \"{id}\" is not found!")
    { }
}