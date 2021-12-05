using System;
using System.ComponentModel.DataAnnotations;

namespace FirstZX.Datalayer.Entities.User
{
    public class BankRequest
    {
        public BankRequest()
        {

        }

        [Key]
        public int  BankRequestId { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm }", ApplyFormatInEditMode = true)]
        [Display(Name = "Register Date")]
        public DateTime RequestTime { get; set; }
        public bool CashOut { get; set; }
        public bool ConfirmCashout { get; set; }
        public bool Reinvest { get; set; }
        public bool ConfirmReinvest { get; set; }
        public bool IsDeActive { get; set; }

        #region Relation

        public virtual User User { get; set; }

        #endregion
    }
}