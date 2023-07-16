using System;
namespace Helios.Entities
{
	public class BlogImages:BaseEntity
	{
        public string ImagePath { get; set; }

        public bool? IsMain { get; set; }

        public Blog blog { get; set; }

    }
}

