using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelstarCES.Data.Models;
using TelstarCES.Enums;

namespace TelstarCES.Services
{
    interface IRouteService
    {
       Task<Segment[]> CalculateRoute(string fromName, string toName, string parcelType, bool recommended, FilterType filterType);
    }
}
