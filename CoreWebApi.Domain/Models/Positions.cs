using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebApi.Domain.Models
{

    public partial class Positions
    {
        public int Id { get; set; }

        public int? PurchasesID { get; set; }

        public int? ProductsID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Сounts { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Сost { get; set; }

        public virtual Products Products { get; set; }

        public virtual Purchases Purchases { get; set; }
    }
}
