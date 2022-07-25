using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.DAL.Entity
{
    public class Logger
    {
        public void LogMonnify(Exception ex)
        {
            var logDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            if (!System.IO.Directory.Exists(@"" + getApplicationDirectory()))
            {
                System.IO.Directory.CreateDirectory(@"" + getApplicationDirectory());
            }
            System.IO.File.AppendAllText(@"" + getApplicationDirectory() + logDate + "MonnifyLog.txt", ex.Message + " >>> " + ex.StackTrace + (ex.InnerException != null ? "{" + ex.InnerException.Message + ex.InnerException.StackTrace + "} \r \n" : " "));
        }

        public string getApplicationDirectory()
        {
            NameValueCollection section = ConfigurationManager.AppSettings;

            return section["ApplicationDirectory"];
        }
    }
}
