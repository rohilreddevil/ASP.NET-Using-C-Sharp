using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASSIGNMENT_2.Models
{
    public class PlaylistBaseViewModel
    {
        [Key]
        [Display(Name ="Playlist #")]
        public int PlaylistId { get; set; }

        [StringLength(120)]
        [Display(Name = "Playlist Name")]
        public string Name { get; set; }

        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }

        public int TracksCount { get; set; }
    }
}