using Bowling_League_WebApp_ZS_413.Models;
using Bowling_League_WebApp_ZS_413.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bowling_League_WebApp_ZS_413.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //Private context variable to create an interactable session with the database
        private BowlingLeagueContext _context { get; set; }

        //Defined pagesize for all bowler results
        private int pageSize = 5;

        //Constructor for the HomeController that sets up the logger and context variables
        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Main action for the index page
        //Receives teamID, teamName, and pageNumber as parameters to return the desired data
        public IActionResult Index(long teamID, string teamName, int pageNum = 0)
        {
            //Returns the Index view along with necessary information for the Index ViewModel that the Index Page uses
            return View(new IndexViewModel
            {
                //Set the Bowlers IEnumerable variable in the IndexViewModel to the queryable object of the correct bowlers for the right page
                //and the right team
                Bowlers = (_context.Bowlers
                .Where(x => x.TeamId == teamID || teamID == 0)
                .OrderBy(x => x.BowlerLastName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),

                //Sets the Page Numbering info correctly based on the user's clicks and actions
                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    TotalNumItems = (teamID == 0 ? _context.Bowlers.Count() :
                        _context.Bowlers.Where(x => x.TeamId == teamID).Count())

                },

                //Then sets the teamName string variable in the IndexViewModel to what was passed in
                TeamName = teamName

            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
