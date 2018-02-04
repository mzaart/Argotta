using Multilang.Services.MessagingServices;
using Multilang.Services.TranslationServices;
using Multilang.Services.MessagingServices.Firebase;
using Multilang.Services.ConfigurationServices;
using Multilang.Repositories.UserRepository;
using System.Net.Http;


namespace Microsoft.Extensions.DependencyInjection {

    public static class DiExtentionMethods {

        public static void addServices(this IServiceCollection services) {
            services.AddTransient<IMessagingService, MessagingService>();
            services.AddTransient<ITranslationService, UnofficialTranslationService>();
            services.AddTransient<FirebaseClient>();
            services.AddSingleton<IConfigService, Config>();
        }

        public static void addRepositories(this IServiceCollection services) {
            // to be changed
            services.AddSingleton<IUserRepository, UserRepository>();
        }

        public static void AddRequiredClasses(this IServiceCollection services) {
            services.AddSingleton<HttpClient>();
        } 
    }
}