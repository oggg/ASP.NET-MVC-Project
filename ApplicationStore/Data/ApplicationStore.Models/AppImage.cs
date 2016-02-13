namespace ApplicationStore.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AppImage
    {
        [Key]
        [Required]
        public int Id { get; set; }

        // public byte[] Content { get; set; }

        public string Name { get; set; }

        [Required]
        public string Path { get; set; }
    }
}