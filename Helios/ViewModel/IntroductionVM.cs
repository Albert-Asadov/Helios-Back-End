using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Helios.Entities;

namespace Helios.ViewModel
{
	public class IntroductionVM
	{
        public int Id { get; set; }

        public string LeftSideTitle { get; set; }

        public string RightSideTitle { get; set; }

        [NotMapped]
        public List<string> AdditionalCardInsideTitles { get; set; } = new List<string>();
        [NotMapped]
        public List<string> AdditionalCardInsideTexts { get; set; } = new List<string>();

        [NotMapped]
        public List<IntroductionImage> introductionImages { get; set; }

        [NotMapped]
        public IFormFile? MainPhoto { get; set; }

        [NotMapped]
        public IFormFile? FalseImage { get; set; }

        [NotMapped]
        public List<IntroductionImage>? AllImages { get; set; }

        [NotMapped]
        public ICollection<int>? ImageIds { get; set; }
    }
}

