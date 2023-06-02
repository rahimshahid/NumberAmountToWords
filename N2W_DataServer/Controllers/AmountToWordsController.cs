using Microsoft.AspNetCore.Mvc;
using N2W_Core;

namespace N2W_DataServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AmountToWordsController : ControllerBase
    {
        private readonly ILogger<AmountToWordsController> _logger;

        public AmountToWordsController(ILogger<AmountToWordsController> logger)
        {
            _logger = logger;
        }

        [Route("Convert")]
        [HttpGet]
        public async Task<IActionResult> Get(string amount)
        {
            return await Task.Run(() =>
            {
                string words_form = AmountConverter.Convert(amount);
                return new OkObjectResult(words_form);
            });
        }
    }
}