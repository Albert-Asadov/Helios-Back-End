using System;
using Helios.DAL;
using Helios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Helios.Service
{
	public class LayoutService
	{
        readonly HeliosDbContext _context;
        readonly IHttpContextAccessor _accessor;

        public LayoutService(HeliosDbContext context, IHttpContextAccessor accessor)
		{
            _context = context;
            _accessor = accessor;
        }


        public List<Category> AllCategories()
        {
            List<Category> categories = _context.Categories.ToList();

            return categories;
        }
    }
}

