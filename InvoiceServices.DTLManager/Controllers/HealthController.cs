using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InvoiceServices.DTLManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Health")]
    public class HealthController : Controller
    {
        private ILogger logger;

        public HealthController(ILogger<HealthController> logger)
        {
            this.logger = logger;
        }

        public Task<IActionResult> Get()
        {
            logger.LogInformation("Health Check Started");
            List<ResponsePair<string, string>> responseList = new List<ResponsePair<string, string>>();

            responseList.Add(GetCheckDatabaseStatus());
            //Add Depencies as they are added
            IActionResult result = Ok(responseList);
            logger.LogInformation("Healtch Check Complete");
            return Task.FromResult(result);
        }

        private ResponsePair<string, string> GetCheckDatabaseStatus()
        {
            return new ResponsePair<string,string>{ Dependency = "this", Value="Ok"};
        }

        public struct ResponsePair<K, V>
        {
            public K Dependency { get; set; }
            public K Value { get; set; }
        }
    }
}