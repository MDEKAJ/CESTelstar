using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelstarCES.Data.Models;

namespace TelstarCES.Models
{
    public class RouteViewModel
    {
        public Segment[] Segments { get; set; }

        public int TotalDuration { get; set; }

        public float TotalPrice { get; set; }
    }
}
