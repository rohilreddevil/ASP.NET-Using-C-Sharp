using Assignment5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Models
{
     public class TrackBaseViewModel
     {
         [Key]
         public int Id { get; set; }

         [Required]
         [StringLength(50)]
         [Display(Name = "Clerk who helps with Album tasks")]
         public string Clerk { get; set; }

         [Required]
         [StringLength(312)]
         [Display(Name = "Composer names (comma-separated)")]
         public string Composers { get; set; }

         [Required]
         [StringLength(150)]
         [Display(Name = "Track Name")]
         public string Name { get; set; }

         [Display(Name = "Genre")]
         public string Genre { get; set; }
         [Display(Name = "Album Id")]
         public int AlbumId { get; set; }

     }

     public class TrackWithDetailViewModel : TrackBaseViewModel
     {
         [Display(Name = "Albums containing this track")]
         public IEnumerable<AlbumBaseViewModel> Albums { get; set; }

         [Display(Name = "Albums containing this track")]
         public IEnumerable<string> AlbumNames { get; set; }

         [Display(Name = "Sample clip")]
         public string Audio { get { return $"/Audio/{Id}"; } }

     }

     public class TrackViewModel
     {
         public int Id;

         public string AudioContentType { get; set; }

         public byte[] Audio { get; set; }
     } 

}