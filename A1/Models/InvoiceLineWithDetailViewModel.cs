using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{
    public class InvoiceLineWithDetailViewModel: InvoiceLineBaseViewModel
    {

        public string TrackName { get; set; }
        public string TrackComposer { get; set; }

        public decimal TrackUnitPrice { get; set; }

        public string TrackAlbumTitle { get; set; }

        public string TrackAlbumArtistName { get; set; }

        public string TrackMediaTypeName { get; set; }
    }
}