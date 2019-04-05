using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelstarCES.Data.Models;
using TelstarCES.Enums;
using TelstarCES.Models;

namespace TelstarCES.Services
{
    interface IRouteService
    {
       Task<RouteViewModel> CalculateRoute(string fromName, string toName, string parcelType, float weight, bool recommended, FilterType filterType);
    }
}
