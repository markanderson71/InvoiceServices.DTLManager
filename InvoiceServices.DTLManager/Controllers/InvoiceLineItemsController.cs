using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using InvoiceServices.DTLManager.Core;
using InvoiceServices.DTLManager.ViewModels;
using InvoiceServices.DTLManager.Core.Model;

namespace InvoiceServices.DTLManager.Controllers
{
    [Produces("application/json")]
    [Route("api/InvoiceLineItems")]
    public class InvoiceLineItemsController : Controller
    {
        private ILogger<LineItemsController> logger;
        private IMapper mapper;
        private DetailManager detailManager;

        public InvoiceLineItemsController(IRepository repo, IMapper mapper, ILogger<LineItemsController> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.detailManager = new DetailManager(repo);
        }


        [HttpPost("{invoiceId}")]
        public IActionResult AddInvoiceLineItems([FromBody] InvoiceLineItemPost InvoiceLineItem, string invoiceId)
        {

            LineItem lineItem = mapper.Map<LineItem>(InvoiceLineItem);
            lineItem.InvoiceId = invoiceId;
            string detailId = detailManager.Add(lineItem);

            return CreatedAtRoute("GetLineItemById", new { id = detailId }, detailId);
        }
    }
}