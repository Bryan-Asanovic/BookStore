using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyBookStore.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        public IActionResult Index()
        {
            // Hier komt de lijstweergave
            return View();
        }

        public IActionResult Create()
        {
            // View voor het maken van een nieuwe CoverType
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                // Data opslaan in database
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        public IActionResult Edit(int id)
        {
            // Data ophalen en tonen in edit view
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                // Data updaten
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        public IActionResult Delete(int id)
        {
            // Data ophalen voor bevestiging
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Data verwijderen
            return RedirectToAction("Index");
        }
    }
}
