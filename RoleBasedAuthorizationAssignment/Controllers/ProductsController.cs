using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoleBasedAuthorizationAssignment.Models;
using RoleBasedAuthorizationAssignment.Models.Domain;
using RoleBasedAuthorizationAssignment.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notifyService { get; }
        public ProductsController(IUnitOfWork unitOfWork,DatabaseContext context, INotyfService notifyService)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _notifyService = notifyService;
        }
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
               u => new SelectListItem
               {
                   Text = u.CategoryName,
                   Value = u.Id.ToString()
               });
            ViewBag.CategoryList = CategoryList;

            IEnumerable<Product> products = _unitOfWork.Product.GetAll();
            return View(products);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
               u => new SelectListItem
               {
                   Text = u.CategoryName,
                   Value = u.Id.ToString()
               });
            ViewBag.CategoryList = CategoryList;
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(Product product)
        {

            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
                u=>new SelectListItem
                {
                    Text=u.CategoryName,
                    Value=u.Id.ToString()
                });
           
            if (ModelState.IsValid)
            {

                ViewBag.CategoryList = CategoryList;
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();

               
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(int? id)
        {

            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
            u => new SelectListItem
            {
                Text = u.CategoryName,
                Value = u.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;
            if (id == null)
            {
               
                return NotFound();
            }

            var product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
           

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
             u => new SelectListItem
             {
                 Text = u.CategoryName,
                 Value = u.Id.ToString()
             });

            if (ModelState.IsValid)
            {
                ViewBag.CategoryList = CategoryList;

                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
              
                return RedirectToAction("Index");
            }
            return View(product);
        }



        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
            u => new SelectListItem
            {
                Text = u.CategoryName,
                Value = u.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;
            var product = _unitOfWork.Product.GetFirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
            u => new SelectListItem
            {
                Text = u.CategoryName,
                Value = u.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;

            var product = _unitOfWork.Product.GetFirstOrDefault(m => m.Id == Id);
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }
        [Authorize(Roles = "user")]
        public IActionResult Cards()
        {
           

            IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperties:"Categories");
            return View(products);
        }

      

        public IActionResult UserProductDetails(int productId)
        {
            Cart cart = new Cart()
            {
                Count = 1,
                ProductId = productId,
                //Product = _context.Products.Include(x => x.Categories).FirstOrDefault(m => m.Id == productId)
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Categories")
            };
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public IActionResult UserProductDetails(Cart cart)
        {
            cart.Id = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            cart.ApplicationUserId = claim.Value;

            Cart cartfromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault
             (u => u.ApplicationUserId == claim.Value && u.ProductId == cart.ProductId);
            if (cartfromDb == null)
            {
                _unitOfWork.ShoppingCart.Add(cart);
               
            }
            else
            {
                _unitOfWork.ShoppingCart.IncrementCount(cartfromDb, cart.Count);
            }
            _notifyService.Custom("Cake added to the Cart!", 3, "purple");
            _unitOfWork.Save();


            return RedirectToAction(nameof(Cards));
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Category.GetAll(includeProperties:"Categories");
            return Json(new { data = productList });
        }
    }
}
