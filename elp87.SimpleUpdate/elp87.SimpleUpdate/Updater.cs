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

        private string _stableBuildNumber;
        private string _betaBuildNumber;
        private string _stableLink;
        private string _betaLink;
        private string _stableNews;
        private string _betaNews;
        #endregion

        #region Constructors
        public Updater(string configFileName, int curBuild) 
        {
            this._configFileName = configFileName;
            this._curBuild = curBuild;
            this.NewBuildAvailability = NewBuildAvailabilities.NoInfo;
        }

        public Updater(int curBuild)
        {
            this._configFileName = System.AppDomain.CurrentDomain.BaseDirectory + @"\updconfig.xml";
            this._curBuild = curBuild;
            this.NewBuildAvailability = NewBuildAvailabilities.NoInfo;
        }
        #endregion

        #region Methods
        #region Public
        public void CheckUpdate()
        {
            this.ParseConfigFile();
            this.ParseVersionTable(_versionTableFileName);
            this.CheckAvailability();
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

            this._stableBuildNumber = versionX.Element("build").Value;
            this._stableLink = versionX.Element("link").Value;
            this._stableNews = versionX.Element("news").Value;

            this._betaBuildNumber = versionX.Element("betaBuild").Value;
            this._betaLink = versionX.Element("betaLink").Value;
            this._betaNews = versionX.Element("betaNews").Value;
        }

        private void CheckAvailability()
        {
            if (StableBuildNumber > CurrentBuild) this.NewBuildAvailability = NewBuildAvailabilities.NewStableAvailable;
            else if (StableBuildNumber <= CurrentBuild && BetaBuildNumber > CurrentBuild) this.NewBuildAvailability = NewBuildAvailabilities.NewBetaOnlyAvailable;
            else this.NewBuildAvailability = NewBuildAvailabilities.NoNewBuilds;

        }   
        #endregion
        #endregion

        #region Properties
        public int CurrentBuild
        {
            get { return this._curBuild; }
            set { this._curBuild = value; }
        }

        public int StableBuildNumber
        {
            get
            {
                try
                {
                    return Convert.ToInt32(this._stableBuildNumber);
                }
                catch (FormatException)
                {
                    throw new InvalidBuildNumberException("StableBuildNumber: " + this._stableBuildNumber + " is not valid build number");
                }
            }

            set { this._stableBuildNumber = value.ToString(); }
        }

        public string StableLink
        {
            get { return this._stableLink; }
            set { this._stableLink = value; }
        }

        public string StableNews
        {
            get { return this._stableNews; }
            set { this._stableNews = value; }
        }

        public int BetaBuildNumber
        {
            get
            {
                try
                {
                    return Convert.ToInt32(this._betaBuildNumber);
                }
                catch (FormatException)
                {
                    throw new InvalidBuildNumberException("BetaBuildNumber: " + this._betaBuildNumber + " is not valid build number");
                }
            }
        }

        public string BetaLink
        {
            get { return this._betaLink; }
            set { this._betaLink = value; }
        }

        public string BetaNews
        {
            get { return this._betaNews; }
            set { this._betaNews = value; }
        }

        public NewBuildAvailabilities NewBuildAvailability { get; set; }
        #endregion

        #region enum
        public enum NewBuildAvailabilities
        {
            NoInfo,
            NoNewBuilds,
            NewBetaOnlyAvailable,
            NewStableAvailable
        }
        #endregion
    }
}
