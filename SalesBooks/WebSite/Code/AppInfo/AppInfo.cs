using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebSite
{
    public class AppInfo
    {
        #region Members
        private static WebConfigSettings m_webConfigSettings;
        #endregion

        #region Properties

        public static WebConfigSettings WebConfig
        {
            get
            {
                if (m_webConfigSettings == null)
                    m_webConfigSettings = new WebConfigSettings();

                return m_webConfigSettings;
            }
        }
        #endregion

        #region Classes
        public class WebConfigSettings
        {
            public string ConnectionString;

            public WebConfigSettings()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["SalesBooks"].ConnectionString;
            }
        }
        #endregion
    }
}