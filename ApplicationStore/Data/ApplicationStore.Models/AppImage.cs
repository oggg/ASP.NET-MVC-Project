namespace ApplicationStore.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AppImage
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }
    }
}