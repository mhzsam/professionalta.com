using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FirstZX.Datalayer.Entities.User
{
    public class User
    {
        public User()
        {

        }


        [Key]
        public int UserId { get; set; }

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


        public string Password { get; set; }
        [Display(Name = "Activation Code")]
        [MaxLength(50, ErrorMessage = "{0} Max is {1}")]
        public string ActiveCode { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        [Display(Name = "avatar")]
        public string UserAvatar { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Register Date")]
        public DateTime RegisterDate { get; set; }


        [Display(Name = "Identifire Email")]
        [EmailAddress(ErrorMessage = "Not Valid Email Address")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string IdentifireEmail { get; set; }


        [Display(Name = "Identifire Key")]
        [Range(1000, 9999, ErrorMessage = "Identifire Key must be  4 digits number and start upper 1000")]
        [Required(ErrorMessage = "Identifire Key must be  4 digits number and start upper 1000")]
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

        [Display(Name = "Rank")]
        [AllowNull]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]

        public string Rank { get; set; }

        [Display(Name = "Selected Plan")]
        [AllowNull]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string SelectedPlan { get; set; }


        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }

       
        public virtual List<BankRequest> BankRequests { get; set; }
        public virtual BankDetailEarn BankDetailEarn { get; set; }
        public virtual BankDetailEarnWeek BankDetailEarnWeek { get; set; }


        public virtual List<UserAnswer> UserAnswers { get; set; }

        public virtual List<BankMonth> BankMonths { get; set; }
        public virtual List<BankWeek> BankWeeks { get; set; }
        #endregion



    }
}