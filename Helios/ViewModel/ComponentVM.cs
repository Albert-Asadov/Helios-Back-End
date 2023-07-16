using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Helios.Entities;

namespace Helios.ViewModel
{
	public class ComponentVM
	{
        public int Id { get; set; }

        public string shortDescription { get; set; }

        public string URL { get; set; }

        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string? textContent { get; set; }

        [NotMapped]
        public ICollection<int> CategoryIds { get; set; } = null!;

        [NotMapped]
        public List<ComponentImage> ComponentImages { get; set; }

        [NotMapped]
        public IFormFile? MainPhoto { get; set; }

        [NotMapped]
        public IFormFile? FalseImage { get; set; }

        [NotMapped]
        public List<ComponentImage>? AllImages { get; set; }

        [NotMapped]
        public ICollection<int>? ImageIds { get; set; }
    }
}

