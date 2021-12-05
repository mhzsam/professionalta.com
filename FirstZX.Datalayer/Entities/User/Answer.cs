using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstZX.Datalayer.Entities.User
{
    public class Answer
    {
        public Answer()
        {
          
        }
        [Key]
        public int AnswerId { get; set; }

        public int UserAnswerId { get; set; }

        [Display(Name = "Coin")]
        [Required(ErrorMessage = "please input {0}")]
        [MaxLength(10, ErrorMessage = "{0} Max is {1}")]
        public string Cryptocurrency { get; set; }

        [Display(Name = "Ratio Of Coin")]
        [Required(ErrorMessage = "please input {0}")]
       
        public string RatioCryptocurrency { get; set; }
        [Display(Name = "Buy Or Sell")]
        [Required(ErrorMessage = "please input {0}")]
       
        public int BuyOrSell { get; set; }

        [Display(Name = "Percentage")]
        [Required(ErrorMessage = "please input {0}")]
        
        public double Percentage { get; set; }
        public bool IsDeactive { get; set; }

        #region relation

        public UserAnswer UserAnswer { get; set; }

        #endregion
    }
}