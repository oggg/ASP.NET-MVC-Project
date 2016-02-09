namespace ApplicationStore.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AppPath
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Destination { get; set; }
    }
}