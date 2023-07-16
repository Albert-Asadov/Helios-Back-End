using System;
namespace Helios.Entities
{
	public class ComponentImage:BaseEntity
	{
        public string imagePath { get; set; }

        public bool? IsMain { get; set; }

        public Component Component { get; set; }
    }
}

