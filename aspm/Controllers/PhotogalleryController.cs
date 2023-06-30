using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspm.Controllers
{
    public class PhotogalleryController : Controller
    {
        // GET: Photogallery
        public ActionResult Index()
        {
            return View();
        }
    }
}