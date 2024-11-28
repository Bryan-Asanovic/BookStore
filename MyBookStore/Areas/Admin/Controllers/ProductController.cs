using BookStore.DataAccess;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using MyBookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MyBookStore.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork context, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = context;
            _productRepository = _unitOfWork.Product;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(_productRepository.GetAll());
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {

                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
            c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }),
                // Items for covertypes dropdownlist
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
            c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
            };

            if (id == null || id == 0)
            { // Create a new product 
                return View(productVM);
            }
            // Update exesting product
            productVM.Product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id);
            return View(productVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string imagesLocation = Path.Combine(wwwRoot, @"images\products");
                    string extension = Path.GetExtension(file.FileName);

                    // Verwijder oude afbeelding als deze bestaat
                    if (obj.Product.ImageUrl != null)
                    {
                        var oldImage = Path.Combine(wwwRoot, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }

                    // Sla de nieuwe afbeelding op
                    using (var fs = new FileStream(Path.Combine(imagesLocation, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fs);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                // Nieuw product toevoegen of bestaand product updaten
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }

                _unitOfWork.Save();
                TempData["Success"] = "Product opgeslagen";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = productList });
        }
        #endregion

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Verwijderen mislukt" });
            }

            if (obj.ImageUrl != null)
            {
                var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product verwijderd" });
        }
    }
}
