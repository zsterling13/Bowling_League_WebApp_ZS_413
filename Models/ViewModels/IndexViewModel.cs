using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bowling_League_WebApp_ZS_413.Models.ViewModels
{
    public class IndexViewModel
    {
        //IEnumerable object to iterate and display every desired bowler's info
        public IEnumerable<Bowler> Bowlers { get; set; }

        //PageNumberingInfo for the page to know how to help the user navigate pages appropriately
        public PageNumberingInfo PageNumberingInfo { get; set; }

        //String variable for the currently selected teamname
        public string TeamName { get; set; }
    }
}
