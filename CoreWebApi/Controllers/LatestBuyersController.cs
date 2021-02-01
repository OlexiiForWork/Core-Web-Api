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
    public class LatestBuyersController : ControllerBase
    {
        ContextTest db;
       
        public LatestBuyersController(ContextTest db)
        {
         
            this.db = db;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var Clients = db.Clients.AsQueryable();
            var Purchases = db.Purchases.AsQueryable();
            var res = Purchases.Join(Clients, x => x.ClientsID, c => c.Id, (x, c) => new { ClientsID = c.Id, FullName = c.FullName, LastDate = x.Date })
                .GroupBy(x => new { x.ClientsID, x.FullName })
                .Select(x => new { ClientsID = x.Key.ClientsID, FullName = x.Key.FullName, LastDate = x.Max(m=>m.LastDate) })
                .ToList();
            return new JsonResult(res);
        }

        [HttpGet("{Nday}")]
        public JsonResult Get(int Nday)
        {
            var Clients = db.Clients.AsQueryable();
            var Purchases = db.Purchases.Where(x => x.Date >= DateTime.Now.Date.AddDays(-Nday));
            var res = Purchases.Join(Clients, x => x.ClientsID, c => c.Id, (x, c) => new { ClientsID = c.Id, FullName = c.FullName, LastDate = x.Date })
                .GroupBy(x => new { x.ClientsID, x.FullName })
                .Select(x => new { ClientsID = x.Key.ClientsID, FullName = x.Key.FullName, LastDate = x.Max(m => m.LastDate) })
                .ToList();
            return new JsonResult(res);

        }
    }
}
