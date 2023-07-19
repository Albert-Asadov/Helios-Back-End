using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Helios.Entities;

namespace Helios.ViewModel
{
	public class BlogVM
	{
        public int Id { get; set; }

        [StringLength(maximumLength: 40)]
        public string? TitleCard { get; set; }

        [StringLength(maximumLength: 40)]

        public string? ShortDescription { get; set; }

        public string? CardTitleDetail { get; set; }

        public bool? DesignFilter { get; set; }

        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string? TextContent { get; set; }
        [NotMapped]
        public List<BlogImages> AllImages { get; set; }

        [NotMapped]
        public IFormFile? MainPhoto { get; set; } = null!;

        public ICollection<int>? ImageIds { get; set; }
    }
}

