using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.DAO;
using DAO.IRepositories;
using DAO.Models;

namespace DAO.Repositories
{
    public class UserRepository : IUserRepository
    {
        public DataTable Login(string username, string password)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = new DbSQL().Login(username, password);
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public bool ChangePassword(string userid, string oldPassword, string newPassword)
        {
            try
            {
                return new DbSQL().ChangePassword(userid, oldPassword, newPassword);
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public int User_InsertUpdate(string xml, out string output)
        {
            try
            {
                return new DbSQL().User_InsertUpdate(xml, out output);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Users User_GetOneByID(int UserID)
        {
            Users u = new Users();
            try
            {
                DataTable dt = new DataTable();
                dt = new DbSQL().User_GetOneByID(UserID);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    u = new Users(dt.Rows[i]);
                }
            }
            catch { }
            return u;
        }

        public Users User_GetOneByEmail(string Email)
        {
            Users u = new Users();
            try
            {
                DataTable dt = new DataTable();
                dt = new DbSQL().User_GetOneByEmail(Email);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    u = new Users(dt.Rows[i]);
                }
            }
            catch { }
            return u;
        }

        public bool User_InsertToken(string Email, string Token)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new DbSQL().User_InsertToken(Email, Token);
                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch { }
            return false;
        }
        public DataTable User_CheckToken(string Email, string Token)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = new DbSQL().User_CheckToken(Email, Token);
                return dt;
            }
            catch { }
            return dt;
        }

        public Users User_GetOneByUserName(string UserName)
        {
            Users u = new Users();
            try
            {
                DataTable dt = new DataTable();
                dt = new DbSQL().User_GetOneByUserName(UserName);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    u = new Users(dt.Rows[i]);
                }
            }
            catch { }
            return u;
        }
    }
}
