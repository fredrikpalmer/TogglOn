﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TogglOn.AspNetCoreClient.Models;
using TogglOn.Client.AspNetCore;

namespace TogglOn.AspNetCoreClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices] IFeatureToggleEvaluater evaluater)
        {
            if (evaluater.IsEnabled("my-awesome-feature")) return View();

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
