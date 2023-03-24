using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleBasedAuthorizationAssignment.Models;
using RoleBasedAuthorizationAssignment.Models.DIO;
using RoleBasedAuthorizationAssignment.Models.Domain;
using RoleBasedAuthorizationAssignment.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Controllers
{
    [Authorize(Roles = "user")]
    public class ShoppingCartController : Controller
    {
        private readonly DatabaseContext _context;

        public INotyfService _notifyService { get; }
        public CartViewModel CartViewModel { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartController(DatabaseContext context, IUnitOfWork unitOfWork, INotyfService notifyService)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _notifyService = notifyService;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartViewModel = new CartViewModel()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Product")
                

            };
            foreach (var cart in CartViewModel.ListCart)
            {
 
                CartViewModel.CartTotal += (cart.Product.Product_Price * cart.Count);
            }

            return View(CartViewModel);
        }
        public IActionResult Summary()
        {
           

            return View();
        }

        public IActionResult Plus(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
			_unitOfWork.ShoppingCart.IncrementCount(cart, 1);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if(cart.Count<=1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
            }
            else
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            _notifyService.Custom("Cake Removed from the Cart!", 3, "red");
            return RedirectToAction(nameof(Index));
        }


    }
}
