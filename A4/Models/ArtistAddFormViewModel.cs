using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment4.Models
{
    public class ArtistAddFormViewModel: ArtistAddViewModel
    {
        [Display(Name = "Genre(s)")]
        public SelectList GenreList { get; set; }
    }
}