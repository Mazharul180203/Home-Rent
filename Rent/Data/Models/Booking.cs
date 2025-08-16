using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? PropertyId { get; set; }

    public int? RenterId { get; set; }

    public DateOnly? CheckInDate { get; set; }

    public DateOnly? CheckOutDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? BookingStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Property? Property { get; set; }

    public virtual User? Renter { get; set; }
}
