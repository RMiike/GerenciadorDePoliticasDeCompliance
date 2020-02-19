using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDePoliticasIdentity.Web.Controllers
{
    public class PoliticaController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}