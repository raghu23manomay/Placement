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
    public partial class CandidateList
    {
        [Key]
        public int candID { get; set; }
        public int StageID { get; set; }
        public int ReqID { get; set; }
        public string Name { get; set; }
        public string address { get; set; }
        public string emailid { get; set; }
        public string StageName { get; set; }
        public string mobile { get; set; }
        IEnumerable<CandidateList> clist { get; set; }
        public int? TotalRows { get; set; }
        public DateTime addeddate { get; set; }
        public string Designation { get; set; }
        public string Client_Name { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDate { get; set; }
        public string ModeName { get; set; }
        public string feedback { get; set; }
        public string InterDescription { get; set; }
        public int? addedby { get; set; }
        public string UserName { get; set; }
        public int? statusid { get; set; }


    }
    public class WorkFLowList
    {
        [Key]
        public int ReqID { get; set; }
        public string designation { get; set; }
        public string req_title { get; set; }
        public string Description { get; set; }
        public int? contactPerson { get; set; }
        public int desig_id { get; set; }
        IEnumerable<WorkFLowList> Reqlist { get; set; }
        public int? TotalRows { get; set; }
        public String addeddate { get; set; }
        public string email_id { get; set; }
        public string mobile { get; set; }
        public int Sourced { get; set; }
        public int Applied { get; set; }
        public int Rejected { get; set; }
        public int Interview { get; set; }
        public int Offer { get; set; }
        public int Selected { get; set; }
        public int Client_ID { get; set; }
        public string Client_Name { get; set; }
        public string contactPerson_Name { get; set; }
        public string c_mobile { get; set; }
        public string Location_details { get; set; }
        public int Attended { get; set; }
        public int NotAttended { get; set; }
        public int Pending { get; set; }
        public int InDescision { get; set; }
        public int? PositionStatusId { get; set; }

    }
    public partial class Candidate
    {
        [Key]
        public int candID { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string Designation { get; set; }
        public int? desig_id { get; set; }
        public string MobileNo { get; set; }
        public string MobileNo2 { get; set; }
        public int Client_id { get; set; }
        public int req_id { get; set; }
        public string SkypeID { get; set; }
        public string DomainName { get; set; }
        public string Location { get; set; }
        public int? location_id { get; set; }
        public string Current_Organization { get; set; }
        public string Current_Position { get; set; }
        public int? Current_Location { get; set; }
        public int? Current_Location_id { get; set; }
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? working_from_Date { get; set; }
        public string Qualification { get; set; }
        public string Qualification_PG { get; set; }
        public string Total_Exp { get; set; }
        public string currently_drawn_salary { get; set; }
        public string expected_salary { get; set; }
        public string Notice_period { get; set; }
        public string req_title { get; set; }
        public int? addedby { get; set; }

        [Key]
        public int Document_id { get; set; }
        public string FolderPath { get; set; }
        public string FileName { get; set; }
        public string ext { get; set; }
        public DateTime? UploadDate { get; set; }
        public int UploadBy { get; set; }

    }

    public partial class FetchcandidatwForCandidate
    {
        [Key]
        public Int64 candID               { get; set; }
        public string Name                 { get; set; }
        public int CurrentReqId { get; set; }
        public string mobile               { get; set; }
        public string emailid              { get; set; }
        public string MobileNo2            { get; set; }
        public string SkypeID              { get; set; }
        public string Current_Organization { get; set; }
        public string Current_Position     { get; set; }
        public int location_id          { get; set; }
        public int Current_Location_id  { get; set; }
        public DateTime? working_from_Date    { get; set; }
        public string Qualification        { get; set; }
        public string Qualification_PG { get; set; }
        public string Total_Exp { get; set; }
        public string currently_drawn_salary { get; set; }
        public string expected_salary { get; set; }
        public string Notice_period { get; set; }
        public IEnumerable<Notes> Notes { get; set; }
        public int? temp_id { get; set; }
        public mail mail { get; set; }
                                 

    }

    public class CandidateDetails
    {
        [Key]
        public int candID { get; set; }
        public string Name { get; set; }
        public string address { get; set; }
        public string emailid { get; set; }
        public string mobile { get; set; }
        public DateTime addeddate { get; set; }
        public string Designation { get; set; }
        public int? desig_id { get; set; }
        public string experience { get; set; }
        public string skilset { get; set; }
        public string description { get; set; }
        [AllowHtml]
        [Display(Name = "Message")]
        public string Education { get; set; }
        public IEnumerable<Notes> Notes { get; set; }
        public mail mail { get; set; }
        public string Client_Name { get; set; }
        public string contactPerson { get; set; }
        public string SkypeID { get; set; }
        public string mobile2 { get; set; }

        public string Location { get; set; }
        public int? location_id { get; set; }
        public string Current_Organization { get; set; }
        public string Current_Position { get; set; }
        public string Current_Location { get; set; }
        public int? Current_Location_id { get; set; }

        public DateTime? working_from_Date { get; set; }
        public string Qualification { get; set; }
        public string Qualification_PG { get; set; }
        public string Total_Exp { get; set; }
        public string currently_drawn_salary { get; set; }
        public string expected_salary { get; set; }
        public string Notice_period { get; set; }
        public string ModeName { get; set; }
        public string CandMobile { get; set; }
        public string ActivityDate { get; set; }
        public string ActivityTime { get; set; }
        public int? temp_id { get; set; }
        public String CompanyProfile { get; set; }
        public String Website { get; set; }
        public String ClientAdress { get; set; }


    }

    public partial class Requirement
    {
        [Key]
        public int ReqID { get; set; }
        public int Position { get; set; }
        public string ReqTitle { get; set; }
        public string ClientName { get; set; }
        public int Client_id { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Designation { get; set; }
        public int desig_id { get; set; }
        public String SalMax { get; set; }
        public String SalMin { get; set; }
        public String ExpMax { get; set; }
        public String ExpMin { get; set; }
        public int? ContactPerson { get; set; }
        public string contactPerson_Name { get; set; }
        public int Design_GroupID { get; set; }

        public string PositionLevel { get; set; }
        public string Location { get; set; }
        public int? location_id { get; set; }
        public string MinimumQulification { get; set; }
        public string RoleResp { get; set; }
        public string KnowledgeSkill { get; set; }
        public int? Age { get; set; }
        public string NoticePeriod { get; set; }
        public int? price { get; set; }
        public int? SectorId { get; set; }



    }
    public partial class RequirementDetails
    {
        [Key]
        public int req_id { get; set; }
        public int position { get; set; }
        public string req_title { get; set; }
        public string ClientName { get; set; }
        public int client_id { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Designation { get; set; }
        public int desig_id { get; set; }
        public String sal_Max { get; set; }
        public String sal_Min { get; set; }
        public String exp_Max { get; set; }
        public String exp_Min { get; set; }
        public int? contactPerson { get; set; }
        IEnumerable<RequirementDetails> Reqlist { get; set; }

        public string contactPerson_Name { get; set; }
        public string PositionLevel { get; set; }
        public string Location { get; set; }
        public int? location_id { get; set; }
        public string MinimumQulification { get; set; }
        public string RoleResp { get; set; }
        public string KnowledgeSkill { get; set; }
        public int? Age { get; set; }
        public string NoticePeriod { get; set; }

    }
    public partial class WFCountList
    {
       [Key]
        public int CandID { get; set; }
        public int StageID { get; set; }
        public int NextStageID { get; set; }
        public string StageName { get; set; }
        public string CandName { get; set; }
        public string ActivityDate { get; set; }
        public string emailid { get; set; }
        public string FeedbackRemark  { get; set; }
        public string InterDescription { get; set; }

    }
    public partial class Schedule
    {
        [Key]
        public int CandID { get; set; }
        public int ModeID { get; set; }
        public int? ScheduleID { get; set; }
        public string ModeName { get; set; }
        public int ReqID { get; set; }
        public int StageID { get; set; }
        public int NextStageID { get; set; }
        public String StageName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ActivityDate { get; set; }
        public DateTime? ActivityTime { get; set; }
        public String CandName { get; set; }
        public String Remark { get; set; }
        public String FeedbackRemark { get; set; }
        public int temp_id { get; set; }
    }
    public partial class ActivitySchedule
    {
        [Key]
        public int CandID { get; set; }
        public int? ScheduleID { get; set; }
        public int ReqID { get; set; }
        public int StageID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ActivityDate { get; set; }
        public DateTime? ActivityTime { get; set; }
        public String CandName { get; set; }
        public String Remark { get; set; }
        public String FeedbackRemark { get; set; }
        public int ActivityID { get; set; }
        public String ActivityName { get; set; }
    }


    public class Notes
    {
        [Key]
        public int NoteID { get; set; }
        public int candID { get; set; }
        public string Description { get; set; }
        public int sendby { get; set; }
        public int? ReplyTo { get; set; }
        public DateTime addedDate { get; set; }
        public DateTime? updatedate { get; set; }
    }
    public class mail
    {
        [Key]
        public int mailID { get; set; }
        [AllowHtml]
        [Display(Name = "Message")]
        public string mailDescription { get; set; }
    }
    public class SourceCandidate
    {
        [Key]
        public int req_id { get; set; }
        public int AppliedReqId { get; set; }
        public int candID { get; set; }
        public int client_id { get; set; }
        public string Client_Name { get; set; }
        public string exp_Max { get; set; }
        public string exp_Min { get; set; }
        public string Description { get; set; }
        public DateTime? req_openDate { get; set; }
        public string req_title { get; set; }
        public string roles_responsibilities { get; set; }
    }

    public class CandidateViewmodel
    {
        public CandidateDetails CandidateDetails { get; set; }
        public IEnumerable<Notes> Notes { get; set; }

    }
    public class ActivityDetails
    {
        [Key]
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public int StageID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ReqID { get; set; }
        public string Remark { get; set; }
        public string StageName { get; set; }
    }
    public partial class ModelUploadJD
    {
        [Key]
        public int Document_id { get; set; }
        public int ReqID { get; set; }
        public string FolderPath { get; set; }
        public string FileName { get; set; }
        public string ext { get; set; }
        public DateTime? UploadDate { get; set; }
        public int UploadBy { get; set; }
    }
    public partial class ModelViewJD
    {
        [Key]
        public int Document_id { get; set; }
        public int RecID { get; set; }
        public string DPath { get; set; }
        public string FileName { get; set; }
        public string ext { get; set; }
        public DateTime? UploadDate { get; set; }
        public int UploadBy { get; set; }
    }

    public partial class ConcernPerForTracker
    {
        [Key]
        public int clientCD_ID { get; set; }
        public int client_id { get; set; }
        public int ClientConcernId { get; set; }
        public string contactPerson_Name { get; set; }
        public string emailid { get; set; }
        public int? ReqID { get; set; }
        public string Description { get; set; }
        public int? Emailid1 { get; set; }
        public int UploadType { get; set; }
        public string MailSubject { get; set; }


    }

    public partial class ConcernPerForTracker1
    {
        [Key]
        public int clientCD_ID { get; set; }
        public int client_id { get; set; }
        public string contactPerson_Name { get; set; }
        public string emailid { get; set; }
        public int? ReqID { get; set; }
        public string Description { get; set; }


    }

    public partial class ResumeDetailsFortracker
    {
        [Key]
        public int Document_id { get; set; }
        public int RecID { get; set; }
        public string DPath { get; set; }
        public string FileName { get; set; }
        public string ext { get; set; }
        public string req_name { get; set; }
        public DateTime? UploadDate { get; set; }
        public int UploadBy { get; set; }
    }

    public partial class LoginDetail
    {
        [Key]
        public int User_id { get; set; }
        public string User_Name { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }

    }

    public partial class ModelUploadResume
    {
        [Key]
        public int Document_id { get; set; }
        public int? candID { get; set; }
        public string FolderPath { get; set; }
        public string Candname { get; set; }
        public string FileName { get; set; }
        public string ext { get; set; }
        public DateTime? UploadDate { get; set; }
        public int UploadBy { get; set; }
    }
    public partial class TrackerInfo
    {
        [Key]
        public int candID { get; set; }
        public string vendor { get; set; }
        public int ReqID { get; set; }
        public string CandidateName { get; set; }
        public string emailid { get; set; }
        public string mobile { get; set; }
        public string Notice_period { get; set; }
        public string Total_Exp { get; set; }
        public string currently_drawn_salary { get; set; }
        public string expected_salary { get; set; }
        public string loc_desc { get; set; }
        public string FullPath { get; set; }

        IEnumerable<TrackerInfo> clist { get; set; }
    }


    public partial class TrackerexpotedInfo
    {
        [Key]
        public string Candidate_Name { get; set; }
        public string Applied_Position { get; set; }
        //  public int candID { get; set; }
        public string vendor { get; set; }
        //  public int ReqID { get; set; }
        public string emailid { get; set; }
        public string mobile { get; set; }
        public string Notice_Period { get; set; }
        public string Total_Exp { get; set; }
        public string working_from_Date { get; set; }
        public string current_Company { get; set; }
        public string Current_Position { get; set; }
        public string currently_Drawn_Salary { get; set; }
        public string Expected_Salary { get; set; }
        public string Location { get; set; }

        public string Qualification { get; set; }
        public string Qualification_PG { get; set; }

        public string SkypeID { get; set; }
        //  public string FullPath { get; set; }

        IEnumerable<TrackerexpotedInfo> clist { get; set; }
    }

    public partial class ModelViewresume
    {
        [Key]
        public int Document_id { get; set; }
        public int Candid { get; set; }
        public string DPath { get; set; }
        public string FileName { get; set; }
        public string ext { get; set; }
        public DateTime? UploadDate { get; set; }
        public int UploadBy { get; set; }
    }

    public partial class ModelUploadClientSheet
    {
        [Key]
        public int Document_id { get; set; }
        public int? ClientId { get; set; }
        public string FolderPath { get; set; }
        public string FileName { get; set; }
        public string ext { get; set; }
        public DateTime? UploadDate { get; set; }
        public int UploadBy { get; set; }
    }

    public partial class AssignUsertoJobPosition
    {
        [Key]
        public int AssignId { get; set; }
        public int? ReqId { get; set; }
        public int UserId { get; set; }
    }

    public partial class FromMail
    {
        [Key]
        public int Emailid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SMTP_Type { get; set; }
    }

    public partial class InterviewTrackList
    {
        [Key]
        public Int64 Candid { get; set; }
        public int ReqID { get; set; }
        public string Name { get; set; }
        public string mobile { get; set; }
        public string req_title { get; set; }
        public int StageID { get; set; }
        public int ModeId { get; set; }
        public string ModeName { get; set; }
        public string ActivityDate { get; set; }
        public string ActivityTIME { get; set; }
        public int statusID { get; set; }

    }

    public partial class TrackerFileUpload
    {
        [Key]
        public int Document_id { get; set; }
        public string FolderPath { get; set; }
        public string FileName { get; set; }
        public string ext { get; set; }
        public int Req_id { get; set; }
        public int uploadby { get; set; }
        public DateTime UploadDate { get; set; }
        public int Isactive { get; set; }

    }

    public partial class SearchForPagination
    {
        [Key]
        public string Name { get; set; }

    }

    public partial class UpdatePosition
    {
        [Key]
        public int ReqId { get; set; }
        public int PositionStatusId { get; set; }
        public int PostionStatusName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int AddedBy { get; set; }
    }

    public partial class AssignMultipalUsertoJobPosition
    {
        [Key]
        public int Assignid { get; set; }
        public int userid { get; set; }
        public string FullName { get; set; }
        public int? req_id { get; set; }
        

    }

    public class Chart
    {

        [Key]
        public string Label { get; set; }
        public Int32 Value1 { get; set; }
        //public Int32 Value2 { get; set; }

    }


    public class StackedChartData
    {
        
        [Key]
        public string Label { get; set; }
        public int Value1 { get; set; }
        
       
    }
    public class Userdetailsforpositionshare
    {

        [Key]
        public int User_id { get; set; }
        public int AssignId { get; set; }        
        public int? Req_id { get; set; }
        public string fullname { get; set; }
        public int IsAssined { get; set; }
        

    }

    public class DashbardData
    {
        [Key]
        public int req_id { get; set; }
        public string req_title { get; set; }
        public string Client_Name { get; set; }
        public string Location_details { get; set; }
        public string Date { get; set; }
        public int Applied { get; set; }
        public int Rejected { get; set; }
        public int Interview { get; set; }
        public int Offer { get; set; }
        public int Selected { get; set; }
        public int Attended { get; set; }
        public int NotAttended { get; set; }
        public int Pending { get; set; }
        public int InDescision { get; set; }

    }

    public class UserPermission
    {
        [Key]
        public int user_id { get; set; }
        public string fullname { get; set; }
        public string RoleName { get; set; }

    }


    public class AddUserPermission
    {
       [Key]
       public int ActionId      { get; set; }
        public int? MenuId { get; set; }        
       public int? SubMenuId     { get; set; }
       public string submenuname   { get; set; }
       public string FeatureName   { get; set; }
       public int? UserId        { get; set; }
       public int? Permission { get; set; }
               

    }

    public partial class SaveUserAccess
    {
        [Key] 
        public int Actionid { get; set; }
        public int Userid { get; set; }
        public int PermissionValue { get; set; }
    }


}