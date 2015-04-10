using System.Xml.Linq;
using System;

namespace elp87.SimpleUpdate
{
    public class Updater
    {
        #region Fileds
        private string _configFileName;
        private string _versionTableFileName;
        #endregion

        #region Constructors
        public Updater(string configFileName)
        {
            this._configFileName = configFileName;
        }

        public Updater()
        {
            this._configFileName = System.AppDomain.CurrentDomain.BaseDirectory + @"\updconfig.xml";
        }
        #endregion

        #region Methods
        #region Public
        public void CheckUpdate()
        {
            this.ParseConfigFile();
            this.ParseVersionTable(_versionTableFileName);
        }
        #endregion

        #region Private
        private void ParseConfigFile()
        {
            XElement updX = XElement.Load(_configFileName);
            string versionTableName = updX.Element("servername").Value;
            if (versionTableName == null) throw new NullVersionTableUriException();
            if (versionTableName.StartsWith(@"/")) versionTableName = System.AppDomain.CurrentDomain.BaseDirectory + versionTableName;
            this._versionTableFileName = versionTableName;
        }

        private void ParseVersionTable(string _versionTableUri)
        {
            XElement versionX = XElement.Load(_versionTableFileName);
        
        }        
        #endregion
        #endregion
    }
}
