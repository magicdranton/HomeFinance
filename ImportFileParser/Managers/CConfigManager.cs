using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ImportFileParser
{
    public class CConfigManager
    {
        public CConfigManager()
        {
            m_DollarRate = Decimal.Parse(ConfigurationManager.AppSettings[CONST_DollarRate], this.EnglishCulture);
            m_EuroRate = Decimal.Parse(ConfigurationManager.AppSettings[CONST_EuroRate], this.EnglishCulture);
            m_LoadFromFolder = ConfigurationManager.AppSettings[CONST_LoadFromFolder];
            m_ConnString = ConfigurationManager.ConnectionStrings[CONST_ConnString].ConnectionString;
            m_IsDevMode = ConfigurationManager.AppSettings[CONST_DevMode] == "1";
        }

        private static CConfigManager m_ConfigManager = null;
        public static CConfigManager Manager
        { 
            get
            {
                if (m_ConfigManager == null)
                {
                    m_ConfigManager = new CConfigManager();
                }

                return m_ConfigManager;
            }
        }
              
        public CultureInfo EnglishCulture { get; } = System.Globalization.CultureInfo.GetCultureInfo("en-EN");                
        public CultureInfo RussianCulture { get; } = System.Globalization.CultureInfo.GetCultureInfo("ru-RU");

        #region DollarRate
        private const string CONST_DollarRate = "DollarRate";
        private decimal m_DollarRate = 0;

        public decimal DollarRate
        {
            get
            {
                return m_DollarRate;                
            }
            set
            {
                ConfigurationManager.AppSettings[CONST_DollarRate] = value.ToString(this.EnglishCulture);
                m_DollarRate = value;
            }
        }
        public string StrDollarRate
        {
            get
            { 
                return m_DollarRate.ToString(this.EnglishCulture);
            }
        }
        #endregion DollarRate

        #region EuroRate
        private const string CONST_EuroRate = "EuroRate";
        private decimal m_EuroRate = 0;

        public decimal EuroRate
        {
            get
            {
                return m_EuroRate;
            }
            set
            {
                ConfigurationManager.AppSettings[CONST_EuroRate] = value.ToString(this.EnglishCulture);
                m_EuroRate = value;
            }
        }
        public string StrEuroRate
        {
            get
            {
                return m_EuroRate.ToString(this.EnglishCulture);
            }
        }
        #endregion EuroRate

        #region LoadFromFolder
        private const string CONST_LoadFromFolder = "LoadFromFolder";
        private string m_LoadFromFolder = @".\SourceFiles";
        public string LoadFromFolder
        {
            get
            {
                return m_LoadFromFolder;
            }
            set
            {
                ConfigurationManager.AppSettings[CONST_LoadFromFolder] = value;                
                m_LoadFromFolder = value;
            }
        }
        #endregion

        #region ConnectionString
        private const string CONST_ConnString = "ConnString";
        private string m_ConnString = null;

        public string ConnectionString
        {
            get
            {
                return m_ConnString;
            }
            set
            {
                ConfigurationManager.ConnectionStrings[CONST_ConnString].ConnectionString = value;
                m_ConnString = value;
            }
        }
        #endregion ConnectionString

        #region IsDevMode
        private const string CONST_DevMode = "DevMode";
        private bool m_IsDevMode = false;
        public bool IsDevMode
        {
            get
            {
                return m_IsDevMode;
            }
            set
            {
                ConfigurationManager.AppSettings[CONST_DevMode] = value?"1":"0";
            }
        }
        #endregion
    }
}
