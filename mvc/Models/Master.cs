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
    public class Master
    {

    }
    
    public class DesignationList
    {
        [Key]
        public int desig_id { get; set; }
        IEnumerable<DesignationList> Reqlist { get; set; }
        public String desg_desc { get; set; }
        public int? TotalRows { get; set; }
       
    }
    public class DesignationDetails
    {
        [Key]
        public int desig_id { get; set; }
        IEnumerable<DesignationDetails> Reqlist { get; set; }
        public String desg_desc { get; set; }
    }

   
    public class UserMasterGridList
    {
        [Key]
        public int User_id { get; set; }
        IEnumerable<UserMasterGridList> Userlist { get; set; }
        public String User_Name { get; set; }
        public String Full_Name { get; set; }
        public String phone { get; set; }
        public String Mobile { get; set; }
        public String Password { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String emailId { get; set; }
        public int RoleID { get; set; }
        public String RoleName { get; set; }
        public System.Nullable<int> Isactive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? TotalRows { get; set; }

    }

    public class UserMasterList
    {
        [Key]
        public int User_id { get; set; }
        IEnumerable<UserMasterList> Userlist { get; set; }
        public String User_Name { get; set; }
        public String F_Name { get; set; }
        public String M_Name { get; set; }
        public String L_Name { get; set; }
        public String phone { get; set; }
        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "The Mobile number is required")]
        [EmailAddress(ErrorMessage = "Invalid Mobile number")]
        public String Mobile { get; set; }
        public String Password { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String emailId { get; set; }
        public int RoleID { get; set; }
        public String RoleName { get; set; }
        public System.Nullable<int> Isactive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? TotalRows { get; set; }

    }

    public class UserDetails
    {
        [Key]
        public int User_id { get; set; }
        IEnumerable<UserMasterList> Userlist { get; set; }
        public String User_Name { get; set; }
        public String F_Name { get; set; }
        public String M_Name { get; set; }
        public String L_Name { get; set; }
        public String phone { get; set; }
        public String Mobile { get; set; }
        public String Password { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String emailId { get; set; }
        public String RoleName { get; set; }
        public int RoleID { get; set; }
        public System.Nullable<int> Isactive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
      
    }
    

    public class ClinetMasterGridList
    {
         [Key]
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
        public String CompanyProfile { get; set; }
        public String Website { get; set; }
        public String BuisinessType { get; set; }
        public int? TotalRows { get; set; }

    }

    public partial class ModelUploadClientDocument
    {
        [Key]
        public int Document_id { get; set; }
        public int ClientID { get; set; }
        public string FolderPath { get; set; }
        public string FileName { get; set; }
        public string ext { get; set; }
        public DateTime? UploadDate { get; set; }
        public int UploadBy { get; set; }
    }


    public class EditClient
    {
        [Key]
        public int client_id { get; set; }
        public String Client_Name { get; set; }
        public int? Client_Type_id { get; set; }
        public int? location_id { get; set; }
        //public String client_profile { get; set; }
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
        public String loc_desc { get; set; }
        public String ct_description { get; set; }
        public String CompanyProfile { get; set; }
        public String Website { get; set; }


    }


    public class AddContactPerson
    {
        [Key]
        public int clientCD_ID { get; set; }
        public int? client_id { get; set; }
        public string contactPerson_Name { get; set; }
        public int cp_desig_Id { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string mobile { get; set; }
        public string description { get; set; }
        public int user_id { get; set; }
        public DateTime update_date { get; set; }
        public int addedBy { get; set; }
        public string emailid { get; set; }
        public String desg_desc { get; set; }
    }

    public class ClientContactDetails
    {
        [Key]
        public int clientCD_ID { get; set; }
        public int client_id { get; set; }
        public string contactPerson_Name { get; set; }
        public int cp_desig_Id { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string mobile { get; set; }
        public string description { get; set; }
        public int user_id { get; set; }
        public DateTime update_date { get; set; }
        public int addedBy { get; set; }
        public string emailid { get; set; }
        public string desg_desc { get; set; }
        public string Client_Name { get; set; }
    }


    public class ClientContactDetailsEdit
    {
        [Key]
        public int clientCD_ID { get; set; }
        public int client_id { get; set; }
        public string contactPerson_Name { get; set; }
        public int cp_desig_Id { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string mobile { get; set; }
        public string description { get; set; }
        public int user_id { get; set; }
        public DateTime update_date { get; set; }
        public int addedBy { get; set; }
        public string emailid { get; set; }
        public string desg_desc { get; set; }

    }

    public class GetRoleUser
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string term { get; set; }
        
    }

    public class MailTemplate
    {
        [Key]
        public int temp_id  { get; set; }
        public string temp_title  { get; set; }
        public string email_subject { get; set; }
        [AllowHtml]
        [Display(Name = "Message")]
        public string emailBody { get; set; }
        public int? lastupdatedBy { get; set; }
        public int?  addedBy { get; set; }
        public DateTime update_date { get; set; }
        public int? TotalRows { get; set; }

    }

    public class MailTemplateForUpdate
    {
        [Key]
        public int temp_id { get; set; }
        public string temp_title { get; set; }
        public string email_subject { get; set; }
        [AllowHtml]
        [Display(Name = "Message")]
        public string emailBody { get; set; }
      
      
    }

    public class ClientLocation
    {
        [Key]
        public int ClientId     { get; set; }
        public int LocationId   { get; set; }
        public string ContactNo { get; set; }

    }
}