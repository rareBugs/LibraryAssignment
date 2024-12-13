﻿namespace Domain.Models
{
    public class Author
    {
        public Guid Id { get; private set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public Author(string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}