using System;
using Helios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Helios.DAL
{
	public class HeliosDbContext:DbContext
	{
        public HeliosDbContext(DbContextOptions<HeliosDbContext> options) : base(options)
        {

        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImages> BlogImages { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ComponentImage> ComponentImages { get; set; }
        public DbSet<ComponentCategory> ComponentCategories { get; set; }
        public DbSet<Styles> Styles { get; set; }
        public DbSet<CategoryStyle> categoryStyles { get; set; }
        public DbSet<StylesCategory> StylesCategories { get; set; }
        public DbSet<StylesImage> StylesImages { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Introduction> Introductions { get; set; }
        public DbSet<IntroductionCards> introductionCards { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>()
               .HasIndex(s => s.Key)
               .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}

