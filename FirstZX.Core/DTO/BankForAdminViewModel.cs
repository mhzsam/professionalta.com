namespace FirstZX.Core.DTO
{
    public class DeleteRowForAdmin
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
    public class DeleteRowForAdminWeek
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
}