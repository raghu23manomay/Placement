using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Metadata;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Mvc;


namespace mvc.Models
{
    public class MRecruiter
    {
    }
    public partial class RecruiterList
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Summery { get; set; }
        public string TotalYearsOfExp { get; set; }
        public string Location { get; set; }
        public string Graduation { get; set; }
        public string PG { get; set; }
        public string CurrentlyWorkingWith { get; set; }
        public string CurrentPosition { get; set; }
        public string CurrentDetailProfile { get; set; }
        public string LastWorkingWith { get; set; }
        public string LastPosition { get; set; }
        public string LastDetailProfile { get; set; }
        public string KeyArea { get; set; }
        public string IndustrySpecialisation { get; set; }
        public string VerticalSpecalization { get; set; }
        public string Expertise { get; set; }
        public string Achivement { get; set; }
        public string LanguagesKnow { get; set; }
        public string ProflieImg { get; set; }
        public String User_Name { get; set; }
        public String F_Name { get; set; }
        public String M_Name { get; set; }
        public String L_Name { get; set; }
        public String phone { get; set; }
        public String Mobile { get; set; }
        public String Password { get; set; }
        public String emailId { get; set; }
        public int? location_id { get; set; }  
        public string VerticalId { get; set; }
        public List<RecruiterList> listboz { get; set; }

    }
}