using Microsoft.AspNetCore.Mvc;
using BookStore.DataAccess;
using BookStore.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using BookStore.DataAccess.Repository.IRepository;


namespace BookStoreWeb.Areas.Admin.Controllers
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
                ModelState.AddModelError("name", "Naam en Volgnummer mogen niet hetzelfde zijn");
            }
            if (_categoryRepository.GetFirsttorDefeault(c => c.Name == category.Name) != null)
            {
                ModelState.AddModelError("uniquename", "Deze categorie bestaat al");
            }
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = $"Categorie {category.Name} succesvol toegevoegd.";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Er is een probleem met de database";
                    return View(category);
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category category = _categoryRepository.GetFirsttorDefeault(c => c.Id == id);
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
                ModelState.AddModelError("name", "Naam en VolgNummer mogen niet hetzelfde zijn");
            }

            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = $"Categorie {category.Name} succesvol gewijzigd.";

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
            Category category = _categoryRepository.GetFirsttorDefeault(c => c.Id == id);
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
                TempData["result"] = $"Categorie {category.Name} succesvol verwijderd.";

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
