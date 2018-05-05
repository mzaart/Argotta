using System;
using Multilang.Utils;

namespace Multilang.Services.TranslationServices
{
    public class TranslationServiceFactory
    {
        private IServiceProvider serviceProvider;

        public TranslationServiceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ITranslationService GetInstance()
        {
            var vars = serviceProvider.GetService(typeof(TransientVars)) as TransientVars;
            switch(vars.TranslationEngine)
            {
                case 0:
                    return serviceProvider.GetService(typeof(UnofficialTranslationService))
                        as UnofficialTranslationService;
                case 1: 
                    return serviceProvider.GetService(typeof(AzureTranslationService))
                        as AzureTranslationService;
                default:
                    return serviceProvider.GetService(typeof(UnofficialTranslationService))
                        as UnofficialTranslationService;
            }
        }
    }
}