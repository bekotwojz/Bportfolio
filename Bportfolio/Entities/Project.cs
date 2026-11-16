namespace Bportfolio.Entities;
public class Project
{
    public int ProjectId { get; set; }
    public string ProjectTitle { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}