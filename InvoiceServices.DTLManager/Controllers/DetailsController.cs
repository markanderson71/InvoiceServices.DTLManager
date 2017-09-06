using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvoiceServices.DTLManager.Core;
using Microsoft.Extensions.Logging;
using InvoiceServices.DTLManager.Core.Model;

namespace InvoiceServices.DTLManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Details")]
    public class DetailsController : Controller
    {
        private ILogger<DetailsController> logger;
        private DetailManager detailManager;

        public DetailsController(IRepository repo, ILogger<DetailsController> logger)
        {
            this.logger = logger;
            this.detailManager = new DetailManager(repo);

        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(detailManager.GetItem(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody] LineItem lineItem)
        {
            return Ok(detailManager.Add(lineItem));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(detailManager.GetAll());
        }


    }
}