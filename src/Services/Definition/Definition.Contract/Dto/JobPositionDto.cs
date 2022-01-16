using System;

namespace Definition.Contract.Dto;

[Serializable]
public class JobPositionDto
{
    public string Id { get; set; }
    public string SectorId { get; set; }
    public string Name { get; set; }
}