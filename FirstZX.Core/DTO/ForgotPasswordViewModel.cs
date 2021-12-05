using System.ComponentModel.DataAnnotations;

namespace FirstZX.Core.DTO
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        [EmailAddress(ErrorMessage = "Not Valid Email Address")]
        public string Email { get; set; }

   
    }
}