namespace Data.Dtos;

public class LoginDto
{
    public string username { get; set; } = null!;

    public string? email { get; set; }

    public string password_hash { get; set; } = null!;

    public string? role { get; set; }

    public string? nid { get; set; }

    public string? full_name { get; set; }

    public string? phone { get; set; }

    public string? address { get; set; }

    public string? preferred_language { get; set; }

    public decimal? screening_score { get; set; }

    public string? background_check { get; set; }

    public long? building_id { get; set; }

    public long? unit_id { get; set; }
}