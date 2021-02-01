using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CoreWebApi.Domain.Models
{

    public partial class Purchases
    {

        public Purchases()
        {
            Positions = new HashSet<Positions>();
        }

        public int Id { get; set; }

        public int? ClientsID { get; set; }

        public int? Number { get; set; }

        public DateTime? Date { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TotalСost { get; set; }

        public virtual Clients Clients { get; set; }

        public virtual ICollection<Positions> Positions { get; set; }
    }
}
