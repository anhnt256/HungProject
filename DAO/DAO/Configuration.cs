using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DAO
{
    class Configuration
    {
        public static string ConnectionStringCaoQuy = ConfigurationManager.ConnectionStrings["Freelancer"].ToString();
    }
}
