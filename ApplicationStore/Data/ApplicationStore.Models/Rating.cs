using System.ComponentModel.DataAnnotations;
using ApplicationStore.Common;

namespace ApplicationStore.Models
{
    public class Rating
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int ApplicationId { get; set; }

        public virtual Application Application { get; set; }

        [Range(ModelsConstants.MinRating, ModelsConstants.MaxRating)]
        public int Value { get; set; }
    }
}
