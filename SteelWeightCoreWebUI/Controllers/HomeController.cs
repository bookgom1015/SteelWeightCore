using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SteelWeightCoreWebUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using SteelWeightCoreWebUI.Models.Abstract;
using SteelWeightCoreWebUI.Models.Concrete;
using SteelWeightCoreWebUI.Models.Entities;

namespace SteelWeightCoreWebUI.Controllers {
    [Authorize]
    public class HomeController : Controller {
        public HomeController(ILogger<HomeController> logger, ISteelRepository repository) {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public ViewResult List() {
            var list = _repository.Steels.Where(s => s.deleted == 0).ToList();
            return View(list);
        }

        [HttpGet]
        public ViewResult Index() {
            ViewBag.Workrooms = Workroom.Workrooms;
            Steel steel = TempData.Get<Steel>("Steel");
            if (steel != null) {
                steel.weight = null;
                return View(steel);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Steel steel) {
            if (ModelState.IsValid) {
                _repository.SaveSteel(steel);
                TempData.Put("Steel", steel);
                TempData["Message"] = string.Format("제출이 성공적으로 완료되었습니다 - {0} / {1} / {2}", 
                    steel.date?.ToString("yyyy-MM-dd"), steel.workroom, steel.weight);
                return RedirectToAction("Index");
            }
            ViewBag.Workrooms = Workroom.Workrooms;
            return View(steel);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private readonly ILogger<HomeController> _logger;
        private readonly ISteelRepository _repository;
    }
}
