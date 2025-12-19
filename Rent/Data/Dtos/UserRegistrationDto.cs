using System.ComponentModel.DataAnnotations;

namespace Data.Dtos;

public class UserRegistrationDto
{
    [Required]
    public string username { get; set; } = null!;
    [Required]
    [RegularExpression(
        @"^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain 1 uppercase letter, 1 number, and 1 special character"
    )]
    public string password { get; set; } = null!;
    [Required]
    public string role { get; set; } = null!;
    [Required]
    public string nid { get; set; } = null!;
    [Required]
    public string address { get; set; } = null!;
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    
}

public class UserContactDto
{
    public string? ContactType { get; set; }
    public string? ContactValue { get; set; }
    public DateTime? CreatedAt { get; set; }
}