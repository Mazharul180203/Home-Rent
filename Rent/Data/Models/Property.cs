using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Property
{
    public int PropertyId { get; set; }

    public int? OwnerId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public decimal? PricePerNight { get; set; }

    public int? Bedrooms { get; set; }

    public int? Bathrooms { get; set; }

    public int? MaxGuests { get; set; }

    public string? PropertyType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User? Owner { get; set; }

    public virtual ICollection<PropertyAvailability> PropertyAvailabilities { get; set; } = new List<PropertyAvailability>();

    public virtual ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();

    public virtual ICollection<Riview> Riviews { get; set; } = new List<Riview>();
}
