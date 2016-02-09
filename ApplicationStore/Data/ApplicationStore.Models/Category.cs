namespace ApplicationStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        private ICollection<Application> applications;

        public Category()
        {
            this.applications = new HashSet<Application>();
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Application> Applications { get { return this.applications; } set { this.applications = value; } }
    }
}