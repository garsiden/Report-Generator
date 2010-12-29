using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    public partial class ReturnData
    {
        private static DateTime startDate = new DateTime(1900,1,1);

        public DateTime DateFromInteger
        {
            get { return startDate.AddDays(this.Date); }
        }

        public static int IntegerDate(DateTime date)
        {
            return ((int)date.ToOADate()) - 2;
        }
    }
}
