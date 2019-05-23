using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApplication.Entities
{
    [Table("MembersInGroup")]
    public class MembersInGroup
    {
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public virtual Group Group { get; set; }
        public virtual Member Member { get; set; }
    }
}