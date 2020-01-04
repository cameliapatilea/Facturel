using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FacturelAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FacturelAPI.Models.Bills;
using FacturelAPI.Services;
using Microsoft.Extensions.Options;

namespace FacturelAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BillsController : ControllerBase
    {
        private IBillService _billService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public BillsController(
            IBillService billService,
            IMapper mapper,
            IOptions<AppSettings> appSettings
            )
        {
            _billService = billService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult AddBill([FromBody] Bill model)
        {
            if (ModelState.IsValid)
            {
               
                try
                {
                    

                    var bill = _billService.AddBill(model);
                    if (bill == null)
                    {
                        return BadRequest();
                    }

                    return Ok(bill);
                }
                catch
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }


        [AllowAnonymous]
        [HttpPut("update")]
        public IActionResult UpdateBill([FromBody] Bill model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bill = _billService.UpdateBill(model);
                    if (bill == null)
                    {
                        return BadRequest();
                    }

                    return Ok(bill);
                }
                catch
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("getById")]
        public IActionResult GetById([FromBody] int id)
        {
            try
            {
                var bill = _billService.GetById(id);
                if (bill == null)
                {
                    return BadRequest();
                }

                return Ok(bill);
            }
            catch
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                var bills = _billService.GetAll().ToList();
                if (bills.Count == 0)
                {
                    return BadRequest();
                }

                return Ok(bills);
            }
            catch
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] int id)
        {
            try
            {
                if (id > 0)
                {
                    _billService.Delete(id);
                    return Ok();
                }
                else return BadRequest();

            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
