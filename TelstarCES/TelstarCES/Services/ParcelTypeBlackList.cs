using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelstarCES.Services
{
    public static class ParcelTypeBlackList
    {
        private static readonly string[] _array =
        {
            "Animals",
            "Weapons",
        };

        public static HashSet<string> BlackList = new HashSet<string>(_array, StringComparer.InvariantCultureIgnoreCase);
    }
}
