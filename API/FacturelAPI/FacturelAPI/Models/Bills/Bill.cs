using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FacturelAPI.Models.Bills
{
    public class Bill
    {
        //auto-incremented id
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime IssuedDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string IssuedBy{ get; set; }

        [Required]
        public double Amount { get; set; }
        
        public bool IsPaid { get; set; }
    }
}
