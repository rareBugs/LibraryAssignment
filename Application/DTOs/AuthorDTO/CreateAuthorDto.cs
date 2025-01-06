namespace Application.DTOs.AuthorDto
{
    public class CreateAuthorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid AuthorId { get; set; }
    }
}