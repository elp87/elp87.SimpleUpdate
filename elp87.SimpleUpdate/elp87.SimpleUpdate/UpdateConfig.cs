
namespace elp87.SimpleUpdate
{
    public class UpdateConfig
    {
        public UpdateConfig(string appName, string serverAddress)
        {
            this.AppName = appName;
            this.ServerAddress = serverAddress;
        }

        public string AppName { get; set; }

        public string ServerAddress { get; set; }
    }
}
