using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;
using PagedList;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Configuration;
using System.Web.UI;


namespace mvc.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        
        public ActionResult AdminIndex()
        {
            return View();
        }


       public ActionResult DashboardCityData()
        {
            using (jobDbContext context = new jobDbContext())
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                var conn = context.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open) conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "GetDashboardCityData";

                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(new SqlParameter("@suppliername", suppliername));
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // error handling
                    var messege = ex.Message;
                }
                finally
                {
                    if (connectionState != ConnectionState.Closed) conn.Close();
                }
                // TempData["Data"] = dt;

                return Request.IsAjaxRequest()
                      ? (ActionResult)PartialView("DashboardCityData", dt)
                      : View("DashboardCityData", dt);
            }
        }

        public ActionResult ColumnChart()
        {

            jobDbContext _db = new jobDbContext();

            IEnumerable<StackedChartData> result = _db.StackedChartData.SqlQuery(@"exec GetSalesChart").ToList<StackedChartData>();

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("ColumnChart", result)
                    : View("ColumnChart", result);           
        }

        public ActionResult DashboardData(int? Locationid , string sectorname = "")
        {
            jobDbContext _db = new jobDbContext();

            IEnumerable<DashbardData> result = _db.DashbardData.SqlQuery(@"exec GetDashbordPositionData @location_id, @Sector_name",
            new SqlParameter("@location_id", Locationid),
            new SqlParameter("@Sector_name", sectorname)).ToList<DashbardData>();
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("DashboardData", result)
                    : View("DashboardData", result);
            //return View(StackedChartData.GetData());
        }
               

        public ActionResult SharePosition(int? Req_id)
        {
            jobDbContext _db = new jobDbContext();

            IEnumerable<Userdetailsforpositionshare> result = _db.Userdetailsforpositionshare.SqlQuery(@"exec [GetUserDetails] @Req_id",
               new SqlParameter("@Req_id", Req_id == null ? (object)DBNull.Value : Req_id)).ToList<Userdetailsforpositionshare>();

            Session["reqid"] = Req_id;
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("SharePosition", result)
                    : View("SharePosition", result);
        }

        //public ActionResult PositionDataDetails(int? Locationid,string groupid = "")
        //{
        //    jobDbContext _db = new jobDbContext();

        //    IEnumerable<DashbardData> result = _db.DashbardData.SqlQuery(@"exec GetDashbordPositionData").ToList<DashbardData>();

        //    return Request.IsAjaxRequest()
        //            ? (ActionResult)PartialView("DashboardData", result)
        //            : View("DashboardData", result);
        //}
    }
}