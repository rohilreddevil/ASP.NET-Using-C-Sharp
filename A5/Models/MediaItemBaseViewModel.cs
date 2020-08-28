using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class MediaItemBaseViewModel : MediaItemWithDetailViewModel
    {
        [Key]
        public int Id { get; set; }

        public string StringId { get; set; }

        public DateTime Timestamp { get; set; }

        [StringLength(100)]
        public string Caption { get; set; }
    }

    public class MediaItemWithDetailViewModel
    {
        public byte[] Content { get; set; }

        public string ContentType { get; set; }
    }
}
