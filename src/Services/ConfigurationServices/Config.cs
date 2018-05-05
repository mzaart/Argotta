using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Multilang.Services.ConfigurationServices {

    public class Config : IConfigService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IConfiguration config;

        public Config(IHostingEnvironment hostingEnvironment) 
        {
            this.hostingEnvironment = hostingEnvironment;
            this.config = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public string GetAzureTranslationKey()
        {
            return config["AzureTranslation:9a2fba344ee241409d6c0b6d01acb801"];
        }

        public string GetDbConnectionString()
        {
            return config["ConnectionString"];
        }

        public string GetLoggingDir()
        {
            return config["Logs:Dir"];
        }

        string IConfigService.GetFirbaseUrl()
        {
            return config["Firebase:Url"];
        }

        string IConfigService.GetFirebaseAuthKey()
        {
            return config["Firebase:AuthKey"];
        }

        string IConfigService.GetGoogleCloudAccountFilePath()
        {
            return hostingEnvironment.ContentRootPath + "/" + config["GoogleCloud:AccountFile"];
        }

        string IConfigService.GetJwtKey()
        {
            return config["JWT:Key"];
        }
    }
}