using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TelstarCES.Data.Models
{
    public class Connection
    {
        [Key]
        public virtual int ConnectionId { get; set; }

        [Required]
        public int City1Id { get; set; }
        
        [Required]
        public int City2Id { get; set; }
        
        [Required]
        public virtual string Provider { get; set; }

        [Required]
        public virtual float Price { get; set; }

        [Required]
        public virtual int Duration { get; set; }
    }
}
