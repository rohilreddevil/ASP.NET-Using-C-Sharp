 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{
    public class InvoiceWithDetailViewModel: InvoiceBaseViewModel
    {
        [Display(Name ="Customer First Name ")]
        public string CustomerFirstName { get; set; }

        [Display(Name = "Customer Last Name ")]
        public string CustomerLastName { get; set; }

        [Display(Name = "Customer City ")]
        public string CustomerCity { get; set; }

        [Display(Name = "Customer State/Province ")]
        public string CustomerState { get; set; }

        [Display(Name = "Employee's First Name")]
        public string CustomerEmployeeFirstName { get; set; }

        [Display(Name = "Employee's Last Name ")]
        public string CustomerEmployeeLastName { get; set; }

        public IEnumerable<InvoiceLineWithDetailViewModel> InvoiceLines { get; set; }
    }
}