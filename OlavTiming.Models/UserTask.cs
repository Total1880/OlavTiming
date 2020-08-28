using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OlavTiming.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IList<Timeframe> Timeframes { get; set; }
        public DateTime Start { get => Timeframes.Min(t => t.Start); }
        public DateTime End { get => Timeframes.Max(t => t.End); }
        
    }
}
