using Bportfolio.Entities;

namespace Bportfolio.Models;

public class DashboardViewModel
{
    public int TotalProjects { get; set; }
    public int ActiveProjects { get; set; }
    public int SkillGroups { get; set; }
    public int Experiences { get; set; }
    public int Educations { get; set; }
    public int TotalMessages { get; set; }
    public int UnreadMessages { get; set; }
    public List<TechnologyLevel> TechnologyLevels { get; set; } = new();
    public List<Message> RecentMessages { get; set; } = new();
    public List<Project> RecentProjects { get; set; } = new();
    public List<CategoryProjectSummary> CategorySummaries { get; set; } = new();
}

public class TechnologyLevel
{
    public string Name { get; set; } = string.Empty;
    public int Percentage { get; set; }
}

public class CategoryProjectSummary
{
    public string CategoryName { get; set; } = string.Empty;
    public int ProjectCount { get; set; }
}
