using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment4.Models
{
    public class GenreBaseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name= "Genre Name")]
        public string Name { get; set; }


    }
}