using System.Collections.Generic;
using FirstZX.Core.DTO;
using FirstZX.Datalayer.Entities.User;

namespace FirstZX.Core.Services.Interface
{
    public interface IRequestStatus
    {

        string UserBankRequestStatus(string email);
        ListOfRequestForAdmin getallBankRequestsForAdmin(int pageId = 1, string filterEmail = "", int filerUserId = 0);
        int GetCountOfRequest();
        void SaveAcceptforUser(int id);
        void DeleteBankRequestForUser(int id);

    }

   
}