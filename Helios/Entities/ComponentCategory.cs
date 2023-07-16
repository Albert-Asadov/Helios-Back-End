using System;
namespace Helios.Entities
{
	public class ComponentCategory:BaseEntity
	{
        public Component Component { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}

