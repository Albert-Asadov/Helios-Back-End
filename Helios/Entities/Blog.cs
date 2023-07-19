using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Helios.Entities
{
	public class Blog:BaseEntity
	{
        [StringLength(maximumLength: 40)]
        public string TitleCard { get; set; }

        [StringLength(maximumLength: 40)]

        public string ShortDescription { get; set; }

		public string CardTitleDetail { get; set; }

        public bool? DesignFilter { get; set; }

        [AllowHtml]
		[UIHint("tinymce_jquery_full")]
		public string TextContent { get; set; }

		public List<BlogImages> blogImages { get; set; }

		public Blog()
		{
			blogImages = new();
		}
    }
}

