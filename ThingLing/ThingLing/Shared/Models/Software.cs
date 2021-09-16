using System.ComponentModel.DataAnnotations;

namespace ThingLing.Shared.Models
{
    public class Software : BaseModel
    {
        [Required]
        public string Details { get; set; }

        [Required]
        public string IconPath { get; set; }

    }
}
