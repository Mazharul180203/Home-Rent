using System.ComponentModel.DataAnnotations;

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
