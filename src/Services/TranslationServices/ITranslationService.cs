using System.Threading.Tasks;

namespace Multilang.Services.TranslationServices {

    public interface ITranslationService
    {
        Task<string> Translate(string text, string targetLanguageCode,
            string sourceLanguageCode);
    }
}