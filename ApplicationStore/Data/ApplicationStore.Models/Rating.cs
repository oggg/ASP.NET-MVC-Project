﻿namespace ApplicationStore.Models
{
    using System.ComponentModel.DataAnnotations;
    using ApplicationStore.Common;

    public class Rating
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public int ApplicationId { get; set; }

        public virtual Application Application { get; set; }

        [Range(ModelsConstants.MinRating, ModelsConstants.MaxRating)]
        public int Value { get; set; }
    }
}
