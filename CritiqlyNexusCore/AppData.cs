using System;
using System.Collections.Generic;
using System.Text;
using CritiqlyNexusCore.Models;

namespace CritiqlyNexusCore
{
    public static class AppData
    {
        public static string Username { get; set; }
        public static DateTime? DailyLastUpdated { get; set; }
        public static DateTime? TrendingLastUpdated { get; set; }

        public static List<Movie> Movies { get; set; } = new List<Movie>();
        public static List<Rating> Ratings { get; set; } = new List<Rating>();
        public static Movie UpdatePageSelectedMovie { get; set; }
        public static List<StreamingVote> streamingVotes { get; set; } = new List<StreamingVote>(); 
    }
}
