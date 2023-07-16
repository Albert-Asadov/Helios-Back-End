using System;
namespace Helios.Entities
{
	public class StylesCategory:BaseEntity
	{
		public Styles styles { get; set; }

		public int CategoryStyleId { get; set; }

		public CategoryStyle CategoryStyle { get; set; }
	}
}

