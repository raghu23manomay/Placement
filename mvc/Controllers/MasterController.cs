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
using System.Configuration;
using System.Data;


namespace mvc.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
       
        public ActionResult Index()
        {
            return View();
        }
       
        //================================== Load Data For designation ===========================================

     

     //====================================================== Designation Master Code ===================================================================

     //================================== Designation Grid Code ===========================================

        public ActionResult PageGrid(int? page, String Name=null)
        {

            StaticPagedList<DesignationList> itemsAsIPagedList;
            itemsAsIPagedList = GridJobList(page,Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("PageGrid", itemsAsIPagedList)
                    : View("PageGrid", itemsAsIPagedList);

        }

        //================================== Designation Page Code ===========================================



        public ActionResult DesignationList()
        {
            return View();
        }


        public ActionResult DesignationForManual()
        {
            return View();
        }

       
        //================================== Index page for Designation Code ===========================================

        public ActionResult IndexForDesignation(int? page, String Name = null)
        {
            StaticPagedList<DesignationList> itemsAsIPagedList;
            itemsAsIPagedList = GridJobList(page,Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("IndexForDesignation", itemsAsIPagedList)
                    : View("IndexForDesignation", itemsAsIPagedList);
        }

        //================================== Insert Designation Code ===========================================
        [HttpPost]
        public ActionResult add_data(DesignationList rs)
        {

            
            try
            {
                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_AddDesignation 
                @desg_desc",
                    new SqlParameter("@desg_desc",rs.desg_desc)
            );

                return Json("IndexForDesignation");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("DesignationList", rs);
                //return PartialView(rs);
            }

        }
               
        //================================== Designation Update Code ===========================================

        [HttpPost]
        public ActionResult edit(DesignationList rs)
        {
           
            try
            {
                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_UpdateDesignation 
                @desig_id,@desg_desc",
                 new SqlParameter("@desig_id", rs.desig_id),
                 new SqlParameter("@desg_desc", rs.desg_desc)
                 );

                return Json("IndexForDesignation");

            }
            catch (Exception)
            {
              
                return Json(JsonRequestBehavior.AllowGet);

            }
            finally
            {
               
            }
        }

        public ActionResult _EditDesignaton()
        {
            return View();
        }

        //================================== Fetch Designation For Update Code ===========================================

        public ActionResult Edit(int desig_id)
        {

            try
            {
                DesignationDetails data = new DesignationDetails();

                jobDbContext _db = new jobDbContext();
                var result = _db.EDList.SqlQuery(@"exec usp_DesignationDetails 
                @desig_id",
                 new SqlParameter("@desig_id", desig_id)).ToList<DesignationDetails>();

                data = result.FirstOrDefault();
                             
                return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("_EditDesignaton", data)
                : View("_EditDesignaton", data);
            }
            catch (Exception)
            {
                return RedirectToAction("_EditDesignaton");

            }
            finally
            {

            }
    }

     //====================================================== Designation Paging Code ========================================================

        public StaticPagedList<DesignationList> GridJobList(int? page, String Name="")
        {

            jobDbContext _db = new jobDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            DesignationList clist = new DesignationList();

            IEnumerable<DesignationList> result = _db.DFList.SqlQuery(@"exec USP_FetchDesignation
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<DesignationList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<DesignationList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }
               

        //===========================================================User Master ==============================================

        public ActionResult UserMaster(int? page, String Name =null)
        {
            StaticPagedList<UserMasterGridList> itemsAsIPagedList;
            itemsAsIPagedList = UserGridList(page,Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("UserMaster", itemsAsIPagedList)
                    : View("UserMaster", itemsAsIPagedList);
        }



        //================================== Load Data For User Master ===========================================

        [HttpPost]
        public ActionResult LoadDataForUser(int? page, String Name)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            StaticPagedList<UserMasterGridList> itemsAsIPagedList;
            itemsAsIPagedList = UserGridList(page, Name);

            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("UserMaster", itemsAsIPagedList)
                   : View(itemsAsIPagedList);
        }

        //================================== Fill user Grid Code ===========================================

        public StaticPagedList<UserMasterGridList> UserGridList(int? page, String Name ="")
        {

            jobDbContext _db = new jobDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            UserMasterGridList Ulist = new UserMasterGridList();

            IEnumerable<UserMasterGridList> result = _db.UMGList.SqlQuery(@"exec USP_FetchUserMaster
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<UserMasterGridList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<UserMasterGridList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;



        }

        //================================== Index page for user Code ===========================================

        public ActionResult IndexForUser(int? page, String Name = null)
        {
            StaticPagedList<UserMasterGridList> itemsAsIPagedList;
            itemsAsIPagedList = UserGridList(page,Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("IndexForUser", itemsAsIPagedList)
                    : View("IndexForUser",itemsAsIPagedList);
        }

        //================================== page for create user Code ===========================================

        public ActionResult CreateUser()
        {
            UserMasterList data = new UserMasterList();
            data.RoleID = 0;
            ViewData["RoleList"] = binddropdown("RoleName", 0);
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("CreateUser", data)
                   : View("CreateUser",data);
        }

        //======================================================= Bind Dropdown List ==================================================================

        public List<SelectListItem> binddropdown(string action, int val = 0)
        {
            jobDbContext _db = new jobDbContext();

            var res = _db.Database.SqlQuery<SelectListItem>("exec USP_BindDropDown @action , @val",
                   new SqlParameter("@action", action),
                    new SqlParameter("@val", val))
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


        //================================== Insert user Code ===========================================

        [HttpPost]
        public ActionResult add_User(UserMasterList rs)
        {
            try
            {
               
                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_AddUser 
                @User_Name,@F_Name,@M_Name,@L_Name,@phone,@Mobile,@Password,@emailId,@RoleID,@Isactive,@CreatedBy,@CreatedDate",
                new SqlParameter("@User_Name", rs.User_Name),
                new SqlParameter("@F_Name", rs.F_Name),
                new SqlParameter("@M_Name", rs.M_Name == null ? (object)DBNull.Value : rs.M_Name),
                new SqlParameter("@L_Name", rs.L_Name),
                new SqlParameter("@phone", rs.phone == null ? (object)DBNull.Value : rs.phone),
                new SqlParameter("@Mobile", rs.Mobile),
                new SqlParameter("@Password", rs.Password),
                new SqlParameter("@emailId", rs.emailId),
                new SqlParameter("@RoleID", rs.RoleID),
                new SqlParameter("@Isactive", rs.Isactive),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@CreatedDate", DateTime.Now)
            );
                return Json("IndexForUser");
                //return Request.IsAjaxRequest()
                //      ? (ActionResult)PartialView("IndexForUser", rs)
                //      : View("IndexForUser", rs);

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);
                //return PartialView(rs);
            }
        }


        
        //================================== Edit User Code ===========================================

        public ActionResult _EditUser()
        {
            UserDetails data = new UserDetails();
            data.RoleID = 0;
            ViewData["RoleList"] = binddropdown("RoleName", 0);
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("_EditUser", data)
                   : View("_EditUser", data);
        }


        //================================== Update User Code ===========================================


        [HttpPost]
        public ActionResult UpdateUser(UserDetails rs)
        {

            try
            {
                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_UpdateUser 
                @User_Id ,	@User_Name,	@F_Name,@M_Name,@L_Name,@phone,@Mobile,@Password,@emailId,@RoleID,@Isactive",
                new SqlParameter("@User_Id", rs.User_id),
                new SqlParameter("@User_Name", rs.User_Name),
                new SqlParameter("@F_Name", rs.F_Name),
                new SqlParameter("@M_Name", rs.M_Name == null ? (object)DBNull.Value : rs.M_Name),
                new SqlParameter("@L_Name", rs.L_Name),
                new SqlParameter("@phone", rs.phone == null ? (object)DBNull.Value : rs.phone),
                new SqlParameter("@Mobile", rs.Mobile),
                new SqlParameter("@Password", rs.Password),
                new SqlParameter("@emailId", rs.emailId),
                new SqlParameter("@RoleID", rs.RoleID),
                new SqlParameter("@Isactive", rs.Isactive)
                 );

                return Json("User Updated Sucessfully.");

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

        //================================== Fetch User For Update Code ===========================================

        public ActionResult FetchUser(int User_id)
        {
                    
            try
            {
                UserDetails data = new UserDetails();

                jobDbContext _db = new jobDbContext();
                var result = _db.UDList.SqlQuery(@"exec usp_UserDetails 
                @User_id",
                 new SqlParameter("@User_id", User_id)).ToList<UserDetails>();

                data = result.FirstOrDefault();

                //data.RoleID = 0;
                ViewData["RoleList"] = binddropdown("RoleName", 0);
                return Request.IsAjaxRequest()
                       ? (ActionResult)PartialView("_EditUser", data)
                       : View("_EditUser", data);
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return RedirectToAction("_EditUser");

            }
            finally
            {

            }
        }


        //=========================================================== Client Master =======================================================

        //================================== Load Data For Client ===========================================

        [HttpPost]
        public ActionResult LoadDataForClient(int? page, String Name)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            StaticPagedList<ClinetMasterGridList> itemsAsIPagedList;
            itemsAsIPagedList = ClientGridList(page, Name);

            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("ClientList", itemsAsIPagedList)
                   : View(itemsAsIPagedList);
        }

        //================================== Index page for user Code ===========================================

        public ActionResult IndexForclient(int? page, String Name = null)
        {
            StaticPagedList<ClinetMasterGridList> itemsAsIPagedList;
            itemsAsIPagedList = ClientGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("IndexForclient", itemsAsIPagedList)
                    : View("IndexForclient", itemsAsIPagedList);
        }

        //===========================================================client Page Gride ==============================================

        public ActionResult ClientList(int? page, String Name = null)
        {
            StaticPagedList<ClinetMasterGridList> itemsAsIPagedList;
            itemsAsIPagedList = ClientGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("ClientList", itemsAsIPagedList)
                    : View("ClientList", itemsAsIPagedList);
        }

        //================================== Fill Client Grid Code ===========================================

        public StaticPagedList<ClinetMasterGridList> ClientGridList(int? page, String Name = "")
        {

            jobDbContext _db = new jobDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            ClinetMasterGridList Ulist = new ClinetMasterGridList();

            IEnumerable<ClinetMasterGridList> result = _db.CMList.SqlQuery(@"exec USP_FetchClient
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<ClinetMasterGridList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<ClinetMasterGridList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;
                        
        }

        //==================================  Create Client Code ===========================================

        public ActionResult CreateClient()
        {
            ClinetMasterGridList data = new ClinetMasterGridList();
            ViewData["BusinessTypeList"] = binddropdown("BusinessTypeList", 0);
            ViewData["LocationList"] = binddropdown("LocationList", 0);
            ViewData["ClientTypeList"] = binddropdown("ClientTypeList", 0);
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("CreateClient", data)
                   : View("CreateClient", data);
        }

        //================================== Insert user Code ===========================================

        [HttpPost]
        public ActionResult add_Client(ClinetMasterGridList rs)
        {
            try
            {

                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_AddClient 
                @Client_Name,@Client_Type_id,@location_id,@address,@DID_no,@fax_no,@Manifactring,@products,@compitators,@buissType_id,@update_date,@CompanyProfile,@Website",
                new SqlParameter("@Client_Name", rs.Client_Name),
                new SqlParameter("@Client_Type_id", rs.Client_Type_id),
                new SqlParameter("@location_id", rs.location_id),
                new SqlParameter("@address", rs.address),
                new SqlParameter("@DID_no", rs.DID_no == null ? (object)DBNull.Value: rs.DID_no),
                new SqlParameter("@fax_no", rs.fax_no == null ? (object)DBNull.Value:rs.fax_no),
                new SqlParameter("@Manifactring", rs.Manifactring == null ? (object)DBNull.Value : rs.Manifactring),
                new SqlParameter("@products", rs.products == null ? (object)DBNull.Value : rs.products),
                new SqlParameter("@compitators", rs.compitators == null ? (object)DBNull.Value : rs.compitators),
                new SqlParameter("@buissType_id", rs.buissType_id == null ? (object)DBNull.Value : rs.buissType_id),
                new SqlParameter("@update_date", DateTime.Now),
                new SqlParameter("@CompanyProfile", rs.CompanyProfile == null ? (object)DBNull.Value : rs.CompanyProfile),
                new SqlParameter("@Website", rs.Website == null ? (object)DBNull.Value : rs.Website)

            );
                return Json("IndexForclient");
                //return Request.IsAjaxRequest()
                //      ? (ActionResult)PartialView("IndexForUser", rs)
                //      : View("IndexForUser", rs);

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);
                //return PartialView(rs);
            }
        }

        //================================== Fill Client Grid Code ===========================================

        public ActionResult AddContactPerson(int? client_id)
        {
            AddContactPerson data = new AddContactPerson();
            ViewData["DesignationList"] = binddropdown("Designation", 0);
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("AddContactPerson", data)
                   : View("AddContactPerson", data);
        }

        //================================== Insert Contct Person  Code ===========================================



        [HttpPost]
        public ActionResult add_ContactPerson(AddContactPerson[] AddContactPerson)
        {

            try
            {
               
                jobDbContext _db = new jobDbContext();
                foreach (var item in AddContactPerson)
                {

                    AddContactPerson O = new AddContactPerson();

                    O.contactPerson_Name = item.contactPerson_Name;
                    O.mobile = item.mobile;
                    O.emailid = item.emailid;
                    O.description = item.description;
                    O.cp_desig_Id = item.cp_desig_Id;
                    O.phone1 = item.phone1;
                    O.phone2 = item.phone2;

                    var result = _db.Database.ExecuteSqlCommand(@"exec USP_AddContactPerson 
                    @contactPerson_Name,@cp_desig_Id,@phone1,@phone2,@mobile,@description,@user_id,@update_date,@addedBy,@emailid",
                    //new SqlParameter("@client_id",0),
                    new SqlParameter("@contactPerson_Name", O.contactPerson_Name),
                    new SqlParameter("@cp_desig_Id", O.cp_desig_Id),
                    new SqlParameter("@phone1", O.phone1 == null ? (object)DBNull.Value : O.phone1),
                    new SqlParameter("@phone2", O.phone2 == null ? (object)DBNull.Value : O.phone2),
                    new SqlParameter("@mobile", O.mobile == null ? (object)DBNull.Value : O.mobile),
                    new SqlParameter("@description", O.description == null ? (object)DBNull.Value : O.description),
                    new SqlParameter("@user_id", 1),
                    new SqlParameter("@update_date", DateTime.Now),
                    new SqlParameter("@addedBy", 1),
                    new SqlParameter("@emailid", O.emailid == null ? (object)DBNull.Value : O.emailid));
            

                }

                return Json("Contact person Added");

            }

            catch (Exception ex)
            {
                string message = ex.Message;
                return Json(message);
            }


        }


        [HttpPost]
        public ActionResult SaveClientLocation(ClientLocation[] ClientLocation)
        {

            try
            {

                jobDbContext _db = new jobDbContext();
                foreach (var item in ClientLocation)
                {

                    ClientLocation O = new ClientLocation();

                    //O.ClientId = item.ClientId;
                    O.LocationId = item.LocationId;
                    O.ContactNo = item.ContactNo;

                    var result = _db.Database.ExecuteSqlCommand(@"exec USP_AddClientLocation 
                    @ClientId,@LocationId,@ContactNo,@Addedby",
                    new SqlParameter("@ClientId", (object)DBNull.Value),
                    new SqlParameter("@LocationId", O.LocationId),
                    new SqlParameter("@ContactNo", O.ContactNo ),
                    new SqlParameter("@Addedby",Convert.ToInt16(Session["User_id"]) == 0 ? (object)DBNull.Value : Convert.ToInt16(Session["User_id"])));

                }

                return Json("Location Added Sucessfully");

            }

            catch (Exception ex)
            {
                string message = ex.Message;
                return Json(message);
            }
            
        }

        //[HttpPost]
        //public ActionResult add_ContactPerson(AddContactPerson rs)
        //{
        //    try
        //    {

        //        jobDbContext _db = new jobDbContext();
        //        var result = _db.Database.ExecuteSqlCommand(@"exec USP_AddContactPerson 
        //        @client_id,@contactPerson_Name,@cp_desig_Id,@phone1,@phone2,@mobile,@description,@user_id,@update_date,@addedBy,@emailid",
        //        new SqlParameter("@client_id", rs.client_id),
        //        new SqlParameter("@contactPerson_Name", rs.contactPerson_Name),
        //        new SqlParameter("@cp_desig_Id", rs.cp_desig_Id),
        //        new SqlParameter("@phone1", rs.phone1 == null ? (object)DBNull.Value : rs.phone1),
        //        new SqlParameter("@phone2", rs.phone2 == null ? (object)DBNull.Value : rs.phone2),
        //        new SqlParameter("@mobile", rs.mobile == null ? (object)DBNull.Value : rs.mobile),
        //        new SqlParameter("@description", rs.description == null ? (object)DBNull.Value : rs.description),
        //        new SqlParameter("@user_id", 1),
        //        new SqlParameter("@update_date",DateTime.Now),
        //        new SqlParameter("@addedBy", 1),
        //        new SqlParameter("@emailid",rs.emailid == null ? (object)DBNull.Value : rs.emailid)

        //    );
        //        return Json("IndexForclient");
        //        //return Request.IsAjaxRequest()
        //        //      ? (ActionResult)PartialView("IndexForUser", rs)
        //        //      : View("IndexForUser", rs);

        //    }
        //    catch (Exception ex)
        //    {

        //        string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
        //        return View("IndexForUser", message);
        //        //return PartialView(rs);
        //    }
        //}

        //================================== Client Contact Person Details Code ===========================================

        public ActionResult ClientContactDetails(int client_id)
        {
            ClientContactDetails data = new ClientContactDetails();
            jobDbContext _db = new jobDbContext();
            var result = _db.CCList.SqlQuery(@"exec GetClientContactDetails 
                @client_id",
                new SqlParameter("@client_id", client_id)).ToList<ClientContactDetails>();

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("ClientContactDetails", result)
                : View("ClientContactDetails", data);
        }

      
        //================================== Fetch contact details for update Code ===========================================

        public ActionResult FetchContactperson(int clientCD_ID)
        {

            try
            {
                ClientContactDetailsEdit data = new ClientContactDetailsEdit();

                jobDbContext _db = new jobDbContext();
                var result = _db.CEList.SqlQuery(@"exec usp_ContactPersonDetails 
                @clientCD_ID",
                 new SqlParameter("@clientCD_ID", clientCD_ID)).ToList<ClientContactDetailsEdit>();

                data = result.FirstOrDefault();

                //data.RoleID = 0;
                ViewData["DesignationList"] = binddropdown("Designation", 0);
                return Request.IsAjaxRequest()
                       ? (ActionResult)PartialView("_EditContactPerson", data)
                       : View("_EditContactPerson", data);
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return RedirectToAction("_EditContactPerson");

            }
            finally
            {

            }
        }
        
        //================================== Edit Contact person Code ===========================================

        public ActionResult _EditContactPerson()
        {
            ClientContactDetailsEdit data = new ClientContactDetailsEdit();
            //data.RoleID = 0;
            ViewData["DesignationList"] = binddropdown("Designation", 0);
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("_EditContactPerson", data)
                   : View("_EditContactPerson", data);
        }

        //================================== Client Contact For Update Code ===========================================

        [HttpPost]
        public ActionResult UpdateContactPerson(ClientContactDetailsEdit rs)
        {

            try
            {
                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_UpdateClientContactPerson 
                @clientCD_ID,@client_id,@contactPerson_Name,@cp_desig_Id,@phone1,@phone2,@mobile,@description,@user_id,@update_date,@addedBy,@emailid", 
                new SqlParameter("@clientCD_ID", rs.clientCD_ID),
                new SqlParameter("@client_id", rs.client_id),
                new SqlParameter("@contactPerson_Name", rs.contactPerson_Name),
                new SqlParameter("@cp_desig_Id", rs.cp_desig_Id),
                new SqlParameter("@phone1", rs.phone1 == null ? (object)DBNull.Value : rs.phone1),
                new SqlParameter("@phone2", rs.phone2 == null ? (object)DBNull.Value : rs.phone2),
                new SqlParameter("@mobile", rs.mobile),
                new SqlParameter("@description", rs.description == null ? (object)DBNull.Value : rs.description),
                new SqlParameter("@user_id", 1),
                new SqlParameter("@update_date", DateTime.Now),
                new SqlParameter("@addedBy", 1),
                new SqlParameter("@emailid", rs.emailid == null ? (object)DBNull.Value : rs.emailid)
                 );

                return Json("IndexForclient");
               

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForclient", message);

            }
            finally
            {

            }

        }

        //================================== Edit client Code ===========================================

        public ActionResult _EditClient()
        {
            EditClient data = new EditClient();
            ViewData["BusinessTypeList"] = binddropdown("BusinessTypeList", 0);
            ViewData["LocationList"] = binddropdown("LocationList", 0);
            ViewData["ClientTypeList"] = binddropdown("ClientTypeList", 0);
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("_EditClient", data)
                   : View("_EditClient", data);
        }

        //================================== Fetch client details for update Code ===========================================

        public ActionResult FetchClinetDetails(int client_id)
        {

            try
            {
                EditClient data = new EditClient();

                jobDbContext _db = new jobDbContext();
                var result = _db.EDTCList.SqlQuery(@"exec usp_ClientDetailsforupdate 
                @client_id",
                 new SqlParameter("@client_id", client_id)).ToList<EditClient>();

                data = result.FirstOrDefault();

                //data.RoleID = 0;
                ViewData["BusinessTypeList"] = binddropdown("BusinessTypeList", 0);
                ViewData["LocationList"] = binddropdown("LocationList", 0);
                ViewData["ClientTypeList"] = binddropdown("ClientTypeList", 0);
                return Request.IsAjaxRequest()
                       ? (ActionResult)PartialView("_EditClient", data)
                       : View("_EditClient", data);
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return RedirectToAction("_EditClient");

            }
            finally
            {

            }
        }

      //  =========================================== Update client Code =============================================

        [HttpPost]
        public ActionResult Edit_Client(EditClient rs)
        {
            try
            {

                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec [USP_UpdateClientMaster] 
                @client_id,@Client_Name,@Client_Type_id,@location_id,@address,@DID_no,@fax_no,@Manifactring,@products,@compitators,@buissType_id,@update_date,@CompanyProfile,@Website",
                new SqlParameter("@client_id", rs.client_id),
                new SqlParameter("@Client_Name", rs.Client_Name),
                new SqlParameter("@Client_Type_id", rs.Client_Type_id),
                new SqlParameter("@location_id", rs.location_id == null ? (object)DBNull.Value : rs.location_id),
                new SqlParameter("@address", rs.address == null ? (object)DBNull.Value : rs.address),
                new SqlParameter("@DID_no", rs.DID_no == null ? (object)DBNull.Value : rs.DID_no),
                new SqlParameter("@fax_no", rs.fax_no == null ? (object)DBNull.Value : rs.fax_no),
                new SqlParameter("@Manifactring", rs.Manifactring == null ? (object)DBNull.Value : rs.Manifactring),
                new SqlParameter("@products", rs.products == null ? (object)DBNull.Value : rs.products),
                new SqlParameter("@compitators", rs.compitators == null ? (object)DBNull.Value : rs.compitators),
                new SqlParameter("@buissType_id", rs.buissType_id == null ? (object)DBNull.Value : rs.buissType_id),
                new SqlParameter("@update_date", DateTime.Now),
                new SqlParameter("@CompanyProfile", rs.CompanyProfile == null ? (object)DBNull.Value : rs.CompanyProfile),
                new SqlParameter("@Website", rs.Website == null ? (object)DBNull.Value : rs.Website)

            );
                 return Json("IndexForclient");

                //StaticPagedList<ClinetMasterGridList> itemsAsIPagedList;
                //itemsAsIPagedList = ClientGridList(page, Name = " ");

                //return Request.IsAjaxRequest()
                //  ? (ActionResult)PartialView("ClientList", itemsAsIPagedList)
                //  : View("ClientList", itemsAsIPagedList);


            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForclient", message);
                //return PartialView(rs);
            }
        }

        public JsonResult GetLocationList()
        {
            jobDbContext _db = new jobDbContext();
            var lstItem = binddropdown("LocationList", 0).Select(i => new { i.Value, i.Text }).ToList();
            //_spService.BindDropdown("PricingUser", "", "").Select(i => new { i.Value, i.Text }).ToList();
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbusinesstypeList()
        {
            jobDbContext _db = new jobDbContext();
            var lstItem = binddropdown("BusinessTypeList", 0).Select(i => new { i.Value, i.Text }).ToList();
            //_spService.BindDropdown("PricingUser", "", "").Select(i => new { i.Value, i.Text }).ToList();
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetclienttypeList()
        {
            jobDbContext _db = new jobDbContext();
            var lstItem = binddropdown("ClientTypeList", 0).Select(i => new { i.Value, i.Text }).ToList();
            //_spService.BindDropdown("PricingUser", "", "").Select(i => new { i.Value, i.Text }).ToList();
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetdesignationList()
        {
            jobDbContext _db = new jobDbContext();
            var lstItem = binddropdown("Designation", 0).Select(i => new { i.Value, i.Text }).ToList();
            //_spService.BindDropdown("PricingUser", "", "").Select(i => new { i.Value, i.Text }).ToList();
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRoleList()
        {
            jobDbContext _db = new jobDbContext();
            var lstItem = binddropdown("RoleName", 0).Select(i => new { i.Value, i.Text }).ToList();
            //_spService.BindDropdown("PricingUser", "", "").Select(i => new { i.Value, i.Text }).ToList();
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }


        //===========================================================MailTemplate Master ==============================================

        public ActionResult MailTemplateMaster()
        {
            return View("MailTemplateMaster");
        }

        public ActionResult IndexFotMailTemplateMaster(int? page, String Name = null)
        {
            StaticPagedList<MailTemplate> itemsAsIPagedList;
            itemsAsIPagedList = MailTempalteList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("IndexFotMailTemplateMaster", itemsAsIPagedList)
                    : View("IndexFotMailTemplateMaster", itemsAsIPagedList);
        }


        //================================== Fill Client Grid Code ===========================================

        public StaticPagedList<MailTemplate> MailTempalteList(int? page, String Name = "")
        {

            jobDbContext _db = new jobDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            MailTemplate Ulist = new MailTemplate();

            IEnumerable<MailTemplate> result = _db.MailTemplate.SqlQuery(@"exec USP_FetchMailTemplateMaster
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<MailTemplate>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<MailTemplate>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }


        //================================== Insert Contct Person  Code ===========================================
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult add_MailTemplates(MailTemplate rs)
        {
            try
            {

                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_AddMailTemplate 
               @temp_title,@email_subject,@emailBody,@lastupdatedBy,@addedBy",
                new SqlParameter("@temp_title", rs.temp_title),
                new SqlParameter("@email_subject", rs.email_subject),
                new SqlParameter("@emailBody", rs.emailBody),
                new SqlParameter("@lastupdatedBy", rs.lastupdatedBy == null ? (object)DBNull.Value : rs.lastupdatedBy),
                new SqlParameter("@addedBy", rs.addedBy == null ? (object)DBNull.Value : rs.addedBy)
               );

                return Json("IndexFotMailTemplateMaster");
                
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexFotMailTemplateMaster", message);
                //return PartialView(rs);
            }
        }

        //================================== Fetch User For Update Code ===========================================

        public ActionResult FetchMailTemplateForUpdate(int temp_id)
        {

            try
            {
                MailTemplateForUpdate data = new MailTemplateForUpdate();

                jobDbContext _db = new jobDbContext();
                var result = _db.MailTemplateForUpdate.SqlQuery(@"exec usp_MailTemplateDetailsForUpdate
                @temp_id",
                 new SqlParameter("@temp_id", temp_id)).ToList<MailTemplateForUpdate>();

                data = result.FirstOrDefault();
                              
                return Request.IsAjaxRequest()
                       ? (ActionResult)PartialView("EditMailTemplateMaster", data)
                       : View("EditMailTemplateMaster", data);
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return RedirectToAction("EditMailTemplateMaster");

            }
            finally
            {

            }
        }

        // =========================================== Update client Code =============================================

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateMailTemplate(MailTemplate rs)
        {
            try
            {
                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_UpdateMailTemplate 
               @temp_id,@temp_title,@email_subject,@emailBody,@lastupdatedBy,@addedBy",
                new SqlParameter("@temp_id", rs.temp_id),
                new SqlParameter("@temp_title", rs.temp_title),
                new SqlParameter("@email_subject", rs.email_subject),
                new SqlParameter("@emailBody", rs.emailBody),
                new SqlParameter("@lastupdatedBy", rs.lastupdatedBy == null ? (object)DBNull.Value : rs.lastupdatedBy),
                new SqlParameter("@addedBy", rs.addedBy == null ? (object)DBNull.Value : rs.addedBy)
               );

                return Json("IndexFotMailTemplateMaster");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexFotMailTemplateMaster", message);
                //return PartialView(rs);
            }
        }


        [HttpPost]
        public ActionResult LoadDataForMailTemplateLst(int? page, String Name="" )
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }


            StaticPagedList<MailTemplate> itemsAsIPagedList;
            itemsAsIPagedList = MailTempalteList(page, Name);

            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("_MailTemplateList", itemsAsIPagedList)
                   : View(itemsAsIPagedList);
        }


        //=========================================== Upload Client Document ================================================

        public ActionResult UploadClientDocument(int client_id)
        {
            ModelUploadClientDocument u = new ModelUploadClientDocument();
            u.ClientID = client_id;

            jobDbContext _db = new jobDbContext();
            var result = _db.ModelUploadClientDocument.SqlQuery(@"exec [usp_ClienrDocumentList]
                @ClientID",
                new SqlParameter("@ClientID", client_id)).ToList<ModelUploadClientDocument>();
            IEnumerable<ModelUploadClientDocument> data = result;
            TempData["clientid"] = client_id;
            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("UploadClientDocument", data)
                : View("UploadClientDocument", data);
        }


        [HttpPost]
        public object UploadClientDocument(HttpPostedFileBase file, ModelUploadClientDocument data, int? page, String Name)
        {
            data.ClientID = Convert.ToInt16(TempData["clientid"].ToString());
            string fName = "";
            try
            {
                fName = file.FileName;

                if (file != null && file.ContentLength > 0)
                {
                    string Fpath = "clientDocumentUpload/documents/D" + data.ClientID + "/";
                    var originalDirectory = new DirectoryInfo(string.Format("{0}clientDocumentUpload\\documents\\D" + data.ClientID, Server.MapPath(@"\")));
                    string pathString = System.IO.Path.Combine(originalDirectory.ToString());
                    var fileName1 = Path.GetFileName(fName);
                    bool isExists = System.IO.Directory.Exists(pathString);
                    if (!isExists) { System.IO.Directory.CreateDirectory(pathString); }

                    string extension = Path.GetExtension(file.FileName);

                    string FileSaveName = ("D" + data.ClientID + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + extension).ToString();
                    var path = string.Format("{0}\\{1}", pathString, FileSaveName);
                    file.SaveAs(path);

                    string ext = System.IO.Path.GetExtension(file.FileName);
                    data.FileName = FileSaveName;
                    data.ext = ext;
                    data.FolderPath = Fpath;
                    jobDbContext _db = new jobDbContext();
                    var result = _db.Database.ExecuteSqlCommand(@"exec [USP_UploadClientDocument] 
                        @ClientId,
                        @FolderPath,
                        @FileName,
                        @ext,
                        @uploadby   
                        ",
                       new SqlParameter("@ClientId", data.ClientID),
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
            StaticPagedList<ClinetMasterGridList> itemsAsIPagedList;
            itemsAsIPagedList = ClientGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("IndexForclient", itemsAsIPagedList)
                    : View("IndexForclient", itemsAsIPagedList);


        }
        
        public ActionResult UserPermissions()
        {
            jobDbContext _db = new jobDbContext();
            var result = _db.UserPermission.SqlQuery(@"exec [GetUSerList]").ToList<UserPermission>();
            IEnumerable<UserPermission> data = result;
            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("UserPermissions", data)
                : View("UserPermissions", data);
        }

        public ActionResult _UserPermissionGrid(int? userid)
        {
            jobDbContext _db = new jobDbContext();
            var result = _db.AddUserPermission.SqlQuery(@"exec GetUserPermissionDetail @userid",
               new SqlParameter("@userid",userid)).ToList<AddUserPermission>();
            IEnumerable<AddUserPermission> data = result;
            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("_UserPermissionGrid", data)
                : View("_UserPermissionGrid", data);
        }


        [HttpPost]
        public ActionResult SaveUserPermission(SaveUserAccess[] SaveUserAccess)
        {

            try
            {
                var result = 0;
                jobDbContext _db = new jobDbContext();
                foreach (var item in SaveUserAccess)
                {

                    SaveUserAccess O = new SaveUserAccess();

                    O.Userid = item.Userid;
                    O.Actionid = item.Actionid;
                    O.PermissionValue = item.PermissionValue;

                    result = _db.Database.ExecuteSqlCommand(@"exec USP_SaveUserPermission 
                    @UserId,@Actionid,@PermissionValue",
                    new SqlParameter("@UserId", O.Userid),
                    new SqlParameter("@Actionid", O.Actionid),
                    new SqlParameter("@PermissionValue", O.PermissionValue));

                }

                return Json("User Permission Saved");

            }

            catch (Exception ex)
            {
                string message = ex.Message;
                return Json(message);
            }


        }

        public ActionResult AddNewUser()
        {
            UserMasterList data = new UserMasterList();
            data.RoleID = 0;
            ViewData["RoleList"] = binddropdown("RoleName", 0);
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("AddNewUser", data)
                   : View("AddNewUser", data);
        }

        [HttpPost]
        public ActionResult DeleteClinet(int? clientid)
        {

            try
            {
                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_deleteClient @clinetid",
                new SqlParameter("@clinetid", clientid));

                return Json("Clients Deleted");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);

            }
            finally
            {

            }
        }

        
        public ActionResult DeleteUser(int? userid)
        {
            try
            {
                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_DeleteUSer 
                @userid",
                    new SqlParameter("@userid", userid)
            );

                return Json("User Deleted");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }

        }
    }
}