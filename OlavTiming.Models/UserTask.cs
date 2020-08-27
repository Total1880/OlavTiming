using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OlavTiming.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IList<Timeframe> Timeframes { get; set; }
    }
}
