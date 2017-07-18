using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PingPong.Entities;

//GWC-PingPong: File implemented. No Action required.
//This files controls the routing of html pages and sending of data to those html pages.
//Research .Net Core MVC Controllers for more information.
namespace PingPong.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Player()
        {
            //Static player object. Can be changed to an api call to pull data from the database.
            Player player = new Player(){
                Name = "Joe Pong",
                Office = "London",
                numberWins = 232,
                numberLosses = 88
            };
            ViewData["Player"] = player;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
