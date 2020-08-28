using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment4.Models
{
    public class AlbumBaseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(300)]
        [Display(Name = "Album image (cover art)")]
        public string UrlAlbum { get; set; }

        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }

        [Display(Name = "Coordinator who looks after the album")]
        public string Coordinator { get; set; }

    }  
        public class AlbumWithDetailViewModel: AlbumBaseViewModel
        {
            public AlbumWithDetailViewModel()
            {
                Artists = new List<Artist>();
                Tracks = new List<Track>();
            }

            [Display(Name = "Number of tracks on this album")]
            public int TracksCount { get; set; }

            [Display(Name = "Number of artists on this album")]
            public int ArtistsCount { get; set; }

            public IEnumerable<Artist> Artists { get; set; }

            public IEnumerable<Track> Tracks { get; set; }

        }
    }
