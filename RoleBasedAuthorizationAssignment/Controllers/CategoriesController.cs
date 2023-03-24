using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleBasedAuthorizationAssignment.Models;
using RoleBasedAuthorizationAssignment.Models.Domain;
using RoleBasedAuthorizationAssignment.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            
            IEnumerable<Category> categories = _unitOfWork.Category.GetAll();
            return View(categories);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(Category category)
        {


            if (ModelState.IsValid)
            {
              
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();


                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var category =_unitOfWork.Category.GetFirstOrDefault(u=>u.Id==Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }

       

        [Authorize(Roles = "admin")]
        public  IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var category =  _unitOfWork.Category.GetFirstOrDefault(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Id)
        {
            
            var category = _unitOfWork.Category.GetFirstOrDefault(m=>m.Id==Id);
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }
        
    }
}
