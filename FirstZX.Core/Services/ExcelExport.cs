using System.Collections.Generic;
using System.Linq;
using FirstZX.Core.DTO;
using FirstZX.Core.Services.Interface;
using FirstZX.Datalayer.Context;

namespace FirstZX.Core.Services
{
    public class ExcelExport : IExcelExport
    {
        private FirstZXContext _context;

        public ExcelExport(FirstZXContext context)
        {
            _context = context;
        }

        public List<ExportExcelForecast> GetInformationForExcelFromForecaste()
        {
            var ExportLiast = (from u in _context.UserAnswer
                join a in _context.Answers
                    on u.UserAnswerId equals a.UserAnswerId
                select new ExportExcelForecast()
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    DateTime =u.DateTime ,
                    UserAnswerId =u.UserAnswerId ,
                    AnswerId = a.AnswerId,
                    Cryptocurrency =a.Cryptocurrency ,
                    RatioCryptocurrency =a.RatioCryptocurrency ,
                    BuyOrSell =a.BuyOrSell ,
                    Percentage =a.Percentage ,
                    IsDelete = u.IsDelete
                }).ToList();
            return ExportLiast;
        }
    }
}