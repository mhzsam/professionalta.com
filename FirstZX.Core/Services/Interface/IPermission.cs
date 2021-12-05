using System.Collections.Generic;
using FirstZX.Datalayer.Entities.User;

namespace FirstZX.Core.Services.Interface
{
    public interface IPermission
    {
        #region  roles

        List<Role> GetRoleForAdmin();
        void AddRoleToUserForCreateUserForAdmin(List<int> roleId ,int userId);
        List<int> GetUserRoleForEditeUserAdmin(int userId);

        void EditeRoleForEditeUserForAdmin(List<int> roleId, int userId);
        void DeletUeserForAdmin(string emial);

        #endregion

        #region permission checker

        bool UserCheckpermission(int roleId, string userEmail);

        #endregion
    }
}