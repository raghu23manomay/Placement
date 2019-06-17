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
   
    public class BusinessDev
    {
        [Key]
        public int client_id { get; set; }
        public string Client_Name { get; set; }
        //public string client_profile { get; set; }
        public string address { get; set; }
        public string BuisinessType { get; set; }

    }

    public partial class ClientGrid
    {
    [Key]
    public int client_id { get; set; }
    public string Client_Name { get; set; }
    public int buissType_id { get; set; }
    public string BuisinessType { get; set; }
    public int? TotalRows { get; set; }
    }
    //public class FetchClientBD
    //{
    //    [Key]
    //    public string Client_Name { get; set; }
    //    public string address { get; set; }
    //    public string contactPerson_Name { get; set; }
    //    public string mobile { get; set; }

    //}

    //public class clientdetailsforbussinesdev
    //{

    //      [Key]
    //        public int client_id { get; set; }
    //        IEnumerable<ClinetMasterGridList> Clientlist { get; set; }
    //        public String Client_Name { get; set; }
    //        public int? Client_Type_id { get; set; }
    //        public String ct_description { get; set; }
    //        public String loc_desc { get; set; }
    //        public int? location_id { get; set; }
    //        public String client_profile { get; set; }
    //        public String address { get; set; }
    //        public String DID_no { get; set; }
    //        public String fax_no { get; set; }
    //        public String Manifactring { get; set; }
    //        public String Prod_processes { get; set; }
    //        public String products { get; set; }
    //        public String compitators { get; set; }
    //        public String specification { get; set; }
    //        public int? user_id { get; set; }
    //        public DateTime update_date { get; set; }
    //        public int? Ref_Inquiry_id { get; set; }
    //        public String Client_Followup { get; set; }
    //        public String Meeting_Followup { get; set; }
    //        public int? Added_By { get; set; }
    //        public DateTime last_Tel_Follw_Date { get; set; }
    //        public DateTime next_Tel_Follw_Date { get; set; }
    //        public DateTime last_meet_Follw_Date { get; set; }
    //        public DateTime next_meet_Follw_Date { get; set; }
    //        public int? assigned_C_Support_userid { get; set; }
    //        public int? assigned_B_Office_userid { get; set; }
    //        public int? assignedBy { get; set; }
    //        public int? buissType_id { get; set; }
    //        public String BuisinessType { get; set; }
    //        //[Key]
    //        //[Column(Order = 1)]
    //        //public int clientCD_ID { get; set; }
    //        public String contactPerson_Name { get; set; }
    //        public String mobile { get; set; }
    //        public String emailid { get; set; }
    //        public int? TotalRows { get; set; }



    //}


    //public class clientdetailsforcontactPerson
    // {

    //    [Key]
    //    public int clientCD_ID { get; set; }
    //    public String contactPerson_Name { get; set; }
    //    public int client_id { get; set; }
    //    IEnumerable<ClinetMasterGridList> Clientlist { get; set; }
    //    public String Client_Name { get; set; }
    //    public int? Client_Type_id { get; set; }
    //    public String ct_description { get; set; }
    //    public String loc_desc { get; set; }
    //    public int? location_id { get; set; }
    //    public String client_profile { get; set; }
    //    public String address { get; set; }
    //    public String DID_no { get; set; }
    //    public String fax_no { get; set; }
    //    public String Manifactring { get; set; }
    //    public String Prod_processes { get; set; }
    //    public String products { get; set; }
    //    public String compitators { get; set; }
    //    public String specification { get; set; }
    //    public int? user_id { get; set; }
    //    public DateTime update_date { get; set; }
    //    public int? Ref_Inquiry_id { get; set; }
    //    public String Client_Followup { get; set; }
    //    public String Meeting_Followup { get; set; }
    //    public int? Added_By { get; set; }
    //    public DateTime last_Tel_Follw_Date { get; set; }
    //    public DateTime next_Tel_Follw_Date { get; set; }
    //    public DateTime last_meet_Follw_Date { get; set; }
    //    public DateTime next_meet_Follw_Date { get; set; }
    //    public int? assigned_C_Support_userid { get; set; }
    //    public int? assigned_B_Office_userid { get; set; }
    //    public int? assignedBy { get; set; }
    //    public int? buissType_id { get; set; }
    //    public String BuisinessType { get; set; }
    //    //[Key]
    //    //[Column(Order = 1)]
    //    //public int clientCD_ID { get; set; }

    //    public String mobile { get; set; }
    //    public String emailid { get; set; }
    //    public int? TotalRows { get; set; }

    //}

    //public class clientLocationdetails
    //{

    //    [Key]
    //    public int client_id { get; set; }
    //    public String DID_no { get; set; }
    //    IEnumerable<ClinetMasterGridList> Clientlist { get; set; }
    //    public String Client_Name { get; set; }
    //    public String loc_desc { get; set; }
    //    public int? location_id { get; set; }
    //    public int? TotalRows { get; set; }

    //}

    public class FetchClientBD
    {

        [Key]
        public int clientCD_ID { get; set; }
        public String contactPerson_Name { get; set; }
        public int client_id { get; set; }
        IEnumerable<ClinetMasterGridList> Clientlist { get; set; }
        public String Client_Name { get; set; }
        public int? Client_Type_id { get; set; }
        public String ct_description { get; set; }
        public String loc_desc { get; set; }
        public int? location_id { get; set; }
        public String client_profile { get; set; }
        public String address { get; set; }
        public String DID_no { get; set; }
        public String fax_no { get; set; }
        public String Manifactring { get; set; }
        public String Prod_processes { get; set; }
        public String products { get; set; }
        public String compitators { get; set; }
        public String specification { get; set; }
        public int? user_id { get; set; }
        public DateTime update_date { get; set; }
        public int? Ref_Inquiry_id { get; set; }
        public String Client_Followup { get; set; }
        public String Meeting_Followup { get; set; }
        public int? Added_By { get; set; }
        public DateTime last_Tel_Follw_Date { get; set; }
        public DateTime next_Tel_Follw_Date { get; set; }
        public DateTime last_meet_Follw_Date { get; set; }
        public DateTime next_meet_Follw_Date { get; set; }
        public int? assigned_C_Support_userid { get; set; }
        public int? assigned_B_Office_userid { get; set; }
        public int? assignedBy { get; set; }
        public int? buissType_id { get; set; }
        public String BuisinessType { get; set; }
        //[Key]
        //[Column(Order = 1)]
        //public int clientCD_ID { get; set; }

        public String mobile { get; set; }
        public String emailid { get; set; }
        public int? TotalRows { get; set; }
    }


    public class clientLocationdetails
    {

        [Key]
        public int? location_id { get; set; }
        public int client_id { get; set; }
        public String DID_no { get; set; }
        IEnumerable<ClinetMasterGridList> Clientlist { get; set; }
        public String Client_Name { get; set; }
        public String loc_desc { get; set; }        
        public int? TotalRows { get; set; }

    }

    public class clientdetailsforcontactPerson
    {

        [Key]
        public int clientCD_ID { get; set; }
        public String contactPerson_Name { get; set; }
        public int client_id { get; set; }
        IEnumerable<ClinetMasterGridList> Clientlist { get; set; }
        public String Client_Name { get; set; }
        public int? Client_Type_id { get; set; }
        public String ct_description { get; set; }
        public String loc_desc { get; set; }
        public int? location_id { get; set; }
        
        public String address { get; set; }
        public String DID_no { get; set; }
        public String fax_no { get; set; }
        public String Manifactring { get; set; }
        public String Prod_processes { get; set; }
        public String products { get; set; }
        public String compitators { get; set; }
        public String specification { get; set; }
        public int? user_id { get; set; }
        public DateTime update_date { get; set; }
        public int? Ref_Inquiry_id { get; set; }
        public String Client_Followup { get; set; }
        public String Meeting_Followup { get; set; }
        public int? Added_By { get; set; }
        public DateTime last_Tel_Follw_Date { get; set; }
        public DateTime next_Tel_Follw_Date { get; set; }
        public DateTime last_meet_Follw_Date { get; set; }
        public DateTime next_meet_Follw_Date { get; set; }
        public int? assigned_C_Support_userid { get; set; }
        public int? assigned_B_Office_userid { get; set; }
        public int? assignedBy { get; set; }
        public int? buissType_id { get; set; }
        public String BuisinessType { get; set; }
        //[Key]
        //[Column(Order = 1)]
        //public int clientCD_ID { get; set; }

        public String mobile { get; set; }
        public String emailid { get; set; }
        public int? TotalRows { get; set; }

    }


}