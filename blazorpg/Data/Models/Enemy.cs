using System.ComponentModel.DataAnnotations;

namespace blazorpg.Data.Models;

public class Enemy
{
    public int ID { get; set; }
    [Required(ErrorMessage = "Name is a required field.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Url Image is a required field.")]
    public string? UrlImage { get; set; }
    [Required(ErrorMessage = "Abilities is a required field.")]
    public string? Abilities { get; set; }
    public int HP { get; set; }
    [Range(1, 100, ErrorMessage = "Level must be between 1 and 100.")]
    public int Level { get; set; }
    public int Reward { get; set; }
}