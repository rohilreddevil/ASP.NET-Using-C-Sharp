using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class AlbumAddViewModel
    {
        public AlbumAddViewModel()
        {
            ReleaseDate = DateTime.Now;
            ArtistIds = new List<int>();
            TrackIds = new List<int>();
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Album's coordinator")]
        public string Coordinator { get; set; }

        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }

        [Display(Name = "Album depiction")]
        [DataType(DataType.MultilineText)]
        public string Depiction { get; set; }

        [Required, StringLength(300)]
        [Display(Name = "Url to album's image")]
        public string UrlAlbum { get; set; }

        [Required]
        public IEnumerable<int> ArtistIds { get; set; }
        public IEnumerable<int> TrackIds { get; set; }
    }
}