using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApplicationWithRegistration.Models
{
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class User
        {
            [System.ComponentModel.DataAnnotations.Compare("Password")]
            public string ConfirmPassword { get; set; }


        }

    public class EmployeeMetaData
    {
        [StringLength(50, MinimumLength = 6)]
        [Required]
        [Remote("IsUserNameAvailable", "Home", ErrorMessage = "User Name is already in use. Try another?")]
        [DisplayAttribute(Name = "User Name")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email")]
        public string UserName { get; set; }

        [StringLength(20, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
    }


}