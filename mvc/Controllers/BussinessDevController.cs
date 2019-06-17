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
    public class BussinessDevController : Controller
    {
        // GET: BussinessDev
        //public ActionResult Index()
        //{
        //    return View();
        //}

        
        ///*********************************************************************************************/
        

        //[HttpPost]
        //public ActionResult LoadDataForBusinessDev(int? page, String Name)
        //{
        //    if (Session["UserName"] == null)
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }
        //    StaticPagedList<clientdetailsforbussinesdev> itemsAsIPagedList;
        //    itemsAsIPagedList = BusinessDevGridList(page, Name);

        //    return Request.IsAjaxRequest()
        //           ? (ActionResult)PartialView("BusinessDevList", itemsAsIPagedList)
        //           : View(itemsAsIPagedList);
        //}

        ////================================== Index page for Client of Business Developer  ===========================================

        //public ActionResult IndexForBusinessDev(int? page, String Name = null)
        //{
        //    FetchClientBD data = new FetchClientBD();

        //    StaticPagedList<clientdetailsforbussinesdev> itemsAsIPagedList;
        //    itemsAsIPagedList = BusinessDevGridList(page, Name);


        //    return Request.IsAjaxRequest()
        //            ? (ActionResult)PartialView("IndexForBusinessDev", itemsAsIPagedList)
        //            : View("IndexForBusinessDev", itemsAsIPagedList);


        //}

        ////===========================================================client Page Gride For Business Developer ==============================================

        //public ActionResult BusinessDevList(int? page, String Name = null)
        //{
        //    StaticPagedList<clientdetailsforbussinesdev> itemsAsIPagedList;
        //    itemsAsIPagedList = BusinessDevGridList(page, Name);

        //    return Request.IsAjaxRequest()
        //            ? (ActionResult)PartialView("BusinessDevList", itemsAsIPagedList)
        //            : View("BusinessDevList", itemsAsIPagedList);
        //}

        ////================================== Fill Client Grid Code For Business Developer ===========================================

        //public StaticPagedList<clientdetailsforbussinesdev> BusinessDevGridList(int? page, String Name = "")
        //{

        //    jobDbContext _db = new jobDbContext();
        //    var pageIndex = (page ?? 1);
        //    const int pageSize = 10;
        //    int totalCount = 10;
        //    clientdetailsforbussinesdev Ulist = new clientdetailsforbussinesdev();

        //    IEnumerable<clientdetailsforbussinesdev> result = _db.clientdetailsforbussinesdev.SqlQuery(@"exec USP_FetchClientraghu
        //           @pPageIndex, @pPageSize,@Name",
        //       new SqlParameter("@pPageIndex", pageIndex),
        //       new SqlParameter("@pPageSize", pageSize),
        //       new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
        //       ).ToList<clientdetailsforbussinesdev>();

        //    totalCount = 0;
        //    if (result.Count() > 0)
        //    {
        //        totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
        //    }
        //    var itemsAsIPagedList = new StaticPagedList<clientdetailsforbussinesdev>(result, pageIndex, pageSize, totalCount);
        //    return itemsAsIPagedList;

        //}

        //public ActionResult ClientDetails(int? Client_ID, int? page)
        //{

        //    StaticPagedList<clientdetailsforbussinesdev> itemsAsIPagedList;
        //    itemsAsIPagedList = clientsingle_details(Client_ID,page);

        //    return Request.IsAjaxRequest()
        //            ? (ActionResult)PartialView("ClientDetails", itemsAsIPagedList)
        //            : View("ClientDetails", itemsAsIPagedList);
                    

        //}



        //public StaticPagedList<clientdetailsforbussinesdev> clientsingle_details(int? Client_ID, int? page)
        //{

        //    jobDbContext _db = new jobDbContext();
        //    var pageIndex = (page ?? 1);
        //    const int pageSize = 10;
        //    int totalCount = 10;
        //    clientdetailsforbussinesdev Ulist = new clientdetailsforbussinesdev();

        //    IEnumerable<clientdetailsforbussinesdev> result = _db.clientdetailsforbussinesdev.SqlQuery(@"exec USP_FetchClientsingleDetail
        //          @Client_ID,@pPageIndex, @pPageSize",
        //       new SqlParameter("@Client_ID", Client_ID),
        //       new SqlParameter("@pPageIndex", pageIndex),
        //       new SqlParameter("@pPageSize", pageSize)

        //       ).ToList<clientdetailsforbussinesdev>();

        //    totalCount = 0;
        //    if (result.Count() > 0)
        //    {
        //        totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
        //    }
        //    var itemsAsIPagedList = new StaticPagedList<clientdetailsforbussinesdev>(result, pageIndex, pageSize, totalCount);
        //    return itemsAsIPagedList;

        //}

        
        //public ActionResult ContactPersonDetails(int? page, string ClientName = "")
        //{

        //    StaticPagedList<clientdetailsforcontactPerson> itemsAsIPagedList;
        //    itemsAsIPagedList = clientdetailsforconperson(page,ClientName);

        //    return Request.IsAjaxRequest()
        //            ? (ActionResult)PartialView("ContactPersonDetails", itemsAsIPagedList)
        //            : View("ContactPersonDetails", itemsAsIPagedList);


        //}

        //public StaticPagedList<clientdetailsforcontactPerson> clientdetailsforconperson(int? page, string ClientName = "")
        //{

        //    jobDbContext _db = new jobDbContext();
        //    var pageIndex = (page ?? 1);
        //    const int pageSize = 10;
        //    int totalCount = 10;
        //    clientdetailsforcontactPerson Ulist = new clientdetailsforcontactPerson();

        //    IEnumerable<clientdetailsforcontactPerson> result = _db.CDCP.SqlQuery(@"exec USP_FetchClientcontact_details
        //          @ClientName,@pPageIndex, @pPageSize",
        //       new SqlParameter("@ClientName", ClientName),
        //       new SqlParameter("@pPageIndex", pageIndex),
        //       new SqlParameter("@pPageSize", pageSize)
               
        //       ).ToList<clientdetailsforcontactPerson>();

        //    totalCount = 0;
        //    if (result.Count() > 0)
        //    {
        //        totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
        //    }
        //    var itemsAsIPagedList = new StaticPagedList<clientdetailsforcontactPerson>(result, pageIndex, pageSize, totalCount);
        //    return itemsAsIPagedList;

        //}

        
        //public ActionResult CllientLocationDetails(int? page ,String ClientName = "")
        //{

        //    StaticPagedList<clientLocationdetails> itemsAsIPagedList;
        //    itemsAsIPagedList = CllientLocationDetailsList(page, ClientName);

        //    return Request.IsAjaxRequest()
        //            ? (ActionResult)PartialView("CllientLocationDetails", itemsAsIPagedList)
        //            : View("CllientLocationDetails", itemsAsIPagedList);
            
        //}

        //public StaticPagedList<clientLocationdetails> CllientLocationDetailsList(int? page, String ClientName = "")
        //{

        //    jobDbContext _db = new jobDbContext();
        //    var pageIndex = (page ?? 1);
        //    const int pageSize = 10;
        //    int totalCount = 10;
        //    clientLocationdetails Ulist = new clientLocationdetails();

        //    IEnumerable<clientLocationdetails> result = _db.clientLocationdetails.SqlQuery(@"exec USP_FetchClientLocation_details
        //          @ClientName,@pPageIndex, @pPageSize",
        //       new SqlParameter("@ClientName", ClientName),
        //       new SqlParameter("@pPageIndex", pageIndex),
        //       new SqlParameter("@pPageSize", pageSize)

        //       ).ToList<clientLocationdetails>();

        //    totalCount = 0;
        //    if (result.Count() > 0)
        //    {
        //        totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
        //    }
        //    var itemsAsIPagedList = new StaticPagedList<clientLocationdetails>(result, pageIndex, pageSize, totalCount);
        //    return itemsAsIPagedList;

        //}

        public ActionResult Logout()
        {
            Session["UserName"] = null;
            return RedirectToAction("Login", "Home");
        }
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

        [HttpPost]
        public ActionResult LoadDataForBusinessDev(int? page, String Name)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            StaticPagedList<ClientGrid> itemsAsIPagedList;
            itemsAsIPagedList = BusinessDevGridList(page, Name);

            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("BusinessDevList", itemsAsIPagedList)
                   : View(itemsAsIPagedList);
        }

        //================================== Index page for Client of Business Developer  ===========================================

        public ActionResult IndexForBusinessDev(int? page, String Name = null)
        {
            StaticPagedList<ClientGrid> itemsAsIPagedList;
            itemsAsIPagedList = BusinessDevGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("IndexForBusinessDev", itemsAsIPagedList)
                    : View("IndexForBusinessDev", itemsAsIPagedList);
        }


        public ActionResult BusinessDevList(int? page, String Name = null)
        {
            StaticPagedList<ClientGrid> itemsAsIPagedList;
            itemsAsIPagedList = BusinessDevGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("BusinessDevList", itemsAsIPagedList)
                    : View("BusinessDevList", itemsAsIPagedList);
        }

        //================================== Fill Client Grid Code For Business Developer ===========================================

        public StaticPagedList<ClientGrid> BusinessDevGridList(int? page, String Name = "")
        {

            jobDbContext _db = new jobDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            ClientGrid Ulist = new ClientGrid();

            IEnumerable<ClientGrid> result = _db.ClientGrid.SqlQuery(@"exec USP_FetchClient
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<ClientGrid>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<ClientGrid>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }

        public ActionResult ClientDetails(int? client_id)
        {
            try
            {
                BusinessDev data = new BusinessDev();

                jobDbContext _db = new jobDbContext();


                var result = _db.clientdetails.SqlQuery(@"exec Usp_GetClientDetails
                @Client_ID",
                 new SqlParameter("@Client_ID", client_id)).ToList<BusinessDev>();

                data = result.FirstOrDefault();
                if (data == null)
                {
                    ViewBag.Javascript = "<script language='javascript' type='text/javascript'>alert('Data Already Exists');</script>";
                    return RedirectToAction("IndexForBusinessDev", "BussinessDev");

                }
                return Request.IsAjaxRequest()
                     ? (ActionResult)PartialView("ClientDetails", data)
                     : View("ClientDetails", data);
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return RedirectToAction("ClientDetails");

            }
            finally
            {

            }

        }


        //public ActionResult Loc_ContListBD(int? client_id)
        //{
        //    try
        //    {
        //        FetchClientBD data = new FetchClientBD();

        //        jobDbContext _db = new jobDbContext();


        //        var result = _db.ClientBDList.SqlQuery(@"exec USP_GetClientDB
        //        @Client_ID",
        //         new SqlParameter("@Client_ID", client_id)).ToList<FetchClientBD>();

        //        data = result.FirstOrDefault();
        //        if (data == null)
        //        {
        //            ViewBag.Javascript = "<script language='javascript' type='text/javascript'>alert('Data Already Exists');</script>";
        //            return RedirectToAction("IndexForBusinessDev", "Master");

        //        }
        //        return Request.IsAjaxRequest()
        //      ? (ActionResult)PartialView("Loc_ContListBD", data)
        //      : View("Loc_ContListBD", data);
        //    }
        //    catch (Exception ex)
        //    {

        //        string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
        //        return RedirectToAction("Loc_ContListBD");

        //    }
        //    finally
        //    {

        //    }

        //}
        /*******************************Contact List on Client details *********************************/
        public ActionResult Loc_ContListBD(int? page, int? client_id)
        {

            StaticPagedList<FetchClientBD> itemsAsIPagedList;
            itemsAsIPagedList = Loc_ContGridList(page, client_id);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("Loc_ContListBD", itemsAsIPagedList)
                    : View("Loc_ContListBD", itemsAsIPagedList);

        }


        //================================== Fill Client Grid Code For Client Details in BD ===========================================

        public StaticPagedList<FetchClientBD> Loc_ContGridList(int? page, int? client_id)
        {

            jobDbContext _db = new jobDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 5;
            int totalCount = 5;
            FetchClientBD Ulist = new FetchClientBD();

            IEnumerable<FetchClientBD> result = _db.ClientBDList.SqlQuery(@"exec USP_FetchClientcontact_details
                   @Client_ID,@pPageIndex, @pPageSize",
                    new SqlParameter("@Client_ID", client_id),
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize)

               ).ToList<FetchClientBD>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<FetchClientBD>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }

        public ActionResult Address(int? client_id)
        {
            try
            {
                BusinessDev data = new BusinessDev();

                jobDbContext _db = new jobDbContext();


                var result = _db.clientdetails.SqlQuery(@"exec Usp_GetClientDetails
                @Client_ID",
                 new SqlParameter("@Client_ID", client_id)).ToList<BusinessDev>();

                data = result.FirstOrDefault();
                if (data == null)
                {
                    ViewBag.Javascript = "<script language='javascript' type='text/javascript'>alert('Data Already Exists');</script>";
                    return RedirectToAction("IndexForBusinessDev", "BussinessDev");

                }
                return Request.IsAjaxRequest()
                     ? (ActionResult)PartialView("Address", data)
                     : View("Address", data);
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return RedirectToAction("Address");

            }
            finally
            {

            }

        }       

        public ActionResult ContactPersonDetails( int? client_id)
        {

            ClientContactDetails data = new ClientContactDetails();
            jobDbContext _db = new jobDbContext();
            var result = _db.CCList.SqlQuery(@"exec GetClientContactDetails 
                @client_id",
                new SqlParameter("@client_id", client_id)).ToList<ClientContactDetails>();

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("ContactPersonDetails", result)
                : View("ContactPersonDetails", data);

        }

        public ActionResult CllientLocationDetails(int? page, int? client_id)
        {

            StaticPagedList<clientLocationdetails> itemsAsIPagedList;
            itemsAsIPagedList = CllientLocationDetailsList(page, client_id);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("CllientLocationDetails", itemsAsIPagedList)
                    : View("CllientLocationDetails", itemsAsIPagedList);

        }

        public StaticPagedList<clientLocationdetails> CllientLocationDetailsList(int? page, int? client_id)
        {

            jobDbContext _db = new jobDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            clientLocationdetails Ulist = new clientLocationdetails();

            IEnumerable<clientLocationdetails> result = _db.clientLocationdetails.SqlQuery(@"exec USP_FetchClientLocation_details
                  @ClientId,@pPageIndex, @pPageSize",
               new SqlParameter("@ClientId", client_id),
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize)

               ).ToList<clientLocationdetails>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<clientLocationdetails>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }

        //public ActionResult ContactPersonDetails(int? page, int? client_id)
        //{

        //    StaticPagedList<clientdetailsforcontactPerson> itemsAsIPagedList;
        //    itemsAsIPagedList = clientdetailsforconperson(page, client_id);

        //    return Request.IsAjaxRequest()
        //            ? (ActionResult)PartialView("ContactPersonDetails", itemsAsIPagedList)
        //            : View("ContactPersonDetails", itemsAsIPagedList);


        //}

        //public StaticPagedList<clientdetailsforcontactPerson> clientdetailsforconperson(int? page, int? client_id)
        //{

        //    jobDbContext _db = new jobDbContext();
        //    var pageIndex = (page ?? 1);
        //    const int pageSize = 10;
        //    int totalCount = 10;
        //    clientdetailsforcontactPerson Ulist = new clientdetailsforcontactPerson();

        //    IEnumerable<clientdetailsforcontactPerson> result = _db.clientdetailsforcontactPerson.SqlQuery(@"exec USP_FetchClientcontact_details
        //          @ClientId,@pPageIndex, @pPageSize",
        //       new SqlParameter("@ClientId", client_id),
        //       new SqlParameter("@pPageIndex", pageIndex),
        //       new SqlParameter("@pPageSize", pageSize)

        //       ).ToList<clientdetailsforcontactPerson>();

        //    totalCount = 0;
        //    if (result.Count() > 0)
        //    {
        //        totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
        //    }
        //    var itemsAsIPagedList = new StaticPagedList<clientdetailsforcontactPerson>(result, pageIndex, pageSize, totalCount);
        //    return itemsAsIPagedList;

        //}

        //================================== Edit client Code For Business Developer===========================================

        //================================== Edit client Code For Business Developer===========================================

        public ActionResult _EditClientBusiness()
        {
            EditClient data = new EditClient();
            ViewData["BusinessTypeList"] = binddropdown("BusinessTypeList", 0);
            ViewData["LocationList"] = binddropdown("LocationList", 0);
            ViewData["ClientTypeList"] = binddropdown("ClientTypeList", 0);
            return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("_EditClientBusiness", data)
                   : View("_EditClientBusiness", data);
        }

        //================================== Fetch client details for update Code For Business Developer===========================================

        public ActionResult FetchClinetBusiness(int client_id)
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
                       ? (ActionResult)PartialView("_EditClientBusiness", data)
                       : View("_EditClientBusiness", data);
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return RedirectToAction("_EditClientBusiness");

            }
            finally
            {

            }
        }

        //  =========================================== Update client Code For Business Developer=============================================

        [HttpPost]
        public ActionResult Edit_ClientBusiness(EditClient rs)
        {
            try
            {

                jobDbContext _db = new jobDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec [USP_UpdateClientMaster] 
                @client_id,@Client_Name,@Client_Type_id,@location_id,@address,@DID_no,@fax_no,@Manifactring,@products,@compitators,@buissType_id,@update_date",
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
                new SqlParameter("@update_date", DateTime.Now)
            );
                return Json("IndexForBusinessDev");
            }

            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForBusinessDev", message);

            }

        }




        public ActionResult Icon(int client_id)
        {
            ClientGrid data = new ClientGrid();
            data.client_id = client_id;
            return PartialView("Icon", data);

        }

        public ActionResult Icon1()
        {
            return PartialView();

        }
    }
}