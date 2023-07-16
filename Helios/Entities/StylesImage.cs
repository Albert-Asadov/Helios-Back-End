using System;
namespace Helios.Entities
{
	public class StylesImage:BaseEntity
	{
        public string imagePath { get; set; }

        public bool? IsMain { get; set; }

        public Styles styles { get; set; }
    }
}

