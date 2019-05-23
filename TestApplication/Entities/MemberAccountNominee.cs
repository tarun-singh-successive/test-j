using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestApplication.Entities
{
    public class MemberAccountNominee
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nominee Name")]
        public string Name { get; set; }

        public int MemberId { get; set; }

        [Display(Name = "Relationship with Nominee")]
        public string Relationship { get; set; }

        [Display(Name = "Nominee Date of Birth")]
        public DateTime? Dob { get; set; }

        [Display(Name = "Nominee Assigned Date")]
        public DateTime AssignedDate { get; set; }

        [Display(Name = "Nominee Mobile No.")]
        public string Mobile { get; set; }

        [Display(Name = "Nominee Address")]
        public string Address { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
    }
}