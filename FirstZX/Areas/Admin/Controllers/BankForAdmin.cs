using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstZX.Core.DTO;
using FirstZX.Core.Security;
using FirstZX.Core.Services.Interface;
using Microsoft.AspNetCore.SignalR;

namespace FirstZX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker(2)]

    public class BankForAdmin : Controller
    {
        private IUserService _userService;
        private IPermission _permission;

        public BankForAdmin(IUserService userService, IPermission permission)
        {
            _userService = userService;
            _permission = permission;
        }

        [Route("bankuserlist")]
        public IActionResult UserListForBankEdite(int pageId = 1, string filterEmail = "", string filterUserId = "")
        {
            return View(_userService.GetUserForAdmin(pageId, filterEmail, filterUserId));
        }

        [Route("EditeUserForBank")]
        public IActionResult EditeUserForBank(int id)
        {
            
            ViewBag.BackEarnDetail = _userService.getBankDetailEarn(id);
            if (TempData["status"]!=null)
            {
                ViewBag.Issuccuss = true;
                
            }
            if (TempData["status2"] != null)
            {
                ViewBag.IssuccussEBD = true;

            }


            var myModel = new EditUserForBankBankDetailEarn
            {
                BankDetailEarn = _userService.getBankDetailEarn(id),
                EditBankForAdminViewModels = _userService.GetbankDetailForAdmin(id),
                UserId = id
            };

            
            return View(myModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBankRow(int id)
        {
            
            _userService.AddRowToBankUser(id);
            return Redirect($"/EditeUserForBank?id={id}");
        }

        [Route("DeleteRowForAdmin")]
        public IActionResult DeleteRow(int BankId)
        {
            
            return View(_userService.FinddeleteRowForAdmin(BankId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmeDeleteRow(DeleteRowForAdmin DeleteRowForAdmin)
        {
            int id = DeleteRowForAdmin.UserId;

            if (!ModelState.IsValid)
            {
                return View("DeleteRow", DeleteRowForAdmin);
            }
            _userService.DeleteBankRowForAdmin(DeleteRowForAdmin.BankMonthId);
            return Redirect($"/EditeUserForBank?id={id}");
        }

        public IActionResult SaveEditeRowForAdmin(EditUserForBankBankDetailEarn models )
        {
            // case in list all userId is Same 
            var id = models.UserId;
            if (!ModelState.IsValid)
            {

                return Redirect($"/EditeUserForBank?id={id}");
            }

            if (models.EditBankForAdminViewModels != null)
            {
                _userService.SaveEditedRowForAdmin(models.EditBankForAdminViewModels);
            }
           TempData["status"] = "Success";
            return Redirect($"/EditeUserForBank?id={id}");
        }

        [Route("CreateEarnBank")]
        public IActionResult CreateEarnBankForUser(int id)
        {
            _userService.CreateBankDetailEarnRow(id);
            return Redirect($"/EditeUserForBank?id={id}");
        } 


        public IActionResult SaveEarnBankOfUser(EditUserForBankBankDetailEarn models)
        {
            var id = models.BankDetailEarn.UserId;
            if (!ModelState.IsValid)
            {
                return Redirect($"/EditeUserForBank?id={id}");
            }
            _userService.SaveEarnBankForAdmin(models.BankDetailEarn);
            TempData["status2"] = "Success";
            return Redirect($"/EditeUserForBank?id={id}");
        }
    }
}
