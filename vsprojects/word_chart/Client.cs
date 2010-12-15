using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.ReportGenerator
{
    public class Client
    {
        public string Name { get; set; }
        public DateTime MeetingDate { get; set; }
        public string StrategyId { get; set; }
        public bool ExistingAssets { get; set; }
        public int Investment { get; set; }
        public string ReportingFrequency { get; set; }
        public decimal InitialFee { get; set; }
        public int TimeHorizon { get; set; }

    }
}
