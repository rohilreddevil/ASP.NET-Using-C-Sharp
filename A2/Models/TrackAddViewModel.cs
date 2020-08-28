using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASSIGNMENT_2.Models
{
    public class TrackAddViewModel
    {

            [Required]
            [StringLength(200)]
            [Display(Name = "Track Name")]
            public string Name { get; set; }

            [Display(Name = "Track Composer")]
            [StringLength(220)]
            public string Composer { get; set; }

            [Display(Name = "Length(ms)")]
            public int Milliseconds { get; set; }

            [Display(Name = "Unit Price")]
            public decimal UnitPrice { get; set; }

            [Required]
            [Range(1, Int32.MaxValue)]
            public int AlbumId { get; set; }

            [Required]
            [Range(1, Int32.MaxValue)]
            public int MediaTypeId { get; set; } 


        }
    }
