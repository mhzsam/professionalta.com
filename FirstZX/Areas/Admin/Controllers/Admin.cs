using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstZX.Core.Convertor;
using FirstZX.Core.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using FirstZX.Core.DTO;
using FirstZX.Core.Generator;
using FirstZX.Core.Security;
using FirstZX.Datalayer.Entities.User;


namespace FirstZX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker(2)]
    public class Admin : Controller
    {
        private IUserService _userService;
        private IPermission _permission;

        public Admin(IUserService userService, IPermission permission)
        {
            _userService = userService;
            _permission = permission;
        }
        [Route("admin")]
        public IActionResult Index()
        {
            return View(_userService.getUserInformation(User.Identity.Name));
        }

        
        public IActionResult EditeUserAdmin(int pageId = 1, string filterEmail = "", string filterUserId = "")
        {
            return View(_userService.GetUserForAdmin(pageId, filterEmail, filterUserId));
        }

        [Route("DeletUserAdmin")]


        public IActionResult DeletUserForAdmin(string id)
        {
            var findUser = _userService.GetUserByEmail(id);
            var UserForAdminDeletViewModel = new UserForAdminDeletViewModel
            {
                Email = findUser.Email
            };
            return View(UserForAdminDeletViewModel);
        }
        [HttpPost]
        [Route("DeletUserAdmin")]
        [ValidateAntiForgeryToken]

        public IActionResult DeletUserForAdmin(UserForAdminDeletViewModel userForAdminDeletViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(userForAdminDeletViewModel);
            }

            var user = userForAdminDeletViewModel.Email.ToString();
            //_permission.DeletUeserForAdmin(user);
            _userService.DeletUserForAdmin(user);
           

            return Redirect("/Admin/Admin/EditeUserAdmin");
        }

        [Route("EditeUserAdmin")]
        public IActionResult EditeUserForAdmin(string id)
        {
            ViewBag.Roles = _permission.GetRoleForAdmin();
            var findUser = _userService.GetUserByEmail(id);
            var userId = findUser.UserId;
            var user = new EditeUserForAdminViewModel
            {
                Email = findUser.Email,
                UserAvatar = findUser.UserAvatar,
                LastName = findUser.LastName,
                Country = findUser.Country,
                DigitalWalletId = findUser.DigitalWalletId,
                FirstName = findUser.FirstName,
                IdentifireEmail = findUser.IdentifireEmail,
                UserAvatarFormFile = null,
                IdentifireKey = findUser.IdentifireKey,
                IsActive = findUser.IsActive,
                RegisterDate = findUser.RegisterDate,
                ValidId = findUser.ValidId,
                UserSelectedRoles = _permission.GetUserRoleForEditeUserAdmin(userId),
                Rank = findUser.Rank,
                SelectedPlan = findUser.SelectedPlan

            };
            return View(user);
        }

        [HttpPost]
        [Route("EditeUserAdmin")]
        [ValidateAntiForgeryToken]
        public IActionResult EditeUserForAdmin(EditeUserForAdminViewModel editeUserForAdminViewModel, List<int> SelectedRole)
        {
            ViewBag.Roles = _permission.GetRoleForAdmin();
            var findUser = _userService.GetUserByEmail(editeUserForAdminViewModel.Email);
            var userId = findUser.UserId;
            if (editeUserForAdminViewModel.UserSelectedRoles==null)
            {
                editeUserForAdminViewModel.UserSelectedRoles = SelectedRole;
            }

           
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _permission.GetRoleForAdmin();
               
                return View(editeUserForAdminViewModel);
            }

            _userService.SaveEditeUserForEAdmin(editeUserForAdminViewModel);
            _permission.EditeRoleForEditeUserForAdmin(SelectedRole,userId);

            ViewBag.IsSuccess = true;
            return Redirect("/Admin/Admin/EditeUserAdmin");
        }


        [Route("CreateUserForAdmin")]
        public IActionResult CreateUserForAdmin()
        {
            ViewBag.Roles = _permission.GetRoleForAdmin();
            return View();

        }



        [Route("CreateUserForAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUserForAdmin(User userModel,List<int> SelectedRole)
        {
            

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _permission.GetRoleForAdmin();
                return View(userModel);
            }
            if (_userService.IsExistEmail(FixedText.FixEmail(userModel.Email)))
            {
                ModelState.AddModelError("Email", "Email Address Is Exist");
                return View(userModel);
            }

            User user = new User()
            {
                Email = userModel.Email,
                UserAvatar = "Default.png",
                LastName = userModel.LastName,
                FirstName = userModel.FirstName,
                IdentifireEmail = userModel.IdentifireEmail,
                RegisterDate = userModel.RegisterDate,
                Password = PasswordGeneratorMD5.EncodePasswordMd5(userModel.Password),
                IsActive = userModel.IsActive,
                ActiveCode = CharGenerator.UnicCodeGenerate(),
                Country = userModel.Country,
                DigitalWalletId = userModel.DigitalWalletId,
                IdentifireKey = userModel.IdentifireKey,
                ValidId = userModel.ValidId
            };
            int userId= _userService.AddUser(user);
            _permission.AddRoleToUserForCreateUserForAdmin(SelectedRole,userId);
            

            return Redirect("/Admin/Admin/EditeUserAdmin");
        }




    }
}
