using Microsoft.AspNetCore.Mvc;
using BookStore.DataAccess;
using BookStore.Models;
using MyBookStore.Models;
using BookStore.DataAccess.Repository.IRepository;

namespace MyBookStore.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(IUnitOfWork context)
        {
            _unitOfWork = context;
            _categoryRepository = _unitOfWork.Category;
        }
        public IActionResult Index()
        {
            return View(_categoryRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DisplayOrder", "Naam en Volgnummer mogen niet hetzelfde zijn");
            }
            if (_categoryRepository.GetFirstOrDefault(c => c.Name == category.Name) != null)
            {
                ModelState.AddModelError("uniquename", "Deze categorie bestaat al");
            }
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = $"Categorie {category.Name} is succesvol toegevoegd";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Er is een probleem met de database!";
                    return View(category);
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Naam en Volgnummer mogen niet hetzelfde zijn");
            }
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = $"Categorie {category.Name} is succesvol gewijzigd";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Er is een probleem met de database!";
                    return View(category);
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(Category category)
        {
            _categoryRepository.Remove(category);
            try
            {
                _unitOfWork.Save();
                TempData["result"] = $"Categorie {category.Name} is succesvol verwijderd";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Er is een probleem met de database!";
                return View(category);
            }
            return RedirectToAction("Index");
        }
    }
}
