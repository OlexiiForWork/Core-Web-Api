using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CoreWebApi.Domain.Models
{

    public partial class Clients
    {

        public Clients()
        {
            Purchases = new HashSet<Purchases>();
        }

        public int Id { get; set; }

        [StringLength(300)]
        public string FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<Purchases> Purchases { get; set; }
    }
}
