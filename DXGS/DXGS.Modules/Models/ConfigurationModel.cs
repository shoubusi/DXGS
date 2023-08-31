using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace DXGS.Modules.Models
{
    public class ConfigurationModel
    {
        [Display(GroupName = "PDM", Name = "PDM ServerName", Order = 0)]
        public string PDMServerName { get; set; }

        [Display(GroupName = "PDM", Name = "PDM VaultName", Order = 1)]
        public string PDMVaultName { get; set; }

        [Display(GroupName = "PDM", Name = "Database Account", Order = 2)]
        public string DatabaseAccount { get; set; }

        [Display(GroupName = "PDM", Name = "Database Password", Order = 3)]
        public string DatabasePassword { get; set; }

        [Display(GroupName = "PDM", Name = "Connection String", Order = 4)]
        public string ConnectionString { get; set; }

        public ConfigurationModel()
        {
            ReadFromConfigFile();
        }

        // Method to write to app.config
        public void WriteToConfigFile()
        {
            // Update the value of the DatabasePassword in app.config
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["PDMServerName"].Value = PDMServerName;
            config.AppSettings.Settings["PDMVaultName"].Value = PDMVaultName;
            config.AppSettings.Settings["DatabaseAccount"].Value = DatabaseAccount;
            config.AppSettings.Settings["DatabasePassword"].Value = DatabasePassword;
            config.ConnectionStrings.ConnectionStrings["PDM"].ConnectionString = ConnectionString;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        // Method to read from app.config
        public void ReadFromConfigFile()
        {
            PDMServerName = ConfigurationManager.AppSettings["PDMServerName"];
            PDMVaultName = ConfigurationManager.AppSettings["PDMVaultName"];
            DatabaseAccount = ConfigurationManager.AppSettings["DatabaseAccount"];
            DatabasePassword = ConfigurationManager.AppSettings["DatabasePassword"];
            ConnectionString = ConfigurationManager.ConnectionStrings["PDM"].ConnectionString;
        }
    }
}