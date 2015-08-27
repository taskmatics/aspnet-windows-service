using Microsoft.AspNet.Mvc;
using System;

namespace MyDnxService
{
    public class TimeController
    {
        [Route("time")]
        public DateTime GetTime()
        {
            return DateTime.Now;
        }
    }
}
