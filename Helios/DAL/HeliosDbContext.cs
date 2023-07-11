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
    }
}

