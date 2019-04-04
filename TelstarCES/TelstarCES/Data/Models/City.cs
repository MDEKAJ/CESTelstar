using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelstarCES.Data.Models
{
    public class City
    {
        [Key]
        public virtual int CityId { get; set; }

        [Required]
        public virtual string CityName { get; set; }

        public virtual Connection[] Connections { get; set; }

    }
}
