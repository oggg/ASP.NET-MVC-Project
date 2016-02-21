namespace ApplicationStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ApplicationStore.Common;

    public class Application
    {
        private ICollection<User> installedBy;
        private ICollection<Rating> ratings;

        public Application()
        {
            this.installedBy = new HashSet<User>();
            this.ratings = new HashSet<Rating>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ModelsConstants.MaxAppNameLength, ErrorMessage = ModelsConstants.NameTooLong)]
        public string Name { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public User Creator { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public int AppImageId { get; set; }

        public virtual AppImage Image { get; set; }

        [MaxLength(ModelsConstants.MaxAppDescriptionLength, ErrorMessage = ModelsConstants.DescriptionTooLong)]
        public string Description { get; set; }

        [Required]
        public string Path { get; set; }

        public virtual ICollection<User> InstalledBy { get { return this.installedBy; } set { this.installedBy = value; } }

        public virtual ICollection<Rating> Ratings { get { return this.ratings; } set { this.ratings = value; } }
    }
}