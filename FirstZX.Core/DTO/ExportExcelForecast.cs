using System;

namespace FirstZX.Core.DTO
{
    public class ExportExcelForecast
    {
        
        public int UserAnswerId { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; }
       
        
        public DateTime DateTime { get; set; }
        
        public bool IsDelete { get; set; }
        
        public int AnswerId { get; set; }
        
        public string Cryptocurrency { get; set; }
        
        public string RatioCryptocurrency { get; set; }
       public int BuyOrSell { get; set; }
        public double Percentage { get; set; }
        
    }
}