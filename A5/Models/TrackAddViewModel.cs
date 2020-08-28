using Assignment5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class TrackAddViewModel
    {

        [Required]
        [StringLength(312)]
        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Display(Name = "Album Id")]
        public int AlbumId { get; set; }

        [StringLength(50)]
        [Display(Name = "Clerk who helps with Album tasks")]
        public string Clerk { get; set; }

        [Required]
        public HttpPostedFileBase TrackUpload { get; set; }
    }

    public class TrackAddFormViewModel
    {
        [Required]
        [StringLength(312)]
        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Display(Name = "Album Id")]
        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        public SelectList GenreList { get; set; }

        [Required]
        [Display(Name = "Sample clip")]
        [DataType(DataType.Upload)]
        public string TrackUpload { get; set; }
    }


}