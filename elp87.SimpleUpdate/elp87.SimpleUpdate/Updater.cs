using System.Xml.Linq;
using System;

namespace elp87.SimpleUpdate
{
    public class Updater
    {
        #region Fileds
        private string _configFileName;
        private string _versionTableFileName;
        private int _curBuild;
        #endregion

        #region Constructors
        public Updater(string configFileName, int curBuild) 
        {
            this._configFileName = configFileName;
            this._curBuild = curBuild;
        }

        public Updater(int curBuild)
        {
            this._configFileName = System.AppDomain.CurrentDomain.BaseDirectory + @"\updconfig.xml";
            this._curBuild = curBuild;
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

        #region Properties
        public int CurrentBuild
        {
            get { return this._curBuild; }
            set { this._curBuild = value; }
        }
        #endregion
    }
}
