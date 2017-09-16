using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InvoiceServices.DTLManager.Core;

namespace InvoiceServices.DTLManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Health")]
    public class HealthController : Controller
    {
        private ILogger logger;
        private IRepository repository;

        public HealthController(IRepository repository, ILogger<HealthController> logger)
        {
            this.logger = logger;
            this.repository = repository;
            logger.LogInformation("Health Check Initialized");
        }

        public Task<IActionResult> Get()
        {
            string message = "N/A";

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
            string message = "N/A";
            logger.LogInformation("Health Check Started for Mongo");

            try
            {
               if(repository.IsAvailable())
                {
                    message = "OK";
                    logger.LogInformation("Health Check Mongo returned OK");
                }
            }
            catch (System.TimeoutException toe)
            {
                message = "Database TimeOut";
                logger.LogInformation("Health Check Mongo Database TimeOut", toe);
            }

            return new ResponsePair<string,string>{ Dependency = "mongoDB", Value=message};
        }

        public struct ResponsePair<K, V>
        {
            public K Dependency { get; set; }
            public K Value { get; set; }
        }
    }
}