using System.Globalization;
using FirstZX.Core.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;

namespace FirstZX.Core.Security
{
    public class PermissionCheckerAttribute:AuthorizeAttribute,IAuthorizationFilter
    {
        private IPermission _permission;
        private int _roleId = 0;
        public PermissionCheckerAttribute(int roleId)
        {
            _roleId = roleId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permission = (IPermission) context.HttpContext.RequestServices.GetService(typeof(IPermission));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userEmail = context.HttpContext.User.Identity.Name;
                if (!_permission.UserCheckpermission(_roleId,userEmail))
                {
                    context.Result = new RedirectResult("/login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/login");
            }
        }
    }
}