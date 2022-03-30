using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission13.Models;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private IBowlerRepository repo {get; set;}

        public HomeController(IBowlerRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index()
        {
            var blah = repo.Bowlers
                .ToList();
            ViewBag.Teams = repo.Teams.ToList();
            ViewBag.SelectedTeam = "";

            return View(blah);
        }

        
        public IActionResult Filter(int teamID)
        {
            var blah = repo.Bowlers
                .Where(x => x.TeamID == teamID)
                .ToList();
            ViewBag.Teams = repo.Teams.ToList();
            Team team = repo.Teams.Single(x => x.TeamID == teamID);
            ViewBag.SelectedTeam = team.TeamName;
            return View("Index",blah);

        }

        [HttpGet]
        public IActionResult BowlerEntry()
        {
            ViewBag.Teams = repo.Teams.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult BowlerEntry(Bowler b)
        {
            if (ModelState.IsValid)
            {
                b.BowlerID = repo.GetMaxID() + 1;
                repo.CreateBowler(b);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int bowlerID)
        {
            ViewBag.Teams = repo.Teams.ToList();
            Bowler bowler = repo.Bowlers.Single(x => x.BowlerID == bowlerID);
            return View("BowlerEntry", bowler);
        }

        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            repo.SaveBowler(b);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int bowlerID)
        {
            var bowler = repo.Bowlers.Single(x => x.BowlerID == bowlerID);
            return View(bowler);
        }

        [HttpPost]
        public IActionResult Delete(Bowler b)
        {
            Bowler bowler = repo.Bowlers.Single(x => x.BowlerID == b.BowlerID);
            repo.DeleteBowler(bowler);
            return RedirectToAction("Index");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
