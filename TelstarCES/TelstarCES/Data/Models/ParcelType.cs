using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelstarCES.Data.Models
{
    public class ParcelType
    {
        [Key]
        public virtual int ParcelTypeId { get; set; }

        [Required]
        public virtual string ParcelTypeName { get; set; }
    }
}
