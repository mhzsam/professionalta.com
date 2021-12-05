using System.ComponentModel.DataAnnotations;

namespace FirstZX.Datalayer.Entities.User
{
    public class BankMonth
    {
        public BankMonth()
        {
            
        }
        [Key]
        public int BankMonthId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Saving")]
        [MaxLength(50, ErrorMessage = "{0} MaxLenth is 50")]
        public double Saving { get; set; }

        [Display(Name = "InvestMonth")]
        [MaxLength(50, ErrorMessage = "{0} MaxLenth is 50")]
        public double InvestMonth { get; set; }

        [Display(Name = "DirectSell")]
        [MaxLength(50, ErrorMessage = "{0} MaxLenth is 50")]
        public double DirectSell { get; set; }

        [Display(Name = "InDirect")]
        [MaxLength(50, ErrorMessage = "{0} MaxLenth is 50")]
        public double InDirect { get; set; }

        [Display(Name = "UniLevel")]
        [MaxLength(50, ErrorMessage = "{0} MaxLenth is 50")]
        public double UniLevel { get; set; }

        [Display(Name = "Binary")]
        [MaxLength(50, ErrorMessage = "{0} MaxLenth is 50")]
        public double Binary { get; set; }

        [Display(Name = "Matrix")]
        [MaxLength(50, ErrorMessage = "{0} MaxLenth is 50")]
        public double Matrix { get; set; }

        #region relation

        public virtual User User { get; set; }

        #endregion
    }
}