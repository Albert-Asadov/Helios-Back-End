using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Helios.Entities
{
	public class Styles:BaseEntity
	{
        internal object ComponentCategories;

        public string shortDescription { get; set; }

        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string textContent { get; set; }

        public string URL { get; set; }

        public List<StylesCategory> stylesCategories { get; set; }

        public List<StylesImage> stylesImages { get; set; }

        public Styles()
        {
            stylesCategories = new();

            stylesImages = new();
        }
    }
}

