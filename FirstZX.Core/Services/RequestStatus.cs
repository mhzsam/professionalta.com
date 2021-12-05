using System;
using System.Collections.Generic;
using System.Linq;
using FirstZX.Core.DTO;
using FirstZX.Core.Services.Interface;
using FirstZX.Datalayer.Context;
using FirstZX.Datalayer.Entities.User;

namespace FirstZX.Core.Services
{
    public class RequestStatus : IRequestStatus
    {
        private FirstZXContext _context;

        public RequestStatus(FirstZXContext context)
        {
            _context = context;
        }

        public string UserBankRequestStatus(string email)
        {
            int userId = _context.Users.SingleOrDefault(u => u.Email == email).UserId;
            BankRequest bankRequest = _context.BankRequests.Where(B => B.IsDeActive == false).SingleOrDefault(u => u.UserId == userId);
            if (bankRequest != null && bankRequest != default)
            {
                if (bankRequest.Reinvest == false && bankRequest.CashOut == false)
                {
                    return "No Request";
                }

                if (bankRequest.Reinvest == true && bankRequest.ConfirmReinvest == false)
                {
                    return "Your Reinvest Request Send...  Wait for Admin Confirme";
                }

                if (bankRequest.Reinvest == true && bankRequest.ConfirmReinvest == true)
                {
                    return "Your Reinvest Request Submited";
                }

                if (bankRequest.CashOut == true && bankRequest.ConfirmCashout == false)
                {
                    return "Your Cash Out Request Send...  Wait for Admin Confirme";
                }

                if (bankRequest.CashOut == true && bankRequest.ConfirmCashout == true)
                {
                    return "Your Cash Out Request Submited";
                }
                else
                {
                    return "No Request Find ";
                }
            }

            return "No Request Find ";
        }

        public ListOfRequestForAdmin getallBankRequestsForAdmin(int pageId = 1, string filterEmail = "", int filerUserId = 0)
        {
            IQueryable<BankRequest> result = _context.BankRequests.Where(u => u.IsDeActive == false);
            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }
            if (filerUserId != 0)
            {
                result = result.Where(u => u.UserId == filerUserId);
            }
            //Showitem in Page
            int take = 10;
            int skip = (pageId - 1) * take;

            ListOfRequestForAdmin list = new ListOfRequestForAdmin()
            {
                BankRequests = result.OrderBy(u => u.UserId).Skip(skip).Take(take).ToList(),
                CurrentPage = pageId,
                PageCount = result.Count() / take
            };
            return list;
        }

        public int GetCountOfRequest()
        {
            int countNum = _context.BankRequests.Where(u => u.IsDeActive == false).Select(s => s.UserId).ToList().Count;
            return (countNum);
        }

        public void SaveAcceptforUser(int id)
        {
            var user = _context.BankRequests.Where(s => s.IsDeActive == false).SingleOrDefault(s => s.UserId == id);
            if (user.CashOut == true && user.ConfirmCashout == false)
            {
                user.ConfirmCashout = true;
            }
            if (user.Reinvest == true && user.ConfirmReinvest == false)
            {
                user.ConfirmReinvest = true;

            }

            _context.BankRequests.Update(user);
            _context.SaveChanges();
        }

        public void DeleteBankRequestForUser(int id)
        {
            var user = _context.BankRequests.Where(s => s.IsDeActive == false).SingleOrDefault(s => s.UserId == id);
            user.IsDeActive = true;

            _context.BankRequests.Update(user);
            _context.SaveChanges();
        }
    }
}