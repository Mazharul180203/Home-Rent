using System;
using System.Collections.Generic;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.DBContexts;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<access_log> access_logs { get; set; }

    public virtual DbSet<billing_transaction> billing_transactions { get; set; }

    public virtual DbSet<communication> communications { get; set; }

    public virtual DbSet<communication_template> communication_templates { get; set; }

    public virtual DbSet<contract_template> contract_templates { get; set; }

    public virtual DbSet<data_consent> data_consents { get; set; }

    public virtual DbSet<data_request> data_requests { get; set; }

    public virtual DbSet<lease> leases { get; set; }

    public virtual DbSet<lookup_type> lookup_types { get; set; }

    public virtual DbSet<maintenance_cost> maintenance_costs { get; set; }

    public virtual DbSet<maintenance_request> maintenance_requests { get; set; }

    public virtual DbSet<maintenance_schedule> maintenance_schedules { get; set; }

    public virtual DbSet<notification> notifications { get; set; }

    public virtual DbSet<payment> payments { get; set; }

    public virtual DbSet<poll> polls { get; set; }

    public virtual DbSet<property> properties { get; set; }

    public virtual DbSet<reward> rewards { get; set; }

    public virtual DbSet<scheduled_task> scheduled_tasks { get; set; }

    public virtual DbSet<subscription> subscriptions { get; set; }

    public virtual DbSet<tenant_feedback> tenant_feedbacks { get; set; }

    public virtual DbSet<tenant_onboarding> tenant_onboardings { get; set; }

    public virtual DbSet<tenant_preference> tenant_preferences { get; set; }

    public virtual DbSet<unit> units { get; set; }

    public virtual DbSet<user> users { get; set; }

    public virtual DbSet<utility> utilities { get; set; }

    public virtual DbSet<vendor> vendors { get; set; }

    public virtual DbSet<visitor_pass> visitor_passes { get; set; }

    public virtual DbSet<vw_financial_summary> vw_financial_summaries { get; set; }

    public virtual DbSet<work_order> work_orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-G8231R3B;Database=ToLetDB;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<access_log>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__access_l__3213E83F7B9B8176");

            entity.HasIndex(e => e.user_id, "idx_access_user_id");

            entity.Property(e => e.action).HasMaxLength(50);
            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.entity_type).HasMaxLength(50);
            entity.Property(e => e.ip_address).HasMaxLength(45);
            entity.Property(e => e.timestamp).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.user).WithMany(p => p.access_logs)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_access_user");
        });

        modelBuilder.Entity<billing_transaction>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__billing___3213E83F406D281D");

            entity.HasIndex(e => e.subscription_id, "idx_billing_subscription_id");

            entity.Property(e => e.amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .HasDefaultValue("pending");
            entity.Property(e => e.transaction_id).HasMaxLength(50);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.subscription).WithMany(p => p.billing_transactions)
                .HasForeignKey(d => d.subscription_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_billing_subscription");
        });

        modelBuilder.Entity<communication>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__communic__3213E83F38944604");

            entity.HasIndex(e => e.building_id, "idx_communications_building_id");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.sent_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.type).HasMaxLength(20);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.building).WithMany(p => p.communications)
                .HasForeignKey(d => d.building_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_communications_building");

            entity.HasOne(d => d.recipient).WithMany(p => p.communicationrecipients)
                .HasForeignKey(d => d.recipient_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_communications_recipient");

            entity.HasOne(d => d.sender).WithMany(p => p.communicationsenders)
                .HasForeignKey(d => d.sender_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_communications_sender");
        });

        modelBuilder.Entity<communication_template>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__communic__3213E83FC718FE5F");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.name).HasMaxLength(100);
            entity.Property(e => e.type).HasMaxLength(20);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<contract_template>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__contract__3213E83F1B375881");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.name).HasMaxLength(100);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<data_consent>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__data_con__3213E83F5582428A");

            entity.HasIndex(e => e.user_id, "idx_consents_user_id");

            entity.Property(e => e.consent_type).HasMaxLength(50);
            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.granted_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.user).WithMany(p => p.data_consents)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_consents_user");
        });

        modelBuilder.Entity<data_request>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__data_req__3213E83F7A3FD7B6");

            entity.HasIndex(e => e.user_id, "idx_requests_user_id");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.request_type).HasMaxLength(50);
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .HasDefaultValue("pending");
            entity.Property(e => e.submitted_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.user).WithMany(p => p.data_requests)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_requests_user");
        });

        modelBuilder.Entity<lease>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__leases__3213E83FDF66C407");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("leases_history", "dbo");
                        ttb
                            .HasPeriodStart("SysStartTime")
                            .HasColumnName("SysStartTime");
                        ttb
                            .HasPeriodEnd("SysEndTime")
                            .HasColumnName("SysEndTime");
                    }));

            entity.HasIndex(e => e.status, "idx_leases_status");

            entity.HasIndex(e => e.unit_id, "idx_leases_unit_id");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.deposit).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.e_signature).HasMaxLength(255);
            entity.Property(e => e.rent_amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .HasDefaultValue("active");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.contract_template).WithMany(p => p.leases)
                .HasForeignKey(d => d.contract_template_id)
                .HasConstraintName("FK_leases_template");

            entity.HasOne(d => d.tenant).WithMany(p => p.leases)
                .HasForeignKey(d => d.tenant_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_leases_tenant");

            entity.HasOne(d => d.unit).WithMany(p => p.leases)
                .HasForeignKey(d => d.unit_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_leases_unit");
        });

        modelBuilder.Entity<lookup_type>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__lookup_t__3213E83F5B131EA4");

            entity.HasIndex(e => new { e.category, e.value }, "UK_lookup_types").IsUnique();

            entity.HasIndex(e => e.category, "idx_lookup_category");

            entity.Property(e => e.category).HasMaxLength(50);
            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.value).HasMaxLength(50);
        });

        modelBuilder.Entity<maintenance_cost>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__maintena__3213E83F47FF1BF2");

            entity.HasIndex(e => e.work_order_id, "idx_costs_work_order_id");

            entity.Property(e => e.amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.cost_type).HasMaxLength(50);
            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.work_order).WithMany(p => p.maintenance_costs)
                .HasForeignKey(d => d.work_order_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_costs_work_order");
        });

        modelBuilder.Entity<maintenance_request>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__maintena__3213E83F2DAF4BCD");

            entity.HasIndex(e => e.unit_id, "idx_maintenance_unit_id");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.priority)
                .HasMaxLength(20)
                .HasDefaultValue("medium");
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .HasDefaultValue("open");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.approved_byNavigation).WithMany(p => p.maintenance_requestapproved_byNavigations)
                .HasForeignKey(d => d.approved_by)
                .HasConstraintName("FK_maintenance_approver");

            entity.HasOne(d => d.tenant).WithMany(p => p.maintenance_requesttenants)
                .HasForeignKey(d => d.tenant_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_maintenance_tenant");

            entity.HasOne(d => d.unit).WithMany(p => p.maintenance_requests)
                .HasForeignKey(d => d.unit_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_maintenance_unit");
        });

        modelBuilder.Entity<maintenance_schedule>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__maintena__3213E83F0927D073");

            entity.HasIndex(e => e.unit_id, "idx_schedules_unit_id");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.frequency).HasMaxLength(20);
            entity.Property(e => e.type).HasMaxLength(50);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.unit).WithMany(p => p.maintenance_schedules)
                .HasForeignKey(d => d.unit_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_schedules_unit");

            entity.HasOne(d => d.vendor).WithMany(p => p.maintenance_schedules)
                .HasForeignKey(d => d.vendor_id)
                .HasConstraintName("FK_schedules_vendor");
        });

        modelBuilder.Entity<notification>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__notifica__3213E83FB3FB2C9D");

            entity.HasIndex(e => e.user_id, "idx_notifications_user_id");

            entity.Property(e => e.channel).HasMaxLength(20);
            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.enabled).HasDefaultValue(true);
            entity.Property(e => e.type).HasMaxLength(50);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.user).WithMany(p => p.notifications)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_notifications_user");
        });

        modelBuilder.Entity<payment>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__payments__3213E83FCB6B990E");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("payments_history", "dbo");
                        ttb
                            .HasPeriodStart("SysStartTime")
                            .HasColumnName("SysStartTime");
                        ttb
                            .HasPeriodEnd("SysEndTime")
                            .HasColumnName("SysEndTime");
                    }));

            entity.HasIndex(e => e.due_date, "idx_payments_due_date");

            entity.HasIndex(e => e.lease_id, "idx_payments_lease_id");

            entity.Property(e => e.amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.late_fee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.method).HasMaxLength(20);
            entity.Property(e => e.partial_amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.receipt_pdf).HasMaxLength(255);
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .HasDefaultValue("pending");
            entity.Property(e => e.transaction_id).HasMaxLength(50);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.lease).WithMany(p => p.payments)
                .HasForeignKey(d => d.lease_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_payments_lease");
        });

        modelBuilder.Entity<poll>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__polls__3213E83F8A10DE6B");

            entity.HasIndex(e => e.building_id, "idx_polls_building_id");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.building).WithMany(p => p.polls)
                .HasForeignKey(d => d.building_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_polls_building");
        });

        modelBuilder.Entity<property>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__properti__3213E83F4E596534");

            entity.HasIndex(e => e.owner_id, "idx_properties_owner_id");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.name).HasMaxLength(100);
            entity.Property(e => e.type).HasMaxLength(20);
            entity.Property(e => e.units_count).HasDefaultValue(0);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.owner).WithMany(p => p.properties)
                .HasForeignKey(d => d.owner_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_properties_owner");
        });

        modelBuilder.Entity<reward>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__rewards__3213E83F87B522F9");

            entity.HasIndex(e => e.tenant_id, "idx_rewards_tenant_id");

            entity.Property(e => e.action_type).HasMaxLength(50);
            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.reward_type).HasMaxLength(50);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.tenant).WithMany(p => p.rewards)
                .HasForeignKey(d => d.tenant_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_rewards_tenant");
        });

        modelBuilder.Entity<scheduled_task>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__schedule__3213E83FED70A73C");

            entity.HasIndex(e => new { e.entity_type, e.entity_id }, "idx_tasks_entity");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.entity_type).HasMaxLength(50);
            entity.Property(e => e.frequency).HasMaxLength(20);
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .HasDefaultValue("pending");
            entity.Property(e => e.task_type).HasMaxLength(50);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<subscription>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__subscrip__3213E83F9818069A");

            entity.HasIndex(e => e.user_id, "idx_subscriptions_user_id");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.plan_type).HasMaxLength(50);
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .HasDefaultValue("active");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.user).WithMany(p => p.subscriptions)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_subscriptions_user");
        });

        modelBuilder.Entity<tenant_feedback>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__tenant_f__3213E83F3BC088DF");

            entity.ToTable("tenant_feedback");

            entity.HasIndex(e => e.building_id, "idx_feedback_building_id");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.submitted_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.building).WithMany(p => p.tenant_feedbacks)
                .HasForeignKey(d => d.building_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_feedback_building");

            entity.HasOne(d => d.user).WithMany(p => p.tenant_feedbacks)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_feedback_user");
        });

        modelBuilder.Entity<tenant_onboarding>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__tenant_o__3213E83FF786906E");

            entity.ToTable("tenant_onboarding");

            entity.HasIndex(e => e.user_id, "idx_onboarding_user_id");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .HasDefaultValue("pending");
            entity.Property(e => e.step).HasMaxLength(50);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.user).WithMany(p => p.tenant_onboardings)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_onboarding_user");
        });

        modelBuilder.Entity<tenant_preference>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__tenant_p__3213E83F3B739669");

            entity.HasIndex(e => e.user_id, "idx_preferences_user_id");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.user).WithMany(p => p.tenant_preferences)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_preferences_user");
        });

        modelBuilder.Entity<unit>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__units__3213E83F2D8C3D46");

            entity.ToTable(tb => tb.HasTrigger("tr_units_count"));

            entity.HasIndex(e => e.building_id, "idx_units_building_id");

            entity.HasIndex(e => e.status, "idx_units_status");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.rent_amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .HasDefaultValue("vacant");
            entity.Property(e => e.unit_number).HasMaxLength(20);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.building).WithMany(p => p.units)
                .HasForeignKey(d => d.building_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_units_building");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__users__3213E83F58E94D1E");

            entity.ToTable(tb => tb.HasTrigger("tr_access_log"));

            entity.HasIndex(e => e.email, "UQ__users__AB6E61643503945E").IsUnique();

            entity.HasIndex(e => e.nid, "UQ__users__DF97D0F400E399D7").IsUnique();

            entity.HasIndex(e => e.username, "UQ__users__F3DBC5724B1F7B0F").IsUnique();

            entity.HasIndex(e => e.building_id, "idx_users_building_id");

            entity.HasIndex(e => e.role, "idx_users_role");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.full_name).HasMaxLength(100);
            entity.Property(e => e.nid).HasMaxLength(20);
            entity.Property(e => e.password_hash).HasMaxLength(255);
            entity.Property(e => e.phone).HasMaxLength(15);
            entity.Property(e => e.preferred_language)
                .HasMaxLength(20)
                .HasDefaultValue("bengali");
            entity.Property(e => e.role)
                .HasMaxLength(20)
                .HasDefaultValue("tenant");
            entity.Property(e => e.screening_score).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.username).HasMaxLength(50);

            entity.HasOne(d => d.building).WithMany(p => p.users)
                .HasForeignKey(d => d.building_id)
                .HasConstraintName("FK_users_building");

            entity.HasOne(d => d.unit).WithMany(p => p.users)
                .HasForeignKey(d => d.unit_id)
                .HasConstraintName("FK_users_unit");
        });

        modelBuilder.Entity<utility>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__utilitie__3213E83FBDF46BFA");

            entity.HasIndex(e => e.unit_id, "idx_utilities_unit_id");

            entity.Property(e => e.amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.meter_reading).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .HasDefaultValue("billed");
            entity.Property(e => e.type).HasMaxLength(20);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.lease).WithMany(p => p.utilities)
                .HasForeignKey(d => d.lease_id)
                .HasConstraintName("FK_utilities_lease");

            entity.HasOne(d => d.unit).WithMany(p => p.utilities)
                .HasForeignKey(d => d.unit_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_utilities_unit");
        });

        modelBuilder.Entity<vendor>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__vendors__3213E83F21A363ED");

            entity.HasIndex(e => e.user_id, "idx_vendors_user_id");

            entity.Property(e => e.availability)
                .HasMaxLength(20)
                .HasDefaultValue("available");
            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.expertise).HasMaxLength(50);
            entity.Property(e => e.rating).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.user).WithMany(p => p.vendors)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_vendors_user");
        });

        modelBuilder.Entity<visitor_pass>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__visitor___3213E83FE0A7EB4E");

            entity.HasIndex(e => e.tenant_id, "idx_visitors_tenant_id");

            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.qr_code).HasMaxLength(255);
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .HasDefaultValue("active");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.visitor_name).HasMaxLength(100);

            entity.HasOne(d => d.tenant).WithMany(p => p.visitor_passes)
                .HasForeignKey(d => d.tenant_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_visitors_tenant");
        });

        modelBuilder.Entity<vw_financial_summary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_financial_summary");

            entity.Property(e => e.property_name).HasMaxLength(100);
            entity.Property(e => e.total_revenue).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<work_order>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__work_ord__3213E83F61072D86");

            entity.HasIndex(e => e.request_id, "idx_work_orders_request_id");

            entity.Property(e => e.cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.created_at).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.updated_at).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.request).WithMany(p => p.work_orders)
                .HasForeignKey(d => d.request_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_work_orders_request");

            entity.HasOne(d => d.vendor).WithMany(p => p.work_orders)
                .HasForeignKey(d => d.vendor_id)
                .HasConstraintName("FK_work_orders_vendor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
