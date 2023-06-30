using aspm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspm.Controllers
{
    public class TCController : Controller
    {
        ASPMDBContext _ASPMDBContext = new ASPMDBContext();
        // GET: TC
        public ActionResult Index()
        {
            ViewBag.Title = "TC";
            //List<TcInfo> tcInfos = _ASPMDBContext.TCs.Where(x => x.IsActive == true).ToList();
            List<Tcs> tcInfos = null;
            ViewBag.tcs = "";
            return View(tcInfos);
           // return View();
        }
        [HttpPost]
        public ActionResult Index(string admno)
        {
            int iadmno = Convert.ToInt32(admno);
            List<Tcs> tcInfos = _ASPMDBContext.TCs.Where(x => x.AdmNo == iadmno).ToList();
            return View(tcInfos);
        }
    }
}