using System;
namespace Helios.Entities
{
	public class Category:BaseEntity
	{
		public string Name { get; set; }

        public List<ComponentCategory> componentCategories { get; set; }

		public Category()
		{
            componentCategories = new();

        }
    }
}

