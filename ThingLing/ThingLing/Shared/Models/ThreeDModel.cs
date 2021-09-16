using System.ComponentModel.DataAnnotations;

namespace ThingLing.Shared.Models
{
    public class ThreeDModel : BaseModel
    {
        [Required]
        public string DisplayImagePath { get; set; }
    }
}
