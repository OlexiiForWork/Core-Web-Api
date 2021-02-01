using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApi.Domain.Models.ModDBContextels
{

    public partial class ContextTest : DbContext
    {
        public ContextTest()  
        {
        }
        public ContextTest(DbContextOptions<ContextTest> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Purchases> Purchases { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb; Database = TestDB; Trusted_Connection = True; ");
        //}
    }
}
