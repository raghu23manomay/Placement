using mvc.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Configuration;
using System.Web.UI;


namespace mvc.Controllers
{
    public class PlacementController : Controller
    {
        // GET: Placement

       // [HttpPost]
        public ActionResult LoadData(int? page,int? PageRowNumber, DateTime? Date,int? userid, String Name = null, String DName = null, String CName = null, String Eid = null, String Mob = null, String Stage = null, String Activity = null)
       {
            try
            {

            
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }          

            // =====  Session  passed to index view for filter  =====
           // DName.Replace("\t", "");
            if (Name != null) { Session["Name"] = Name; } else { Session["Name"] = ""; }
            if (CName != null) { Session["CName"] = CName; } else { Session["CName"] = ""; }
            if (Mob != null) { Session["Mob"] = Mob; } else { Session["Mob"] = ""; }
            if (DName != null) { Session["DName"] = DName; } else { Session["DName"] = ""; }
            if (Stage != null) { Session["Stage"] = Stage; } else { Session["Stage"] = ""; }
            if (Activity != null) { Session["Activity"] = Activity; } else { Session["Activity"] = ""; }
            // if (Date != null) { Session["Date"] = Date; } else { Session["Date"] = "1/1/1991"; }
            if (PageRowNumber != null) { Session["PageRowNumber"] = PageRowNumber; } else { Session["PageRowNumber"] = 8; }
            if (userid != null) { Session["userid"] = userid; } else { Session["userid"] = 0; }

                StaticPagedList<CandidateList> itemsAsIPagedList;
            itemsAsIPagedList = GridList(page,Convert.ToInt16(Session["PageRowNumber"].ToString()), Session["Name"].ToString(),Session["DName"].ToString(), Session["CName"].ToString(), Eid, Session["Mob"].ToString(),Session["Stage"].ToString(),Session["Activity"].ToString(),Date,Convert.ToInt16(Session["userid"].ToString()));
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("_PartialGrid", itemsAsIPagedList)
                   : View("_PartialGrid",itemsAsIPagedList);
            }
            catch(Exception ex)
            {
                var mgs = ex.Message;
                return Json(mgs);
            }
        }

        public StaticPagedList<CandidateList> GridList(int? page, int? PageRowNumber, String Name, String DName, String CName, String Eid, String Mob, String Stage, string Activity, DateTime? Date, int? userid, int? reqid = 0)
        {
            jobDbContext _db = new jobDbContext();
            var pageIndex = (page ?? 1);
            int pageSize = (PageRowNumber ?? 8);
            int totalCount = (PageRowNumber ?? 8);
            CandidateList clist = new CandidateList();
            if (Name == null) Name = "";
            if (DName == null) DName = "";
            if (CName == null) CName = "";
            if (Eid == null) Eid = "";
            if (Mob == null) Mob = "";
            if (Stage == null) Stage = "";
            if (Activity == null) Activity = "";
            if (Date.ToString() == "01/01/1991 0:00:00") Date = null;

            if(Convert.ToInt16(Session["RoleID"].ToString()) == 7)
            {

            }
            else if (userid == 0)
            {
                userid = Convert.ToInt16(Session["User_id"].ToString());
            }
            

            IEnumerable<CandidateList> result = _db.CList.SqlQuery(@"exec usp_CandidateList
              @pPageIndex, @pPageSize,@pName,@count,@dName,@cName,@ename,@mob,@stage,@activity,@date,@userid,@reqid",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@pName", Name),
               new SqlParameter("@count", totalCount),
               new SqlParameter("@dName", DName),
                new SqlParameter("@cName", CName),
                new SqlParameter("@ename", Eid),
                new SqlParameter("@mob", Mob),
                new SqlParameter("@stage", Stage),
                new SqlParameter("@activity", Activity),
                new SqlParameter("@date", Date == null ? (object)DBNull.Value : Date),
                new SqlParameter("@userid", userid == null ? (object)DBNull.Value : userid),
                new SqlParameter("@reqid", reqid)
                ).ToList<CandidateList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<CandidateList>(result, pageIndex, pageSize, totalCount);
            
