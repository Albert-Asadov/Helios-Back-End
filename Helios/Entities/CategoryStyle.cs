using System;
namespace Helios.Entities
{
	public class CategoryStyle:BaseEntity
	{
        public string Name { get; set; }

        public List<StylesCategory> stylesCategories { get; set; }

        public CategoryStyle()
        {
            stylesCategories = new();
        }
    }
}

