using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Assignment4.Models
{
    public class TrackBaseViewModel
    {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [Display(Name = "Composer Name(s) ")]
        public string Composers { get; set; }

        [Display(Name = "Track Genre")]
        public string Genre { get; set; }

        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }
    }

    public class TrackWithDetailViewModel: TrackBaseViewModel
    {
        public TrackWithDetailViewModel()
        {
            Artists = new List<Artist>();
            AlbumNames = new List<string>();

        }

        [Display(Name = "Albums with this track")]
        public IEnumerable<string> AlbumNames { get; set; }

        public IEnumerable<Artist> Artists { get; set; }

        [Display(Name = "Number of albums with this track")]
        public int AlbumsCount { get; set; }


    }
}