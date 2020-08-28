using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Models
{
     public class TrackEditFormViewModel
     {
         public int Id;

         [Required]
         [StringLength(150)]
         [Display(Name = "Track Name")]
         public string Name { get; set; }

         [Required]
         [Display(Name = "Clip")]
         [DataType(DataType.Upload)]
         public string TrackUpload { get; set; }
     }

     public class TrackEditViewModel
     {
         public int Id;

         [Required]
         [StringLength(150)]
         [Display(Name = "Track Name")]
         public string Name { get; set; }

         [Required]
         public HttpPostedFileBase TrackUpload { get; set; }
     } 

} 

