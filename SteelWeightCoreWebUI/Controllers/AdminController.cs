using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SteelWeightCoreWebUI.Models.Abstract;
using SteelWeightCoreWebUI.Models.Concrete;
using SteelWeightCoreWebUI.Models.Entities;
using SteelWeightCoreWebUI.Helpers;

namespace SteelWeightCoreWebUI.Controllers {
    [Authorize(Roles = "Super, Admin")]
    public class AdminController : Controller {
        public AdminController(ISteelRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public ViewResult Index() {
            ViewBag.IsMobile = UserAgentHelper.IsMobile(Request);
            var list = _repository.Steels.Where(s => s.deleted == 0).OrderBy(s => s.date).ToList();
            return View(list);
        }

        [HttpGet]
        public ViewResult Edit(int uid) {
            var steel = _repository.Find(uid);
            if (steel == null) {
                TempData["Failure"] = "자료가 존재하지 않습니다";
                return View("Index");
            }
            ViewBag.Workrooms = Workroom.Workrooms;
            return View(steel);
        }

        [HttpPost]
        public IActionResult Edit(Steel steel) {
            if (ModelState.IsValid) {
                _repository.SaveSteel(steel);
                TempData["Message"] = "수정이 완료되었습니다";
                return RedirectToAction("Index");
            }
            ViewBag.Workrooms = Workroom.Workrooms;
            return View(steel);
        }

        [HttpGet]
        public IActionResult Delete(int uid) {
            var steel = _repository.Find(uid);
            steel.deleted = 1;
            _repository.SaveSteel(steel);
            TempData["Message"] = string.Format("삭제가 왼료되었습니다 - {0} / {1} / {2}", steel.date?.ToString("yyyy-MM-dd"), steel.workroom, steel.weight);
            return RedirectToAction("Index");
        }

        private readonly ISteelRepository _repository;
    }
}
