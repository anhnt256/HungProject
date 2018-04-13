using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Models;

namespace DAO.DAO
{
    class DbSQL : DbData
    {
        public DbSQL()
        {
            ConnectionString = Configuration.ConnectionStringCaoQuy;
        }
        #region User
        public DataTable Login(string username, string password)
        {
            SqlParameter p1 = new SqlParameter("@UserName", username);
            SqlParameter p2 = new SqlParameter("@Password", password);
            return GetData("usp_Users_Login", new SqlParameter[] { p1, p2 });
        }
        public bool ChangePassword(string userid, string oldPassword, string newPassword)
        {
            SqlParameter p1 = new SqlParameter("@UserID", userid);
            SqlParameter p2 = new SqlParameter("@OldPassword", oldPassword);
            SqlParameter p3 = new SqlParameter("@NewPassword", newPassword);
            return UpdateData("usp_User_ChangePassword", new SqlParameter[] { p1, p2, p3 });
        }

        public int User_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_User_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }
        public DataTable User_GetOneByID(int UserID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@UserID", UserID);
            dt = GetData("usp_User_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public DataTable User_GetOneByEmail(string Email)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@Email", Email);
            dt = GetData("usp_User_GetOneByEmail", new SqlParameter[] { p1 });
            return dt;
        }
        public DataTable User_GetOneByUserName(string UserName)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@UserName", UserName);
            dt = GetData("usp_User_GetOneByUserName", new SqlParameter[] { p1 });
            return dt;
        }
        public DataTable User_InsertToken(string Email, string Token)
        {
            SqlParameter p1 = new SqlParameter("@Email", Email);
            SqlParameter p2 = new SqlParameter("@Token", Token);
            return GetData("usp_User_InsertToken", new SqlParameter[] { p1, p2});
        }
        public DataTable User_CheckToken(string Email, string Token)
        {
            SqlParameter p1 = new SqlParameter("@Email", Email);
            SqlParameter p2 = new SqlParameter("@Token", Token);
            return GetData("usp_User_CheckToken", new SqlParameter[] { p1, p2 });
        }
        #endregion

        #region Product
        public DataTable Product_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Product_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable Product_GetByCat_Paging(int pageindex, int pagesize, int LanguageID, ref int totalpage, int categoryid)
        {
            DataTable dt;
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            SqlParameter p5 = new SqlParameter("@CategoryID", categoryid);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Product_GetByCat_Paging", new SqlParameter[] { p2, p3, p4, p5 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable Product_GetByCatGroup_Paging(int pageindex, int pagesize, int LanguageID, ref int totalpage, int categorygroupId)
        {
            DataTable dt;
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            SqlParameter p5 = new SqlParameter("@CategoryGroupID", categorygroupId);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Product_GetByGroupCat_Paging", new SqlParameter[] { p2, p3, p4, p5 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable Product_GetBySupplier_Paging(int pageindex, int pagesize, int LanguageID, ref int totalpage, int supplierid)
        {
            DataTable dt;
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            SqlParameter p5 = new SqlParameter("@SupplierID", supplierid);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Product_GetBySupplier_Paging", new SqlParameter[] { p2, p3, p4, p5 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable User_Product_GetAll(int pagesize)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@PageSize", pagesize);
            string output = "";
            dt = GetData("usp_Product_User_GetAll", new SqlParameter[] { p1 }, out output);
            return dt;
        }
        public DataTable User_Product_GetTopSeller(int pagesize)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@PageSize", pagesize);
            string output = "";
            dt = GetData("usp_Product_GetTopSeller", new SqlParameter[] { p1 }, out output);
            return dt;
        }
        public DataTable User_Product_GetTopViewer(int pagesize)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@PageSize", pagesize);
            string output = "";
            dt = GetData("usp_Product_GetTopViewer", new SqlParameter[] { p1 }, out output);
            return dt;
        }
        public DataTable User_Product_GetTopSale(int pagesize)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@PageSize", pagesize);
            string output = "";
            dt = GetData("usp_Product_GetTopSale", new SqlParameter[] { p1 }, out output);
            return dt;
        }
        public DataTable Product_Index(int pagesize)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@Top", pagesize);
            string output = "";
            dt = GetData("usp_Product_Index", new SqlParameter[] { p1 }, out output);
            return dt;
        }
        public DataTable Product_GetOneByID(int ProductID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@ProductID", ProductID);
            dt = GetData("usp_Product_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }
        public DataTable Product_GetOneByIDAndOther(int ProductID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@ProductID", ProductID);
            dt = GetData("usp_Product_GetOneByIDAndOther", new SqlParameter[] { p1 });
            return dt;
        }
        public int Product_InsertUpdate(string xml, string listimage, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@ListImage", listimage);
            SqlParameter p3 = new SqlParameter("@Output", output);
            p3.Direction = ParameterDirection.InputOutput;
            p3.SqlDbType = SqlDbType.NVarChar;
            p3.Size = 2000;
            return UpdateData("usp_Product_InsertUpdate", new SqlParameter[] { p1, p2, p3 }, out output);
        }
        public bool Product_Delete(int ProductID)
        {
            SqlParameter p1 = new SqlParameter("@ProductID", ProductID);

            return UpdateData("usp_Product_Delete", new SqlParameter[] { p1 });
        }
        public DataTable ProductSearch(string search)
        {
            SqlParameter p1 = new SqlParameter("@Search", search);
            return GetData("usp_Product_Search", new SqlParameter[] { p1 });
        }
        #endregion

        #region Company
        public DataTable Company_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Company_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }

        public DataTable Company_GetOneByID(int CompanyID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@CompanyID", CompanyID);
            dt = GetData("usp_Company_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int Company_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Company_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }
        #endregion

        #region Partner
        public DataTable Partner_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Partner_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }

        public DataTable Partner_GetOneByID(int PartnerID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@PartnerID", PartnerID);
            dt = GetData("usp_Partner_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int Partner_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Partner_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool Partner_Delete(int PartnerID)
        {
            SqlParameter p1 = new SqlParameter("@PartnerID", PartnerID);

            return UpdateData("usp_Partner_Delete", new SqlParameter[] { p1 });
        }
        #endregion

        #region Category
        public DataTable Category_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Category_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable Category_GetAll()
        {
            DataTable dt;
            dt = GetData("usp_Category_GetAll");
            return dt;
        }

        public DataTable Category_GetOneByID(int CategoryID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@CategoryID", CategoryID);
            dt = GetData("usp_Category_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int Category_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Category_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool Category_Delete(int CategoryID)
        {
            SqlParameter p1 = new SqlParameter("@CategoryID", CategoryID);

            return UpdateData("usp_Category_Delete", new SqlParameter[] { p1 });
        }
        public DataTable Category_GetAllByGroup(int categoryGroup)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@CategoryGroup", categoryGroup);
            dt = GetData("usp_Category_GetAllByGroup", new SqlParameter[] { p1 });
            return dt;
        }
        #endregion

        #region CategoryGroup
        public DataTable CategoryGroup_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_CategoryGroup_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable CategoryGroup_GetAll()
        {
            DataTable dt;
            dt = GetData("usp_CategoryGroup_GetAll");
            return dt;
        }

        public DataTable CategoryGroup_GetOneByID(int CategoryGroupID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@CategoryGroupID", CategoryGroupID);
            dt = GetData("usp_CategoryGroup_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int CategoryGroup_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_CategoryGroup_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool CategoryGroup_Delete(int CategoryGroupID)
        {
            SqlParameter p1 = new SqlParameter("@CategoryGroupID", CategoryGroupID);

            return UpdateData("usp_CategoryGroup_Delete", new SqlParameter[] { p1 });
        }
        #endregion

        #region Banner
        public DataTable Banner_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Banner_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable Banner_GetAll()
        {
            DataTable dt;
            dt = GetData("usp_Banner_GetAll");
            return dt;
        }
        public DataTable Banner_GetOneByID(int BannerID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@BannerID", BannerID);
            dt = GetData("usp_Banner_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int Banner_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Banner_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool Banner_Delete(int BannerID)
        {
            SqlParameter p1 = new SqlParameter("@BannerID", BannerID);

            return UpdateData("usp_Banner_Delete", new SqlParameter[] { p1 });
        }
        #endregion

        #region Career
        public DataTable Career_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Career_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }

        public DataTable Career_GetOneByID(int CareerID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@CareerID", CareerID);
            dt = GetData("usp_Career_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int Career_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Career_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool Career_Delete(int CareerID)
        {
            SqlParameter p1 = new SqlParameter("@CareerID", CareerID);

            return UpdateData("usp_Career_Delete", new SqlParameter[] { p1 });
        }
        #endregion

        #region Feedback
        public DataTable Feedback_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Feedback_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }

        public DataTable Feedback_GetOneByID(int FeedbackID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@FeedbackID", FeedbackID);
            dt = GetData("usp_Feedback_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int Feedback_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Feedback_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool Feedback_Delete(int FeedbackID)
        {
            SqlParameter p1 = new SqlParameter("@FeedbackID", FeedbackID);

            return UpdateData("usp_Feedback_Delete", new SqlParameter[] { p1 });
        }
        #endregion

        #region Color
        public DataTable Color_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Color_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }

        public DataTable Color_GetOneByID(int ColorID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@ColorID", ColorID);
            dt = GetData("usp_Color_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int Color_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Color_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool Color_Delete(int ColorID)
        {
            SqlParameter p1 = new SqlParameter("@ColorID", ColorID);

            return UpdateData("usp_Color_Delete", new SqlParameter[] { p1 });
        }
        public DataTable GetColorList()
        {
            return GetData("usp_Color_GetColorList");
        }
        #endregion

        #region Size
        public DataTable Size_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Size_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }

        public DataTable Size_GetOneByID(int SizeID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@SizeID", SizeID);
            dt = GetData("usp_Size_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int Size_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Size_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool Size_Delete(int SizeID)
        {
            SqlParameter p1 = new SqlParameter("@SizeID", SizeID);

            return UpdateData("usp_Size_Delete", new SqlParameter[] { p1 });
        }
        public DataTable GetSizeList()
        {
            return GetData("usp_Size_GetSizeList");
        }
        #endregion

        #region Schedule
        public DataTable Schedule_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Schedule_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }

        public DataTable Schedule_ShowAll_Now(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Schedule_ShowAll_Now", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable Schedule_Index(int top)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@Top", top);
            dt = GetData("usp_Schedule_Index", new SqlParameter[] { p1 });
            return dt;
        }
        public DataTable Schedule_GetByTypeAndPage(string top, string page, string type, ref int totalPage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@Top", top);
            SqlParameter p2 = new SqlParameter("@Page", page);
            SqlParameter p3 = new SqlParameter("@Type", type);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalPage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Schedule_GetByTypeAndPage", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalPage = int.Parse(output);
            return dt;
        }
        public DataTable Schedule_ShowAll_Next(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("Schedule_ShowAll_Next", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }

        public DataTable Schedule_ShowAll_Old(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("Schedule_ShowAll_Old", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }

        public DataTable Schedule_GetOneByID(int ScheduleID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@ScheduleID", ScheduleID);
            dt = GetData("usp_Schedule_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public DataTable Schedule_User_GetOneByID(int ScheduleID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@ScheduleID", ScheduleID);
            dt = GetData("usp_Schedule_User_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }
        public DataTable Schedule_User_GetOneByTextID(string textID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@TextID", textID);
            dt = GetData("usp_Schedule_User_GetOneByTextID", new SqlParameter[] { p1 });
            return dt;
        }
        public int Schedule_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Schedule_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool Schedule_Delete(int ScheduleID)
        {
            SqlParameter p1 = new SqlParameter("@ScheduleID", ScheduleID);

            return UpdateData("usp_Schedule_Delete", new SqlParameter[] { p1 });
        }
        #endregion

        #region Supplier
        public DataTable Supplier_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Supplier_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable Supplier_User_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Supplier_User_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable Supplier_GetAll()
        {
            DataTable dt;
            dt = GetData("usp_Supplier_GetAll");
            return dt;
        }
        public DataTable GetSupplierList()
        {
            return GetData("usp_Supplier_GetSupplierList");
        }
        public DataTable Supplier_GetOneByID(int SupplierID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@SupplierID", SupplierID);
            dt = GetData("usp_Supplier_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int Supplier_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Supplier_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool Supplier_Delete(int SupplierID)
        {
            SqlParameter p1 = new SqlParameter("@SupplierID", SupplierID);

            return UpdateData("usp_Supplier_Delete", new SqlParameter[] { p1 });
        }
        #endregion

        #region SupplierGroup
        public DataTable SupplierGroup_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_SupplierGroup_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }

        public DataTable SupplierGroup_GetOneByID(int SupplierGroupID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@SupplierGroupID", SupplierGroupID);
            dt = GetData("usp_SupplierGroup_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int SupplierGroup_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_SupplierGroup_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool SupplierGroup_Delete(int SupplierGroupID)
        {
            SqlParameter p1 = new SqlParameter("@SupplierGroupID", SupplierGroupID);

            return UpdateData("usp_SupplierGroup_Delete", new SqlParameter[] { p1 });
        }
        #endregion

        #region ProductGroup
        public DataTable ProductGroup_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_ProductGroup_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable GetProductGroupList()
        {
            return GetData("usp_ProductGroup_GetProductGroupList");
        }
        public DataTable ProductGroup_GetOneByID(int SupplierGroupID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@SupplierGroupID", SupplierGroupID);
            dt = GetData("usp_ProductGroup_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int ProductGroup_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_ProductGroup_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool ProductGroup_Delete(int ProductGroupID)
        {
            SqlParameter p1 = new SqlParameter("@ProductGroupID", ProductGroupID);

            return UpdateData("usp_ProductGroup_Delete", new SqlParameter[] { p1 });
        }
        #endregion

        #region Image
        public int Image_Insert(string xml, string folder, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p3 = new SqlParameter("@Folder", folder);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Image_Insert", new SqlParameter[] { p1, p2, p3 }, out output);
        }
        public DataTable Image_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Image_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public bool Image_Delete(int ImageID)
        {
            SqlParameter p1 = new SqlParameter("@ImageID", ImageID);

            return UpdateData("usp_Image_Delete", new SqlParameter[] { p1 });
        }
        #endregion

        #region Folder
        public int Folder_Insert(string folderName, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@FolderName", folderName);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Folder_Insert", new SqlParameter[] { p1, p2 }, out output);
        }
        #endregion

        #region Order
        public DataTable Order_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Order_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }

        public DataTable Order_GetAll_Paging_ByUser(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_Order_GetAll_Paging_ByUser", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }

        public DataTable Order_GetOneByID(int OrderID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@OrderID", OrderID);
            dt = GetData("usp_Order_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int Order_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Order_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }
        public bool Order_Delete(int OrderID)
        {
            SqlParameter p1 = new SqlParameter("@OrderID", OrderID);

            return UpdateData("usp_Order_Delete", new SqlParameter[] { p1 });
        }
        public int OrderDetail_Delete(int OrderDetailID, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@OrderDetailID", OrderDetailID);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_OrderDetail_Delete", new SqlParameter[] { p1, p2 }, out output);
        }
        #endregion

        #region City
        public DataTable City_GetAll()
        {
            DataTable dt;
            dt = GetData("usp_City_GetAll");
            return dt;
        }
        #endregion

        #region District
        public DataTable District_GetDistrictByCityID(int cityID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@CityID", cityID);
            dt = GetData("usp_District_GetDistrictByCityID", new SqlParameter[] { p1 });
            return dt;
        }
        #endregion

        #region Ward
        public DataTable Ward_GetWardByDistrictID(int districtID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@DistrictID", districtID);
            dt = GetData("usp_Ward_GetWardByDistrictID", new SqlParameter[] { p1 });
            return dt;
        }
        #endregion

        #region WISH
        public bool Wish_Insert(string xml)
        {
            SqlParameter p1 = new SqlParameter("@XML", xml);
            return UpdateData("usp_Wish_Insert", new SqlParameter[] { p1 });
        }
        #endregion

        #region Request
        public int Request_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_Request_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public DataTable Request_CheckToken(string Token)
        {
            SqlParameter p1 = new SqlParameter("@Token", Token);
            return GetData("usp_Request_CheckToken", new SqlParameter[] { p1 });
        }
        #endregion

        #region News
        public DataTable News_GetAll_Paging(string xml, int pageindex, int pagesize, int LanguageID, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p3 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p4 = new SqlParameter("@TotalRow", totalpage);
            p4.Direction = ParameterDirection.InputOutput;
            p4.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_News_GetAll_Paging", new SqlParameter[] { p1, p2, p3, p4 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable News_GetAll(int pageindex, int pagesize, ref int totalpage)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@PageIndex", pageindex);
            SqlParameter p2 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p3 = new SqlParameter("@TotalRow", totalpage);
            p3.Direction = ParameterDirection.InputOutput;
            p3.SqlDbType = SqlDbType.Int;
            string output = "";
            dt = GetData("usp_News_GetAll",new SqlParameter[] { p1, p2, p3 }, out output);
            totalpage = int.Parse(output);
            return dt;
        }
        public DataTable News_GetOneByID(int NewsID)
        {
            DataTable dt;
            SqlParameter p1 = new SqlParameter("@NewsID", NewsID);
            dt = GetData("usp_News_GetOneByID", new SqlParameter[] { p1 });
            return dt;
        }

        public int News_InsertUpdate(string xml, out string output)
        {
            output = "";
            SqlParameter p1 = new SqlParameter("@XML", xml);
            SqlParameter p2 = new SqlParameter("@Output", output);
            p2.Direction = ParameterDirection.InputOutput;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 2000;
            return UpdateData("usp_News_InsertUpdate", new SqlParameter[] { p1, p2 }, out output);
        }

        public bool News_Delete(int NewsID)
        {
            SqlParameter p1 = new SqlParameter("@NewsID", NewsID);

            return UpdateData("usp_News_Delete", new SqlParameter[] { p1 });
        }
        #endregion
    }
}
