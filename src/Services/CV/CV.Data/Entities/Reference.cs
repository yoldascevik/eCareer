using Career.Mongo.Document;

namespace CurriculumVitae.Data.Entities
{
    public class Reference: Document
    {
        public string FullName { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}