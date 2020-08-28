using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASSIGNMENT_2.Models
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        public string AlbumTitle { get; set; }

        public string AlbumArtistName { get; set; }

        public MediaTypeBaseViewModel MediaType { get; set; }
    }
}