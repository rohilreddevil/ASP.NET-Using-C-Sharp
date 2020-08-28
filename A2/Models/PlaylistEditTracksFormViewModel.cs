using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASSIGNMENT_2.Models
{
    public class PlaylistEditTracksFormViewModel
    {
        [Key]
        [Display(Name = "Playlist #")]
        public int PlaylistId { get; set; }

        [StringLength(120)]
        [Display(Name = "Playlist Name")]
        public string Name { get; set; }

        public int TracksCount { get; set; }

        public MultiSelectList TrackList { get; set; }

        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}