using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multilang.Models.Requests.Translations;
using Multilang.Models.Responses;
using Multilang.Models.Responses.Translation;
using Multilang.RequestPipeline.Filters;
using Multilang.Services.TranslationServices;

namespace Multilang.Controllers
{
    [ServiceFilter(typeof(TokenAuth))]
    [ValidateModel]
    [Route("/api/[controller]")]
    public class TrasnlationController : Controller
    {
        private ITranslationService translationService;

        public TrasnlationController(ITranslationService translationService)
        {
            this.translationService = translationService;
        }

        /// <summary>
        /// Tanslates and sends a message.
        /// </summary>
        [HttpPost("sendMessage")]
        [ProducesResponseType(typeof(TranslationResponse), 200)]
        public async Task<JsonResult> Translate([FromBody] TranslationRequest request)
        {
            var response = new TranslationResponse();
            response.Translation = await translationService.Translate(
                request.Text, request.To, request.From);
            return Json(response);
        }
    }
}