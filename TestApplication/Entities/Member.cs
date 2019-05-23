using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestApplication.Entities
{
    [Table("Member")]
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Agent")]
        public int? AgentId { get; set; }

        [Display(Name = "Group")]
        public int? GroupId { get; set; }

        [Display(Name = "Branch")]
        public int? BranchId { get; set; }

        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Title")]
        public int Title { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Display(Name = "Occupation")]
        public string Occupation { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile No.")]
        public string Mobile { get; set; }

        [Display(Name = "Address Line 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "Aadhar No.")]
        public string AadharNumber { get; set; }

        [Display(Name = "Voter Id No.")]
        public string VoterId { get; set; }

        [Display(Name = "Pan No.")]
        public string PanNo { get; set; }

        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Display(Name = "Husband/Wife Name")]
        public string HusbandWifeName { get; set; }

        public bool IsMinor { get; set; }
        public int? MemberAccountId { get; set; }
        public bool IsPromoter { get; set; }

        public virtual Country Country { get; set; }

        public virtual MemberAccount MemberAccount { get; set; }

        public virtual State State { get; set; }

        //public MemberAccountNominee Nominee { get; set; }

        public virtual ICollection<MemberAccountNominee> MemberAccountNominees { get; set; }

        [InverseProperty("Member")]
        public virtual ICollection<MembersInGroup> MembersInGroup { get; set; }
        [NotMapped]
        public ICollection<Group> Groups { get; set; }

        #region Additional Properties

        [NotMapped]
        public string AgentName
        {
            get
            {
                switch (AgentId)
                {
                    case 1: return "Akash";
                    case 2: return "Gautam";
                    case 3: return "Agent1";
                    default: return string.Empty;
                }
            }
        }
        [NotMapped]
        public string GroupName
        {
            get
            {
                switch (GroupId)
                {
                    case 1: return "Gurgaon Ladies";
                    case 2: return "Delhi Ladies";
                    case 3: return "Groups";
                    default: return string.Empty;
                }
            }
        }
        [NotMapped]
        public string BranchName
        {
            get
            {
                switch (BranchId)
                {
                    case 1: return "Gurgaon branch";
                    case 2: return "Delhi branch";
                    case 3: return "Other brnach"; 
                    default: return string.Empty;
                }
            }
        }
        [NotMapped]
        public string TitleText
        {
            get
            {
                switch (AgentId)
                {
                    case 1: return "Mr.";
                    case 2: return "Ms.";
                    case 3: return "Mrs."; 
                    default: return string.Empty;
                }
            }
        }

        [NotMapped]
        public string EnrollmentDateString => EnrollmentDate.ToShortDateString();

        [NotMapped]
        public string Name => $"{FirstName} {(string.IsNullOrWhiteSpace(MiddleName) ? string.Empty : MiddleName)}{LastName}";
        [NotMapped]
        [Display(Name ="Address")]
        public string Address => Address1 + ", " + Address2;
        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DOB.Year;
                if (DOB.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        #endregion
    }
}