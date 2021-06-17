using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_Services
{
    public class DateService : IDateTimeService
    {
        public string Get()
        {
            return DateTime.Now.Date.ToShortDateString();
        }
    }
}
