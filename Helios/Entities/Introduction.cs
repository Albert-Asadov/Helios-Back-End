using System;
namespace Helios.Entities
{
	public class Introduction:BaseEntity
	{
		public string LeftSideTitle { get; set; }

        public string RightSideTitle { get; set; }

        public List<IntroductionImage> introductionImages { get; set; }


		public Introduction()
		{
            introductionImages = new();
        }
		
	}
}

