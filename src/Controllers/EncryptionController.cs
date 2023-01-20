using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using WebMvc.Models;
using WebMvc.Services;

namespace WebMvc.Controllers
{
    public class EncryptionController : Controller
    {
        private readonly ILogger<EncryptionController> _logger;
        private readonly IConfiguration _configuration;

        public EncryptionController(
            ILogger<EncryptionController> logger,
            IConfiguration configuration,
            IEncryptService encryptService)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public EncryptionConfiguration Keys()
        {
            var keys = new EncryptionConfiguration();

            keys.IV = _configuration["Encryption:IV"];
            keys.Password = _configuration["Encryption:Password"];
            keys.Salt = _configuration["Encryption:Salt"];

            return keys;
        }

        public class EncryptionConfiguration
        {
            public string IV { get; set; }
            public string Password { get; set; }
            public string Salt { get; set; }
        }       
    }
}