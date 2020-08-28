using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{
    public class EmployeeEditFormViewModel
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        //the following properties are PERMITTED TO BE EDITED

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [StringLength(60)]
        public string Email { get; set; }

        //extra properties- NOT PERSISTED

        [Required, StringLength(100)]
        [Display(Name = "Password")]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&'])[^ ]{8,}", ErrorMessage = "Password must be 8+ characters, have 1+ digits, 1+ upper-case characters, 1+ lower-case characters, and 1+ special characters ( ! @ # $ % ^ &)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Range(1,4)]
        [Display(Name ="Number of Weeks of Vacation")]
        public int Weeks { get; set; }

    }

}