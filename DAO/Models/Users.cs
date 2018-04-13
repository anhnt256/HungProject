using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models
{
    public class Users
    {
        public int UserID { set; get; }
        public int StatusID { set; get; }
        [Required]
        [Display(Name = "Username")]
        public string UserName { set; get; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { set; get; }
        public string FullName { set; get; }
        public int CityID { get; set; }
        public int DistrictID { get; set; }
        public int WardID { get; set; }
        public int BirthDay_Day { get; set; }
        public int BirthDay_Month { get; set; }
        public int BirthDay_Year { get; set; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Gender { set; get; }
        public string Token { set; get; }
        public string Phone { set; get; }
        public string Avatar { set; get; }
        public bool IsActive { set; get; }
        public string CreateDate { set; get; }
        public string UpdateDate { set; get; }
        public string CityName { set; get; }
        public string DistrictName { set; get; }
        public string WardName { set; get; }
        public string GenderName { set; get; }
        public bool RememberMe { set; get; }

        public Users() { }

        public Users(DataRow dr)
        {
            if (dr.Table.Columns.Contains("UserID") && dr["UserID"] != DBNull.Value)
                UserID = int.Parse(dr["UserID"].ToString());
            if (dr.Table.Columns.Contains("StatusID") && dr["StatusID"] != DBNull.Value)
                StatusID = int.Parse(dr["StatusID"].ToString());
            if (dr.Table.Columns.Contains("UserName") && dr["UserName"] != DBNull.Value)
                UserName = dr["UserName"].ToString();
            if (dr.Table.Columns.Contains("Password") && dr["Password"] != DBNull.Value)
                Password = dr["Password"].ToString();
            if (dr.Table.Columns.Contains("FullName") && dr["FullName"] != DBNull.Value)
                FullName = dr["FullName"].ToString();
            if (dr.Table.Columns.Contains("CityName") && dr["CityName"] != DBNull.Value)
                CityName = dr["CityName"].ToString();
            if (dr.Table.Columns.Contains("DistrictName") && dr["DistrictName"] != DBNull.Value)
                DistrictName = dr["DistrictName"].ToString();
            if (dr.Table.Columns.Contains("WardName") && dr["WardName"] != DBNull.Value)
                WardName = dr["WardName"].ToString();
            if (dr.Table.Columns.Contains("GenderName") && dr["GenderName"] != DBNull.Value)
                GenderName = dr["GenderName"].ToString();
            if (dr.Table.Columns.Contains("CityID") && dr["CityID"] != DBNull.Value)
                CityID = int.Parse(dr["CityID"].ToString());
            if (dr.Table.Columns.Contains("DistrictID") && dr["DistrictID"] != DBNull.Value)
                DistrictID = int.Parse(dr["DistrictID"].ToString());
            if (dr.Table.Columns.Contains("WardID") && dr["WardID"] != DBNull.Value)
                WardID = int.Parse(dr["WardID"].ToString());
            if (dr.Table.Columns.Contains("BirthDay_Day") && dr["BirthDay_Day"] != DBNull.Value)
                BirthDay_Day = int.Parse(dr["BirthDay_Day"].ToString());
            if (dr.Table.Columns.Contains("BirthDay_Month") && dr["BirthDay_Month"] != DBNull.Value)
                BirthDay_Month = int.Parse(dr["BirthDay_Month"].ToString());
            if (dr.Table.Columns.Contains("BirthDay_Year") && dr["BirthDay_Year"] != DBNull.Value)
                BirthDay_Year = int.Parse(dr["BirthDay_Year"].ToString());
            if (dr.Table.Columns.Contains("Email") && dr["Email"] != DBNull.Value)
                Email = dr["Email"].ToString();
            if (dr.Table.Columns.Contains("Token") && dr["Token"] != DBNull.Value)
                Token = dr["Token"].ToString();
            if (dr.Table.Columns.Contains("Address") && dr["Address"] != DBNull.Value)
                Address = dr["Address"].ToString();
            if (dr.Table.Columns.Contains("Gender") && dr["Gender"] != DBNull.Value)
                Gender = dr["Gender"].ToString();
            if (dr.Table.Columns.Contains("Phone") && dr["Phone"] != DBNull.Value)
                Phone = dr["Phone"].ToString();
            if (dr.Table.Columns.Contains("Avatar") && dr["Avatar"] != DBNull.Value)
                Avatar = dr["Avatar"].ToString();
            if (dr.Table.Columns.Contains("IsActive") && dr["IsActive"] != DBNull.Value)
                IsActive = bool.Parse(dr["IsActive"].ToString());
            if (dr.Table.Columns.Contains("CreateDate") && dr["CreateDate"] != DBNull.Value)
                CreateDate = DateTime.Parse(dr["CreateDate"].ToString()).ToString("dd/MM/yyyy");
            if (dr.Table.Columns.Contains("UpdateDate") && dr["UpdateDate"] != DBNull.Value)
                UpdateDate = DateTime.Parse(dr["UpdateDate"].ToString()).ToString("dd/MM/yyyy");
        }

        public class Helpers
        {
            public static string SHA1_Encode(string value)
            {
                var hash = System.Security.Cryptography.SHA1.Create();
                var encoder = new System.Text.ASCIIEncoding();
                var combined = encoder.GetBytes(value ?? "");
                return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
            }
        }
    }
}
