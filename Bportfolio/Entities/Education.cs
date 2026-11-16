namespace Bportfolio.Entities
{
    public class Education
    {
        public int EducationId { get; set; }
        public string Program { get; set; } 
        public string InstitutionName { get; set; }
        public string Description { get; set; } 
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}