using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace elp87.SimpleUpdate
{
    public class Updater
    {
        #region Fileds
        private string _configFileName;
        private string _versionTableFileName;
        private string _appName;
        private int _curBuild;

        private string _stableBuildNumber;
        private string _betaBuildNumber;
        private string _stableLink;
        private string _betaLink;
        private string _stableNews;
        private string _betaNews;

        private Window progressWindow;
        private ProgressBar pBar;

        private string _updaterDir;
        private string _appUpdDir;
        private string _setupFileName;
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
            ParseConfigFile();
            ParseVersionTable();
            CheckAvailability();
        }

        public void GetUpdate(UpdateTypes updateType)
        {
            string link;

            this._updaterDir = System.Environment.GetEnvironmentVariable("appdata") + @"\elp87\Updater\";
            this._appUpdDir = this._updaterDir + this._appName;

            switch (updateType)
            {
                case UpdateTypes.Stable:
                    {
                        link = this.StableLink;
                        break;
                    }
                case UpdateTypes.Beta:
                    {
                        link = this.BetaLink;
                        break;
                    }
                default:
                    {
                        link = null;
                        break;
                    }
            }
            this._setupFileName = this._appUpdDir + @"\setup.zip";
            WebClient client = new WebClient();

            if (!Directory.Exists(this._appUpdDir)) Directory.CreateDirectory(this._appUpdDir);
            if (File.Exists(this._setupFileName)) File.Delete(this._setupFileName);
            client.DownloadFileAsync(new Uri(link), _setupFileName);
            progressWindow = GenerateProgressWindow();
            progressWindow.Show();
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadProgressChanged += client_DownloadProgressChanged;

        }

        public void GenerateUpdateConfigFile(string filename, UpdateConfig config)
        {
            XElement configX = new XElement("updconfig",
                new XElement("appName", config.AppName),
                new XElement("servername", config.ServerAddress)
                );
            configX.Save(filename);

        }
        #endregion

        #region Private
        private void ParseConfigFile()
        {
            XElement updX = null;
            try
            {
                updX = XElement.Load(_configFileName);
            }
            catch (FileNotFoundException ex)
            {
                throw new NoUpdConfigFileException("no updConfig file in " + _configFileName, ex);
            }

            string versionTableName = updX.Element("servername").Value;
            if (versionTableName == null) throw new NullVersionTableUriException();
            if (versionTableName.StartsWith(@"/")) versionTableName = System.AppDomain.CurrentDomain.BaseDirectory + versionTableName;
            this._versionTableFileName = versionTableName;

            this._appName = updX.Element("appName").Value;
            if (this._appName == "") throw new EmptyAppNameException();
        }

        private void ParseVersionTable()
        {
            XElement versionX = XElement.Load(_versionTableFileName);

            // Замена Descendants(name).First вместо Element(name) по причине возможных отличий таблицы обновлений
            this._stableBuildNumber = versionX.Descendants("build").First().Value;
            this._stableLink = versionX.Descendants("link").First().Value;
            this._stableNews = versionX.Descendants("news").First().Value;

            this._betaBuildNumber = versionX.Descendants("betaBuild").First().Value;
            this._betaLink = versionX.Descendants("betaLink").First().Value;
            this._betaNews = versionX.Descendants("betaNews").First().Value;
        }

        private void CheckAvailability()
        {
            if (StableBuildNumber > CurrentBuild) this.NewBuildAvailability = NewBuildAvailabilities.NewStableAvailable;
            else if (StableBuildNumber <= CurrentBuild && BetaBuildNumber > CurrentBuild) this.NewBuildAvailability = NewBuildAvailabilities.NewBetaOnlyAvailable;
            else this.NewBuildAvailability = NewBuildAvailabilities.NoNewBuilds;

        }

        private Window GenerateProgressWindow()
        {
            Window window = new Window();
            window.Height = 80;
            window.Width = 300;
            Grid grid = new Grid();
            window.Content = grid;
            pBar = new ProgressBar();
            pBar.Minimum = 0;
            pBar.Maximum = 100;
            pBar.Width = 250;
            pBar.Height = 20;
            pBar.HorizontalAlignment = HorizontalAlignment.Center;
            pBar.VerticalAlignment = VerticalAlignment.Center;
            grid.Children.Add(pBar);
            return window;
        }
        #endregion

        #region Event Handlers
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            progressWindow.Close();
            ZipFile zip = new ZipFile(this._setupFileName);
            foreach (ZipEntry entry in zip.Entries)
            {
                entry.Extract(this._appUpdDir, ExtractExistingFileAction.OverwriteSilently);
            }
            try
            {
                Process.Start(this._appUpdDir + @"\Setup.exe");
            }
            catch (System.ComponentModel.Win32Exception) { }
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

        public enum UpdateTypes
        {
            Beta,
            Stable
        }
        #endregion
    }
}
