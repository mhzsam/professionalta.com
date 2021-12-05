using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FirstZX.Core.DTO
{
    #region max file size upload

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            //var extension = Path.GetExtension(file.FileName);
            //var allowedExtensions = new[] { ".jpg", ".png" };`enter code here`
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is { _maxFileSize/(1024*1024)} Mbytes.";
        }
    }

    #endregion
    public class UserInformationViewModel
    {
        

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

        
        [Display(Name = "avatar")]
        public string UserAvatar { get; set; }

        [Display(Name = "Register Date")]
        public DateTime RegisterDate { get; set; }


        [Display(Name = "Identifire Email")]
        [EmailAddress(ErrorMessage = "Not Valid Email Address")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string IdentifireEmail { get; set; }


        [Display(Name = "Identifire Key")]
        [MaxLength(8, ErrorMessage = "{0} Max is {1}")]
        public int IdentifireKey { get; set; }


        [Display(Name = "Digital Wallet Id")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string DigitalWalletId { get; set; }
        [Display(Name = "Valid Id")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string ValidId { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string Country { get; set; }
        public string SelectedPlan { get; set; }

    }

    public class SideBarUserPanelViewModel
    {
        public DateTime RegisterDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserAvatar { get; set; }
        public string rank { get; set; }
    }

    public class EditeProfileViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string LastName { get; set; }

    
        [Display(Name = "Digital Wallet Id")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string DigitalWalletId { get; set; }
        [Display(Name = "Valid Id")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string ValidId { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string Country { get; set; }

        [MaxFileSize(1 * 1024 * 1024)]
        public IFormFile UserAvatarFormFile { get; set; }
        [Display(Name = "avatar")]
        public string UserAvatar { get; set; }

    }
}