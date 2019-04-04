using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelstarCES.Data.Models
{
    public class Connection
    {
        [Key]
        public virtual int ConnectionId { get; set; }

        [Required]
        public virtual City City1 { get; set; }

        [Required]
        public virtual City City2 { get; set; }

        [Required]
        public virtual string Provider { get; set; }

        [Required]
        public virtual float Price { get; set; }

        [Required]
        public virtual int Duration { get; set; }
    }
}
