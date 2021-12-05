using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace FirstZX.Datalayer.Entities.User
{
    
    public class BankDetailEarn
    {
        public BankDetailEarn()
        {
            
        }
        [Key]
        public int BankDetailEarnId { get; set; }

        public int UserId { get; set; }
        public double RewardEarnmonth1 { get; set; }
        public double RewardEarnmonth2 { get; set; }
        public double RewardEarnmonth3 { get; set; }
        public double RewardEarnmonth4 { get; set; }

        #region relation

        public virtual User User { get; set; }

        #endregion
    }
}