using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class MediaItemAddFormViewModel
    {
        public int ArtistId { get; set; }

        [Display(Name = "Artist Information")]
        public string ArtistInfo { get; set; }

        [Display(Name = "Caption")]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "Media item")]
        [DataType(DataType.Upload)]
        public string Upload { get; set; }

    }

    public class MediaItemAddViewModel
    {
        public int ArtistId { get; set; }

        [Display(Name = "Caption")]
        public string Caption { get; set; }

        [Required]
        public HttpPostedFileBase Upload { get; set; }
    }
}