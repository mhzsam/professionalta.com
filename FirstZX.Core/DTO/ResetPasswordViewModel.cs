using System.ComponentModel.DataAnnotations;

namespace FirstZX.Core.DTO
{
    public class ResetPasswordViewModel
    {

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

        public string ActiveCode { get; set; }
    }
}