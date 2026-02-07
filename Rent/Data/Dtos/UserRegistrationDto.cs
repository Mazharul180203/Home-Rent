using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Data.Dtos;

public class UserRegistrationDto
{
    [Required]
    public string username { get; set; } = null!;
    [Required]
    public string email { get; set; } = null!;
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

public class ProfileUpdateDto
{
    public string? full_name { get; set; }

    public string? phone { get; set; }
    public string? email { get; set; }
    public decimal? screening_score { get; set; }
    public string? background_check { get; set; }
    public string? profile_pic { get; set; }
}