using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    public class User
    { 
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("First Name")]
        public string GivenName { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Last Name")]
        public string Surname { get; set; }
        
        [MaxLength(50)]
        [DefaultValue("")]
        public string Nickname { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? ActivityDate { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool Notifications { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool Admin { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool Enabled { get; set; }

        public ICollection<UserRating> UserRatings { get; set; }

        public ICollection<Team> Teams { get; set; }

        [NotMapped]
        public string DisplayName => string.IsNullOrWhiteSpace(Nickname) ? $"{GivenName} {Surname}" : $"{GivenName} \"{Nickname}\" {Surname}";
    }
}