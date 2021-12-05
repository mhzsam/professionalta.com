using System.Collections.Generic;
using FirstZX.Core.DTO;
using FirstZX.Datalayer.Entities.User;

namespace FirstZX.Core.Services.Interface
{
    public interface IUserService
    {
        bool IsExistEmail(string email);
        int AddUser(User user);
        User LoginUser(LoginViewModel loginViewModel);
        User CheckUserEmailForReferral(string email);

        #region Active accont

        bool ActiveAccount(string activeCode);

        #endregion

        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activecode);

        void UpdateUser(User user);

        #region UserPanel

        UserInformationViewModel getUserInformation(string email);
        SideBarUserPanelViewModel getSideBarUserPanelViewModel(string email);
        EditeProfileViewModel getEditeProfileViewModel(string email);
        bool EditeProfile(string email, EditeProfileViewModel editeProfileViewModel);
        int GetUserIdByEmail(string email);
        #endregion

        #region admin

        UsersForAdminViewModel GetUserForAdmin(int pageId = 1, string filterEmail = "", string filerUserId = "");
        void DeletUserForAdmin(string Email);
        void SaveEditeUserForEAdmin(EditeUserForAdminViewModel EditeUserForAdminViewModel);



        #endregion

        #region bank

        List<EditeBankForAdminViewModel> GetbankDetailForAdmin(int id);
        List<EditeBankForAdminViewModelWeek> GetbankDetailForAdminWeek(int id);
        void AddRowToBankUser(int id);
        void AddRowToBankUserWeek(int id);
        DeleteRowForAdmin FinddeleteRowForAdmin(int bankId);
        DeleteRowForAdminWeek FinddeleteRowForAdminWeek(int bankId);
        void DeleteBankRowForAdmin(int bankId);
        void DeleteBankRowForAdminWeek(int bankId);

        void SaveEditedRowForAdmin(List<FirstZX.Core.DTO.EditeBankForAdminViewModel> models);
        void SaveEditedRowForAdminWeek(List<FirstZX.Core.DTO.EditeBankForAdminViewModelWeek> models);
        void SaveEarnBankForAdmin(BankDetailEarn models);
        void SaveEarnBankForAdminWeek(BankDetailEarnWeek models);
        List<BankMonth> GetBankDetailForUser(string email);
        List<BankWeek> GetBankDetailForUserWeek(string email);
        BankRequest getBankRequest(string email);

        BankDetailEarn getBankDetailEarn(string email);
        BankDetailEarnWeek getBankDetailEarnWeek(string email);

        void SubmitUserReinvest(int userId);
        void SubmitUserCasOut(int userId);

        BankDetailEarn getBankDetailEarn(int id);
        BankDetailEarnWeek getBankDetailEarnWeek(int id);
        void CreateBankDetailEarnRow(int id);
        void CreateBankDetailEarnRowWeek(int id);

        #endregion


        #region answer

        List<UserAnswer> getUserAnswers(string email);
        void StartForeCast(int userId);
        List<Answer> getAnswers(int UAId);
        bool SaveForcastAnswer(List<Answer> answers);
        bool CheckUAIdIsDeactive(int uaId);
        bool NextLevelForcastChecker(int UAId);
        GetUserForecastForAdninViewModel GetUserForecastForAdnin(int pageId = 1, string filterEmail = "", string filterUserId = "");
        List<Answer> getUserAnswersForAdmin(int UAId);
        void RestartUserForcast(int UAId);

        #endregion



    }
}