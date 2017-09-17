﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvoiceServices.DTLManager.Core;
using Microsoft.Extensions.Logging;
using InvoiceServices.DTLManager.Core.Model;
using AutoMapper;
using InvoiceServices.DTLManager.ViewModels;

//Note a Better Name would be InvoiceService.LineItemManager (LIM)
namespace InvoiceServices.DTLManager.Controllers
{
    [Produces("application/json")]
    [Route("api/LineItems")]
    public class LineItemsController : Controller
    {
        private ILogger<LineItemsController> logger;
        private IMapper mapper;
        private DetailManager detailManager;

        public LineItemsController(IRepository repo, IMapper mapper, ILogger<LineItemsController> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.detailManager = new DetailManager(repo);

        }

        [HttpPost]
        public IActionResult Add([FromBody] LineItemViewModel lineItemViewModel)
        {
            if (ModelState.IsValid)
            {
                LineItem lineItem = mapper.Map<LineItem>(lineItemViewModel);
                return Ok(detailManager.Add(lineItem));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(detailManager.GetItem(id));
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(detailManager.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            detailManager.Cancel(id);
            return Ok();
        }


    }
}