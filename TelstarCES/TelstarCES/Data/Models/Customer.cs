using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TelstarCES.Data.Models
{
    public class Customer
    {
        [Key]
        public virtual int CustomerId { get; set; }

        public virtual string CustomerName { get; set; }

        public virtual string Email { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string Address1 { get; set; }

        public virtual string Address2 { get; set; }

        public virtual string ZipCode { get; set; }

        public virtual string POBox { get; set; }

        public virtual string City { get; set; }

        public virtual string Country { get; set; } 
    }
}
