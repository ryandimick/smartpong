﻿using System.ComponentModel.DataAnnotations;

namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// A system configuration parameter for SmartPong.
    /// 
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// 
        /// The unique name of the setting.
        /// 
        /// </summary>
        [Key]
        [MaxLength(20)]
        public string Key { get; set; }

        /// <summary>
        /// 
        /// The value of the setting.
        /// 
        /// </summary>
        [Required]
        public string Value { get; set; }
    }
}