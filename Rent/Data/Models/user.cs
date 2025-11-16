using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class user
{
    public long id { get; set; }

    public string username { get; set; } = null!;

    public string email { get; set; } = null!;

    public string password_hash { get; set; } = null!;

    public string role { get; set; } = null!;

    public string? nid { get; set; }

    public string? full_name { get; set; }

    public string? phone { get; set; }

    public string? address { get; set; }

    public string? preferred_language { get; set; }

    public decimal? screening_score { get; set; }

    public string? background_check { get; set; }

    public long? building_id { get; set; }

    public long? unit_id { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<access_log> access_logs { get; set; } = new List<access_log>();

    public virtual property? building { get; set; }

    public virtual ICollection<communication> communicationrecipients { get; set; } = new List<communication>();

    public virtual ICollection<communication> communicationsenders { get; set; } = new List<communication>();

    public virtual ICollection<data_consent> data_consents { get; set; } = new List<data_consent>();

    public virtual ICollection<data_request> data_requests { get; set; } = new List<data_request>();

    public virtual ICollection<lease> leases { get; set; } = new List<lease>();

    public virtual ICollection<maintenance_request> maintenance_requestapproved_byNavigations { get; set; } = new List<maintenance_request>();

    public virtual ICollection<maintenance_request> maintenance_requesttenants { get; set; } = new List<maintenance_request>();

    public virtual ICollection<maintenance_schedule> maintenance_schedules { get; set; } = new List<maintenance_schedule>();

    public virtual ICollection<notification> notifications { get; set; } = new List<notification>();

    public virtual ICollection<property> properties { get; set; } = new List<property>();

    public virtual ICollection<reward> rewards { get; set; } = new List<reward>();

    public virtual ICollection<subscription> subscriptions { get; set; } = new List<subscription>();

    public virtual ICollection<tenant_feedback> tenant_feedbacks { get; set; } = new List<tenant_feedback>();

    public virtual ICollection<tenant_onboarding> tenant_onboardings { get; set; } = new List<tenant_onboarding>();

    public virtual ICollection<tenant_preference> tenant_preferences { get; set; } = new List<tenant_preference>();

    public virtual unit? unit { get; set; }

    public virtual ICollection<vendor> vendors { get; set; } = new List<vendor>();

    public virtual ICollection<visitor_pass> visitor_passes { get; set; } = new List<visitor_pass>();

    public virtual ICollection<work_order> work_orders { get; set; } = new List<work_order>();
}
