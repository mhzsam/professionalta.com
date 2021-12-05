using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstZX.Core.DTO;
using FirstZX.Core.Security;
using FirstZX.Core.Services.Interface;

namespace FirstZX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker(2)]
    public class BankForAdminWeek : Controller
    {
        private IUserService _userService;
        private IPermission _permission;

        public BankForAdminWeek(IUserService userService, IPermission permission)
        {
            _userService = userService;
            _permission = permission;
        }
        [Route("bankuserlistweek")]
        public IActionResult UserListForBankEditeWeek(int pageId = 1, string filterEmail = "", string filterUserId = "")
        {
            return View(_userService.GetUserForAdmin(pageId, filterEmail, filterUserId));
        }

        [Route("EditeUserForBankWeek")]
        public IActionResult EditeUserForBankWeek(int id)
        {

            ViewBag.BackEarnDetail = _userService.getBankDetailEarn(id);
            if (TempData["status"] != null)
            {
                ViewBag.Issuccuss = true;

            }
            if (TempData["status2"] != null)
            {
                ViewBag.IssuccussEBD = true;

            }


            var myModel = new EditUserForBankBankDetailEarnWeek()
            {
                BankDetailEarnWeek = _userService.getBankDetailEarnWeek(id),
                EditBankForAdminViewModelsWeek = _userService.GetbankDetailForAdminWeek(id),
                UserId = id
            };


            return View(myModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBankRowWeek(int id)
        {

            _userService.AddRowToBankUserWeek(id);
            return Redirect($"/EditeUserForBankWeek?id={id}");
        }
        [Route("DeleteRowForAdminWeek")]
        public IActionResult DeleteRowWeek(int BankId)
        {

            return View(_userService.FinddeleteRowForAdminWeek(BankId));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmeDeleteRowWeek(DeleteRowForAdminWeek DeleteRowForAdminWeek)
        {
            int id = DeleteRowForAdminWeek.UserId;

            if (!ModelState.IsValid)
            {
                return View("DeleteRowWeek", DeleteRowForAdminWeek);
            }
            _userService.DeleteBankRowForAdminWeek(DeleteRowForAdminWeek.BankWeekId);
            return Redirect($"/EditeUserForBankWeek?id={id}");
        }
        public IActionResult SaveEditeRowForAdminWeek(EditUserForBankBankDetailEarnWeek modelsWeek)
        {
            // case in list all userId is Same 
            var id = modelsWeek.UserId;
            if (!ModelState.IsValid)
            {

                return Redirect($"/EditeUserForBankWeek?id={id}");
            }

            if (modelsWeek.EditBankForAdminViewModelsWeek != null)
            {
                _userService.SaveEditedRowForAdminWeek(modelsWeek.EditBankForAdminViewModelsWeek);
            }
            TempData["status"] = "Success";
            return Redirect($"/EditeUserForBankWeek?id={id}");
        }
        [Route("CreateEarnBankWeek")]
        public IActionResult CreateEarnBankForUserWeek(int id)
        {
            _userService.CreateBankDetailEarnRowWeek(id);
            return Redirect($"/EditeUserForBankWeek?id={id}");
        }
        public IActionResult SaveEarnBankOfUserWeek(EditUserForBankBankDetailEarnWeek models)
        {
            var id = models.BankDetailEarnWeek.UserId;
            if (!ModelState.IsValid)
            {
                return Redirect($"/EditeUserForBankWeek?id={id}");
            }
            _userService.SaveEarnBankForAdminWeek(models.BankDetailEarnWeek);
            TempData["status2"] = "Success";
            return Redirect($"/EditeUserForBankWeek?id={id}");
        }
    }
}
