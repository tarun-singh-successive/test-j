using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestApplication.Entities
{
    public class Group
    {
        public Group()
        {
            MembersInGroup = new HashSet<MembersInGroup>();
        }
        [Key]
        public int Id { get; set; }

        [Display(Name ="Group Name")]
        public string Name { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name ="Open Date")]
        public DateTime CreationDate { get; set; }

        [NotMapped]
        public ICollection<Member> Members { get; set; }

        [InverseProperty("Group")]
        public virtual ICollection<MembersInGroup> MembersInGroup { get; set; }

        [NotMapped]
        [Display(Name ="Member")]
        public string[] MemberIds { get; set; }
        [NotMapped]
        public string CreationDateString => CreationDate.ToShortDateString();
        [NotMapped]
        public string IsActiveString => IsActive ? "Yes" : "No";
        [NotMapped]
        public int MembersCount => MembersInGroup?.Count ?? 0;


    }
}