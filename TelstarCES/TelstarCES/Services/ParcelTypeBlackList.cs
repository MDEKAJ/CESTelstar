using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelstarCES.Services
{
    public static class ParcelTypeBlackList
    {
        private static string[] array =
        {
            "Animals",
            "animals",
            "Weapons",
            "weapons"
        };

        public static HashSet<string> BlackList = new HashSet<string>(array);
    }
}
