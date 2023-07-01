using aspm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace aspm.Controllers
{
    public class CommonApisController : ApiController
    {
        ASPMDBContext _ASPMDBContext = new ASPMDBContext();

        [HttpPost]
        [Route("api/UploadFiles")]
        public async Task<IHttpActionResult> Uploadfiles()
        {
            HttpResponseMessage result = null;

            CommonResult output = new CommonResult();
            try
            {

                var httpRequest = HttpContext.Current.Request;
                //System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                if (httpRequest.Files.Count > 0)
                {
                    String filenames = "";
                    var newfilename = "";
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var desc = HttpContext.Current.Request.Form["info"];
                        FileInfo fi = new FileInfo(postedFile.FileName);
                        var path = "";
                        filenames = postedFile.FileName;
                        var filePath = "";
                        switch (desc)
                        {
                            case "news":
                                newfilename = "newsevents_" + DateTime.Now.Ticks + fi.Extension;
                                path = Path.Combine(HttpContext.Current.Server.MapPath("~/image/newsevents/"), newfilename);
                                filePath = HttpContext.Current.Server.MapPath("~/image/newsevents/" + newfilename);
                                break;
                            case "noticeboard":
                                newfilename = "noticeboard_" + DateTime.Now.Ticks + fi.Extension;
                                path = Path.Combine(HttpContext.Current.Server.MapPath("~/image/noticeboard/"), newfilename);
                                filePath = HttpContext.Current.Server.MapPath("~/image/noticeboard/" + newfilename);
                                break;
                            case "photo":
                                newfilename = "photo_" + DateTime.Now.Ticks + fi.Extension;
                                path = Path.Combine(HttpContext.Current.Server.MapPath("~/image/photo/"), newfilename);
                                filePath = HttpContext.Current.Server.MapPath("~/image/photo/" + newfilename);
                                break;
                            case "vacancies":
                                newfilename = "vacancie_" + DateTime.Now.Ticks + fi.Extension;
                                path = Path.Combine(HttpContext.Current.Server.MapPath("~/image/vacancie/"), newfilename);
                                filePath = HttpContext.Current.Server.MapPath("~/image/vacancie/" + newfilename);
                                break;
                            case "download":
                                newfilename = "vacancie_" + DateTime.Now.Ticks + fi.Extension;
                                path = Path.Combine(HttpContext.Current.Server.MapPath("~/image/download/"), newfilename);
                                filePath = HttpContext.Current.Server.MapPath("~/image/download/" + newfilename);
                                break;
                            case "facilities":
                                newfilename = "facilities_" + DateTime.Now.Ticks + fi.Extension;
                                path = Path.Combine(HttpContext.Current.Server.MapPath("~/image/facilities/"), newfilename);
                                filePath = HttpContext.Current.Server.MapPath("~/image/facilities/" + newfilename);
                                break;
                            case "ncert":
                                newfilename = "ncert_" + DateTime.Now.Ticks + fi.Extension;
                                path = Path.Combine(HttpContext.Current.Server.MapPath("~/image/ncert/"), newfilename);
                                filePath = HttpContext.Current.Server.MapPath("~/image/ncert/" + newfilename);
                                break;
                            case "banner":
                                newfilename = "Banner_" + DateTime.Now.Ticks + fi.Extension;
                                path = Path.Combine(HttpContext.Current.Server.MapPath("~/image/banner/"), newfilename);
                                filePath = HttpContext.Current.Server.MapPath("~/image/banner/" + newfilename);
                                break;
                            case "eventngallary":
                                newfilename = "eventngallary_" + DateTime.Now.Ticks + fi.Extension;
                                path = Path.Combine(HttpContext.Current.Server.MapPath("~/image/eventngallary/"), newfilename);
                                filePath = HttpContext.Current.Server.MapPath("~/image/eventngallary/" + newfilename);
                                break;
                            default:

                                break;
                        }

                        postedFile.SaveAs(filePath);

                    }

                    output.Result = 1;
                    output.Message = "File Saved";
                    output.ResutlData = newfilename;
                    //output.ResutlData.oldfilename = filenames; 9167461676
                    return Ok(output);
                }
                else
                {
                    output.Result = 0;
                    output.Message = "No file found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }

            }
            catch (Exception ex)
            {

                CommonResult output1 = new CommonResult();
                output1.Result = 0;
                output1.Message = ex.Message;
                output1.ResutlData = "";
                // result = Request.CreateResponse(HttpStatusCode.BadRequest, output1);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output1));
            }

        }


        #region TopBanners
        [HttpGet]
        [Route("api/topbanner/GetAllTopBanner")]
        [ResponseType(typeof(Banner))]
        public IHttpActionResult GetAllTopBanner()
        {

            var hb = _ASPMDBContext.TopBanners.Where(x => x.IsActive == true).OrderBy(p => p.Id).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = hb;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }

        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/topbanner/SaveTopBanner")]
        public IHttpActionResult SaveTopBanner([FromBody] TopBanners topbanners)
        {
            try
            {
                CommonResult output = new CommonResult();
                if (topbanners.Id != null)
                {
                    TopBanners _topbanners = new TopBanners();
                    var existedItem = _ASPMDBContext.TopBanners.Where(x => x.Id == topbanners.Id).FirstOrDefault();

                    if (existedItem != null && existedItem.Id != 0)
                    {
                        //update
                        topbanners.ModifiedOn = DateTime.Now;
                        _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(topbanners);
                        _topbanners = existedItem;
                        output.ResutlData = _topbanners;
                    }
                    else
                    {
                        topbanners.ModifiedOn = DateTime.Now;
                        topbanners.IsActive = true;
                        _ASPMDBContext.TopBanners.Add(topbanners);
                        output.ResutlData = topbanners;

                    }
                    _ASPMDBContext.SaveChanges();
                    //CommonResult output = new CommonResult();
                    output.Result = 1;
                    output.Message = "";

                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record to add/update";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }

        #endregion

        #region Banners
        [HttpGet]
        [Route("api/banner/GetAllBanner")]
        [ResponseType(typeof(Banner))]
        public IHttpActionResult GetAllBanner()
        {

            var hb = _ASPMDBContext.HomeBanners.Where(x => x.IsActive == true).OrderBy(p => p.SortId).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = hb;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }

        }
        [HttpPost]
        [Route("api/banner/deleteBanner")]
        public async Task<IHttpActionResult> deleteBanner([FromBody] Banner banner)
        {
            try
            {
                CommonResult output = new CommonResult();
                var homebanner = _ASPMDBContext.HomeBanners.Where(x => x.Id == banner.Id).FirstOrDefault();
                if (homebanner != null)
                {
                    homebanner.IsActive = false;
                    homebanner.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.SaveChanges();

                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = homebanner;
                    return Ok(output);
                }
                else
                {
                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }



        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/banner/SaveBanner")]
        public IHttpActionResult SaveTC([FromBody] Banner banner)
        {
            try
            {
                CommonResult output = new CommonResult();
                if (banner.Id != null)
                {
                    Banner _banner = new Banner();
                    var existedItem = _ASPMDBContext.HomeBanners.Where(x => x.Id == banner.Id).FirstOrDefault();

                    if (existedItem != null && existedItem.Id != 0)
                    {
                        //update
                        _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(banner);
                        _banner = existedItem;
                        output.ResutlData = _banner;
                    }
                    else
                    {

                        banner.IsActive = true;
                        banner.ModifiedOn = DateTime.Now;
                        _ASPMDBContext.HomeBanners.Add(banner);
                        output.ResutlData = banner;

                    }
                    _ASPMDBContext.SaveChanges();
                    //CommonResult output = new CommonResult();
                    output.Result = 1;
                    output.Message = "";

                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record to add/update";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }

        [HttpPost]
        [Route("api/banner/UploadBanner1")]
        public async Task<IHttpActionResult> UploadBanner1()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            CommonResult output = new CommonResult();
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    String filenames = "";
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        FileInfo fi = new FileInfo(postedFile.FileName);
                        var newfilename = "Banner_" + DateTime.Now.Ticks + fi.Extension;

                        var path = Path.Combine(HttpContext.Current.Server.MapPath("~/image/banner/"), newfilename);
                        filenames = newfilename;


                        var filePath = HttpContext.Current.Server.MapPath("~/image/banner/" + newfilename);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                        Banner hb = new Banner();
                        hb.IsActive = true;
                        hb.CreatedOn = DateTime.Now;
                        hb.NewFilename = newfilename;
                        hb.OldFilename = postedFile.FileName;
                        hb.ModifiedOn = DateTime.Now;
                        
                        _ASPMDBContext.HomeBanners.Add(hb);
                        _ASPMDBContext.SaveChanges();
                    }
                    output.Result = 1;
                    output.Message = "File Saved";
                    output.ResutlData = filenames;
                    return Ok(output);
                }
                else
                {
                    output.Result = 0;
                    output.Message = "No file found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }

            }
            catch (Exception ex)
            {
                CommonResult output1 = new CommonResult();
                output1.Result = 0;
                output1.Message = ex.Message;
                output1.ResutlData = "";
                result = Request.CreateResponse(HttpStatusCode.BadRequest, output1);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }

        #endregion

        #region TC
        [HttpGet]
        [Route("api/TC/GetAllTcs")]
        [ResponseType(typeof(Tcs))]
        public IHttpActionResult GetAllTcs()
        {

            var hb = _ASPMDBContext.TCs.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = hb;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }
        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Tc/SaveTC")]
        public IHttpActionResult SaveTC([FromBody] Tcs tc)
        {
            try
            {
                CommonResult output = new CommonResult();
                if (tc.Id != null)
                {
                    Tcs _tc = new Tcs();
                    var existedItem = _ASPMDBContext.TCs.Where(x => x.Id == tc.Id).FirstOrDefault();

                    if (existedItem != null && existedItem.Id != 0)
                    {
                        //update
                        tc.ModifiedOn = DateTime.Now;
                        _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(tc);
                        _tc = existedItem;
                        output.ResutlData = _tc;
                    }
                    else
                    {

                        tc.IsActive = true;
                        _ASPMDBContext.TCs.Add(tc);
                        output.ResutlData = tc;

                    }
                    _ASPMDBContext.SaveChanges();
                    //CommonResult output = new CommonResult();
                    output.Result = 1;
                    output.Message = "";

                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record to add/update";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }
        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Tc/deleteTC")]
        public IHttpActionResult deleteTC([FromBody] Tcs _tc)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.TCs.Where(x => x.Id == _tc.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var tc = existedItem;
                    tc.IsActive = false;
                    tc.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(tc);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = tc;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }

        #endregion

        #region Users
        [HttpGet]
        [Route("api/tc/GetAllUsers")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetAllUser()
        {

            var hb = _ASPMDBContext.Users.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = hb;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }

        [HttpPost]
        [Route("api/user/DeleteUser")]
        public async Task<IHttpActionResult> DeleteUser([FromBody] User users)
        {
            try
            {
                CommonResult output = new CommonResult();
                var homebanner = _ASPMDBContext.Users.Where(x => x.Id == users.Id).FirstOrDefault();
                if (homebanner != null)
                {
                    homebanner.IsActive = false;
                    homebanner.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.SaveChanges();

                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = homebanner;
                    return Ok(output);
                }
                else
                {
                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }



        }
        [HttpPost]
        [Route("api/user/GetAuth")]
        public async Task<IHttpActionResult> GetAuth(User _user)
        {
            CommonResult output = new CommonResult();

            User userobj = new User();
            try
            {

                userobj.Username = _user.Username;
                var encryptdata = CommonResult.Encrypt(_user.Password);
                User userinfo = _ASPMDBContext.Users.Where(x => x.Username.Trim() == _user.Username.Trim().ToString() &&
                                x.Password.Trim() == encryptdata.Trim()).FirstOrDefault();

                if (userinfo != null)
                {
                    userinfo.Password = "";
                    output.Result = 1;
                    output.Message = "Welcome " + userinfo.Username;
                    output.ResutlData = userinfo;
                    return Ok(output);

                }
                else
                {
                    output.Result = 0;
                    output.Message = "Invaild Username Password";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }


        [HttpPost]
        [Route("api/user/GetEncrypt")]
        public IHttpActionResult GetEncrypt(User _user)
        {
            var encryptdata = CommonResult.Encrypt(_user.Password);
            return Ok(encryptdata);
        }
        [HttpPost]
        [Route("api/user/GetDeEncrypt")]
        public IHttpActionResult GetDeEncrypt(User _user)
        {
            var encryptdata = CommonResult.Decrypt(_user.Password);
            return Ok(encryptdata);
        }


        #endregion

        #region NewsEvents
        
        [HttpGet]
        [Route("api/NewsEvent/GetAllNewsEvents")]
        [ResponseType(typeof(NewsEvents))]
        public IHttpActionResult GetAllNewsEvents()
        {

            var ne = _ASPMDBContext.NewsEvents.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = ne;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }
       
        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/NewsEvent/SaveNewsEvents")]
        public IHttpActionResult SaveNewsEvents([FromBody] NewsEvents newsevents)
        {
            try
            {
                CommonResult output = new CommonResult();
                if (newsevents.Id != null)
                {
                    NewsEvents _newsevents= new NewsEvents();
                    var existedItem = _ASPMDBContext.NewsEvents.Where(x => x.Id == newsevents.Id).FirstOrDefault();

                    if (existedItem != null && existedItem.Id != 0)
                    {
                        //update
                        newsevents.ModifiedOn = DateTime.Now;
                        _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(newsevents);
                        _newsevents = existedItem;
                        output.ResutlData = _newsevents;
                    }
                    else
                    {

                        newsevents.IsActive = true;
                        _ASPMDBContext.NewsEvents.Add(newsevents);
                        output.ResutlData = newsevents;

                    }
                    _ASPMDBContext.SaveChanges();
                    //CommonResult output = new CommonResult();
                    output.Result = 1;
                    output.Message = "";

                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record to add/update";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }

        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/NewsEvent/deleteNewsEvent")]
        public IHttpActionResult deleteNewsEvent([FromBody] NewsEvents _newevents)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.NewsEvents.Where(x => x.Id == _newevents.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var newevents = existedItem;
                    newevents.IsActive = false;
                    newevents.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(newevents);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = newevents;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }

        #endregion


        #region Staff
        [HttpGet]
        [Route("api/staff/GetAllStaff")]
        [ResponseType(typeof(Staff))]
        public IHttpActionResult GetAllStaff()
        {

            //var st = _ASPMDBContext.Staffs.Where(x => x.IsActive == true).ToList();
            //var dt = _ASPMDBContext.Designations.Where(x => x.IsActive == true).ToList();
            //var GroupJoinMS = from staff in _ASPMDBContext.Staffs.ToList()
            //                  join des in _ASPMDBContext.Designations.ToList()
            //                  on staff.Designation.Id equals des.Id
            //                  into EmployeeGroups
            //                  select new { Sid=staff.Id, EmployeeGroups };
            var innerJoin = from s in _ASPMDBContext.Staffs
                            join d in _ASPMDBContext.Designations 
                                on s.DesignationId equals d.Id
                            join c in _ASPMDBContext.Categorys
                                on d.CategoryId equals c.Id 
                            join sc in _ASPMDBContext.SubCategorys
                                on d.SubCategoryId equals sc.Id
                            where s.IsActive==true && c.categoryTypeId == 1
                            orderby d.SubCategoryId
                            select new
                            {
                                Staffid = s.Id,
                                StaffName = s.Name,
                                DesignationId = d.Id,
                                DesignationName = d.Description,
                                CategoryId = c.Id,
                                CategoryName = c.Description,
                                SubCategoryId = sc.Id,
                                SubCategoryName = sc.Description,
                            };

            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = innerJoin;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/staff/SaveStaff")]
        public IHttpActionResult SaveStaff([FromBody] Staff staffs) {
            try
            {
                CommonResult output = new CommonResult();
                if (staffs.Id != null)
                {
                    Staff _staffs = new Staff();
                    
                    var existedItem = _ASPMDBContext.Staffs.Where(x => x.Id == staffs.Id).FirstOrDefault();

                    if (existedItem != null && existedItem.Id != 0)
                    {
                        //update
                        staffs.ModifiedOn = DateTime.Now;
                        _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(staffs);
                        _staffs = existedItem;
                        output.ResutlData = _staffs;
                    }
                    else
                    {
                        staffs.CreatedOn = DateTime.Now;
                        staffs.ModifiedOn = DateTime.Now;
                        staffs.IsActive = true;
                        _ASPMDBContext.Staffs.Add(staffs);
                        output.ResutlData = staffs;
                    }
                    _ASPMDBContext.SaveChanges();
                    //CommonResult output = new CommonResult();
                    output.Result = 1;
                    output.Message = "";

                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record to add/update";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/staff/deleteStaff")]
        public IHttpActionResult deleteStaffs([FromBody] Staff _staffs)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.Staffs.Where(x => x.Id == _staffs.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var staff = existedItem;
                    staff.IsActive = false;
                    staff.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(staff);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = staff;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }

        #endregion

        #region Category
        [HttpGet]
        [Route("api/category/GetAllCategory")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetAllCategory()
        {

            var hb = _ASPMDBContext.Categorys.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = hb;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }
        #endregion
        #region SubCategory
        [HttpGet]
        [Route("api/subcategory/GetAllSubCategory")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetAllSubCategory()
        {
            var innerJoin = from sc in _ASPMDBContext.SubCategorys
                            join c in _ASPMDBContext.Categorys
                                on sc.CategoryId equals c.Id
                            where sc.IsActive == true
                            select new
                            {

                                Categoryid = c.Id,
                                Description = sc.Description,
                                IsActive = sc.IsActive,
                                SubCategoryId=sc.Id
                            };
            var hb = _ASPMDBContext.SubCategorys.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = innerJoin;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }
        #endregion
        #region Designation
        [HttpGet]
        [Route("api/designation/GetAllDesignation")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetAllDesignation()
        {

            var hb = _ASPMDBContext.Designations.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = hb;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }
        #endregion



        #region NoticeBoard
       


        [HttpGet]
        [Route("api/NoticeBoard/GetAllNoticeBoard")]
        [ResponseType(typeof(NewsEvents))]
        public IHttpActionResult GetAllNoticeBoard()
        {

            var ne = _ASPMDBContext.NoticeBoards.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = ne;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }

        
        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/NoticeBoard/deleteNoticeBoard")]
        public IHttpActionResult deleteNoticeBoard([FromBody] NoticeBoard _noticeboards)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.NewsEvents.Where(x => x.Id == _noticeboards.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var noticeboards = existedItem;
                    noticeboards.IsActive = false;
                    noticeboards.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(noticeboards);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = noticeboards;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }

        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/NoticeBoard/saveNoticeBoard")]
        public IHttpActionResult saveNoticeBoard([FromBody] NoticeBoard _noticeboards) {
            try
            {
                CommonResult output = new CommonResult();
                var existedItemNB = _ASPMDBContext.NoticeBoards.Where(x => x.Id == _noticeboards.Id).FirstOrDefault();
                if (existedItemNB != null && existedItemNB.Id != 0)
                {
                    //update
                    _noticeboards.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItemNB).CurrentValues.SetValues(_noticeboards);
                    _noticeboards = existedItemNB;
                    output.ResutlData = _noticeboards;
                }
                else
                {
                    _noticeboards.CreatedOn = DateTime.Now;
                    _noticeboards.ModifiedOn = DateTime.Now;
                    _noticeboards.IsActive = true;
                    _ASPMDBContext.NoticeBoards.Add(_noticeboards);
                    output.ResutlData = _noticeboards;
                }
                _ASPMDBContext.SaveChanges();
                //CommonResult output = new CommonResult();
                output.Result = 1;
                output.Message = "";

                return Ok(output);
            }
            catch (Exception ex) {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }

        #endregion


        #region Vacancies
        [HttpGet]
        [Route("api/Vacancies/GetAllVacancie")]
        [ResponseType(typeof(NewsEvents))]
        public IHttpActionResult GetAllVacancie()
        {

            var vac = _ASPMDBContext.Vacancies.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = vac;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Vacancies/deleteVacancie")]
        public IHttpActionResult deleteVacancie([FromBody] Vacancies _Vacancies)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.Vacancies.Where(x => x.Id == _Vacancies.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var vacancies = existedItem;
                    vacancies.IsActive = false;
                    vacancies.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(vacancies);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = vacancies;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Vacancies/saveVacancies")]
        public IHttpActionResult saveVacancies([FromBody] Vacancies _vacancies)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItemNB = _ASPMDBContext.Vacancies.Where(x => x.Id == _vacancies.Id).FirstOrDefault();
                if (existedItemNB != null && existedItemNB.Id != 0)
                {
                    //update
                    _vacancies.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItemNB).CurrentValues.SetValues(_vacancies);
                    _vacancies = existedItemNB;
                    output.ResutlData = _vacancies;
                }
                else
                {
                    _vacancies.CreatedOn = DateTime.Now;
                    _vacancies.ModifiedOn = DateTime.Now;
                    _vacancies.IsActive = true;
                    _ASPMDBContext.Vacancies.Add(_vacancies);
                    output.ResutlData = _vacancies;
                }
                _ASPMDBContext.SaveChanges();
                //CommonResult output = new CommonResult();
                output.Result = 1;
                output.Message = "";

                return Ok(output);
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }

        #endregion


        #region Photo
        [HttpGet]
        [Route("api/Photo/GetAllPhoto")]
        [ResponseType(typeof(Photo))]
        public IHttpActionResult GetAllPhoto()
        {

            var vac = _ASPMDBContext.Photos.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = vac;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Photo/deletePhoto")]
        public IHttpActionResult deletePhoto([FromBody] Photo _Photo)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.Photos.Where(x => x.Id == _Photo.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var vacancies = existedItem;
                    vacancies.IsActive = false;
                    vacancies.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(vacancies);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = vacancies;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }



        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Photo/savePhoto")]
        public IHttpActionResult savePhoto([FromBody] Photo _Photo)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItemNB = _ASPMDBContext.Photos.Where(x => x.Id == _Photo.Id).FirstOrDefault();
                if (existedItemNB != null && existedItemNB.Id != 0)
                {
                    //update
                    _Photo.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItemNB).CurrentValues.SetValues(_Photo);
                    _Photo = existedItemNB;
                    output.ResutlData = _Photo;
                }
                else
                {
                    _Photo.CreatedOn = DateTime.Now;
                    _Photo.ModifiedOn = DateTime.Now;
                    _Photo.IsActive = true;
                    _ASPMDBContext.Photos.Add(_Photo);
                    output.ResutlData = _Photo;
                }
                _ASPMDBContext.SaveChanges();
                //CommonResult output = new CommonResult();
                output.Result = 1;
                output.Message = "";

                return Ok(output);
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }
        #endregion


        #region facilities
        [HttpGet]
        [Route("api/Facilitie/GetAllFacilities")]
        [ResponseType(typeof(Facilities))]
        public IHttpActionResult GetAllFacilities()
        {
            var innerJoin = from f in _ASPMDBContext.Facilities
                            join c in _ASPMDBContext.Categorys
                               on f.CategoryId equals c.Id
                            where f.IsActive == true 
                            orderby f.Id
                            select new
                            {
                                Id = f.Id,
                                Description = f.Description,
                                Name = f.Name,
                                Oldname = f.Oldname,
                                CategoryId = f.CategoryId,
                                IsActive = f.IsActive,
                                CategoryName = c.Description,
                                
                            };


          //  var vac = _ASPMDBContext.Facilities.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = innerJoin;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Facilitie/deleteFacilitie")]
        public IHttpActionResult deleteFacilitie([FromBody] Facilities _Facilities)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.Facilities.Where(x => x.Id == _Facilities.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var Facilities = existedItem;
                    Facilities.IsActive = false;
                    Facilities.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(Facilities);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = Facilities;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }



        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Facilitie/saveFacilities")]
        public IHttpActionResult saveFacilities([FromBody] Facilities _Facilities)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItemNB = _ASPMDBContext.Facilities.Where(x => x.Id == _Facilities.Id).FirstOrDefault();
                if (existedItemNB != null && existedItemNB.Id != 0)
                {
                    //update
                    _Facilities.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItemNB).CurrentValues.SetValues(_Facilities);
                    _Facilities = existedItemNB;
                    output.ResutlData = _Facilities;
                }
                else
                {
                    _Facilities.CreatedOn = DateTime.Now;
                    _Facilities.ModifiedOn = DateTime.Now;
                    _Facilities.IsActive = true;
                    _ASPMDBContext.Facilities.Add(_Facilities);
                    output.ResutlData = _Facilities;
                }
                _ASPMDBContext.SaveChanges();
                //CommonResult output = new CommonResult();
                output.Result = 1;
                output.Message = "";

                return Ok(output);
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }
        #endregion


        #region NCERT
        [HttpGet]
        [Route("api/NCERT/GetAllNCERT")]
        [ResponseType(typeof(Facilities))]
        public IHttpActionResult GetAllNCERT()
        {
              var NCERT = _ASPMDBContext.NCRETs.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = NCERT;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/NCERT/deleteNCERT")]
        public IHttpActionResult deleteNCERT([FromBody] NCRET _NCRET)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.NCRETs.Where(x => x.Id == _NCRET.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var ncret = existedItem;
                    ncret.IsActive = false;
                    ncret.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(ncret);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = ncret;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }



        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/NCERT/saveNCERT")]
        public IHttpActionResult saveNCERT([FromBody] NCRET _NCRET)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItemNB = _ASPMDBContext.NCRETs.Where(x => x.Id == _NCRET.Id).FirstOrDefault();
                if (existedItemNB != null && existedItemNB.Id != 0)
                {
                    //update
                    _NCRET.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItemNB).CurrentValues.SetValues(_NCRET);
                    _NCRET = existedItemNB;
                    output.ResutlData = _NCRET;
                }
                else
                {
                    _NCRET.CreatedOn = DateTime.Now;
                    _NCRET.ModifiedOn = DateTime.Now;
                    _NCRET.IsActive = true;
                    _ASPMDBContext.NCRETs.Add(_NCRET);
                    output.ResutlData = _NCRET;
                }
                _ASPMDBContext.SaveChanges();
                //CommonResult output = new CommonResult();
                output.Result = 1;
                output.Message = "";

                return Ok(output);
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }


        #endregion


        #region AWESBOOK
        [HttpGet]
        [Route("api/AWESBOOK/GetAllAWESBOOK")]
        [ResponseType(typeof(Facilities))]
        public IHttpActionResult GetAllAWESBOOK()
        {
            var awesbook = _ASPMDBContext.AWESBooks.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = awesbook;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/AWESBOOK/deleteAWESBOOK")]
        public IHttpActionResult deleteAWESBOOK([FromBody] AWESBook _AWESBook)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.AWESBooks.Where(x => x.Id == _AWESBook.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var awesbook = existedItem;
                    awesbook.IsActive = false;
                    awesbook.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(awesbook);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = awesbook;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }



        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/AWESBOOK/saveAWESBOOK")]
        public IHttpActionResult saveAWESBOOK([FromBody] AWESBook _AWESBook)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItemNB = _ASPMDBContext.AWESBooks.Where(x => x.Id == _AWESBook.Id).FirstOrDefault();
                if (existedItemNB != null && existedItemNB.Id != 0)
                {
                    //update
                    _AWESBook.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItemNB).CurrentValues.SetValues(_AWESBook);
                    _AWESBook = existedItemNB;
                    output.ResutlData = _AWESBook;
                }
                else
                {
                    _AWESBook.CreatedOn = DateTime.Now;
                    _AWESBook.ModifiedOn = DateTime.Now;
                    _AWESBook.IsActive = true;
                    _ASPMDBContext.AWESBooks.Add(_AWESBook);
                    output.ResutlData = _AWESBook;
                }
                _ASPMDBContext.SaveChanges();
                //CommonResult output = new CommonResult();
                output.Result = 1;
                output.Message = "";

                return Ok(output);
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }


        #endregion


        #region Emagazine
        [HttpGet]
        [Route("api/Emagazine/GetAllEmagazine")]
        [ResponseType(typeof(Facilities))]
        public IHttpActionResult GetAllEmagazine()
        {
            var awesbook = _ASPMDBContext.AWESBooks.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = awesbook;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Emagazine/deleteEmagazine")]
        public IHttpActionResult deleteEmagazine([FromBody] AWESBook _AWESBook)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.AWESBooks.Where(x => x.Id == _AWESBook.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var awesbook = existedItem;
                    awesbook.IsActive = false;
                    awesbook.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(awesbook);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = awesbook;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }



        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Emagazine/saveEmagazine")]
        public IHttpActionResult saveEmagazine([FromBody] Emagazine _Emagazine)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItemNB = _ASPMDBContext.Emagazines.Where(x => x.Id == _Emagazine.Id).FirstOrDefault();
                if (existedItemNB != null && existedItemNB.Id != 0)
                {
                    //update
                    _Emagazine.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItemNB).CurrentValues.SetValues(_Emagazine);
                    _Emagazine = existedItemNB;
                    output.ResutlData = _Emagazine;
                }
                else
                {
                    _Emagazine.CreatedOn = DateTime.Now;
                    _Emagazine.ModifiedOn = DateTime.Now;
                    _Emagazine.IsActive = true;
                    _ASPMDBContext.Emagazines.Add(_Emagazine);
                    output.ResutlData = _Emagazine;
                }
                _ASPMDBContext.SaveChanges();
                //CommonResult output = new CommonResult();
                output.Result = 1;
                output.Message = "";

                return Ok(output);
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }


        #endregion

        #region Download
        [HttpGet]
        [Route("api/Download/GetAllDownload")]
        [ResponseType(typeof(NewsEvents))]
        public IHttpActionResult GetAllDownload()
        {

            var vac = _ASPMDBContext.Downloads.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = vac;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Download/deleteDownload")]
        public IHttpActionResult deleteDownload([FromBody] Downloads _Downloads)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.Downloads.Where(x => x.Id == _Downloads.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var downloads = existedItem;
                    downloads.IsActive = false;
                    downloads.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(downloads);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = downloads;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Download/saveDownload")]
        public IHttpActionResult saveDownload([FromBody] Downloads _Downloads)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItemNB = _ASPMDBContext.Downloads.Where(x => x.Id == _Downloads.Id).FirstOrDefault();
                if (existedItemNB != null && existedItemNB.Id != 0)
                {
                    //update
                    _Downloads.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItemNB).CurrentValues.SetValues(_Downloads);
                    _Downloads = existedItemNB;
                    output.ResutlData = _Downloads;
                }
                else
                {
                    _Downloads.CreatedOn = DateTime.Now;
                    _Downloads.ModifiedOn = DateTime.Now;
                    _Downloads.IsActive = true;
                    _ASPMDBContext.Downloads.Add(_Downloads);
                    output.ResutlData = _Downloads;
                }
                _ASPMDBContext.SaveChanges();
                //CommonResult output = new CommonResult();
                output.Result = 1;
                output.Message = "";

                return Ok(output);
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }

        #endregion



        #region Video
        [HttpGet]
        [Route("api/Video/GetAllVideo")]
        [ResponseType(typeof(Video))]
        public IHttpActionResult GetAllVideo()
        {
            var awesbook = _ASPMDBContext.Videos.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = awesbook;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Video/deleteVideo")]
        public IHttpActionResult deleteVideo([FromBody] Video _Video)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.Videos.Where(x => x.Id == _Video.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var video = existedItem;
                    video.IsActive = false;
                    video.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(video);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = video;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }



        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Video/saveVideo")]
        public IHttpActionResult saveVideo([FromBody] Video _Video)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItemNB = _ASPMDBContext.Videos.Where(x => x.Id == _Video.Id).FirstOrDefault();
                if (existedItemNB != null && existedItemNB.Id != 0)
                {
                    //update
                    _Video.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItemNB).CurrentValues.SetValues(_Video);
                    _Video = existedItemNB;
                    output.ResutlData = _Video;
                }
                else
                {
                    _Video.CreatedOn = DateTime.Now;
                    _Video.ModifiedOn = DateTime.Now;
                    _Video.IsActive = true;
                    _ASPMDBContext.Videos.Add(_Video);
                    output.ResutlData = _Video;
                }
                _ASPMDBContext.SaveChanges();
                //CommonResult output = new CommonResult();
                output.Result = 1;
                output.Message = "";

                return Ok(output);
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }


        #endregion

        #region fees
        [HttpGet]
        [Route("api/Fees/GetAllFess")]
        [ResponseType(typeof(FeeStructure))]
        public IHttpActionResult GetAllFees()
        {
            // var innerJoin = from f in _ASPMDBContext.fees
            //join c in _ASPMDBContext.Categorys
            //    on f.CategoryId equals c.Id
            //join sc in _ASPMDBContext.SubCategorys
            //    on f.SubCategoryId equals sc.Id
            // select new
            //{

            //    //CategoryId = c.Id,
            //    //CategoryName = c.Description,
            //    //SubCategory = sc,
            //    fees=f
            //};

            //var innerJoin = _ASPMDBContext.fees.Where(x => x.IsActive == true).ToList();
            var innerJoin = from f in _ASPMDBContext.fees
                            join c in _ASPMDBContext.Categorys
                             on f.CategoryId equals c.Id
                            join s in _ASPMDBContext.SubCategorys
                                 on f.SubCategoryId equals s.Id
                            where f.IsActive == true
                            select new
                            {   catID= c.Id,
                                catDesc= c.Description,
                                feesDesc = f.Description,
                                feesID = f.Id,
                                FeeJCOs = f.JCOs,
                                FeeOffrs = f.Offrs,
                                FeeOR = f.OR,
                                FeeCivil = f.Civil,
                                SubCat=s.Id,
                                SubCatDesc=s.Description,
                                SubCatGroup = s.isGroup
                            };
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = innerJoin;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        
        }

        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Fees/deleteFees")]
        public IHttpActionResult deleteFees([FromBody] FeeStructure _Video)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.fees.Where(x => x.Id == _Video.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var video = existedItem;
                    video.IsActive = false;
                    video.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(video);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "Record deleted successfully.";
                    output.ResutlData = video;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/Fees/saveFees")]
        public IHttpActionResult saveFees([FromBody] FeeStructure _Video)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItemNB = _ASPMDBContext.fees.Where(x => x.Id == _Video.Id).FirstOrDefault();
                if (existedItemNB != null && existedItemNB.Id != 0)
                {
                    //update
                    _Video.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItemNB).CurrentValues.SetValues(_Video);
                    _Video = existedItemNB;
                    _Video.ModifiedOn = DateTime.Now;
                    output.ResutlData = _Video;
                }
                else
                {
                    if (_Video.Id == 0) {

                        var innerJoin = from f in _ASPMDBContext.fees
                                        join c in _ASPMDBContext.Categorys
                                         on f.CategoryId equals c.Id
                                        join s in _ASPMDBContext.SubCategorys
                                             on f.SubCategoryId equals s.Id
                                        where f.IsActive == true && f.SubCategoryId==_Video.SubCategoryId && s.isGroup==false
                                        select new
                                        {
                                            catID = c.Id,
                                            catDesc = c.Description,
                                            feesDesc = f.Description,
                                            feesID = f.Id,
                                            FeeJCOs = f.JCOs,
                                            FeeOffrs = f.Offrs,
                                            FeeOR = f.OR,
                                            FeeCivil = f.Civil,
                                            SubCat = s.Id,
                                            SubCatDesc = s.Description,
                                            SubCatGroup = s.isGroup
                                        };
                        var list = innerJoin.ToList();
                        if (list.Count()>0) {
                            output.Result = 0;
                            output.Message ="Record already exisit for this subcategory";
                            output.ResutlData = "";
                            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                        }
                        else
                        {
                            _Video.CreatedOn = DateTime.Now;
                            _Video.ModifiedOn = DateTime.Now;
                            _Video.IsActive = true;
                            _ASPMDBContext.fees.Add(_Video);
                            output.ResutlData = _Video;

                        }

                    }
                    

                   
                }
                _ASPMDBContext.SaveChanges();
                //CommonResult output = new CommonResult();
                output.Result = 1;
                output.Message = "";

                return Ok(output);
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }


        #endregion fees

        #region EventNGallary
        [HttpGet]
        [Route("api/eventngallary/GetAllEventnGallary")]
        [ResponseType(typeof(Photo))]
        public IHttpActionResult GetAllEventnGallary()
        {

            var vac = _ASPMDBContext.EventNGallarys.Where(x => x.IsActive == true).ToList();
            CommonResult output = new CommonResult();
            try
            {

                output.Result = 1;
                output.Message = "";
                output.ResutlData = vac;
                return Ok(output);



            }
            catch (Exception ex)
            {
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = ex;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));

            }
        }


        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/eventngallary/deleteEventnGallary")]
        public IHttpActionResult deleteEventnGallary([FromBody] EventNGallarys _EventNGallarys)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItem = _ASPMDBContext.EventNGallarys.Where(x => x.Id == _EventNGallarys.Id).FirstOrDefault();
                if (existedItem != null)
                {
                    var eventNGallarys = existedItem;
                    eventNGallarys.IsActive = false;
                    eventNGallarys.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItem).CurrentValues.SetValues(eventNGallarys);
                    _ASPMDBContext.SaveChanges();
                    output.Result = 1;
                    output.Message = "record deleted successfully.";
                    output.ResutlData = eventNGallarys;
                    return Ok(output);
                }
                else
                {

                    output.Result = 0;
                    output.Message = "No record found";
                    output.ResutlData = "";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
                }
            }
            catch (Exception ex)
            {

                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }



        [HttpPost]
        [ResponseType(typeof(CommonResult))]
        [Route("api/eventngallary/saveEventnGallary")]
        public IHttpActionResult saveEventnGallary([FromBody] EventNGallarys _EventNGallarys)
        {
            try
            {
                CommonResult output = new CommonResult();
                var existedItemNB = _ASPMDBContext.EventNGallarys.Where(x => x.Id == _EventNGallarys.Id).FirstOrDefault();
                if (existedItemNB != null && existedItemNB.Id != 0)
                {
                    //update
                    _EventNGallarys.ModifiedOn = DateTime.Now;
                    _ASPMDBContext.Entry(existedItemNB).CurrentValues.SetValues(_EventNGallarys);
                    _EventNGallarys = existedItemNB;
                    output.ResutlData = _EventNGallarys;
                }
                else
                {
                    _EventNGallarys.CreatedOn = DateTime.Now;
                    _EventNGallarys.ModifiedOn = DateTime.Now;
                    _EventNGallarys.IsActive = true;
                    _ASPMDBContext.EventNGallarys.Add(_EventNGallarys);
                    output.ResutlData = _EventNGallarys;
                }
                _ASPMDBContext.SaveChanges();
                //CommonResult output = new CommonResult();
                output.Result = 1;
                output.Message = "";

                return Ok(output);
            }
            catch (Exception ex)
            {
                CommonResult output = new CommonResult();
                output.Result = 0;
                output.Message = ex.Message;
                output.ResutlData = "";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, output));
            }
        }

        #endregion
    }
}
