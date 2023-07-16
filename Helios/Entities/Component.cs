using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Helios.Entities
{
	public class Component:BaseEntity
	{
        internal object ComponentCategories;

        public string shortDescription { get; set; }

        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string textContent { get; set; }

		public string URL { get; set; }

		public List<ComponentCategory> componentCategories { get; set; }

		public List<ComponentImage> ComponentImages { get; set; }

		public Component()
		{
			componentCategories = new();

            ComponentImages = new();
		}
	}
}

