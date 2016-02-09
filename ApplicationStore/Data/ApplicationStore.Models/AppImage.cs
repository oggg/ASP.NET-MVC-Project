namespace ApplicationStore.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AppImage
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Path { get; set; }
    }
}