using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{
    public class TrackBaseViewModel
    {
        public TrackBaseViewModel() { }

        [Key]
        public int TrackId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [Display(Name = "Album Identifier")]
        public int? AlbumId { get; set; }

        [Display(Name = "Media Type Identifier")]
        public int MediaTypeId { get; set; }

        [Display(Name ="Genre Type Identifier")]
        public int? GenreId { get; set; }

        [StringLength(220)]
        [Display(Name = "Composer Name(s)")]
        public string Composer { get; set; }

        [Display(Name = "Track length in Milliseconds")]
        public int Milliseconds { get; set; }

        [Display(Name = "Track Size in Bytes")]
        public int? Bytes { get; set; }

        [Display(Name = "Selling Price")]
        public decimal UnitPrice { get; set; }
    }
}