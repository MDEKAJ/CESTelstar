using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TelstarCES.Data;
using TelstarCES.Data.Models;
using TelstarCES.Models;
using TelstarCES.Services;

namespace TelstarCES.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IDataService _dataService;

        public HomeController(IStringLocalizer<HomeController> localizer, ApplicationDbContext db)
        {
            _localizer = localizer;
            _dataService = new DataService(db);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Order()
        {
            ViewData["Message"] = _localizer["Your application description page."];

            return View();
        }

        public async Task<IActionResult> Admin()
        {
            return View(new CityViewModal
            {
                Cities = await _dataService.GetCities()
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
