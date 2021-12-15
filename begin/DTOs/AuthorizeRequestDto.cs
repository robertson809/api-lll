using System.ComponentModel.DataAnnotations;
// POCO (Plain old CLR (.NET Common Language Runtime) Object)
namespace ExploreCalifornia.DTOs
{
    public class AuthorizeRequestDto
    {
        [Required]
        [MinLength(32),MaxLength(32)]
        public string AppToken { get; set; }

        [Required]
        [MinLength(32), MaxLength(32)]
        public string AppSecret { get; set; }
    }
}