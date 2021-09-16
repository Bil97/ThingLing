using System.ComponentModel.DataAnnotations;

namespace ThingLing.Shared.Models
{
    public class BaseModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Url]
        public string Url { get; set; }
    }
}
