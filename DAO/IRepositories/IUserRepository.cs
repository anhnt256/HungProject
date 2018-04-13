using DAO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.IRepositories
{
    public interface IUserRepository
    {
        DataTable Login(string username, string password);
        bool ChangePassword(string userid, string oldPassword, string newPassword);
        int User_InsertUpdate(string xml, out string output);
        Users User_GetOneByID(int UserID);
        Users User_GetOneByEmail(string Email);
        Users User_GetOneByUserName(string UserName);
        bool User_InsertToken(string Email, string Token);
        DataTable User_CheckToken(string Email, string Token);
    }
}
