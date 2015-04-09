using System.Xml.Linq;
using System;

namespace elp87.SimpleUpdate
{
    public class Updater
    {
        #region Fileds
        private string _configFileName;
        private Uri _versionTableUri;
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
        }
        #endregion

        #region Private
        private void ParseConfigFile()
        {
            XElement updX = XElement.Load(_configFileName);
            string versionTableName = updX.Element("updconfig").Element("servername").Value;
            if (versionTableName == null) throw new NullVersionTableUriException();
            this._versionTableUri = new Uri(versionTableName);
        }
        #endregion
        #endregion
    }
}
