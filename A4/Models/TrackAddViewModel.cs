using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment4.Models
{
    public class TrackAddViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name ="Track Name")]
        public string Name { get; set; }

        [Display(Name = "Composer Name(s) ")]
        public string Composers { get; set; }

        [Display(Name = "Track Genre")]
        public string Genre { get; set; }

        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }

        [Required]
        public int AlbumId { get; set; }
    }
}