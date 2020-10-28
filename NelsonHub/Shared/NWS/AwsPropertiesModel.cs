using System;
using System.Collections.Generic;
using System.Text;

namespace NelsonHub.Shared.NWS
{
    public class AwsPropertiesModel
    {
        public string Updated { get; set; }
        public string Units { get; set; }
        public string ForecastGenerator { get; set; }
        public string GeneratedAt { get; set; }
        public string UpdateTime { get; set; }
        public string ValidTimes { get; set; }
        public AwsPeriodModel[] Periods { get; set; }

    }
}
