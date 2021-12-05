using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FirstZX.Core.Convertor;
using FirstZX.Core.DTO;
using FirstZX.Core.Generator;
using FirstZX.Core.Senders;
using FirstZX.Core.Services.Interface;
using FirstZX.Datalayer.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FirstZX.Controllers
{
    public class Account : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRender;
        private IPermission _permission;

        public Account(IUserService userService, IViewRenderService viewRender, IPermission permission)
        {
            _userService = userService;
            _viewRender = viewRender;
            _permission = permission;
        }



        #region account reg and log
        [Route("Register")]
        public IActionResult CheckReferralForRegister()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Register")]
        public IActionResult CheckReferralForRegister(IdentifireEmailChechkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!_userService.IsExistEmail(FixedText.FixEmail(model.IdentifireEmail)))
            {
                ModelState.AddModelError("IdentifireEmail", "Email Address Is Not Exist");
                return View(model);
            }

            return Redirect("RegisterF?referralEmail=" + model.IdentifireEmail);
        }
        [Route("RegisterF")]
        public IActionResult Register(string referralEmail)
        {
            var referraluser = _userService.GetUserByEmail(referralEmail);
            var referralName = referraluser.LastName + " " + referraluser.FirstName;
            TempData["refferalEmail"] = referraluser.Email;
            TempData["refferalName"] = referralName;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("RegisterF")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {

                return View(register);
            }

            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "Email Address Is Exist");
                TempData["refferalEmail"] = register.IdentifireEmail;
                TempData["refferalName"] = register.IdentifireName;
                return View(register);
            }

            User user = new User()
            {
                LastName = register.LastName,
                FirstName = register.FirstName,
                Email = FixedText.FixEmail(register.Email),
                IdentifireEmail = FixedText.FixEmail(register.IdentifireEmail),
                RegisterDate = DateTime.Now,
                Password = PasswordGeneratorMD5.EncodePasswordMd5(register.Password),
                ActiveCode = CharGenerator.UnicCodeGenerate(),
                IsActive = false,
                UserAvatar = "Default.png",
                Country = register.Country,
                ValidId = register.ValidId,
                Rank = "Active",
                SelectedPlan = "Uni Level"


            };
            int userId = _userService.AddUser(user);
            List<int> userRole = new List<int>() { 3 };

            _permission.AddRoleToUserForCreateUserForAdmin(userRole, userId);


            #region sed active email

            string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "Activation Account", body);

            #endregion

            return View("SuccessRegister", user);
        }


        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]

        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userService.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.Email),

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.Rememberme
                    };
                    HttpContext.SignInAsync(principal, properties);




                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("Email", "Please activate the account");
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Email or Password is incorrect");
            }

            return View();



        }
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/login");
        }
        #endregion
        #region Active Account

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }

        #endregion

        #region forgot passwprd

        [Route("Forgotpassword")]
        public IActionResult Forgotpassword()
        {
            return View();
        }
        [HttpPost]
        [Route("Forgotpassword")]
        [ValidateAntiForgeryToken]

        public IActionResult Forgotpassword(ForgotPasswordViewModel forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPassword);
            }

            string fixedEmail = FixedText.FixEmail(forgotPassword.Email);
            User user = _userService.GetUserByEmail(fixedEmail);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email address Not found");
                return View(forgotPassword);
            }

            string bodyEmail = _viewRender.RenderToStringAsync("_ForgotPassword", user);
            SendEmail.Send(user.Email, "Reset Password", bodyEmail);
            return View("SuccessResetPassword", user);
        }
        public IActionResult ResetPasword(string id)
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ResetPasword(ResetPasswordViewModel ressPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(ressPassword);
            }

            User user = _userService.GetUserByActiveCode(ressPassword.ActiveCode);
            if (user == null)
            {
                return NotFound();
            }

            string hashPasword = PasswordGeneratorMD5.EncodePasswordMd5(ressPassword.Password);
            user.Password = hashPasword;
            _userService.UpdateUser(user);
            return Redirect("login");
        }

        #endregion
    }
}
