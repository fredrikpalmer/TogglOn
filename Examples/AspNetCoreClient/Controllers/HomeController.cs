﻿using System.Diagnostics;
using AspNetCoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using TogglOn.Client.AspNetCore;

namespace AspNetCoreClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices] IFeatureToggleEvaluator evaluator)
        {
            if (evaluator.IsEnabled("my-awesome-feature")) return View();

            return NotFound();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
