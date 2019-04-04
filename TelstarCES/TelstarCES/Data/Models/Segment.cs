using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TelstarCES.Data.Models
{
    public class Segment
    {
        [Key]
        public virtual int SegmentId { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        [Required]
        public virtual float Price { get; set; }

        [Required]
        public virtual int Duration { get; set; }

        [Required]
        public virtual string Provider { get; set; }

        [Required]
        public virtual string FromCity { get; set; }

        [Required]
        public virtual string ToCity { get; set; }
    }
}
