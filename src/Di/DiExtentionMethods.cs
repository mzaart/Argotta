using Multilang.Services.MessagingServices;
using Multilang.Services.TranslationServices;
using Multilang.Services.MessagingServices.Firebase;
using Multilang.Services.ConfigurationServices;
using Multilang.Repositories.UserRepository;
using Multilang.Services.AuthTokenServices;
using Multilang.Repositories.ProfilePicRepository;
using Multilang.Models.Jwt;
using Multilang.Models.Db;
using Multilang.Utils;
using System.Net.Http;
using Multilang.RequestPipeline.Filters;
using Multilang.Db.Contexts;
using Multilang.Repositories;
using Multilang.Services.LoggingServices;

namespace Microsoft.Extensions.DependencyInjection {

    public static class DiExtentionMethods {

        public static void addServices(this IServiceCollection services) {
            services.AddTransient<IMessagingService, MessagingService>();
            services.AddTransient<ITranslationService, UnofficialTranslationService>();
            services.AddTransient<FirebaseClient>();
            services.AddSingleton<IConfigService, Config>();
            services.AddTransient<IAuthTokenService<JwtBody>, JwtService<JwtBody>>();
            services.AddTransient<TokenAuth>();
        }

        public static void addRepositories(this IServiceCollection services) {
            // to be changed
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Group>, GroupRepository>();
            services.AddTransient<IProfilePicRepository, ProfilePicRepository>();
        }

        public static void AddRequiredClasses(this IServiceCollection services) {
            services.AddSingleton<HttpClient>();
            services.AddSingleton<ILoggingService, LoggingService>();
            services.AddScoped<ApplicationDbContext>();
            services.AddTransient<LangCodes>();
        } 
    }
}