﻿using System.Web.Mvc;

namespace BlackJack.Angular.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}