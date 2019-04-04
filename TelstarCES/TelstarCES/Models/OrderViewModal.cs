using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelstarCES.Data.Models;

namespace TelstarCES.Models
{
    public class OrderViewModal
    {
        public City[] Cities { get; set; }

        public ParcelType[] Parcels { get; set; }

    }
}


