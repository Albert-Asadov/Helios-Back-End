using System;
namespace Helios.Entities
{
	public class Blog:BaseEntity
	{
		public string ImagePath { get; set; }

		public string TitleCard { get; set; }

		public string ShortDescription { get; set; }

		public string CardTitleDetail { get; set; }

        public string TextContent { get; set; }
    }
}

