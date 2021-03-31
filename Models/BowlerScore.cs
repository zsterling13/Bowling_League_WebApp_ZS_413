using System;
using System.Collections.Generic;

#nullable disable

namespace Bowling_League_WebApp_ZS_413.Models
{
    public partial class BowlerScore
    {
        public long MatchId { get; set; }
        public long GameNumber { get; set; }
        public long BowlerId { get; set; }
        public long? RawScore { get; set; }
        public long? HandiCapScore { get; set; }
        public byte[] WonGame { get; set; }

        public virtual Bowler Bowler { get; set; }
    }
}