            return itemsAsIPagedList;
        }

        public ActionResult Index(int? page, int? PageRowNumber, String Name = null, String DName = null, String CName = null, String Eid = null, String Mob = null, String Stage = null, String Activity = null, DateTime? Date = null, int pIsShortListed = 0, int pInterview = 0, int reqid = 0)
        {
            ViewData["UserList"] = binddropdown("UserList", 0);

            int? userid;
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Convert.ToInt32(Session["RoleID"].ToString()) != 6)
            {
                userid = null;
            }
            else
            {
                userid = Convert.ToInt32(Session["User_id"].ToString());
            }

            // =====  Session  passed to index view for filter  =====
            if (Session["Name"] != null) { Name = Session["Name"].ToString(); } else { Name = null; }
            if (Session["CName"] != null) { CName = Session["CName"].ToString(); } else { CName = null; }
            if (Session["Mob"] != null) { Mob = Session["Mob"].ToString(); } else { Mob = null; }
            if (Session["DName"] != null) { DName = Session["DName"].ToString(); } else { DName = null; }
            if (Session["Stage"] != null) { Stage = Session["Stage"].ToString(); } else { Stage = null; }
            if (Session["Activity"] != null) { Activity = Session["Activity"].ToString(); } else { Activity = null; }
            // if (Session["Date"] != null) { Date =Convert.ToDateTime(Session["Date"].ToString()); } else { Date = null; }
            if (Session["PageRowNumber"] != null) { PageRowNumber = Convert.ToInt16(Session["PageRowNumber"].ToString()); } else { PageRowNumber = null; }
            if (Session["userid"] != null) { userid = Convert.ToInt16(Session["userid"].ToString()); } else { userid = 0; }
            StaticPagedList<CandidateList> itemsAsIPagedList;
            itemsAsIPagedList = GridList(page,PageRowNumber,Name, DName, CName, Eid, Mob, Stage, Activity, Date, userid, reqid);
            ViewData["UserList"] = binddropdown("UserList", 0);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("index", itemsAsIPagedList)
                    : View("index", itemsAsIPagedList);

        }

     
        public ActionResult Logout()
        {
            Session["UserName"] = null;
            return RedirectToAction("Login", "Home");
        }

        public List<SelectListItem> binddropdown(string action, int val = 0,int GrouId = 0,int userid = 0,int clientid = 0)
        {
            jobDbContext _db = new jobDbContext();

            var res = _db.Database.SqlQuery<SelectListItem>("exec USP_BindDropDown @action , @val , @GrouId, @userid,@clientid",
                    new SqlParameter("@action", action),
                    new SqlParameter("@val", val),
                    new SqlParameter("@GrouId", GrouId == 0 ? (object)DBNull.Value : GrouId),
                    new SqlParameter("@userid", userid),
                    new SqlParameter("@clientid", clientid)
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


        public List<SelectListItem> binddropdownForConcernPersonForTracker(string action, int val = 0, int clientId = 0)
        {
            jobDbContext _db = new jobDbContext();

            var res = _db.Database.SqlQuery<SelectListItem>("exec binddropdownForConcernPersonForTracker @action , @val, @clientId",
                   new SqlParameter("@action", action),
                    new SqlParameter("@val", val),
                    new SqlParameter("@clientId", clientId))
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

        public List<SelectListItem> binddropdownForDegFilter(string action, int val = 0, int? GrouId = 0)
        {
            jobDbContext _db = new jobDbContext();

            if (GrouId == null)
            {  
                GrouId = 0;
            }

            var res = _db.Database.SqlQuery<SelectListItem>("exec USP_BindDropDown @action , @val, @GrouId",
                   new SqlParameter("@action", action),
                    new SqlParameter("@val", val),
                    new SqlParameter("@GrouId", GrouId))
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
        
        public List<SelectListItem> binddropdownForMultipalLocation(string action, int val = 0, int ReqID = 0)
        {
            jobDbContext _db = new jobDbContext();

            var res = _db.Database.SqlQuery<SelectListItem>("exec USP_BindDropDownForAddCandidate @action , @val , @req_id",
                   new SqlParameter("@action", action),
                    new SqlParameter("@val", val),
                    new SqlParameter("@req_id", ReqID))
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


        [ValidateInput(false)]
        public ActionResult AddCandidate()
        {
            Candidate data = new Candidate();
            //int userid;
            //userid = Convert.ToUInt16(Session["User_id"].ToString());
            int userid;
            if (Convert.ToInt16(Session["RoleID"].ToString()) == 7)
            {
                userid = 0;
            }
            else
            {
                userid = Convert.ToUInt16(Session["User_id"].ToString());
            }
            data.candID = 0;
            ViewData["JobList"] = binddropdown("Jobs", 0,0,userid);
            ViewData["DesignationList"] = binddropdown("Designation", 0);
            ViewData["ClientList"] = binddropdown("Client", 0);
            ViewData["LocationList"] = binddropdown("LocationList", 0);
            ViewData["CandidateLocationList"] = binddropdownForMultipalLocation("CandidateLocationList", 0, 0);
            //  ViewData["LocationList"] = binddropdownForMultipalLocation("CandidateLocationList",0);

            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("_AddCandidate", data)
                   : View("_AddCandidate", data);
        }

        [ValidateInput(false)]
        public ActionResult Schedule(int? reqid, int? candID, string CandName, int? StageID, int? NextStageID, string StageName = "")
        {
            Schedule data = new Schedule();
            ViewData["InterviewModeList"] = binddropdown("ModeOfInterview", 0);
            ViewData["EmailTemplateList"] = binddropdown("EmailTemplateList", 0);
            
            if (reqid != null)
            {
                data.StageID = (int)StageID;
                data.CandID = (int)candID;
                data.StageName = StageName;
                data.CandName = CandName;
                data.ReqID = (int)reqid;
                data.NextStageID = (int)NextStageID;
            }
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("_Schedule", data)
                   : View("_Schedule", data);
        }


        [HttpPost]
        public ActionResult AddCandidate(String Name, int req_id, string emailid, String MobileNo, String MobileNo2, String SkypeID, string DomainName, int location_id, string Current_Organization, string Current_Position, int? Current_Location_id, DateTime? working_from_Date, string Qualification, string Qualification_PG, string Total_Exp, string currently_drawn_salary, string expected_salary, string Notice_period, string ModeOfHire, string Skills, string AcadmicDetails, decimal? CostPerMonth, int? PreferedLocation, DateTime? DOB, string AgencyName)
        {
            ViewData["JobList"] = binddropdown("Jobs", 0);
            // ViewData["DesignationList"] = binddropdown("Designation", desig_id);
            //ViewData["ClientList"] = binddropdown("Client", 0);
            jobDbContext _db = new jobDbContext();

            Candidate data = new Candidate();
            data.req_id = req_id;
            data.EmailID = emailid;
            data.Name = Name;
            data.MobileNo = MobileNo;
            data.MobileNo2 = MobileNo2;
            data.SkypeID = SkypeID;
            data.Current_Organization = Current_Organization;
            data.Current_Position = Current_Position;
            data.Current_Location_id = Current_Location_id;
            data.location_id = location_id;
            data.working_from_Date = working_from_Date;
            data.Qualification = Qualification;
            data.Qualification_PG = Qualification_PG;
            data.Total_Exp = Total_Exp;
            data.currently_drawn_salary = currently_drawn_salary;
            data.expected_salary = expected_salary;
            data.Notice_period = Notice_period;
            data.addedby = Convert.ToInt32(Session["User_id"].ToString());
            data.ModeOfHire = ModeOfHire;
            data.Skills = Skills;
            data.AcadmicDetails = AcadmicDetails;
            data.CostPerMonth = CostPerMonth;
            data.PreferedLocation = PreferedLocation;
            data.DOB = DOB;
            data.AgencyName = AgencyName;
            var result = _db.Database.ExecuteSqlCommand(@"exec USP_AddCandidate 
                @Name,
                @req_id,
                @emailid,
                @mobile ,  
                @addedby,
                @MobileNo2,
                @SkypeID,
                @Current_Organization,
		        @Current_Position,
                @location_id,
		        @Current_Location_id,	
		        @working_from_Date,	
		        @Qualification,	
		        @Qualification_PG,	
		        @Total_Exp,	
		        @currently_drawn_salary	,
		        @expected_salary	,
		        @Notice_period,
                @ModeOfHire,
                @Skills,
                @Acadmicdetails,
                @CostPerMonth,
                @PerferedLocation,
                @DOB,
                @AgencyName",
                new SqlParameter("@Name", data.Name),
                new SqlParameter("@req_id", data.req_id),
                new SqlParameter("@emailid", data.EmailID),
                new SqlParameter("@mobile", data.MobileNo),
                new SqlParameter("@addedby", data.addedby == null ? (object)DBNull.Value : data.addedby),
                new SqlParameter("@MobileNo2", data.MobileNo2),
                new SqlParameter("@SkypeID", data.SkypeID),
                new SqlParameter("@Current_Organization", data.Current_Organization == null ? (object)DBNull.Value : data.Current_Organization),
                new SqlParameter("@Current_Position", data.Current_Position),
                new SqlParameter("@location_id", data.location_id == null ? (object)DBNull.Value : data.location_id),
                new SqlParameter("@Current_Location_id", data.Current_Location_id),
                new SqlParameter("@working_from_Date", data.working_from_Date == null ? (object)DBNull.Value : data.working_from_Date),
                new SqlParameter("@Qualification", data.Qualification == null ? (object)DBNull.Value : data.Qualification),
                new SqlParameter("@Qualification_PG", data.Qualification_PG == null ? (object)DBNull.Value : data.Qualification_PG),
                new SqlParameter("@Total_Exp", data.Total_Exp),
                new SqlParameter("@currently_drawn_salary", data.currently_drawn_salary == null ? (object)DBNull.Value : data.currently_drawn_salary),
                new SqlParameter("@expected_salary", data.expected_salary == null ? (object)DBNull.Value : data.expected_salary),
                new SqlParameter("@Notice_period", data.Notice_period == null ? (object)DBNull.Value : data.Notice_period),
                new SqlParameter("@ModeOfHire", data.ModeOfHire == null ? (object)DBNull.Value : data.ModeOfHire),
                new SqlParameter("@Skills", data.Skills == null ? (object)DBNull.Value : data.Skills),
                new SqlParameter("@Acadmicdetails", data.AcadmicDetails == null ? (object)DBNull.Value : data.AcadmicDetails),
                new SqlParameter("@CostPerMonth", data.CostPerMonth == null ? (object)DBNull.Value : data.CostPerMonth),
                new SqlParameter("@PerferedLocation", data.PreferedLocation == null ? (object)DBNull.Value : data.PreferedLocation),
                new SqlParameter("@DOB", data.DOB == null ? (object)DBNull.Value : data.DOB),
                new SqlParameter("@AgencyName", data.AgencyName == null ? (object)DBNull.Value : data.AgencyName)

               );

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult LoadDataForJob(int? page, int? RowCount, String Name , String CName, String DName, int? userid, DateTime? Activitydate)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Convert.ToInt32(Session["RoleID"].ToString()) == 7)
            {
                userid = null;
            }
            else
            {

                userid = Convert.ToInt32(Session["User_id"].ToString());
            }
            if (RowCount != null) { Session["RowCount"] = RowCount; } else { Session["RowCount"] = 7; }

            StaticPagedList<WorkFLowList> itemsAsIPagedList;
            itemsAsIPagedList = GridJobList(page,Convert.ToInt16(Session["RowCount"].ToString()), Name, CName, DName, userid,Activitydate);

            ViewBag.Adate = Activitydate;
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("_JoblistGrid", itemsAsIPagedList)
                   : View(itemsAsIPagedList);
        }
        
        public ActionResult Joblist(int? page, int? RowCount, String Name, String CName, String DName, string param ="")
        {
            int? userid;
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Convert.ToInt32(Session["RoleID"].ToString()) == 7)
            {
                userid = null;
            }
            else
            {

                userid = Convert.ToInt32(Session["User_id"].ToString());
            }

            if (Session["RowCount"] != null) { RowCount = Convert.ToInt16(Session["RowCount"].ToString()); } else { RowCount = null; }

            StaticPagedList<WorkFLowList> itemsAsIPagedList;
            itemsAsIPagedList = GridJobList(page, RowCount, Name, CName, DName, userid,null);

            //this code is for validating param value when mail tracker returning 0 row count
            if(param == "zeroval")
            {
                ViewData["errormsg"] = param;

            }

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("Joblist", itemsAsIPagedList)
                    : View("Joblist", itemsAsIPagedList);
        }


        public StaticPagedList<WorkFLowList> GridJobList(int? page, int? RowCount, String Name, String CName, String DName, int? userid,DateTime? Activitydate)
        {
            jobDbContext _db = new jobDbContext();
            var pageIndex = (page ?? 1);
            int pageSize = (RowCount ?? 7); ;
            int totalCount = 5;
            if (Name == null) Name = "";
            if (CName == null) CName = "";
            if (DName == null) DName = "";
            WorkFLowList clist = new WorkFLowList();

            IEnumerable<WorkFLowList> result = _db.WFList.SqlQuery(@"exec USP_GetWorkFlowStatisstics
              @pPageIndex, @pPageSize,@cName,@dName,@UserId,@Adate",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@cName", CName == null ? (object)DBNull.Value : CName),
               new SqlParameter("@dName", DName == null ? (object)DBNull.Value : DName),
               new SqlParameter("@UserId", userid == null ? (object)DBNull.Value : userid),
               new SqlParameter("@Adate", Activitydate == null ? (object)DBNull.Value : Activitydate)
               ).ToList<WorkFLowList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<WorkFLowList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;
        }
        public ActionResult AddJob()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Requirement req = new Requirement();
            req.ReqID = 1;
            ViewData["DesignationGroupList"] = binddropdown("DesignationGroupList", 0);
            ViewData["ContactPersonList"] = binddropdown("ContactPersons", 0);
            ViewData["DesignationList"] = binddropdownForDegFilter("FilteredDesignation", 0);
            ViewData["ClientList"] = binddropdown("Client", 0);
            ViewData["PositionSectorList"] = binddropdown("PositionSector", 0);
            ViewData["LocationList"] = binddropdown("LocationList", 0);            
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("_AddJob", req)
                    : View("_AddJob", req);
        }

        [HttpPost]
        public ActionResult FllterDesignation(int? GroupId)
        {
            int? temp = GroupId;

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewData["DesignationList"] = binddropdownForDegFilter("FilteredDesignation", 0, temp);

            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("_designationbinddropdown")
                   : View();
        }

        [HttpPost]
        public ActionResult filtercontactperson(int clientid = 0)
        {
           if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewData["ContactPersonList"] = binddropdown("ContactPersons", 0,0,0, clientid);

            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("_dropdowncontacperson")
                   : View();
        }

        [HttpPost]
        public ActionResult AddJob(String ReqTitle, int Position, int desig_id, string Description, String ExpMax, String ExpMin, String SalMin, String SalMax, int Client_ID, int? ContactPerson, string PositionLevel, int? location_id, string MinimumQulification, string RoleResp, string KnowledgeSkill, int? Age, string NoticePeriod,int? SectorId)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int user_id = Convert.ToInt32(Session["User_id"].ToString()); 
            Requirement req = new Requirement();
            req.ReqID = 1;
            ViewData["DesignationList"] = binddropdown("Designation", 0);
            ViewData["ClientList"] = binddropdown("Client", 0);
            ViewData["DesignationGroupList"] = binddropdown("DesignationGroupList", 0);
            jobDbContext _db = new jobDbContext();

            Requirement Req = new Requirement();
            Req.ReqTitle = ReqTitle;
            Req.desig_id = desig_id;
            Req.Description = Description;
            Req.ExpMax = ExpMax;
            Req.ExpMin = ExpMin;
            Req.SalMax = SalMax;
            Req.SalMin = SalMin;
            Req.Client_id = Convert.ToInt32(Client_ID);
            Req.ContactPerson = ContactPerson;
            Req.PositionLevel = PositionLevel;
            Req.location_id = location_id;
            Req.MinimumQulification = MinimumQulification;
            Req.RoleResp = RoleResp;
            Req.KnowledgeSkill = KnowledgeSkill;
            Req.Age = Age;
            Req.NoticePeriod = NoticePeriod;
            Req.SectorId = SectorId;

            var result = _db.Database.ExecuteSqlCommand(@"exec USP_AddRequirement
                @ReqTitle,
                @position,
                @desig_id,
                @Description,
                @ExpMax,
                @ExpMin,
                @SalMin,
                @SalMax,
                @addedby,
                @Client_id,
                @ContactPerson,
                @PositionLevel,
                @location_id ,
                @MinimumQulification ,
                @RoleResp,
                @KnowledgeSkill ,
                @Age,
                @NoticePeriod,
                @SectorId",
                new SqlParameter("@ReqTitle", Req.ReqTitle),
                new SqlParameter("@position", Req.Position),
                new SqlParameter("@desig_id", Req.desig_id),
                new SqlParameter("@Description", Req.Description == null ? (object)DBNull.Value : Req.Description),
                new SqlParameter("@ExpMax", Req.ExpMax),
                new SqlParameter("@ExpMin", Req.ExpMin),
                new SqlParameter("@SalMin", Req.SalMin),
                new SqlParameter("@SalMax", Req.SalMax),
                new SqlParameter("@addedby", user_id),
                new SqlParameter("@Client_id", Req.Client_id),
                new SqlParameter("@ContactPerson", Req.ContactPerson),
                 new SqlParameter("@PositionLevel", Req.PositionLevel),
                  new SqlParameter("@location_id", Req.location_id == null ? (object)DBNull.Value : Req.location_id),
                   new SqlParameter("@MinimumQulification", Req.MinimumQulification),
                    new SqlParameter("@RoleResp", Req.RoleResp),
                     new SqlParameter("@KnowledgeSkill", Req.KnowledgeSkill),
                      new SqlParameter("@Age", Req.Age == null ? (object)DBNull.Value : Req.Age),
                       new SqlParameter("@NoticePeriod", Req.NoticePeriod),
                       new SqlParameter("@SectorId", Req.SectorId)
               );
              return Json("Requirment Added Sucessfully");
            //return Request.IsAjaxRequest()
            //        ? (ActionResult)PartialView("_AddJob", req)
            //        : View("_AddJob", req);
        }

        public ActionResult WorkFlowDetail(DateTime? Adate,int id = 0, string status = "",int clinetid =0,int reqid = 0)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            jobDbContext _db = new jobDbContext();
            WFCountList wf = new WFCountList();
            IEnumerable<WFCountList> result = _db.WFCountList.SqlQuery(@"exec usp_GetWorkflow @ReqID, @stageName,@Adate", 
                new SqlParameter("@ReqID", id),
                new SqlParameter("@stageName", status),
                new SqlParameter("@Adate", Adate == null ? (object)DBNull.Value : Adate)).ToList<WFCountList>();
            Session["clientid"] = clinetid;//client id for interview tracker from feebdbackmail for client consern drop down list
            Session["reqid"] = reqid;//Reqid id for interview tracker from feebdbackmail for client consern drop down list
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("WorkFlowDetail", result)
                    : View("WorkFlowDetail", result);
        }

        [HttpPost]
        public ActionResult Schedule(int StageID, int ModeID, string Remark, int ReqID, int CandID, int? StatusID, String ActivityDate = null, DateTime? ActivityTime = null)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Schedule data = new Schedule();

            jobDbContext _db = new jobDbContext();

            data.ReqID = ReqID;
            data.StageID = StageID;
            data.Remark = Remark;
            data.ModeID = ModeID;
            string d = null;
            if (ActivityDate != null && ActivityDate != "")
            {
                d = ActivityDate.Substring(8, 2) + "/" + ActivityDate.Substring(5, 2) + "/" + ActivityDate.Substring(0, 4);
                if (ActivityDate != null)
                {
                    //data.ActivityDate = DateTime.ParseExact(ActivityDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    data.ActivityDate = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                }
            }
            if (ActivityTime != null)
                data.ActivityTime = (DateTime)ActivityTime;
            data.CandID = CandID;
            if (StatusID == null)
            {
                StatusID = 0;
            }
            var result = _db.Database.ExecuteSqlCommand(@"exec USP_Schedule
                @ReqID,
                @StageID,
                @Remark,
                @ActivityDate,
                @ActivityTime,
                @CandID, 
                @addedby,
                @StatusID,
                @ModeID",
                new SqlParameter("@ReqID", data.ReqID),
                new SqlParameter("@StageID", data.StageID),
                new SqlParameter("@Remark", Remark),
                new SqlParameter("@ActivityDate", data.ActivityDate == null ? (object)DBNull.Value : data.ActivityDate),
                new SqlParameter("@ActivityTime", ActivityTime == null ? (object)DBNull.Value : data.ActivityTime),
                new SqlParameter("@CandID", data.CandID),
                new SqlParameter("@addedby", 1),
                new SqlParameter("@StatusID", StatusID),
                new SqlParameter("@ModeID", ModeID)
               );
            //  ViewData["InterviewModeList"] = binddropdown("ModeOfInterview", 0);
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult SourceCandidate(int StageID, int ModeID, int ReqID, int CandID)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Schedule data = new Schedule();
            jobDbContext _db = new jobDbContext();

            data.ReqID = ReqID;
            data.StageID = StageID;
            data.ModeID = ModeID;

            var result = _db.Database.ExecuteSqlCommand(@"exec USP_Apply
                @ReqID,
                @StageID, 
                @CandID, 
                @addedby,
                @StatusID,
                @ModeID",
                new SqlParameter("@ReqID", data.ReqID),
                new SqlParameter("@StageID", data.StageID),
                new SqlParameter("@CandID", CandID),
                new SqlParameter("@addedby", 1),
                new SqlParameter("@StatusID", 1),
                new SqlParameter("@ModeID", ModeID)
               );

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [Route("Placement/EditSchedule/{CanID?}/{ReqID?}")]
        public ActionResult EditSchedule(int CanID, int ReqID)
        {
            Schedule S = new Schedule();
            jobDbContext _db = new jobDbContext();
            var result = _db.Schedule.SqlQuery(@"exec uspScheduleDetails 
                @CanID,
                @ReqID",
                new SqlParameter("@CanID", CanID),
                new SqlParameter("@ReqID", ReqID)).ToList<Schedule>();

            S = result.FirstOrDefault();
            ViewData["InterviewModeList"] = binddropdown("ModeOfInterview", S.ModeID);
            ViewData["EmailTemplateList"] = binddropdown("EmailTemplateList", 0);
            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("_EditSchedule", S)
                : View("_EditSchedule",S);
        }
        [HttpPost]
        public ActionResult ReSchedule(int StageID, string Remark, int ReqID, int CandID, String ActivityDate = null, DateTime? ActivityTime = null, int ScheduleID = 0, string FeedbackRemark = "", int ModeID = 0, int Statusid = 0)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewData["InterviewModeList"] = binddropdown("ModeOfInterview", 0);
            Schedule data = new Schedule();
            jobDbContext _db = new jobDbContext();
            string d = null;
            if (ActivityDate != null && ActivityDate != "")
            {
                d = ActivityDate.Substring(8, 2) + "/" + ActivityDate.Substring(5, 2) + "/" + ActivityDate.Substring(0, 4);
                if (ActivityDate != null)
                {
                    data.ActivityDate = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            }
            if (ActivityTime != null)
                data.ActivityTime = (DateTime)ActivityTime;

            data.ReqID = ReqID;
            data.StageID = StageID;
            data.Remark = Remark;
            data.ModeID = ModeID;
            if (ActivityTime != null)
                data.ActivityTime = (DateTime)ActivityTime;
            data.CandID = CandID;

            var result = _db.Database.ExecuteSqlCommand(@"exec USP_ReSchedule
                @ReqID,
                @StageID,
                @Remark,
                @FeedbackRemark,
                @ActivityDate,
                @ActivityTime,
                @CandID, 
                @addedby,
                @ScheduleID,
                @ModeID,
                @Statusid                
                ",
                new SqlParameter("@ReqID", data.ReqID),
                new SqlParameter("@StageID", data.StageID),
                new SqlParameter("@Remark", Remark),
                new SqlParameter("@FeedbackRemark", FeedbackRemark),
                new SqlParameter("@ActivityDate", ActivityDate == null || ActivityDate == "" ? (object)DBNull.Value : data.ActivityDate),
                new SqlParameter("@ActivityTime", ActivityTime == null ? (object)DBNull.Value : data.ActivityTime),
                new SqlParameter("@CandID", data.CandID),
                new SqlParameter("@addedby", 1),
                new SqlParameter("@ScheduleID", ScheduleID),
                new SqlParameter("@ModeID", data.ModeID),
                new SqlParameter("@Statusid", Statusid)
               );
            return Json(data, JsonRequestBehavior.AllowGet);
            //return Request.IsAjaxRequest()
            //        ? (ActionResult)PartialView("_EditSchedule", data)
            //        : View("_EditSchedule", data);
        }
        [Route("Placement/EditCandidate/{candID?}")]
        public ActionResult EditCandidate(int candID)
        {
            ViewData["EmailTemplateList"] = binddropdown("EmailTemplateList", 0);
            int userid;
            if (Convert.ToInt16(Session["RoleID"].ToString()) == 7)
            {
                userid = 0;
            }
            else
            {
                userid = Convert.ToUInt16(Session["User_id"].ToString());
            }
                
            ViewData["JobList"] = binddropdown("Jobs", 0, 0, userid);
            ViewData["DesignationList"] = binddropdown("Designation", 0);
            ViewData["ClientList"] = binddropdown("Client", 0);
            ViewData["LocationList"] = binddropdown("LocationList", 0);
          //  ViewData["CandidateLocationList"] = binddropdownForMultipalLocation("CandidateLocationList", 0, 0);
            FetchcandidatwForCandidate data = new FetchcandidatwForCandidate();

            jobDbContext _db = new jobDbContext();
            var result = _db.FetchcandidatwForCandidate.SqlQuery(@"exec [usp_CandidateDetailsForUpdate] 
                @CanID",
                new SqlParameter("@CanID", candID)).ToList<FetchcandidatwForCandidate>();

            data = result.FirstOrDefault();

            IEnumerable<Notes> noteslist = _db.Notes.SqlQuery(@"exec usp_GetNotes @candID",
                new SqlParameter("@candID", data.candID)).ToList<Notes>();
            data.Notes = noteslist;
            mail m = new mail();
            m.mailID = Convert.ToInt16(data.candID);
            m.mailDescription = "aknas";
            data.mail = m;
            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("_EditCandidate", data)
                : View("_EditCandidate", data);
        }

        [HttpPost]
        public ActionResult EditCandidate(FetchcandidatwForCandidate data,int? ReqID)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
    
            jobDbContext _db = new jobDbContext();
          ;
            var result = _db.Database.ExecuteSqlCommand(@"exec USP_EditCandidate
                 @candID ,
                 @Name, 
                 @ReqID ,
                 @CurrentReqId,
                 @mobile ,
                 @emailid ,
                 @MobileNo2 ,
                 @SkypeID ,
                 @Current_Organization ,
                 @Current_Position ,
                 @location_id ,
                 @Current_Location_id ,
                 @working_from_Date, 
                 @Qualification ,
                 @Qualification_PG ,
                 @Total_Exp ,
                 @currently_drawn_salary ,
                 @expected_salary ,
                 @Notice_period ",
                new SqlParameter("@candID", data.candID),
                new SqlParameter("@Name",data.Name),
                new SqlParameter("@ReqID",ReqID),
                new SqlParameter("@CurrentReqId", data.CurrentReqId),
                new SqlParameter("@mobile",data.mobile == null ? (object)DBNull.Value : data.mobile),
                new SqlParameter("@emailid", data.emailid == null ? (object)DBNull.Value : data.emailid),
                new SqlParameter("@MobileNo2", data.MobileNo2 == null ? (object)DBNull.Value : data.MobileNo2),
                new SqlParameter("@SkypeID", data.SkypeID == null ? (object)DBNull.Value : data.SkypeID),
                new SqlParameter("@Current_Organization", data.Current_Organization == null ? (object)DBNull.Value : data.Current_Organization),
                new SqlParameter("@Current_Position", data.Current_Position == null ? (object)DBNull.Value : data.Current_Position),
                 new SqlParameter("@location_id", data.location_id),
                  new SqlParameter("@Current_Location_id", data.Current_Location_id),
                   new SqlParameter("@working_from_Date", data.working_from_Date == null ? (object)DBNull.Value : data.working_from_Date),
                    new SqlParameter("@Qualification", data.Qualification == null ? (object)DBNull.Value : data.Qualification),
                       new SqlParameter("@Qualification_PG", data.Qualification_PG == null ? (object)DBNull.Value : data.Qualification_PG),
                       new SqlParameter("@Total_Exp", data.Total_Exp == null ? (object)DBNull.Value : data.Total_Exp),
                        new SqlParameter("@currently_drawn_salary", data.currently_drawn_salary == null ? (object)DBNull.Value : data.currently_drawn_salary),
                        new SqlParameter("@expected_salary", data.expected_salary == null ? (object)DBNull.Value : data.expected_salary),
                        new SqlParameter("@Notice_period", data.Notice_period == null ? (object)DBNull.Value : data.Notice_period)

               );

            IEnumerable<Notes> noteslist = _db.Notes.SqlQuery(@"exec usp_GetNotes @candID",
            new SqlParameter("@candID", data.candID)).ToList<Notes>();
            data.Notes = noteslist;
            mail m = new mail();
            m.mailID = Convert.ToInt16(data.candID);
            data.mail = m;

            return Json(data, JsonRequestBehavior.AllowGet);
            //return Request.IsAjaxRequest()
            //        ? (ActionResult)PartialView("_EditCandidate", data)
            //        : View("_EditCandidate", data);
        }
        [HttpPost]
        public ActionResult AddNotes(int candID, string Description, int sendby, int ReplyTo)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Notes data = new Notes();

            jobDbContext _db = new jobDbContext();
            data.candID = candID;
            data.Description = Description;
            data.sendby = sendby;
            data.ReplyTo = ReplyTo;

            var result = _db.Database.ExecuteSqlCommand(@"exec USP_AddNotes
                @candID,
                @Description,
                @sendby,
                @ReplyTo                               
                ",
                new SqlParameter("@candID", data.candID),
                new SqlParameter("@Description", data.Description),
                new SqlParameter("@sendby", data.sendby),
                new SqlParameter("@ReplyTo", 1)
                              );
            CandidateDetails Cdata = new CandidateDetails();
            var result1 = _db.CandidateDetails.SqlQuery(@"exec usp_CandidateDetails 
                @CanID",
                new SqlParameter("@CanID", candID)).ToList<CandidateDetails>();

            Cdata = result1.FirstOrDefault();

            IEnumerable<Notes> noteslist = _db.Notes.SqlQuery(@"exec usp_GetNotes @candID",
             new SqlParameter("@candID", data.candID)).ToList<Notes>();
            Cdata.Notes = noteslist;

            ViewData["EmailTemplateList"] = binddropdown("EmailTemplateList", 0);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("_EditCandidate", Cdata)
                    : View("_EditCandidate", Cdata);
        }

        //public ActionResult Sendmail(int candID, string Description, int sendby, int ReplyTo, string mailto)
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sendmail(int candID, string Description, int? msgTemplate)
        {
            try
            {

           
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Notes data = new Notes();

            jobDbContext _db = new jobDbContext();
            data.candID = candID;
            Description = Description.Replace("\\n"," ");
            Description = Description.Replace("\\t"," ");
          
            CandidateDetails Cdata = new CandidateDetails();
            var result1 = _db.CandidateDetails.SqlQuery(@"exec usp_CandidateDetails 
                @CanID",
                new SqlParameter("@CanID", candID)).ToList<CandidateDetails>();

            Cdata = result1.FirstOrDefault();
            Notes n = new Notes();
            IEnumerable<Notes> noteslist = _db.Notes.SqlQuery(@"exec usp_GetNotes @candID",
            new SqlParameter("@candID", data.candID)).ToList<Notes>();
            try
            {
                Cdata.Notes = noteslist;

            }
            catch { }

            String body = Description;// PopulateBody("sunil", "Title", "", "hello");

            if (msgTemplate == 5)
            {
                SendHtmlFormattedEmail(Cdata.emailid, result1[0].Designation, body);
            }
            else if (msgTemplate == 4)
            {
                string temp = "You are invited for Second Round Interview at " + result1[0].Client_Name;
                SendHtmlFormattedEmail(Cdata.emailid, temp, body);
            }
            else if (msgTemplate == 3)
            {
                string temp = "You are Offered " + result1[0].Client_Name;
                SendHtmlFormattedEmail(Cdata.emailid, temp, body);
            }

            else if (msgTemplate == 2)
            {
                string temp = "You are invited for Final Round Interview at " + result1[0].Client_Name;
                SendHtmlFormattedEmail(Cdata.emailid, temp, body);
            }
            else if (msgTemplate == 1)
            {
                string temp = "CONGRATULATION!!!!! Your Profile Has been shortlisted !!!!!! ";
                SendHtmlFormattedEmail(Cdata.emailid, temp, body);
            }
            else if (msgTemplate == null)
            {
                string temp = " ";
                SendHtmlFormattedEmail(Cdata.emailid, temp, body);
            }

            else
            {

                MailTemplateForUpdate data1 = new MailTemplateForUpdate();

                jobDbContext _db1 = new jobDbContext();
                var result = _db1.MailTemplateForUpdate.SqlQuery(@"exec usp_MailTemplateDetailsForUpdate
                     @temp_id",
                 new SqlParameter("@temp_id", msgTemplate)).ToList<MailTemplateForUpdate>();

                data1 = result.FirstOrDefault();

                string temp = data1.email_subject;
                SendHtmlFormattedEmail(Cdata.emailid, temp, body);
            }

            return Json("mail Send Sucessfully");
            }
            catch(Exception ex)
            {
                var mgs = ex.Message;
                return Json(mgs);
            }
            //ViewData["EmailTemplateList"] = binddropdown("EmailTemplateList", 0);
            //return Request.IsAjaxRequest()
            //        ? (ActionResult)PartialView("_EditCandidate", Cdata)
            //        : View("_EditCandidate", Cdata);
        }

        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                body = body.Replace('"',' ');
                mailMessage.From = new MailAddress("ehiring7@gmail.com");
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(recepientEmail));
                SmtpClient smtp = new SmtpClient();
                // smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = "ehiring7@gmail.com";
                NetworkCred.Password = "harishmn1";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
        }

        private string PopulateBody(string userName, string title, string url, string description)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/Html/callLetter.html")))
            {
                body = reader.ReadToEnd();
            }
            SendHtmlFormattedEmail("ehiring7@gmail.com", "Interview Call Letter", body);
            
            return body;
        }

        public ActionResult GetTemplateData(int candID, int? templateid = 0, String ActivityDate = "", String ActivityTime = "", int ModeOfInterview = 0)
        {
            mail m = new mail();
            m.mailID = 1;
            string body = string.Empty;
            if (templateid > 0)
            {
                 if (templateid == 1)
                {
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/Html/CandidateShortlisted.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                }

                else if (templateid == 2)
                {
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/Html/FinalRoundInteview.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                }

                else if (templateid == 3)
                {
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/Html/Offerd.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                }

                else if (templateid == 4)
                {
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/Html/SecondRounInterview.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                }

                else if (templateid == 5)
                {
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/Html/CandidateRejected.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                }
                else if (templateid == 8)
                {
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/Html/tracker.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                }
                 else
                {
                    MailTemplateForUpdate data = new MailTemplateForUpdate();

                    jobDbContext _db1 = new jobDbContext();
                    var result = _db1.MailTemplateForUpdate.SqlQuery(@"exec usp_MailTemplateDetailsForUpdate
                     @temp_id",
                     new SqlParameter("@temp_id", templateid)).ToList<MailTemplateForUpdate>();

                    data = result.FirstOrDefault();
                    body = data.emailBody;
                }
                jobDbContext _db = new jobDbContext();
                CandidateDetails Cdata = new CandidateDetails();
                var result1 = _db.CandidateDetails.SqlQuery(@"exec usp_CandidateDetails 
                @CanID,@ModeId", new SqlParameter("@CanID", candID),
                new SqlParameter("@ModeId", ModeOfInterview)
                ).ToList<CandidateDetails>();

                Cdata = result1.FirstOrDefault();
                if (Cdata != null)
                {
                    body = body.Replace("$$CandidateName$$", Cdata.Name);
                    body = body.Replace("$$Position$$", Cdata.Designation);
                    body = body.Replace("$$Phone$$", Cdata.mobile);
                
                
                if (ActivityDate == "")
                {
                    body = body.Replace("$$InterViewDate$$", Cdata.ActivityDate);
                }
                else
                {
                    body = body.Replace("$$InterViewDate$$", ActivityDate);
                }
                if (ActivityTime == "")
                {
                    body = body.Replace("$$InterViewTime$$", Cdata.ActivityTime);
                }
                else
                {
                    body = body.Replace("$$InterViewTime$$", ActivityTime);
                }
                
                    body = body.Replace("$$InterViewTime$$", ActivityTime);
                    body = body.Replace("$$ClientName$$", Cdata.Client_Name);
                    body = body.Replace("$$ContactPerson$$", Cdata.contactPerson);
                    body = body.Replace("$$ModeOfInterview$$", Cdata.ModeName);
                    body = body.Replace("$$Location$$", Cdata.Location);
                    body = body.Replace("$$address$$", Cdata.ClientAdress);
                    body = body.Replace("$$Website$$", Cdata.Website);
                    body = body.Replace("$$description$$", Cdata.description);
                }
               


            }
            m.mailDescription = body;
            return Request.IsAjaxRequest()
               ? (ActionResult)PartialView("mailTemplatedata", m)
               : View("mailTemplatedata", m);
        }
        public ActionResult SourcedCandidate(int candID)
        {
            jobDbContext _db = new jobDbContext();
            var result = _db.SourceCandidate.SqlQuery(@"exec usp_JobSource
                @CandID",
                new SqlParameter("@CandID", candID)).ToList<SourceCandidate>();
            IEnumerable<SourceCandidate> data = result;

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("_SourceCandidate", data)
                : View("_SourceCandidate", data);
        }
        [ValidateInput(false)]
        public ActionResult ScheduleActivity(int? reqid, int? candID, string CandName, int? StageID, int? ActivityID, string ActivityName = "", string CandidateName = "")
        {
            ActivitySchedule data = new ActivitySchedule();
            if (reqid != null)
            {
                data.StageID = (int)StageID;
                data.CandID = (int)candID;
                data.CandName = CandidateName;
                data.StageID = (int)StageID;
                data.ActivityID = (int)ActivityID;
                data.ReqID = (int)reqid;
                data.ActivityName = ActivityName;
                // data.CandName = CandName;
            }

            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("_ScheduleActivity", data)
                   : View("_ScheduleActivity", data);
        }
        [HttpPost]
        public ActionResult ScheduleActivity(int ActivityID, int StageID, string Remark, int ReqID, int CandID, String ActivityDate = null, DateTime? ActivityTime = null)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ActivitySchedule data = new ActivitySchedule();
            jobDbContext _db = new jobDbContext();
            string d = null;
            if (ActivityDate != null && ActivityDate.Trim() != "")
            {
                d = ActivityDate.Substring(8, 2) + "/" + ActivityDate.Substring(5, 2) + "/" + ActivityDate.Substring(0, 4);
                if (ActivityDate != null)
                {
                    data.ActivityDate = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            }

            if (ActivityTime != null)
                data.ActivityTime = (DateTime)ActivityTime;

            data.ReqID = ReqID;
            data.StageID = StageID;
            data.ActivityID = ActivityID;
            if (ActivityTime != null)
                data.ActivityTime = (DateTime)ActivityTime;
            data.CandID = CandID;

            var result = _db.Database.ExecuteSqlCommand(@"exec USP_ActivitySchedule
               
                @Activityid,
                @reqid,
                @stageid,
                @Remark,
                @ActivityDate,
                @ActivityTime,
                @CandID, 
                @addedby                
                ",
                new SqlParameter("@Activityid", data.ActivityID),
                new SqlParameter("@reqid", data.ReqID),
                new SqlParameter("@stageid", data.StageID),
                new SqlParameter("@Remark", Remark),
                new SqlParameter("@activitydate", ActivityDate == null || ActivityDate == "" ? (object)DBNull.Value : data.ActivityDate),
                new SqlParameter("@activitytime", ActivityTime == null ? (object)DBNull.Value : data.ActivityTime),
                new SqlParameter("@candid", data.CandID),
                new SqlParameter("@addedby", 1)
               );
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("ScheduleActivity", data)
                    : View("ScheduleActivity", data);
        }

        public ActionResult ActivityDetails(int candID)
        {
            ActivityDetails data = new ActivityDetails();
            jobDbContext _db = new jobDbContext();
            var result = _db.ActivityDetails.SqlQuery(@"exec GetCandidateActivity 
                @CanID",
                new SqlParameter("@CanID", candID)).ToList<ActivityDetails>();

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("ActivityDetails", result)
                : View("ActivityDetails", data);
        }

        public ActionResult UploadJD(int ReqID)
        {
            ModelUploadJD u = new ModelUploadJD();
            u.ReqID = ReqID;
            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("UploadJD", u)
                : View("UploadJD", u);
        }


        [HttpPost]
        public object UploadJD(HttpPostedFileBase file, ModelUploadJD data, int? page, String Name, String CName = " ", String DName = " ")
        {
            string fName = "";
            try
            {
                fName = file.FileName;

                if (file != null && file.ContentLength > 0)
                {
                    string Fpath = "uploads/documents/J" + data.ReqID + "/";
                    var originalDirectory = new DirectoryInfo(string.Format("{0}uploads\\documents\\J" + data.ReqID, Server.MapPath(@"\")));
                    string pathString = System.IO.Path.Combine(originalDirectory.ToString());
                    var fileName1 = Path.GetFileName(fName);
                    bool isExists = System.IO.Directory.Exists(pathString);
                    if (!isExists) { System.IO.Directory.CreateDirectory(pathString); }

                    string extension = Path.GetExtension(file.FileName);

                    string FileSaveName = ("J" + data.ReqID + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + extension).ToString();
                    var path = string.Format("{0}\\{1}", pathString, FileSaveName);
                    file.SaveAs(path);

                    string ext = System.IO.Path.GetExtension(file.FileName);
                    data.FileName = FileSaveName;
                    data.ext = ext;
                    data.FolderPath = Fpath;
                    jobDbContext _db = new jobDbContext();
                    var result = _db.Database.ExecuteSqlCommand(@"exec USP_UploadDocument 
                        @RecID,
                        @FolderPath,
                        @FileName,
                        @ext,
                        @uploadby   
                        ",
                       new SqlParameter("@RecID", data.ReqID),
                       new SqlParameter("@FolderPath", data.FolderPath),
                       new SqlParameter("@FileName", data.FileName),
                       new SqlParameter("@ext", data.ext),
                       new SqlParameter("@uploadby", 1)
                     );
                }
            }
            catch (Exception)
            {

            }
            int? userid, RowCount = 7;
            if (Convert.ToInt16(Session["RoleID"].ToString()) == 7)
            {
                userid = 0;              
            }
            else
            {
                userid = Convert.ToInt16(Session["User_id"].ToString());
            }
           
            StaticPagedList<WorkFLowList> itemsAsIPagedList;
            itemsAsIPagedList = GridJobList(page, RowCount, Name, CName, DName, userid,null);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("Joblist", itemsAsIPagedList)
                    : View("Joblist", itemsAsIPagedList);
            
        }
        public ActionResult ViewJD(int ReqID)
        {
            jobDbContext _db = new jobDbContext();
            var result = _db.ModelViewJD.SqlQuery(@"exec usp_JDList
                @ReqID",
                new SqlParameter("@ReqID", ReqID)).ToList<ModelViewJD>();
            IEnumerable<ModelViewJD> data = result;

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("ViewJD", data)
                : View("ViewJD", data);
        }

        //  ================================== Edit job position ===========================================

        public ActionResult _EditJob(int ReqID)
        {
            Requirement req = new Requirement();
            req.ReqID = 1;
            ViewData["DesignationList"] = binddropdown("Designation", 0);
            ViewData["ClientList"] = binddropdown("Client", 0);
            ViewData["LocationList"] = binddropdown("LocationList", 0);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("_EditJob", req)
                    : View("_EditJob", req);
        }

        //  ================================== Fetch Job Position For Update Code ===========================================

        public ActionResult FetchJobPosition(int ReqID)
        {

            try
            {
                RequirementDetails std = new RequirementDetails();

                jobDbContext _db = new jobDbContext();
                var result = _db.RJList.SqlQuery(@"exec usp_RequirmentDetails 
                @req_id",
                 new SqlParameter("@req_id", ReqID)).ToList<RequirementDetails>();

                std = result.FirstOrDefault();
                ViewData["DesignationGroupList"] = binddropdown("DesignationGroupList", 0);
                ViewData["ContactPersonList"] = binddropdown("ContactPersons", 0);
                ViewData["DesignationList"] = binddropdownForDegFilter("FilteredDesignation", 0);
                ViewData["ClientList"] = binddropdown("Client", 0);
                ViewData["PositionSectorList"] = binddropdown("PositionSector", 0);
                ViewData["LocationList"] = binddropdown("LocationList", 0);
                ViewBag.multilocation = std.Location_details;
                return Request.IsAjaxRequest()
                        ? (ActionResult)PartialView("_EditJob", std)
                        : View("_EditJob", std);
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return RedirectToAction("_EditJob");

            }
            finally
            {

            }

        }
        
        //================================== Update job posion Code ===========================================

        [HttpPost]
        public ActionResult UpdateJobPostion(RequirementDetails rs)
        {
            try
            {
                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_UpdateRequirement 
                @req_id,@ReqTitle, @position,@desig_id,@Description, @expMax,@expMin,@salMin,@salMax,@Client_id,@ContactPerson,@PositionLevel,@location_id,@MinimumQulification,@RoleResp,@KnowledgeSkill,@Age,@NoticePeriod,@SectorId ",
                new SqlParameter("@req_id", rs.req_id),
                new SqlParameter("@ReqTitle", rs.req_title),
                new SqlParameter("@position", rs.position),
                new SqlParameter("@desig_id", rs.desig_id),
                new SqlParameter("@Description", rs.Description),
                new SqlParameter("@expMax", rs.exp_Max),
                new SqlParameter("@expMin", rs.exp_Min),
                new SqlParameter("@salMin", rs.sal_Min),
                new SqlParameter("@salMax", rs.sal_Max),
                new SqlParameter("@Client_id", rs.client_id),
                new SqlParameter("@ContactPerson", rs.contactPerson),
                new SqlParameter("@PositionLevel", rs.PositionLevel == null ? (object)DBNull.Value : rs.PositionLevel),
                new SqlParameter("@location_id", rs.location_id),
                new SqlParameter("@MinimumQulification", rs.MinimumQulification == null ? (object)DBNull.Value : rs.MinimumQulification),
                new SqlParameter("@RoleResp", rs.RoleResp),
                new SqlParameter("@KnowledgeSkill", rs.KnowledgeSkill),
                new SqlParameter("@Age", rs.Age == null ? (object)DBNull.Value : rs.Age),
                new SqlParameter("@NoticePeriod", rs.NoticePeriod == null ? (object)DBNull.Value : rs.NoticePeriod),
                new SqlParameter("@SectorId", rs.SectorId == null ? (object)DBNull.Value : rs.SectorId)

                 );

                return Json("IndexForUser");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);

            }
            finally
            {

            }
        }

        public JsonResult GetDesignationList()
        {
            jobDbContext _db = new jobDbContext();
            var lstItem = binddropdown("Designation", 0).Select(i => new { i.Value, i.Text }).ToList();
            //_spService.BindDropdown("PricingUser", "", "").Select(i => new { i.Value, i.Text }).ToList();
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRequirmentList()
        {
            jobDbContext _db = new jobDbContext();
            int userid;
            userid = Convert.ToUInt16(Session["User_id"].ToString());
            var lstItem = binddropdown("Jobs",0,0,userid).Select(i => new { i.Value, i.Text }).ToList();
            //_spService.BindDropdown("PricingUser", "", "").Select(i => new { i.Value, i.Text }).ToList();
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetLocationList()
        {
            jobDbContext _db = new jobDbContext();
            var lstItem = binddropdown("LocationList", 0).Select(i => new { i.Value, i.Text }).ToList();
            //_spService.BindDropdown("PricingUser", "", "").Select(i => new { i.Value, i.Text }).ToList();
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetcurrentLocationList()
        {
            jobDbContext _db = new jobDbContext();
            var lstItem = binddropdown("LocationList", 0).Select(i => new { i.Value, i.Text }).ToList();
            //_spService.BindDropdown("PricingUser", "", "").Select(i => new { i.Value, i.Text }).ToList();
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClientList()
        {
            jobDbContext _db = new jobDbContext();
            var lstItem = binddropdown("Client", 0).Select(i => new { i.Value, i.Text }).ToList();
            //_spService.BindDropdown("PricingUser", "", "").Select(i => new { i.Value, i.Text }).ToList();
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContactPersonsList(int clientid = 0)
        {
            jobDbContext _db = new jobDbContext();
            var lstItem = binddropdown("ContactPersons",0,0,0,clientid).Select(i => new { i.Value, i.Text }).ToList();
            //_spService.BindDropdown("PricingUser", "", "").Select(i => new { i.Value, i.Text }).ToList();
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddMultipalLocation()
        {
            return View();
        }

        //public JsonResult GetLocationforaddcandiateList(int ReqID)
        //{
        //    jobDbContext _db = new jobDbContext();
        //    Requirement rs = new Requirement();

        //    var lstItem = binddropdownForMultipalLocation(Convert.ToInt32(ReqID), "CandidateLocationList", 0).Select(i => new { i.Value, i.Text }).ToList();
        //    //_spService.BindDropdown("PricingUser", "", "").Select(i => new { i.Value, i.Text }).ToList();
        //    return Json(lstItem, JsonRequestBehavior.AllowGet);
        //}

        //================================== Insert Multipal Location ===========================================

        [HttpPost]
        public ActionResult add_MultipalLocation(Requirement[] Requirement)
        {

            try
            {
                var result = 0;
                jobDbContext _db = new jobDbContext();
                foreach (var item in Requirement)
                {

                    Requirement O = new Requirement();

                    O.location_id = item.location_id;

                    result = _db.Database.ExecuteSqlCommand(@"exec USP_AddMultipalLocation 
                    @location_id",

               new SqlParameter("@location_id", O.location_id));

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

        //================================== Update Multipal Location ===========================================

        [HttpPost]
        public ActionResult Update_MultipalLocation(Requirement[] Requirement)
        {

            try
            {
                var result = 0;
                jobDbContext _db = new jobDbContext();
                foreach (var item in Requirement)
                {

                    Requirement O = new Requirement();

                    O.location_id = item.location_id;
                    O.ReqID = item.ReqID;

                    result = _db.Database.ExecuteSqlCommand(@"exec USP_UpdateMultipalLocation @req_id, @location_id",
                    new SqlParameter("@req_id", O.ReqID),
                    new SqlParameter("@location_id", O.location_id));

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

        //================================== delete Multipal Location ===========================================

        [HandleError]
        [HttpPost]
        public ActionResult DeleteMultipalLocation(int? Reqid,int? location_id)
        {

            jobDbContext _db = new jobDbContext();
            var result = _db.Database.ExecuteSqlCommand(@"exec USP_DeleteLocationForPosition @req_id,@location_id",
                 new SqlParameter("@req_id",Reqid),
                new SqlParameter("@location_id",location_id));
            return Json("Deleted sucessfully");
        }

        //=============================================== File Upload Code ==================================================

        public ActionResult FileUploadii(int? CandID, string Candname1 = "")
        {
            ModelUploadResume data = new ModelUploadResume();
            data.candID = CandID;
            data.Candname = Candname1;
            return View("FileUploadii", data);
        }

        [HttpPost]
        public ActionResult FileUploadii(HttpPostedFileBase file, ModelUploadResume data)
        {


            int? page = 1;
            String Name = null;
            String DName = null;
            String CName = null;
            String Eid = null;
            String Mob = null;
            String Stage = null;
            string Activity = null;
            DateTime? Date = null;
            string fName = "";

            try
            {
                fName = file.FileName;

                if (file != null && file.ContentLength > 0)
                {
                    string Fpath = "UploadResume/Documents/" + data.candID + "/";
                    var originalDirectory = new DirectoryInfo(string.Format("{0}UploadResume\\Documents\\" + data.candID, Server.MapPath(@"\")));
                    string pathString = System.IO.Path.Combine(originalDirectory.ToString());
                    var fileName1 = Path.GetFileName(fName);
                    bool isExists = System.IO.Directory.Exists(pathString);
                    if (!isExists) { System.IO.Directory.CreateDirectory(pathString); }

                    string extension = Path.GetExtension(file.FileName);

                    //string FileSaveName = ("J" + data.candID + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + extension).ToString();
                    string FileSaveName = (data.Candname + "_" + "Resume" + extension).ToString();
                    var path = string.Format("{0}\\{1}", pathString, FileSaveName);
                    file.SaveAs(path);

                    string ext = System.IO.Path.GetExtension(file.FileName);
                    data.FileName = FileSaveName;
                    data.ext = ext;
                    data.FolderPath = Fpath;
                    jobDbContext _db = new jobDbContext();
                    var result = _db.Database.ExecuteSqlCommand(@"exec USP_UploadResume 
                    @candID,
                    @FolderPath,
                    @FileName,
                    @ext,
                    @uploadby 
                    ",
                    new SqlParameter("@candID", data.candID),
                    new SqlParameter("@FolderPath", data.FolderPath),
                    new SqlParameter("@FileName", data.FileName),
                    new SqlParameter("@ext", data.ext),
                    new SqlParameter("@uploadby", 1)
                    );
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
            }

            StaticPagedList<CandidateList> itemsAsIPagedList;
            int? PageRowNumber = 8;
            itemsAsIPagedList = GridList(page,PageRowNumber, Name, DName, CName, Eid, Mob, Stage, Activity,Date,null);
            ViewData["UserList"] = binddropdown("UserList", 0);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("index", itemsAsIPagedList)
                    : View("index", itemsAsIPagedList);
        }


        public ActionResult TrackerList(int? ReqID)
        {

            StaticPagedList<TrackerexpotedInfo> itemsAsIPagedList;
            itemsAsIPagedList = TrackerListDetails(ReqID);
            TempData["Data"] = itemsAsIPagedList;
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("TrackerList", itemsAsIPagedList)
                    : View("TrackerList", itemsAsIPagedList);
        }

        //===================================== Function For Exporting excel sheet =======================================

        [HttpGet]
        [ActionName("Download")]
        public void Download(int? ReqID)
        {
            StaticPagedList<TrackerexpotedInfo> emps = TempData["Data"] as StaticPagedList<TrackerexpotedInfo>;
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = emps;
            grid.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            string filePath = Server.MapPath("~/TrackerClientXLSheet/" + ReqID + "/generated/");

            bool isExists = System.IO.Directory.Exists(filePath);
            if (!isExists) { System.IO.Directory.CreateDirectory(filePath); }

            string fileName = "Tracker" + ".xls";
            // Write the rendered content to a file.
            string renderedGridView = sw.ToString();
            System.IO.File.WriteAllText(filePath + fileName, renderedGridView);

        }

        public StaticPagedList<TrackerexpotedInfo> TrackerListDetails(int? req_ID)
        {
            jobDbContext _db = new jobDbContext();
            var pageIndex = 1;
            const int pageSize = 10;
            int totalCount = 10;
            TrackerexpotedInfo clist = new TrackerexpotedInfo();

            IEnumerable<TrackerexpotedInfo> result = _db.TrackerexpotedInfo.SqlQuery(@"exec usp_GetTrackerExportedInfo
                   @req_ID",
               new SqlParameter("@req_ID", req_ID)

               ).ToList<TrackerexpotedInfo>();

            totalCount = 0;

            var itemsAsIPagedList = new StaticPagedList<TrackerexpotedInfo>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }

        //========================================== Mail Tracker Index ==================================================

        public ActionResult TrackerMail(int? ReqID, int? ClientID)
        {
            jobDbContext _db = new jobDbContext();
            StaticPagedList<TrackerexpotedInfo> itemsAsIPagedList;
            itemsAsIPagedList = TrackerListDetails(ReqID);
            TempData["Data"] = itemsAsIPagedList;

            Download(ReqID);
           
            var result = _db.ModelViewJD.SqlQuery(@"exec usp_TrackerCandidateResumeList  @ReqID",
                new SqlParameter("@ReqID", ReqID)).ToList<ModelViewJD>();
           IEnumerable<ModelViewJD> data = result;
                                                                  
                Session["clientid"] = ClientID;
                return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("TrackerMail", data)
                    : View(data);                   
        }


        //========================================== Bind Concern Person for dropdown for Cc ==================================================

        public ActionResult BindConcernForMail(int clientId = 0)
        {
            ConcernPerForTracker data = new ConcernPerForTracker();
            jobDbContext _db = new jobDbContext();
            ViewData["contactPerson_NameList"] = binddropdownForConcernPersonForTracker("ConsernPersonForMail", 0, clientId);


            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("_ConcernPersonForTracker", data)
                : View(data);
        }


        //========================================== Bind Concern Person For sending mail to dropdown ==================================================

        public ActionResult BindEmailList()
        {
            FromMail data = new FromMail();
            //   jobDbContext _db = new jobDbContext();
            ViewData["EmailList"] = binddropdown("EmailList", 0);


            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("_Frommaildropdown", data)
                : View(data);
        }


        //========================================== Bind Concern Person For sending mail to dropdown ==================================================

        public ActionResult BindConcernForMail1(int clientId = 0)
        {
            ConcernPerForTracker data = new ConcernPerForTracker();
            jobDbContext _db = new jobDbContext();
            ViewData["contactPersonList"] = binddropdownForConcernPersonForTracker("ConsernPersonForMail", 0, clientId);


            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("_ConcernPersonForMail", data)
                : View(data);
        }


        //========================================== Send Tracker Mail ==================================================

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendmailTracker(ConcernPerForTracker[] ConcernPerForTracker)
        {
            try
            { 
            //ConcernPerForTracker concernp = new ConcernPerForTracker();
            int? ReqID = ConcernPerForTracker[0].ReqID;
            String Description = ConcernPerForTracker[0].Description;

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Description = Description.Replace("\n", "");
            Description = Description.Replace("\\n", "");
            Description = Description.Replace("\t", "");
            Description = Description.Replace("\\t", "");

            jobDbContext _db = new jobDbContext();
            ResumeDetailsFortracker dd = new ResumeDetailsFortracker();
            var result = _db.ResumeDetailsFortracker.SqlQuery(@"exec usp_TrackerCandidateResumeList
                @ReqID",
             new SqlParameter("@ReqID", ReqID)).ToList<ResumeDetailsFortracker>();
            IEnumerable<ResumeDetailsFortracker> data = result;
            Description = Description.Replace("(Position Name)", result[0].req_name);
            String body = Description;
            var ToEmail = adddccdata(ConcernPerForTracker[0].ClientConcernId);

            SendHtmlFormattedTrackerEmail(ToEmail, ConcernPerForTracker[0].MailSubject, body, ReqID, ConcernPerForTracker, ConcernPerForTracker.First().UploadType);

            //  return Json(data,"TrackerMail");
            return Json("Mail Send Sucessfully", JsonRequestBehavior.AllowGet);
        }
              catch (Exception ex)
            {
                var msg = ex.Message;
                return Json(msg,JsonRequestBehavior.AllowGet);
            }

        }

        //========================================== Function For Fetching Concern Persons Email Id ==================================================

        String adddccdata(int? clientCD_ID)
        {
            ConcernPerForTracker1 data = new ConcernPerForTracker1();

            jobDbContext _db = new jobDbContext();
            var result = _db.ConcernPerForTracker1.SqlQuery(@"exec  usp_concernPersonDetails 
                @clientCD_ID",
             new SqlParameter("@clientCD_ID", clientCD_ID)).ToList<ConcernPerForTracker1>();

            data = result.FirstOrDefault();
            return data.emailid;
        }
        
        [HttpPost]
        public ActionResult ManualTrackerPath(int? req_id)
        { 
        TrackerFileUpload tf = new TrackerFileUpload();
        jobDbContext _db2 = new jobDbContext();
            if(req_id == null)
            {
                req_id = 0;
            }
        var result2 = _db2.TrackerFileUpload.SqlQuery(@"exec usp_GetMaualTrackerPath
                     @req_ID",
        new SqlParameter("@req_ID", req_id == null ? (object)DBNull.Value : req_id)).ToList<TrackerFileUpload>();

            return Request.IsAjaxRequest()
              ? (ActionResult)PartialView("_ManualTrackerDownloadLink", result2)
              : View(result2);
        }
            
        //========================================== Main Code For Sending Tracker Email ==================================================
      
        private void SendHtmlFormattedTrackerEmail(string recepientEmail, string subject, string body ,int? ReqID, ConcernPerForTracker[] ConcernPerForTracker , int UploadType)
        {
                body = body.Replace('"', ' ');

                FromMail data1 = new FromMail();

                jobDbContext _db1 = new jobDbContext();
                var result1 = _db1.FromMail.SqlQuery(@"exec [usp_MailDetails] 
                @Emailid",
                 new SqlParameter("@Emailid", ConcernPerForTracker[0].Emailid1)).ToList<FromMail>();

                data1 = result1.FirstOrDefault();


                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(data1.Email);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(recepientEmail));

                    // code for adding Cc person in mail


                    foreach (var item in ConcernPerForTracker)
                    {
                        if (item.clientCD_ID != 0)
                        {
                            ConcernPerForTracker data = new ConcernPerForTracker();
                            data.clientCD_ID = item.clientCD_ID;

                            var email = adddccdata(data.clientCD_ID);

                            mailMessage.CC.Add(new MailAddress(email));
                        }

                    }

                    mailMessage.CC.Add(new MailAddress("ehiring8@gmail.com"));

                    SmtpClient smtp = new SmtpClient();
                    // smtp.Host = ConfigurationManager.AppSettings["Host"];
                    if (data1.SMTP_Type == "gmail")
                    {
                        smtp.Host = "smtp.gmail.com";
                    }
                    else if (data1.SMTP_Type == "rediff")
                    {
                        smtp.Host = " smtp.rediffmailpro.com";
                    }
                    smtp.EnableSsl = true;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                    //Code for fetcting Resume Path of candidates

                    jobDbContext _db = new jobDbContext();
                    var result = _db.TrackerInfo.SqlQuery(@"exec usp_GetTrackerInfo
                     @req_ID",
                    new SqlParameter("@req_ID", ReqID)).ToList<TrackerInfo>();
                    IEnumerable<TrackerInfo> r = result;

                    String FullPath = "";
                    for (int i = 0; i < result.Count; i++)
                    {
                        FullPath = "../" + r.ElementAt(i).FullPath;
                        Attachment at = new Attachment(Server.MapPath(FullPath));
                        mailMessage.Attachments.Add(at);
                    }

                    //Code For Attaching excel sheet to mail

                    String excelpath = "";
                    if (UploadType == 0)
                    {
                        excelpath = "../" + "TrackerClientXLSheet/" + ReqID + "/generated/Tracker.xls";
                    }
                    else
                    {
                        TrackerFileUpload tf = new TrackerFileUpload();
                        jobDbContext _db2 = new jobDbContext();
                        var result2 = _db2.TrackerFileUpload.SqlQuery(@"exec usp_GetMaualTrackerPath
                     @req_ID",
                        new SqlParameter("@req_ID", ReqID)).ToList<TrackerFileUpload>();
                        tf = result2.FirstOrDefault();

                        excelpath = "../" + "TrackerClientXLSheet/" + ReqID + "/manual/" + tf.FileName + tf.ext;
                    }

                    Attachment ct = new Attachment(Server.MapPath(excelpath));
                    mailMessage.Attachments.Add(ct);

                    mailMessage.Priority = MailPriority.High;
                    mailMessage.IsBodyHtml = true;

                    NetworkCred.UserName = data1.Email;
                    NetworkCred.Password = data1.Password;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mailMessage);
                }

            jobDbContext _db12 = new jobDbContext();
            var result12 = _db12.Database.ExecuteSqlCommand(@"exec usp_Removefromtracker 
                @req_ID",
             new SqlParameter("@req_ID", ReqID)
             );
                       
        }

        
        public ActionResult AssignPossitionToUser(int? Req_Id = 0)
        {
            ViewData["UserList"] = binddropdown("AssignUserToPosition", 0);
            

            jobDbContext _db = new jobDbContext();

            IEnumerable<AssignMultipalUsertoJobPosition> result = _db.AssignMultipalUsertoJobPosition.SqlQuery(@"exec USP_GetAssignedUserToPosition @Req_Id",
            new SqlParameter("@Req_Id", Req_Id)).ToList<AssignMultipalUsertoJobPosition>();

            ViewBag.reqid = Req_Id;
                return Request.IsAjaxRequest()
                     ? (ActionResult)PartialView("AssignPossitionToUser", result)
                     : View("AssignPossitionToUser", result);
            
            
        }

        //================================== Assign Possition to user Code ===========================================
        //[HttpPost]
        //public ActionResult AddAssignUser(AssignUsertoJobPosition rs)
        //{

        //    try
        //    {
        //        jobDbContext _db = new jobDbContext();
        //        var result = _db.Database.ExecuteSqlCommand(@"exec USP_AddAsignUserForPosition 
        //        @UserId,@Req_id",
        //            new SqlParameter("@UserId", rs.UserId),
        //            new SqlParameter("@Req_id", rs.ReqId)
        //    );

        //        return Json("Joblist");

        //    }
        //    catch (Exception ex)
        //    {
        //        string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
        //        return View("Joblist", rs);
        //        //return PartialView(rs);
        //    }


        //}

        public ActionResult InterviewTrackerList(int? ReqID)
        {

            StaticPagedList<InterviewTrackList> itemsAsIPagedList;
            itemsAsIPagedList = InterviewTrackerListDetails(ReqID);
            TempData["Data"] = itemsAsIPagedList;
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("InterviewTrackerList", itemsAsIPagedList)
                    : View("InterviewTrackerList", itemsAsIPagedList);
        }


        public StaticPagedList<InterviewTrackList> InterviewTrackerListDetails(int? req_ID)
        {

            jobDbContext _db = new jobDbContext();
            var pageIndex = 1;
            const int pageSize = 10;
            int totalCount = 10;
            InterviewTrackList clist = new InterviewTrackList();

            IEnumerable<InterviewTrackList> result = _db.InterviewTrackList.SqlQuery(@"exec usp_GetIntrviewTrackerInfo
                   @req_ID",
               new SqlParameter("@req_ID", req_ID)

               ).ToList<InterviewTrackList>();

            totalCount = 0;

            var itemsAsIPagedList = new StaticPagedList<InterviewTrackList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }


        public StaticPagedList<InterviewTrackList> InterviewTrackerStatusWiseFilter(int? req_ID, int? statusID)
        {

            jobDbContext _db = new jobDbContext();
            var pageIndex = 1;
            const int pageSize = 10;
            int totalCount = 10;
            InterviewTrackList clist = new InterviewTrackList();
            IEnumerable<InterviewTrackList> result = _db.InterviewTrackList.SqlQuery(@"exec usp_GetStatusWiseIntrviewTrackerInfo
                   @req_ID,@statusID",
               new SqlParameter("@req_ID", req_ID),
               new SqlParameter("@statusID", statusID)

               ).ToList<InterviewTrackList>();

            totalCount = 0;

            var itemsAsIPagedList = new StaticPagedList<InterviewTrackList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BindTableDataForInterviewMail(String abc)
        {
            mail m = new mail();
            m.mailID = 1;
            string body = string.Empty;
            body = abc;
            m.mailDescription = body;
            return Request.IsAjaxRequest()
               ? (ActionResult)PartialView("mailTemplatedata", m)
               : View("mailTemplatedata", m);
        }



        public ActionResult StatusWiseInterviewTrackerFilter(int? ReqID, int? ClientID, int? statusID)
        {
            
            StaticPagedList<InterviewTrackList> itemsAsIPagedList;
            itemsAsIPagedList = InterviewTrackerStatusWiseFilter(ReqID,statusID);
            TempData["Data"] = itemsAsIPagedList;
            Session["clientid"] = ClientID;
            Session["reqid"] = ReqID;
            //return PartialView("PartialInterviewTrackerList", itemsAsIPagedList);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("InterviewTrackerList", itemsAsIPagedList)
                    : View("InterviewTrackerList", itemsAsIPagedList);

        }



        public ActionResult InterviewTrackerMail(int? ReqID, int? ClientID)
        {
            StaticPagedList<InterviewTrackList> itemsAsIPagedList;
            itemsAsIPagedList = InterviewTrackerListDetails(ReqID);
            TempData["Data"] = itemsAsIPagedList;
            Session["clientid"] = ClientID;
            Session["reqid"] = ReqID;
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("InterviewTrackerMail", itemsAsIPagedList)
                    : View("InterviewTrackerMail", itemsAsIPagedList);
                                  
        }

         //========================================== Send Tracker Mail ==================================================

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendmailTrackerForInterview(ConcernPerForTracker[] ConcernPerForTracker)
        {
            //ConcernPerForTracker concernp = new ConcernPerForTracker();
            int? ReqID = ConcernPerForTracker[0].ReqID;
            String Description = ConcernPerForTracker[0].Description;

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Description = Description.Replace("\n", "");
            Description = Description.Replace("\\n", "");
            Description = Description.Replace("\t", "");
            Description = Description.Replace("\\t", "");

            //jobDbContext _db = new jobDbContext();
            //ResumeDetailsFortracker dd = new ResumeDetailsFortracker();
            //var result = _db.ResumeDetailsFortracker.SqlQuery(@"exec usp_InterviewTrackerCandidateList
            //    @ReqID",
            // new SqlParameter("@ReqID", ReqID)).ToList<ResumeDetailsFortracker>();
            //IEnumerable<ResumeDetailsFortracker> data = result;
            //Description = Description.Replace("(Position Name)", result[0].req_name);
            String body = Description;
            var ToEmail = adddccdata(ConcernPerForTracker[0].ClientConcernId);

            SendHtmlFormattedTrackerEmailOfInterview(ToEmail, ConcernPerForTracker[0].MailSubject, body, ReqID, ConcernPerForTracker);

            //  return Json(data,"TrackerMail");
            return Json(JsonRequestBehavior.AllowGet);

        }

        //========================================== Main Code For Sending Tracker Email ==================================================

        private void SendHtmlFormattedTrackerEmailOfInterview(string recepientEmail, string subject, string body, int? ReqID, ConcernPerForTracker[] ConcernPerForTracker)
        {
            body = body.Replace('"',' ');

            FromMail data1 = new FromMail();

            jobDbContext _db1 = new jobDbContext();
            var result1 = _db1.FromMail.SqlQuery(@"exec [usp_MailDetails] 
                @Emailid",
             new SqlParameter("@Emailid", ConcernPerForTracker[0].Emailid1)).ToList<FromMail>();

            data1 = result1.FirstOrDefault();
            
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(data1.Email);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(recepientEmail));

                // code for adding Cc person in mail
                
                foreach (var item in ConcernPerForTracker)
                {
                    if (item.clientCD_ID != 0)
                    {
                        ConcernPerForTracker data = new ConcernPerForTracker();
                        data.clientCD_ID = item.clientCD_ID;

                        var email = adddccdata(data.clientCD_ID);

                        mailMessage.CC.Add(new MailAddress(email));
                    }
                }

                mailMessage.CC.Add(new MailAddress("ehiring8@gmail.com"));

                SmtpClient smtp = new SmtpClient();
                // smtp.Host = ConfigurationManager.AppSettings["Host"];
                if (data1.SMTP_Type == "gmail")
                {
                    smtp.Host = "smtp.gmail.com";
                }
                else if (data1.SMTP_Type == "rediff")
                {
                    smtp.Host = " smtp.rediffmailpro.com";
                }
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                
                mailMessage.Priority = MailPriority.High;
                mailMessage.IsBodyHtml = true;
                NetworkCred.UserName = data1.Email;
                NetworkCred.Password = data1.Password;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                TrackerFileUpload rs = new TrackerFileUpload();
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
                        int ReqID =Convert.ToInt32(Request.Form[0]);
                        string filePath = Server.MapPath("~/TrackerClientXLSheet/" + ReqID + "/manual/");

                        bool isExists = System.IO.Directory.Exists(filePath);
                        if (!isExists) { System.IO.Directory.CreateDirectory(filePath); }


                        string extension = Path.GetExtension(file.FileName);
                        string fileName = "Tracker" + extension;

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(filePath, fileName);
                        file.SaveAs(fname);
                                               
                        rs.uploadby = Convert.ToInt32(Session["User_id"].ToString());
                        jobDbContext _db = new jobDbContext();
                        var result = _db.Database.ExecuteSqlCommand(@"exec [USP_AddTrackerDocument] 
                        @FolderPath,@FileName,@ext,@Req_id,@uploadby",
                            new SqlParameter("@FolderPath", filePath),
                            new SqlParameter("@FileName", "Tracker"),
                            new SqlParameter("@ext", extension),
                            new SqlParameter("@Req_id", ReqID),
                            new SqlParameter("@uploadby", rs.uploadby));

                    }

                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");

                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }



        //================================== Delete Candidate Resume ===========================================
      
        public ActionResult AskForFeedback(int? Candid)
        {
            Candidate rs = new Candidate();

            try
            {
                
                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_DeleteCandidateResume 
                @candID",
                    new SqlParameter("@candID", Candid)
            );
                int? page =null;
                string Name = "";
                string DName = "";
                string CName = "";
                String Eid ="";
                String Mob ="";
                String Stage ="";
                string Activity = null;
                DateTime? Date = null;

                int? userid;
             
                if (Convert.ToInt32(Session["RoleID"].ToString()) != 6)
                {
                    userid = null;
                }
                else
                {

                    userid = Convert.ToInt32(Session["User_id"].ToString());
                }
                int? PageRowNumber = 8;
                StaticPagedList<CandidateList> itemsAsIPagedList;
                itemsAsIPagedList = GridList(page, PageRowNumber, Name, DName, CName, Eid, Mob, Stage,Activity, Date, userid);
                ViewData["UserList"] = binddropdown("UserList", 0);
                return Request.IsAjaxRequest()
                        ? (ActionResult)PartialView("index", itemsAsIPagedList)
                        : View("index", itemsAsIPagedList);

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
                //return PartialView(rs);
            }
        }


        //================================== Delete Candidate  ===========================================

        public ActionResult DeleteCandidate(int? Candid)
        {
            Candidate rs = new Candidate();

            try
            {

                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_DeleteCandidate 
                @candID",
                    new SqlParameter("@candID", Candid)
            );
                int? page = null;
                string Name = "";
                string DName = "";
                string CName = "";
                String Eid = "";
                String Mob = "";
                String Stage = "";
                string Activity = null;
                DateTime? Date = null;


                int? userid;

                if (Convert.ToInt32(Session["RoleID"].ToString()) != 6)
                {
                    userid = null;
                }
                else
                {

                    userid = Convert.ToInt32(Session["User_id"].ToString());
                }
                int? PageRowNumber = 8;
                StaticPagedList<CandidateList> itemsAsIPagedList;
                itemsAsIPagedList = GridList(page, PageRowNumber, Name, DName, CName, Eid, Mob, Stage,Activity, Date, userid);

                ViewBag.error = "Cadidate Deleted";
                ViewData["UserList"] = binddropdown("UserList", 0);
                return Request.IsAjaxRequest()
                        ? (ActionResult)PartialView("index", itemsAsIPagedList)
                        : View("index", itemsAsIPagedList);

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(JsonRequestBehavior.AllowGet);
                //return PartialView(rs);
            }
        }


        //================================== Delete Position  ===========================================

        public ActionResult DeletePosition(int? @req_id)
        {
            
            try
            {

                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_DeletePosition 
                @req_id",
                new SqlParameter("@req_id",req_id)
            );
                int? page = null;
                string Name = "";
                string DName = "";
                string CName = "";
                WorkFLowList clist = new WorkFLowList();

                int? userid, RowCount = 7;

                if (Convert.ToInt32(Session["RoleID"].ToString()) != 6)
                {
                    userid = null;
                }
                else
                {
                    userid = Convert.ToInt32(Session["User_id"].ToString());
                }
                
                StaticPagedList<WorkFLowList> itemsAsIPagedList;
                itemsAsIPagedList = GridJobList(page,RowCount,Name, CName, DName, userid,null);

                ViewBag.error = "Position Deleted";
                return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("Joblist", itemsAsIPagedList)
                    : View("Joblist", itemsAsIPagedList);

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(JsonRequestBehavior.AllowGet);
                //return PartialView(rs);
            }
        }


        //================================== Update Postion  ===========================================


        public ActionResult UpdatePostionstatus()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePostionstatus(int? Rqid, int? Statusid, String StatusName="")
        {
            UpdatePosition rs = new UpdatePosition();
            int? userid;
            
            if(Session["User_id"] !=null)
            {
                userid = Convert.ToInt16(Session["User_id"].ToString());
            }
            else
            {
                userid = null;
            }

            try
            {

                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_UpdateRequirmentStatus 
                 @ReqId,
	             @PositionStatusId,
	             @PostionStatusName,
	             @UpdatedDate,
	             @AddedBy",
                    new SqlParameter("@ReqId", Rqid),
                     new SqlParameter("@PositionStatusId", Statusid),
                      new SqlParameter("@PostionStatusName", StatusName),
                      new SqlParameter("@UpdatedDate",rs.UpdatedDate == null ? (object)DBNull.Value : rs.UpdatedDate),
                       new SqlParameter("@AddedBy", userid)
            );


                return Json("Status Updated sucessfully.");
                //return Request.IsAjaxRequest()
                //        ? (ActionResult)PartialView("JobList")
                //        : View("JobList");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                //return Json(JsonRequestBehavior.AllowGet);
                return Json(message);

            }
        }

        [HttpPost]
        public ActionResult AssignMultipalUserToPosition(AssignUsertoJobPosition[] AssignUsertoJobPosition)
        {

            try
            {
                var result = 0;
                jobDbContext _db = new jobDbContext();
                foreach (var item in AssignUsertoJobPosition)
                {

                    AssignUsertoJobPosition O = new AssignUsertoJobPosition();

                    O.ReqId = item.ReqId;
                    O.UserId = item.UserId;                    
                  
                    result = _db.Database.ExecuteSqlCommand(@"exec USP_AddAsignUserForPosition 
                    @UserId,@Req_id",
                    new SqlParameter("@UserId", O.UserId),
                    new SqlParameter("@Req_id", O.ReqId));                

                 }

                return Json("Users Assigned To Position Sucessfull");

            }

            catch (Exception ex)
            {
                string message = ex.Message;
                return Json(message);
            }


        }

        public ActionResult DeleteAssignedUser(int? Assignid)
        {
            jobDbContext _db = new jobDbContext();
            var result = _db.Database.ExecuteSqlCommand(@"exec USP_DeleteAssignedUser 
                @@Assignid",
                new SqlParameter("@@Assignid", Assignid));
            return Json("");
        }

        [HttpPost]
        public ActionResult FllterLocationForAddCandidate(int Reqid)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewData["CandidateLocationList"] = binddropdownForMultipalLocation("CandidateLocationList", 0, Reqid);
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("FllterLocationForAddCandidate")
                   : View();
        }

        [HttpPost]
        public ActionResult filterLocationForEditCandidate(int Reqid,int Locationid = 0)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            FetchcandidatwForCandidate data = new FetchcandidatwForCandidate();
            data.location_id = Locationid;
            ViewData["CandidateLocationList"] = binddropdownForMultipalLocation("CandidateLocationList", 0, Reqid);
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("filterLocationForEditCandidate",data)
                   : View(data);
        }


        public ActionResult FeedbackmailTracker(int? ReqID)
        {
            StaticPagedList<InterviewTrackList> itemsAsIPagedList;
            itemsAsIPagedList = InterviewTrackerListDetails(ReqID);
            TempData["Data"] = itemsAsIPagedList;
            Session["reqid"] = ReqID;
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("InterviewTrackerMail", itemsAsIPagedList)
                    : View("InterviewTrackerMail", itemsAsIPagedList);

        }

    }
  
}    
    
