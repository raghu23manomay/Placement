using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mvc.Models
{
    public class jobDbContext : DbContext
    { 
        static jobDbContext()
        {
            Database.SetInitializer<jobDbContext>(null);
        }
        public jobDbContext()
            : base("Name=jobDbContext")
        {
        } 
        public DbSet<CandidateList> CList { get; set; }
        public DbSet<WorkFLowList> WFList { get; set; }
        public DbSet<WFCountList> WFCountList { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<CandidateDetails> CandidateDetails { get; set; }
        public DbSet<SourceCandidate> SourceCandidate { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<ActivityDetails> ActivityDetails { get; set; }
        public DbSet<ModelViewJD> ModelViewJD { get; set; }
        public DbSet<ModelViewresume> ModelViewresume { get; set; }
        public DbSet<LoginDetail> LoginDetail { get; set; }

        //============================== Master ============================
        public DbSet<DesignationList> DFList { get; set; }
        public DbSet<UserMasterList> UMList { get; set; }
        public DbSet<DesignationDetails> EDList { get; set; }
        public DbSet<UserDetails> UDList { get; set; }
        public DbSet<UserMasterGridList> UMGList { get; set; }
        public DbSet<RequirementDetails> RJList { get; set; }
        public DbSet<ClinetMasterGridList> CMList { get; set; }
        public DbSet<ClientContactDetails> CCList { get; set; }
        public DbSet<AddContactPerson> ACList { get; set; }
        public DbSet<ClientContactDetailsEdit> CEList { get; set; }
        public DbSet<EditClient> EDTCList { get; set; }
        public DbSet<GetRoleUser> RUList { get; set; }
        public DbSet<Requirement> RList { get; set; }
        public DbSet<TrackerInfo> TrackerInfo{ get; set; }
        public DbSet<TrackerexpotedInfo> TrackerexpotedInfo { get; set; }
        public DbSet<ResumeDetailsFortracker> ResumeDetailsFortracker { get; set; }
       

        public DbSet<FetchClientBD> ClientBDList { get; set; }
        public DbSet<BusinessDev> clientdetails { get; set; }
        public DbSet<clientLocationdetails> clientLocationdetails { get; set; }
        public DbSet<clientdetailsforcontactPerson> clientdetailsforcontactPerson { get; set; }
        public DbSet<ConcernPerForTracker1> ConcernPerForTracker1 { get; set; }
        public DbSet<ConcernPerForTracker> ConcernPerForTracker { get; set; }
        public DbSet<FromMail> FromMail { get; set; }
        public DbSet<InterviewTrackList> InterviewTrackList { get; set; }
        public DbSet<TrackerFileUpload> TrackerFileUpload { get; set; }
        public DbSet<MailTemplate> MailTemplate { get; set; }
        public DbSet<MailTemplateForUpdate> MailTemplateForUpdate { get; set; }
        public DbSet<ModelUploadClientDocument> ModelUploadClientDocument { get; set; }
        public DbSet<AssignMultipalUsertoJobPosition> AssignMultipalUsertoJobPosition { get; set; }
        public DbSet<Chart> Chart { get; set; }
        public DbSet<StackedChartData> StackedChartData { get; set; }
        public DbSet<DashbardData> DashbardData { get; set; }
        public DbSet<Userdetailsforpositionshare> Userdetailsforpositionshare { get; set; }
        public DbSet<FetchcandidatwForCandidate> FetchcandidatwForCandidate { get; set; }
        public DbSet<UserPermission> UserPermission { get; set; }
        public DbSet<AddUserPermission> AddUserPermission { get; set; }
        public DbSet<ClientGrid> ClientGrid { get; set; }
        public DbSet<RecruiterList> editlist { get; set; }
        public DbSet<clientlocationdetail> clientlocationdetail { get; set; }
        public DbSet<VerticalDetails> VerticalDetails { get; set; }
        public DbSet<CityDetails> CityDetails { get; set; }
        public DbSet<GetUserEmail> GetUserEmail { get; set; }
        



    }

}