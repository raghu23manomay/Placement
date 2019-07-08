using mvc.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.Controllers
{
    public class CRecruiterController : Controller
    {
        // GET: CRecruiter
        public ActionResult Index()
        {
            return View();
        }


        public List<SelectListItem> binddropdown(string action, int val = 0, int GrouId = 0, int userid = 0)
        {
            jobDbContext _db = new jobDbContext();

            var res = _db.Database.SqlQuery<SelectListItem>("exec USP_BindDropDown @action , @val , @GrouId, @userid",
                    new SqlParameter("@action", action),
                    new SqlParameter("@val", val),
                    new SqlParameter("@GrouId", GrouId == 0 ? (object)DBNull.Value : GrouId),
                    new SqlParameter("@userid", userid)
                   )
                   .ToList()
                   .AsEnumerable()
                   .Select(r => new SelectListItem
                   {
                       Text = r.Text.ToString(),
                       Value = r.Value.ToString(),
                       Selected = r.Value.Equals(Convert.ToString(val))
                   }).ToList();

            return res;
        }

        public ActionResult AddRecruiterProfile(int? userid)
        {
            ViewData["LocationList"] = binddropdown("LocationList", 0);
            jobDbContext db = new jobDbContext();
            var result = db.editlist.SqlQuery(@"exec disp @Id", new SqlParameter("@Id", 39)).ToList<RecruiterList>();
            RecruiterList ed = new RecruiterList();
            ed = result.FirstOrDefault();
            return View("AddRecruiterProfile", ed);
        }
        //upload//DataFromView
        [HttpPost]
        public ActionResult SaveRecruiterProfile(RecruiterList cl)
        {

            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {

                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFileBase file = files[i];
                        string fname;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;

                        }

                        var name = Request.Form[0].ToString();
                        var summery = Request.Form[1].ToString();
                        var totalYearsOfExp = Request.Form[2].ToString();
                        var location = Request.Form[3].ToString();
                        var graduation = Request.Form[4].ToString();
                        var PG = Request.Form[5].ToString();
                        var currentlyWorkingWith = Request.Form[6].ToString();
                        var cPosition = Request.Form[7].ToString();
                        var cDetailProfile = Request.Form[8].ToString();
                        var lastWorkingWith = Request.Form[9].ToString();
                        var lPosition = Request.Form[10].ToString();
                        var lDetailProfile = Request.Form[11].ToString();
                        var KeyArea = Request.Form[12].ToString();
                        var industrySpecialisation = Request.Form[13].ToString();
                        var verticalSpecalization = Request.Form[14].ToString();
                        var expertise = Request.Form[15].ToString();
                        var achivement = Request.Form[16].ToString();
                        var LanguagesKnow = Request.Form[17].ToString();


                        string filePath = Server.MapPath("~/ProfileImage/");

                        bool isExists = System.IO.Directory.Exists(filePath);
                        if (!isExists) { System.IO.Directory.CreateDirectory(filePath); }


                        string extension = Path.GetExtension(file.FileName);
                        string fileName = name + extension;

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(filePath, fileName);
                        var imgpath = filePath + name + extension;
                        var DbPath = "~/ProfileImages/" + name + extension;
                        file.SaveAs(fname);

                        jobDbContext db = new jobDbContext();

                        var res = db.Database.ExecuteSqlCommand(@"exec [SpUserProfile] @userid,@name,@summery,@totalYearsOfExp,@location,@graduation,@PG,@currentlyWorkingWith,@cPosition,@cDetailProfile,@lastWorkingWith,@lPosition,@lDetailProfile,@KeyArea,@industrySpecialisation,@verticalSpecalization,@expertise,@achivement,@LanguagesKnow,@profileImg",
                               new SqlParameter("@userid", 007),
                               new SqlParameter("@name", name),
                               new SqlParameter("@summery", summery),
                               new SqlParameter("@totalYearsOfExp", totalYearsOfExp),
                               new SqlParameter("@location", location),
                               new SqlParameter("@graduation", graduation),
                               new SqlParameter("@PG", PG),
                               new SqlParameter("@currentlyWorkingWith", currentlyWorkingWith),
                               new SqlParameter("@cPosition", cPosition),
                               new SqlParameter("@cDetailProfile", cDetailProfile),
                               new SqlParameter("@lastWorkingWith", lastWorkingWith),
                               new SqlParameter("@lPosition", lPosition),
                               new SqlParameter("@lDetailProfile", lDetailProfile),
                               new SqlParameter("@KeyArea", KeyArea),
                               new SqlParameter("@industrySpecialisation", industrySpecialisation),
                               new SqlParameter("@verticalSpecalization", verticalSpecalization),
                               new SqlParameter("@expertise", expertise),
                               new SqlParameter("@achivement", achivement),
                               new SqlParameter("@LanguagesKnow", LanguagesKnow),
                               new SqlParameter("@profileImg", DbPath));

                        return Json("Data Inserted Successfully");

                    }


                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");

                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            return Json("Data Inserted Successfully");
        }
        //EditRecruiterProfile
        public ActionResult FetchRecruiterProfile(int? userid)
        {
            ViewData["LocationList"] = binddropdown("LocationList", 0);
            ViewData["PositionSectorList"] = binddropdown("PositionSector", 0);            
            jobDbContext db = new jobDbContext();
            var result = db.editlist.SqlQuery(@"exec disp @Id", new SqlParameter("@Id", userid)).ToList<RecruiterList>();

            RecruiterList ed = new RecruiterList();
            ed = result.FirstOrDefault();
            ViewBag.Verticaldata = result.FirstOrDefault().VerticalId;
            return View("FetchRecruiterProfile", ed);

        }
        [HttpPost]
        public ActionResult UpdateRecruiterProfile(RecruiterList rm)
        {
            var DbPath="";
            if (Request.Files.Count > 0)
            {
                var name = rm.F_Name;
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {

                    HttpPostedFileBase file = files[i];
                    string fname;

                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;

                    }

                    string filePath = Server.MapPath("~/ProfileImage/");

                    bool isExists = System.IO.Directory.Exists(filePath);
                    if (!isExists) { System.IO.Directory.CreateDirectory(filePath); }


                    string extension = Path.GetExtension(file.FileName);
                    string fileName = name + extension;

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(filePath, fileName);
                    var imgpath = filePath + name + extension;
                     DbPath = "~/ProfileImage/" + name + extension;
                    file.SaveAs(fname);
                }
            }
            else
            {
                DbPath = rm.ProflieImg;
            }
           
            jobDbContext db = new jobDbContext();
                    var res = db.Database.ExecuteSqlCommand(@"exec SpUpdatedata @userid,@F_Name,@M_Name,@L_Name,@phone,@Mobile,@Password,@emailId,@summery,@totalYearsOfExp,@location,
                     @graduation,@pG,@currentlyWorkingWith,@cPosition,@cDetailProfile,@lastWorkingWith,@lPosition,@lDetailProfile,@KeyArea,
                     @industrySpecialisation,@verticalSpecalization,@expertise,@achivement,@languagesKnow,@profileImg",
                                       new SqlParameter("@userid", rm.UserId),
                                       new SqlParameter("@F_Name", rm.F_Name),
                                       new SqlParameter("@M_Name", rm.M_Name == null ? (object)DBNull.Value : rm.M_Name),
                                       new SqlParameter("@L_Name", rm.L_Name),
                                       new SqlParameter("@phone", rm.phone == null ? (object)DBNull.Value : rm.phone),
                                       new SqlParameter("@Mobile", rm.Mobile),
                                       new SqlParameter("@Password", rm.Password),
                                       new SqlParameter("@emailId", rm.emailId == null ? (object)DBNull.Value : rm.emailId),
                                       new SqlParameter("@summery", rm.Summery == null ? (object)DBNull.Value : rm.Summery),
                                       new SqlParameter("@totalYearsOfExp", rm.TotalYearsOfExp == null ? (object)DBNull.Value : rm.TotalYearsOfExp),
                                       new SqlParameter("@location", rm.Location == null ? (object)DBNull.Value : rm.Location),
                                       new SqlParameter("@graduation", rm.Graduation == null ? (object)DBNull.Value : rm.Graduation),
                                       new SqlParameter("@pg", rm.PG == null ? (object)DBNull.Value : rm.PG),
                                       new SqlParameter("@CurrentlyWorkingWith", rm.CurrentlyWorkingWith == null ? (object)DBNull.Value : rm.CurrentlyWorkingWith),
                                       new SqlParameter("@cPosition", rm.CurrentPosition == null ? (object)DBNull.Value : rm.CurrentPosition),
                                       new SqlParameter("@cDetailProfile", rm.CurrentDetailProfile == null ? (object)DBNull.Value : rm.CurrentDetailProfile),
                                       new SqlParameter("@lastWorkingWith", rm.LastWorkingWith == null ? (object)DBNull.Value : rm.LastWorkingWith),
                                       new SqlParameter("@lPosition", rm.LastPosition == null ? (object)DBNull.Value : rm.LastPosition),
                                       new SqlParameter("@lDetailProfile", rm.LastDetailProfile == null ? (object)DBNull.Value : rm.LastDetailProfile),
                                       new SqlParameter("@KeyArea", rm.KeyArea == null ? (object)DBNull.Value : rm.KeyArea),
                                       new SqlParameter("@industrySpecialisation", rm.IndustrySpecialisation == null ? (object)DBNull.Value : rm.IndustrySpecialisation),
                                       new SqlParameter("@verticalSpecalization", rm.VerticalSpecalization == null ? (object)DBNull.Value : rm.VerticalSpecalization),
                                       new SqlParameter("@expertise", rm.Expertise == null ? (object)DBNull.Value : rm.Expertise),
                                       new SqlParameter("@achivement", rm.Achivement == null ? (object)DBNull.Value : rm.Achivement),
                                       new SqlParameter("@LanguagesKnow", rm.LanguagesKnow == null ? (object)DBNull.Value : rm.LanguagesKnow),
                                       new SqlParameter("@profileImg", DbPath));

                
            
            return Json("Data Updated Sucessfully");
        }

        
        public ActionResult DisplayRecruiterProfile(int? userid)
        {
            jobDbContext db = new jobDbContext();
            var result = db.editlist.SqlQuery(@"exec disp @Id", new SqlParameter("@Id",Convert.ToInt16(Session["User_id"].ToString()))).ToList<RecruiterList>();

            RecruiterList ed = new RecruiterList();
            ed = result.FirstOrDefault();
            return View("DisplayRecruiterProfile", ed);

        }


        //================================== Insert Multipal Location ===========================================

        [HttpPost]
        public ActionResult add_MultipalVertical(Requirement[] Requirement)
        {

            try
            {
                var result = 0;
                jobDbContext _db = new jobDbContext();
                foreach (var item in Requirement)
                {
                    Requirement O = new Requirement();
                    O.SectorId = item.SectorId;
                    result = _db.Database.ExecuteSqlCommand(@"exec USP_AddUserVerticals 
                    @UserId,@VerticalID",
                    new SqlParameter("@UserId",Convert.ToInt16(Session["User_id"].ToString())),
                    new SqlParameter("@VerticalID", O.SectorId));

                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForDesignation");
                //return PartialView(rs);
            }


        }
        
        [HandleError]
        [HttpPost]
        public ActionResult DeleteVehical(int vehicalid)
        {

            jobDbContext _db = new jobDbContext();
            var result = _db.Database.ExecuteSqlCommand(@"exec USP_DeleteUserVerticals @UserId,@VerticalID",
                 new SqlParameter("@UserId", Convert.ToInt16(Session["User_id"].ToString())),
                new SqlParameter("@VerticalID", vehicalid));
            return Json("Deleted sucessfully");
        }
    }
}