using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TestApplication.Models;

namespace TestApplication.Entities
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        
    }
}