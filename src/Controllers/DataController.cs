using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMvc.Services;

namespace WebMvc.Controllers
{
    public class DataController : Controller
    {
        private readonly ILogger<DataController> _logger;
        private readonly IEncryptService _encryptService;

        public DataController(
            ILogger<DataController> logger,
            IEncryptService encryptService)
        {
            _logger = logger;
            _encryptService = encryptService;
        }

        public string Index()
        {
            var values = new Dictionary<string, string>
            {
                { "date", DateTime.Now.ToShortDateString() },
                { "role", "reader" },
                { "random", Guid.NewGuid().ToString() }
            };

            var json = JsonConvert.SerializeObject(values);

            var data = _encryptService.Encrypt(json);

            return data;
        }
    }
}