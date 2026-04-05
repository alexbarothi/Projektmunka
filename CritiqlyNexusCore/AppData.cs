using System;
using System.Collections.Generic;
using System.Text;

namespace CritiqlyNexusCore
{
    public static class AppData
    {
        public static string Username { get; set; }
        public static DateTime? DailyLastUpdated { get; set; }
        public static DateTime? TrendingLastUpdated { get; set; }
    }
}
