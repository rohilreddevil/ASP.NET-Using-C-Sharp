using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASSIGNMENT_2.Models
{
    public class TrackBaseViewModel
    {
        [Key]
        public int TrackId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public decimal UnitPrice { get; set; }

        public string NameDetailed { get { var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1); var composer = string.IsNullOrEmpty(Composer) ? "" : ", composer " + Composer; var trackLength = (ms > 0) ? ", " + ms.ToString() + " minutes" : ""; var unitPrice = (UnitPrice > 0) ? ", $ " + UnitPrice.ToString() : ""; return string.Format("{0}{1}{2}{3}", Name, composer, trackLength, unitPrice); } } // Composed read-only property 
        public string NameShort {    get    {       var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);       var unitPrice = (UnitPrice > 0) ? " $ " + UnitPrice.ToString() : "";       return string.Format("{0} - {1}", Name, unitPrice);    } } 

    }
}