using System.ComponentModel.DataAnnotations;

namespace FirstZX.Datalayer.Entities.User
{
    public class BankDetailEarnWeek
    {
        public BankDetailEarnWeek()
        {
            
        }
        [Key]
        public int BankDetailEarnWeekId { get; set; }

        public int UserId { get; set; }
        public double RewardEarnWeek1 { get; set; }
        public double RewardEarnWeek2 { get; set; }
        public double RewardEarnWeek3 { get; set; }
        public double RewardEarnWeek4 { get; set; }

        #region relation

        public virtual User User { get; set; }

        #endregion
    }
}