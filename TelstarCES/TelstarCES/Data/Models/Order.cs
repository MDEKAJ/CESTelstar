using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TelstarCES.Data.Models
{
    public class Order
    {
        [Key]
        public virtual int OrderId { get; set; }

        [Required]
        public virtual bool Recommended { get; set; }

        [Required]
        public virtual float TotalPrice { get; set; }

        [Required]
        public virtual int TotalDuration { get; set; }

        [Required]
        public virtual string FromCity { get; set; }

        [Required]
        public virtual string ToCity { get; set; }

        [Required]
        public virtual float Weight { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }

        [Required]
        public virtual int ParcelTypeId { get; set; }
    }
}
