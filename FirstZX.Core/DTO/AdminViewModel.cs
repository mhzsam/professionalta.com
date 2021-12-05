using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;
using FirstZX.Datalayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FirstZX.Core.DTO
{
    public class UsersForAdminViewModel
    {
        public List<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }

    public class UserForAdminDeletViewModel
    {
        public string Email { get; set; }
    }

    public class EditeUserForAdminViewModel
    {

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string FirstName { get; set; }

        public string SelectedPlan { get; set; }
        public string Rank { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string LastName { get; set; }

        //[Display(Name = "Email")]
        //[Required(ErrorMessage = "please input {0}")]
        //[MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        //[EmailAddress(ErrorMessage = "Not Valid Email Address")]
        public string Email { get; set; }


        [Display(Name = "Status")] public bool IsActive { get; set; }

        //[Display(Name = "avatar")]
        public string UserAvatar { get; set; }

        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Register Date")] public DateTime RegisterDate { get; set; }


        [Display(Name = "Identifire Email")]
        [EmailAddress(ErrorMessage = "Not Valid Email Address")]
        [MaxLength(200, ErrorMessage = "{0} Max is {1}")]
        public string IdentifireEmail { get; set; }

        [Display(Name = "Identifire Key")]
        [Required(ErrorMessage = "Identifire Key must be  4 digits number and start upper 1000")]
        [Range(1000, 9999, ErrorMessage = "Identifire Key must be  4 digits number and start upper 1000")]
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

        [MaxFileSize(1 * 1024 * 1024)] public IFormFile UserAvatarFormFile { get; set; }

        public List<int> UserSelectedRoles { get; set; }


    }


    public class EditeBankForAdminViewModel
    {
        public int BankMonthId { get; set; }
        public int UserId { get; set; }
        public double Saving { get; set; }

        public double InvestMonth { get; set; }

        public double DirectSell { get; set; }

        public double InDirect { get; set; }

        public double UniLevel { get; set; }

        public double Binary { get; set; }

        public double Matrix { get; set; }
    }

    public class EditeBankForAdminViewModelWeek
    {
        public int BankWeekId { get; set; }
        public int UserId { get; set; }
        public double Saving { get; set; }

        public double InvestWeek { get; set; }

        public double DirectSell { get; set; }

        public double InDirect { get; set; }

        public double UniLevel { get; set; }

        public double Binary { get; set; }

        public double Matrix { get; set; }
    }

    #region forecast

    public class GetUserForecastForAdninViewModel
    {
        public List<UserAnswer> UserAnswers { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }

    #endregion

}




