using mvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login L)
        {
            jobDbContext _db = new jobDbContext();
            var result = _db.LoginDetail.SqlQuery(@"exec usp_login 
                @User_Name,@Password",
                new SqlParameter("@User_Name", L.UserName),
                new SqlParameter("@Password", L.Password)).ToList<LoginDetail>();
            LoginDetail data = new LoginDetail();
            data = result.FirstOrDefault();

            if (data == null)
            { 
                    ViewBag.error = "Please enter valid user Name password";
            }
            else
            {
                Session["UserName"] = data.User_Name;
                Session["User_id"] = data.User_id;
                Session["RoleName"] = data.RoleName;
                Session["RoleID"] = data.RoleID;

                setPermissionData(data.User_id); //set user permission to session using data table 

                if (data.RoleName == "Business development")
                {
                    return RedirectToAction("IndexForBusinessDev", "BussinessDev");

                }
                else if(data.RoleID == 7)
                {
                    return RedirectToAction("AdminIndex", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Placement");
                }
            } 
           
            return View();
        }


        public void setPermissionData(int userid)
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
                        cmd.CommandText = "GetUserPermissionDetail";

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@userid", userid));
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    Session["UserPermission"] = dt;
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
                              
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}