using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApi.Domain.Models;
using CoreWebApi.Domain.Models.ModDBContextels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoreWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        ContextTest db;
 

        public ClientsController(ContextTest db)
        {
                 this.db = db;
        }

        [HttpGet]
        public IEnumerable<Clients> Get()
        {
            return db.Clients.ToList();
        }

        [HttpGet("{Birthday}")]
        public JsonResult Get(DateTime Birthday)
        {
            var RES = db.Clients.Where(x => x.DateOfBirth.HasValue && x.DateOfBirth.Value.Date == Birthday.Date).Select(x =>new { Id = x.Id, FullName = x.FullName}).ToList();
            //JsonSerializer.Serialize(RES);
            return new JsonResult(RES);
            //return Json(RES);
        }
    }
}
