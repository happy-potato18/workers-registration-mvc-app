using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WorkersRegistrationApp.Domain
{
    public static class Base
    {
        private static string ConnectionString
        {
            get
            { return System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]; }
        }

        public static string strConnect
        {
            get
            { return System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; }
        }
    }
}