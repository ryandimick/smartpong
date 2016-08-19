using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// The representation of an end-user that has registered within SmartPong.
    /// 
    /// </summary>
    public class User
    {
        /// <summary>
        /// 
        /// The unique identifier of the user.
        /// 
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// The authentication credential for the user.
        /// 
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// 
        /// The given name of the user.
        /// 
        /// </summary>
        [Required]
        [MaxLength(50)]
        [DisplayName("First Name")]
        public string GivenName { get; set; }

        /// <summary>
        /// 
        /// The family name of the user.
        /// 
        /// </summary>
        [Required]
        [MaxLength(50)]
        [DisplayName("Last Name")]
        public string Surname { get; set; }

        /// <summary>
        /// 
        /// An optional nickname for the user to be used throughout the system.
        /// 
        /// </summary>
        [MaxLength(50)]
        [DefaultValue("")]
        public string Nickname { get; set; }

        /// <summary>
        /// 
        /// A valid e-mail address for the user used for notifications.
        /// 
        /// </summary>
        [MaxLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// The timestamp the user was registered.
        /// 
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 
        /// The timestamp the user was last active.
        /// 
        /// </summary>
        public DateTime? ActivityDate { get; set; }

        /// <summary>
        /// 
        /// Boolean switch for receiving non-essential notifications.
        /// 
        /// </summary>
        [Required]
        [DefaultValue(true)]
        public bool Notifications { get; set; }

        /// <summary>
        /// 
        /// Boolean switch signifying whether or not a user is a system administrator.
        /// 
        /// </summary>
        [Required]
        [DefaultValue(false)]
        public bool Admin { get; set; }

        /// <summary>
        /// 
        /// Boolean switch for whether or not the user is currently valid.
        /// 
        /// </summary>
        [Required]
        [DefaultValue(true)]
        public bool Enabled { get; set; }
        
        /// <summary>
        /// 
        /// The commonly used fully constructed name for the user.
        /// 
        /// </summary>
        [NotMapped]
        public string DisplayName => string.IsNullOrWhiteSpace(Nickname) ? $"{GivenName} {Surname}" : $"{GivenName} \"{Nickname}\" {Surname}";
    }
}
