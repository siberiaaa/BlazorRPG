using System.ComponentModel.DataAnnotations;

namespace blazorpg.Data.Models
{
    public class CharacterType
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is a required field.")]
        public string? Name { get; set; }

    }
} 