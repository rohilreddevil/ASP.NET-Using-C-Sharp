using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment4.Models
{
    public class ArtistAddViewModel
    {
        public ArtistAddViewModel()
        {
            BirthOrStartDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display (Name ="Artist Name or Stage Name")]
        public string Name { get; set; }

        [Display(Name = "If applicable, artist's birth name ")]
        public string BirthName { get; set; }

        public string Executive { get; set; }

        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        [Required, StringLength(300)]
        [Display(Name = "URL to artist photo")]
        public string UrlArtist { get; set; }

        [Display(Name = "Birth date or start date ")]
        [DataType(DataType.Date)]
        public DateTime BirthOrStartDate { get; set; }

    }
}