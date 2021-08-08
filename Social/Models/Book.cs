using System;
using System.ComponentModel;
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
        
        [Description("فانتزی")]
        Fantasy,
        [Description("علمی تخیلی")]
        SciFi,
        [Description("راز")]
        Mystery,
        [Description("وحشت")]
        Thriller,
        [Description("عاشقانه")]
        Romance,
        [Description("وسترن")]
        Westerns,
        [Description("دیستوپی")]
        Dystopian,
        [Description("امروزی")]
        Contemporary,
        [Description("داستانی")]
        Story
    }
}