using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using FirstZX.Core.DTO;
using FirstZX.Core.Security;
using FirstZX.Core.Services.Interface;
using FirstZX.Datalayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace FirstZX.Areas.UserPanel.Controllers
{
    

    [Area("Userpanel")]
   [PermissionChecker(3)]

    public class UserPanel : Controller
    {
        private IUserService _userService;
        private IRequestStatus _requestStatus;

        public UserPanel(IUserService userService, IRequestStatus requestStatus)
        {
            _userService = userService;
            _requestStatus = requestStatus;
        }

        public IActionResult Index()
        {
            return View(_userService.getUserInformation(User.Identity.Name));
        }

        [Route("userpanel/editeprofile")]
        public IActionResult EditeProfile()
        {
            return View(_userService.getEditeProfileViewModel(User.Identity.Name));
        }

        [Route("userpanel/editeprofile")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditeProfile(EditeProfileViewModel editeProfile)
        {
            if (!ModelState.IsValid)
            {
                return View(editeProfile);
            }

            _userService.EditeProfile(User.Identity.Name, editeProfile);
            ViewBag.IsSuccess = true;
            return View(_userService.getEditeProfileViewModel(User.Identity.Name));
        }
       
        public IActionResult BankForUser()
        {
            string userEmail = User.Identity.Name;
            
            TempData["RequestStatus"]= _requestStatus.UserBankRequestStatus(userEmail);
            
            
            BankAndBankRequest bankAndBankRequest = new BankAndBankRequest()
            {
                BankRequest = _userService.getBankRequest(userEmail),
                Banks = _userService.GetBankDetailForUser(userEmail),
                BankDetailEarn = _userService.getBankDetailEarn(userEmail),
                UserID = _userService.GetUserIdByEmail(userEmail),
                

            };
            
            return View(bankAndBankRequest);
        }
        public IActionResult BankForUserWeek()
        {
            string userEmail = User.Identity.Name;

            TempData["RequestStatus"] = _requestStatus.UserBankRequestStatus(userEmail);


            BankAndBankRequestweek bankAndBankRequestweek = new BankAndBankRequestweek()
            {
                BankRequest = _userService.getBankRequest(userEmail),
                BankWeeks = _userService.GetBankDetailForUserWeek(userEmail),
                BankDetailEarnWeek = _userService.getBankDetailEarnWeek(userEmail),
                UserID = _userService.GetUserIdByEmail(userEmail),


            };

            return View(bankAndBankRequestweek);
        }
        [Route("UserReinvet")]
        public IActionResult UserReinvest(int id)
        {
            string userEmail = User.Identity.Name;
            BankAndBankRequest bankAndBankRequest = new BankAndBankRequest()
            {
                BankRequest = _userService.getBankRequest(userEmail),
                Banks = _userService.GetBankDetailForUser(userEmail),
                BankDetailEarn = _userService.getBankDetailEarn(userEmail),
                UserID = id
            };
            
            _userService.SubmitUserReinvest(id);
            TempData["RequestStatus"] = _requestStatus.UserBankRequestStatus(userEmail);
          return View("BankForUser", bankAndBankRequest);
        }

        [Route("UserCashOut")]
        public IActionResult UserCashOut(int id)
        {
            string userEmail = User.Identity.Name;
            BankAndBankRequest bankAndBankRequest = new BankAndBankRequest()
            {
                BankRequest = _userService.getBankRequest(userEmail),
                BankDetailEarn = _userService.getBankDetailEarn(userEmail),
                Banks = _userService.GetBankDetailForUser(userEmail),
                UserID = id
            };
            _userService.SubmitUserCasOut(id);
            TempData["RequestStatus"] = _requestStatus.UserBankRequestStatus(userEmail);
            return View("BankForUser", bankAndBankRequest);

        }

        public IActionResult Forecast()
        {
            if (TempData["status"] != null)
            {
                ViewBag.UAIdIsDeactive = true;

            }
            if (TempData["status2"] != null)
            {
                ViewBag.TimeOfNextLevel= true;

            }

            string userEmail = User.Identity.Name;
            ViewBag.userId = _userService.GetUserByEmail(userEmail).UserId;
            List < UserAnswer > userAnswers= _userService.getUserAnswers(userEmail);
            
            return View(userAnswers);
        }
        [Route("StartForecast")]
        public IActionResult StartForecast(int userId)
        {

            _userService.StartForeCast(userId);
            string userEmail = User.Identity.Name;
            List<UserAnswer> userAnswers = _userService.getUserAnswers(userEmail);
            return Redirect("/userpanel/userpanel");
        }

        public IActionResult AddForcast(int UAId)
        {
            var UAIdIsDeactive = _userService.CheckUAIdIsDeactive(UAId);
            if (UAIdIsDeactive==false)
            {
                var answer = _userService.getAnswers(UAId);
                return View(answer);
            }
            else
            {
                TempData["status"] = "fail";
                return Redirect("/Userpanel/UserPanel/Forecast");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddForcast(List<Answer> answer)
        {
            
            if (!ModelState.IsValid)
            {
                return View(answer);
            }
            var result= _userService.SaveForcastAnswer(answer);
            
            return Redirect("/Userpanel/UserPanel/Forecast");
        }
        [Route("NextLevelForcast")]
        public IActionResult NextLevelForcast(int userId)
        {
            var resualt = _userService.NextLevelForcastChecker(userId);
            if (resualt==false)
            {
                TempData["status2"] = "fail";
                return Redirect("/Userpanel/UserPanel/Forecast");
              
            }
            else
            {
                var url = "/StartForecast?userId=" + userId;
                return Redirect(url);
            }
            
        }
    }
}
