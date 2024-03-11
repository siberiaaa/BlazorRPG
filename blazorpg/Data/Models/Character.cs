using System.ComponentModel.DataAnnotations;

namespace blazorpg.Data.Models
{
    public class Character
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "UrlImage is a required field.")]
        public string? UrlImage { get; set; }
        [Required(ErrorMessage = "Name is a required field.")]
        public string? Name { get; set; }
        public int Level { get; set; }
        public int CharacterTypeId { get; set; }
        public CharacterType? CharacterType { get;set;}
        public int HP { get; set; }
        public int MP { get; set; }
        [Range(1, 100, ErrorMessage = "IQ must be between 1 and 100.")]
        public int IQ { get; set; }
        [Range(1, 100, ErrorMessage = "Strenght must be between 1 and 100.")]
        public int Strenght { get; set; }
        [Range(1, 100, ErrorMessage = "Agility must be between 1 and 100.")]
        public int Agility { get; set; }
        public int Exp { get; set; } 
    }
} 