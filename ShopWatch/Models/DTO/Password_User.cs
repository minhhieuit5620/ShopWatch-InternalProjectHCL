using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopWatch.Models.DTO
{
    public class Password_User
    {
        public int User_Id { get; set; }
        public string CurrentPassword { get; set; }

        public string CurrentPasswordInput { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "New Password")]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$",
           ErrorMessage = "Password: at least one lower case letter, one upper case letter, special character, one number and at least 8 characters length")]
        public string NewPassword { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "New Password and Confirm Password do not match")]
        public string ReNewPassword { get; set; }
    }
}