using System;
using System.ComponentModel.DataAnnotations;

namespace Social.Models
{
    public class Book
    {
        [Key] public long Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public long? AuthorId { get; set; }
        public BookCategory BookCategory { get; set; }
        public string Summery { get; set; }
    }

    public enum BookCategory
    {
        Fantasy,
        SciFi,
        Mystery,
        Thriller,
        Romance,
        Westerns,
        Dystopian,
        Contemporary,
    }
}