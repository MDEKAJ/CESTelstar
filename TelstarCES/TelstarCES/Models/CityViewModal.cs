using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelstarCES.Data.Models;

namespace TelstarCES.Models
{
    public class CityViewModal
    {
        public Dictionary<City, Connection[]> Cities { get; set; }

    }
}
