namespace Data.Dtos;

public class UserRegistrationDto
{
    public long id { get; set; }
    public string username { get; set; } = null!;
    public string password_hash { get; set; } = null!;
    public string role { get; set; } = null!;
    public string nid { get; set; } = null!;
    public string address { get; set; } = null!;
    public long building_id { get; set; }
    public long unit_id { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    
}

public class UserContactDto
{
    public string? ContactType { get; set; }
    public string? ContactValue { get; set; }
    public DateTime? CreatedAt { get; set; }
}