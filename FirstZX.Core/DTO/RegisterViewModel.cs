using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FirstZX.Core.DTO
{
    public class RegisterViewModel
    {


        
        
        [Display(Name = "Identifire Email")]
        [EmailAddress(ErrorMessage = "Not Valid Email Address")]
   
        [Required]
        public string IdentifireEmail { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        [EmailAddress(ErrorMessage = "Not Valid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Pasword")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        [MinLength(8, ErrorMessage = "Password must be more than 8 Char")]

        public string Password { get; set; }

        [Display(Name = "Confirm Pasword")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        [Compare("Password")]
        public string RePassword { get; set; }


        [Display(Name = "Valid Id")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string ValidId { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string Country { get; set; }

        public string IdentifireName { get; set; }

    }
    public class IdentifireEmailChechkViewModel{
        [Display(Name = "Referral Email")]
        [Required(ErrorMessage = "please input {0}")]
        [EmailAddress(ErrorMessage = "Not Valid Email Address")]
        public string IdentifireEmail { get; set; }
    }

}