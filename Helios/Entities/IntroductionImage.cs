using System;
namespace Helios.Entities
{
	public class IntroductionImage:BaseEntity
	{
        public string imagePath { get; set; }

        public bool? IsMain { get; set; }

        public Introduction introduction { get; set; }
	}
}

