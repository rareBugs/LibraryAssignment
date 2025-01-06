using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Author
    {
        public Guid Id { get; private set; }
        [Required]
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(100)]

        [JsonIgnore]
        public List<Book> Books {  get; set; } = new List<Book>();

        public Author(string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}