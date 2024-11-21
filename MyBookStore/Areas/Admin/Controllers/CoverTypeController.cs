using Microsoft.AspNetCore.Mvc;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStoreWeb.Areas.Admin.Controllers
{

    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICoverTypeRepository _covertypeRepository;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var coverTypes = _unitOfWork.CoverType.GetAll();
            return View(coverTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(coverType);
                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = $"Categorie {coverType.Name} succesvol toegevoegd.";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Er is een probleem met de database";
                    return View(coverType);
                }
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        public IActionResult Edit(int id)
        {
            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(coverType);
                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = $"Categorie {coverType.Name} succesvol gewijzigd.";

                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Er is een probleem met de database!";
                    return View(coverType);
                }
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        public IActionResult Delete(int id)
        {
            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            if (coverType != null)
            {
                _unitOfWork.CoverType.Remove(coverType);
                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = $"Categorie {coverType.Name} succesvol verwijderd.";

                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Er is een probleem met de database!";
                    return View(coverType);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
