using Bowling_League_WebApp_ZS_413.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bowling_League_WebApp_ZS_413.Components
{
    public class TeamFilterViewComponent : ViewComponent
    {
        //Creates a private context variable
        private BowlingLeagueContext _context;

        //Constructor of the class that sets the private context variable to the passed-in parameter
        public TeamFilterViewComponent(BowlingLeagueContext ctx)
        {
            _context = ctx;
        }

        //Main method called when the viewcomponent is put into action
        public IViewComponentResult Invoke()
        {
            //Adds to the ViewBag, which is accessed by all views, the name of the currently selected team name
            ViewBag.SelectedTeam = RouteData?.Values["teamName"];

            //Returns the view component with the list of all unique team names in alphabeticaly order
            return View(_context.Teams.Distinct().OrderBy(x => x));
        }
    }
}
