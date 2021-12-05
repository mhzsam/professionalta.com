using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstZX.Core.Services.Interface;

namespace FirstZX.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BankRequestForAdmin : Controller
    {
        private IRequestStatus _requestStatus;

        public BankRequestForAdmin(IRequestStatus requestStatus)
        {
            _requestStatus = requestStatus;
        }
        public IActionResult ListOfRequest(int pageId = 1, string filterEmail = "", int filerUserId = 0)
        {
            return View(_requestStatus.getallBankRequestsForAdmin(pageId, filterEmail, filerUserId));
        }
        [Route("AcceptForUser")]

        public IActionResult AcceptForUser(int id,int pageId)
        {
            _requestStatus.SaveAcceptforUser(id);
            var model = _requestStatus.getallBankRequestsForAdmin(pageId, "", 0);
            return View("ListOfRequest", model);
        }
        [Route("DeleteBankRequestForUser")]
        public IActionResult DeleteBankRequestForUser(int id, int pageId)
        {
            _requestStatus.DeleteBankRequestForUser(id);
            var model = _requestStatus.getallBankRequestsForAdmin(pageId, "", 0);
            return View("ListOfRequest", model);
        }
    }
}
