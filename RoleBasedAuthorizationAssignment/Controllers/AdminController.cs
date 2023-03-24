using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoleBasedAuthorizationAssignment.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        
        public IActionResult Display()
        {
         
            return View();
        }
    }
}
