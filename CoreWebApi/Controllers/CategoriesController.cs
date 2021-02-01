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
    public class CategoriesController : ControllerBase
    {
        ContextTest db;
      

        public CategoriesController( ContextTest db)
        {
           
            this.db = db;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var Clients = db.Clients.AsQueryable();
            var Purchases = db.Purchases.AsQueryable();
            var Positions = db.Positions.AsQueryable();
            var Products = db.Products.AsQueryable();


            var ClientsPurchases = Purchases.Join(Clients, x => x.ClientsID, c => c.Id, (x, c) => new { ClientsID = c.Id, PurchasesID = x.Id })
                .GroupBy(x => new { x.ClientsID, x.PurchasesID })
                .Select(x => new { ClientsID = x.Key.ClientsID, PurchasesID = x.Key.PurchasesID });

            var res = ClientsPurchases
                .Join(Positions, x => x.PurchasesID, c => c.PurchasesID, (x, c) => new { ClientsID = x.ClientsID, PurchasesID = x.PurchasesID, ProductsID = c.ProductsID, Сounts=c.Сounts })
                .Join(Products, x => x.ProductsID, c => c.Id, (x, c) => new { ClientsID = x.ClientsID, Сounts = x.Сounts, Category = c.Category})
                .GroupBy(x => new { x.ClientsID, x.Category })
                .Select(x => new { ClientsID = x.Key.ClientsID, Category = x.Key.Category, Сounts = x.Sum(x=>x.Сounts) })
                .ToList();

            return new JsonResult(res);
        }

        [HttpGet("{ID}")]
        public JsonResult Get(int ID)
        {
            var Clients = db.Clients.Where(x=>x.Id == ID);
            var Purchases = db.Purchases.AsQueryable();
            var Positions = db.Positions.AsQueryable();
            var Products = db.Products.AsQueryable();


            var ClientsPurchases = Purchases.Join(Clients, x => x.ClientsID, c => c.Id, (x, c) => new { ClientsID = c.Id, PurchasesID = x.Id })
                .GroupBy(x => new { x.ClientsID, x.PurchasesID })
                .Select(x => new { ClientsID = x.Key.ClientsID, PurchasesID = x.Key.PurchasesID });

            var res = ClientsPurchases
                .Join(Positions, x => x.PurchasesID, c => c.PurchasesID, (x, c) => new { ClientsID = x.ClientsID, PurchasesID = x.PurchasesID, ProductsID = c.ProductsID, Сounts = c.Сounts })
                .Join(Products, x => x.ProductsID, c => c.Id, (x, c) => new { ClientsID = x.ClientsID, Сounts = x.Сounts, Category = c.Category })
                .GroupBy(x => new { x.ClientsID, x.Category })
                .Select(x => new { ClientsID = x.Key.ClientsID, Category = x.Key.Category, Сounts = x.Sum(x => x.Сounts) })
                .ToList();

            return new JsonResult(res);
        }
    }
}
