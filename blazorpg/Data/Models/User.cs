using System.ComponentModel.DataAnnotations;

namespace blazorpg.Data.Models;

public class User
{

    public int ID { get; set;}
    [Required(ErrorMessage = "Username is a required field.")]
    public string Username { get; set;}
    [Required(ErrorMessage = "Password is a required field.")]
    public string Password { get; set;}
}