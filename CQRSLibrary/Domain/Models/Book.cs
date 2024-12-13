﻿namespace Domain.Models
{
    public class Book
    {
        public Guid Id { get; private set; }
        public string? Title { get; set; }

        public string? Description { get; set; }

        public Book(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
        }
    }
}