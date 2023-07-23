using aspm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspm.Controllers
{
    public class TcMVCController : Controller
    {
        ASPMDBContext _ASPMDBContext = new ASPMDBContext();
        // GET: TcMVC
        public ActionResult Index()
        {
            ViewBag.Title = "TC";
            //List<TcInfo> tcInfos = _ASPMDBContext.TCs.Where(x => x.IsActive == true).ToList();
            List<Tcs> tcInfos=null;
            List<TopBanners> topbanner = _ASPMDBContext.TopBanners.Where(x => x.IsActive == true).ToList();
            ViewBag.topbanner = topbanner;
            ViewBag.tcs = "";
            return View(tcInfos);
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