using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RickAndMortySolution.IServices;
using RickAndMortySolution.Models;

namespace RickAndMortySolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRickMortyApi _rickMortyApi;

        public HomeController(IRickMortyApi rickMortyApi)
        {
            _rickMortyApi = rickMortyApi;
        }

        public async Task<IActionResult> Index(int? page = 1)
        {
            ViewBag.page = page + 1; // Auto increment the page value
            var characters = await _rickMortyApi.GetAll(page); // get list of characters
            return View(characters);
        }

        public async Task<IActionResult> Get(int id)
        {
            var characters = await _rickMortyApi.Get(id); // get specific character
            return Json(characters);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

