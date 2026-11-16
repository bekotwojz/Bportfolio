using Bportfolio.Entities;
using Bportfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace Bportfolio.Context;

public class PortfolioContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database= PortfolioDB; integrated security=true; Trust Server Certificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .Property(project => project.IsActive)
            .HasDefaultValue(true);
    }

    public DbSet<Experience> Experiences { get; set; }
    public DbSet<HeroSection> HeroSections { get; set; }
    public DbSet<HeroTitle> HeroTitles { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<SkillItem> SkillItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<AboutMe> AboutMes { get; set; }
    public DbSet<Education> Educations { get; set; }
}


