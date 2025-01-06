using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Book
    {
        public Guid Id { get; private set; }
        [Required]
        [MaxLength(255)]
        public string? Title { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Description { get; set; }
        [Required]

        [JsonPropertyName("AuthorInfo")]
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }

        public Book(string title, string description, Guid authorId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            AuthorId = authorId;
            
        }
    }
}