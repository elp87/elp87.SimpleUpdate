
namespace elp87.SimpleUpdate
{
    public class Updater
    {
        #region Fileds
        private string _configFileName;
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
    }
}
