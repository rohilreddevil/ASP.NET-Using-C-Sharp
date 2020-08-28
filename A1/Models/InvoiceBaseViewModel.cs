using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{
    public class InvoiceBaseViewModel
    {
        [Key]
        public int InvoiceId { get; set; }

        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        [Display(Name = "Date of Invoice")]
        public DateTime InvoiceDate { get; set; }

        [StringLength(70)]
        [Display(Name = "Invoice Billing Address")]
        public string BillingAddress { get; set; }

        [Display(Name = "City")]
        [StringLength(40)]
        public string BillingCity { get; set; }

        [Display(Name = "State/Province")]
        [StringLength(40)]
        public string BillingState { get; set; }

        [Display(Name = "Country")]
        [StringLength(40)]
        public string BillingCountry { get; set; }

        [Display(Name = "Postal Code")]
        [StringLength(10)]
        public string BillingPostalCode { get; set; }

        [Display(Name = "Invoice Total")]
        public decimal Total { get; set; }
    }
}