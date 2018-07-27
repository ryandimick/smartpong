using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// The status type signifying the stage of processing a match is in.
    /// 
    /// </summary>
    //public class MatchStatus
    //{
    //    /// <summary>
    //    /// 
    //    /// The unique identifier of the match status.
    //    /// 
    //    /// </summary>
    //    [Key]
    //    public int MatchStatusId { get; set; }

    //    /// <summary>
    //    /// 
    //    /// The text describing the match status.
    //    /// 
    //    /// </summary>
    //    [Required]
    //    [MaxLength(50)]
    //    public string Description { get; set; }

    //    /// <summary>
    //    /// 
    //    /// The collection of matches in the match status.
    //    /// 
    //    /// </summary>
    //    public ICollection<Match> Matches { get; set; }

    //    public enum Type : int
    //    {
    //        Scheduled = 1,
    //        Submitted = 2,
    //        Pending = 3,
    //        Posted = 4
    //    }

    public enum MatchStatus 
        {
        Scheduled = 1,
        Submitted = 2,
        Pending = 3,
        Posted = 4,
        Denied = 5
    
}
}
