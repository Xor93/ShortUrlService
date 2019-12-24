using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace TinyURL.Models
{
    public class Link
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [UIHint("Url")]
        [Display(Name = "Url")]
        public string LongURL { get; set; }

        [RegularExpression(@"[a-z0-9]{0,}", ErrorMessage = "Invalid value")]
        [Display(Name = "Hash")]
        public string ShortURL { get; set; }

        [Required]
        public DateTime Added { get; set; }
        public int ClickCounter { get; set; }
    }
}
