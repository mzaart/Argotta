
namespace Multilang.Services.ConfigurationServices {

    public interface IConfigService
    {
        string GetFirbaseUrl();
        string GetFirebaseAuthKey();
        string GetGoogleCloudAccountFilePath();
        string GetJwtKey();
        string GetDbConnectionString();
        string GetLoggingDir();
    }
}