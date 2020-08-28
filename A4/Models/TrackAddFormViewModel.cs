using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment4.Models
{
    public class TrackAddFormViewModel : TrackAddViewModel
    {

        public string AlbumName { get; set; }

        [Display(Name = "Genre of the track")]
        public SelectList GenreList { get; set; }

    }
}