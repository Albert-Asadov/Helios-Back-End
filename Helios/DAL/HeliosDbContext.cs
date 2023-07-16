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

    }
}

