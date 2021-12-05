using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FirstZX.Core.Services.Interface;
using FirstZX.Datalayer.Context;
using FirstZX.Datalayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace FirstZX.Core.Services
{
    public class Permission : IPermission
    {
        private FirstZXContext _context;

        public Permission(FirstZXContext context)
        {
            _context = context;
        }



        public List<Role> GetRoleForAdmin()
        {
            return _context.Roles.ToList();
        }

        public void AddRoleToUserForCreateUserForAdmin(List<int> roleId, int userId)
        {
            foreach (int item in roleId)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = item,
                    UserId = userId,
                });
            }

            _context.SaveChanges();
        }

        public List<int> GetUserRoleForEditeUserAdmin(int userId)
        {
            return _context.UserRoles.Where(w => w.UserId == userId).Select(s => s.RoleId).ToList();

        }

        public void EditeRoleForEditeUserForAdmin(List<int> roleId, int userId)
        {
            _context.UserRoles.Where(u=>u.UserId == userId).ToList().ForEach(r=>_context.UserRoles.Remove(r));
            _context.SaveChanges();
            AddRoleToUserForCreateUserForAdmin(roleId, userId);
        }

        public void DeletUeserForAdmin(string email)
        {
            var userFind = _context.Users.SingleOrDefault(u => u.Email == email);
            var userId = userFind.UserId;
            var UserRoleForDelet= _context.UserRoles.Where(r => r.UserId == userId);
           foreach (var item in UserRoleForDelet)
           {
               _context.UserRoles.Remove(item);
               _context.SaveChanges();
           }
        }

        public bool UserCheckpermission(int roleId, string userEmail)
        {
            int UserId = _context.Users.Single(u => u.Email == userEmail).UserId;
            List<int> UserRole = _context.UserRoles.Where(r => r.UserId == UserId).Select(s => s.RoleId).ToList();
            if (!UserRole.Any())
            {
                return false;
            }

            return UserRole.Any(u=>u== roleId);
        }
    }
}