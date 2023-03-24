using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuthorizationAssignment.Models.DIO;
using RoleBasedAuthorizationAssignment.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _service;
        public INotyfService _notifyService { get; }
        public UserAuthenticationController(IUserAuthenticationService service, INotyfService notifyService)
        {
            this._service = service;
            _notifyService = notifyService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _service.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                TempData["Message"] = "Successfully Logged In! ";
                _notifyService.Custom("Successfully Logged In!", 3, "lightgreen");
                return RedirectToAction("Display", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                

                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
           
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Role = "user";
            var result = await _service.RegistrationAsync(model);
            _notifyService.Custom("Successfully Registered!", 3, "lightgreen");
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Login));
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> RegisterAdmin()
        //{
        //    RegistrationModel model = new RegistrationModel
        //    {

        //        Username = "Admin",
        //        Email = "admin@gmail.com",
        //        Password = "Admin@1830"
        //    };
        //    model.Role = "admin";
        //    var result = await _service.RegistrationAsync(model);
        //    return Ok(result);
        //}
    }
}
