using System.Collections.Generic;
using FirstZX.Datalayer.Entities.User;

namespace FirstZX.Core.DTO
{
    public class BankAndBankRequest
    {
        public List<BankMonth> Banks { get; set; }
        public BankRequest BankRequest { get; set; }
        public BankDetailEarn BankDetailEarn { get; set; }
        public int UserID { get; set; }
    }
    public class BankAndBankRequestweek
    {
        public List<BankWeek> BankWeeks { get; set; }
        public BankRequest BankRequest { get; set; }
        public BankDetailEarnWeek BankDetailEarnWeek { get; set; }
        public int UserID { get; set; }
    }
    public class EditUserForBankBankDetailEarn
    {
        public List<FirstZX.Core.DTO.EditeBankForAdminViewModel> EditBankForAdminViewModels { get; set; }
        public BankDetailEarn BankDetailEarn { get; set; }
        public int UserId { get; set; }

    }
    public class EditUserForBankBankDetailEarnWeek
    {
        public List<FirstZX.Core.DTO.EditeBankForAdminViewModelWeek> EditBankForAdminViewModelsWeek { get; set; }
        public BankDetailEarnWeek BankDetailEarnWeek { get; set; }
        public int UserId { get; set; }

    }




    public class ListOfRequestForAdmin
    {
        public List<BankRequest> BankRequests { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }

   
}