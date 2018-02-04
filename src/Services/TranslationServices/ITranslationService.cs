

namespace Multilang.Services.TranslationServices {

    public interface ITranslationService
    {
        string Translate(string text, string targetLanguageCode,
            string sourceLanguageCode);
    }
}