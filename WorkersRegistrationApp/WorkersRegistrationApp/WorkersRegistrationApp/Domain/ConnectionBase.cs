using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WorkersRegistrationApp.Domain
{
    /// <summary>
    /// The <c>Base</c> class.
    /// Retrieves string for connection to server from config.
    /// Used for creating connection to database.
    /// </summary>
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