using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class AlbumAddFormViewModel : AlbumAddViewModel
    {
        public string ArtistName { get; set; }

        [Display(Name = "Album's primary genre")]
        public SelectList GenreList { get; set; }

        [Display(Name = "All Artists")]
        public MultiSelectList ArtistList { get; set; }

        [Display(Name = "All Tracks")]
        public MultiSelectList TracksList { get; set; }



    }
}