using aspm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspm.Controllers
{
    public class HomeController : Controller
    {
        ASPMDBContext _ASPMDBContext = new ASPMDBContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            List<Banner> banner=_ASPMDBContext.HomeBanners.Where(x => x.IsActive == true).OrderBy(p => p.SortId).ToList();
            ViewBag.homebanner  = banner;

            List<NewsEvents> newsevent = _ASPMDBContext.NewsEvents.Where(x => x.IsActive == true).ToList();
            ViewBag.newsevent = newsevent;

            List<NoticeBoard> noticeboards = _ASPMDBContext.NoticeBoards.Where(x => x.IsActive == true).ToList();
            ViewBag.noticeBoards = noticeboards;

            List<Vacancies> vacancies = _ASPMDBContext.Vacancies.Where(x => x.IsActive == true).ToList();
            ViewBag.vacancies = vacancies;

            List<Downloads> downloads = _ASPMDBContext.Downloads.Where(x => x.IsActive == true).ToList();
            ViewBag.downloads = downloads;

            return View(banner);
        }
        
        //admin@ideaworz.com
        //XCUDEjyJqf3QSLSwtoOhyEjcZ7rHAFHoMpaV75QKWdA=

    }
}
