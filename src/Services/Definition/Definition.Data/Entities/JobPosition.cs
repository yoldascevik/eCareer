namespace Definition.Data.Entities
{
    public class JobPosition: LookupDocument
    {
        public string SectorId { get; set; }
        public string Name { get; set; }
    }
}