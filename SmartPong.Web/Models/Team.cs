using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPong.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? ActivityDate { get; set; }

        public ICollection<User> Users { get; set; }

        public Team()
        {
            Users = new List<User>();
        }
    }
}