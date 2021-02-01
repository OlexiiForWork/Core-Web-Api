using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebApi.Domain.Models
{
 
    public partial class Products
    {
        public Products()
        {
            Positions = new HashSet<Positions>();
        }

        public int Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Category { get; set; }

        [Required]
        [StringLength(20)]
        public string Artikul { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Price { get; set; }

        public virtual ICollection<Positions> Positions { get; set; }
    }
}
