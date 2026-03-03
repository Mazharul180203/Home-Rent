using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Data.Dtos;


public class propetiesDto
{
    [Required]
    public long owner_id { get; set; }
    [Required]
    public string name { get; set; } = null!;
    [Required]
    public string address { get; set; } = null!;
    [Required]
    public string type { get; set; } = null!;
    
    public int? units_count { get; set; }

    public string? amenities { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public string? location_geo { get; set; }
}

public class unitDto
{
    [Required]
    public long building_id { get; set; }
    [Required]
    public string unit_number { get; set; }
    [Required]
    public string? status { get; set; }
    [Required]
    public decimal? rent_amount { get; set; }
    [Required]
    public string? description { get; set; }
    [Required]
    public int? square_feet { get; set; }
    [Required]
    public int? bedrooms { get; set; }
    [Required]
    public int? bathrooms { get; set; }
    [Required]
    public DateTime? deleted_at { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
    [Required]
    public IFormFile? photos { get; set; }
}
