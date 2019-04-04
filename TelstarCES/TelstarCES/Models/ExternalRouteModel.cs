using System;

namespace TelstarCES.Models
{
    public class ExternalRouteModel
    {
        public virtual bool Valid { get; set; }

        public virtual int Duration { get; set; }

        public virtual decimal Price { get; set; }
    }
}