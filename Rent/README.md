# Home-Rent 🏢

**A Scalable Multi-Tenant Property Management SaaS Platform**

Home-Rent is a comprehensive, cloud-based SaaS solution designed to streamline property management for **landlords**, **tenants**, **vendors**, and **administrators**. It offers end-to-end functionality for managing buildings, leases, payments, maintenance, communication, and tenant engagement while ensuring strict compliance with **Bangladesh’s Personal Data Protection Ordinance (PDPO)** standards.

---

## 🚀 Features Overview

- **Multi-Role User Management**: Supports tenants, landlords, vendors, and admins with role-specific access.
- **Property & Unit Management**: Manage buildings, units, and occupancy with real-time updates.
- **Tenant Lifecycle**: Streamlined onboarding, verification, and lease signing workflows.
- **Digital Lease Management**: Create, sign, and track leases with e-signature support.
- **Payment Automation**: Handle rent, utilities, and SaaS subscriptions via multiple gateways.
- **Maintenance Management**: Coordinate reactive and proactive maintenance with vendors.
- **Communication System**: Facilitate messaging and automated notifications (email, SMS, in-app).
- **Tenant Engagement**: Organize events, polls, visitor QR passes, and a rewards system.
- **SaaS Monetization**: Free and premium plans with advanced features.
- **PDPO Compliance**: Secure data handling, audit logging, and user consent management.
- **Analytics & Reporting**: Actionable insights on occupancy, revenue, and maintenance.

---

## 🛠️ Core Modules

### 1. User Management
- **Registration**: Supports role-based sign-up with email, password, phone, address, and NID.
- **Security**: Password hashing and encryption for sensitive fields (e.g., NID).
- **Role-Based Access**:
    - **Tenants**: Access leases, payments, maintenance, and community features.
    - **Landlords**: Manage properties, units, leases, and analytics.
    - **Vendors**: View work orders and update availability.
    - **Admins**: Full system oversight and control.
- **Profile Updates**: Customize language, name, phone, and notification preferences.
- **Tenant Screening**: JSON-stored screening data with calculated scores.
- **Multi-Tenancy**: Enforced via `building_id` for data isolation.

### 2. Property Management
- **Building Creation**: Landlords define buildings with address, type, coordinates, and amenities (JSON).
- **Unit Management**: Configure units with rent, bedrooms, bathrooms, photos, description, and status.
- **Automation**: Auto-updates `units_count` and occupancy rates.
- **Status Transitions**: Enforces valid unit states (vacant → occupied → maintenance).

### 3. Tenant Lifecycle
- **Onboarding**: Document upload, verification, and lease signing with status tracking.
- **Feedback**: Tenant ratings and comments visible to landlords.
- **Preferences**: JSON-stored UI mode and notification channel settings.

### 4. Lease Management
- **Templates**: Reusable lease templates managed by landlords.
- **Lease Creation**: Define tenant, unit, date range, rent, deposit, and terms.
- **E-Signatures**: Integration with DocuSign or similar services.
- **Statuses**: Track active, expired, or terminated leases.
- **Auditing**: Temporal history for lease changes and renewal reminders.

### 5. Payments
- **Invoicing**: Auto-generate monthly rent invoices per lease.
- **Gateways**: Supports bKash, Nagad, cards, and more.
- **Tracking**: Monitor payment statuses (pending, paid, overdue).
- **Features**: Handles partial payments, late fees, and utility bills (water, electricity, gas).
- **SaaS Subscriptions**: Manage landlord subscription payments.
- **Reminders**: Automated notifications for upcoming and overdue payments.

### 6. Maintenance Management
#### Reactive Maintenance
- **Requests**: Tenants submit requests with priority and attachments.
- **Workflow**: Landlords approve/reject and assign vendors.
- **Work Orders**: Track lifecycle (open → in_progress → resolved).

#### Proactive Maintenance
- **Scheduling**: Configure recurring tasks (monthly/yearly).
- **Reminders**: Auto-notify vendors and landlords.

#### Additional Features
- **Cost Transparency**: Store labor and material cost breakdowns.
- **Vendor Profiles**: Include availability, expertise, and ratings.

### 7. Communication System
- **Channels**: Email, SMS, and in-app chat.
- **Logging**: Store message details (sender, recipient, timestamps).
- **Templates**: Customizable notification templates.
- **Auditing**: Log all notifications for compliance.

### 8. Automation Engine
- **Scheduled Tasks**:
    - Payment and maintenance reminders.
    - Subscription status checks.
- **Triggers**:
    - Auto-update `units_count` on property changes.
    - Log create/update/delete actions for auditing.

### 9. Tenant Engagement
- **Events**: Landlords create events; tenants RSVP.
- **Visitor Passes**: Tenant-generated QR codes for guest entry.
- **Polls**: Landlords create polls; tenants vote; results aggregated.
- **Rewards System**: Points for timely payments and participation.

### 10. SaaS Monetization
- **Free Plan**: Basic property, unit, and lease management.
- **Premium Plan**: Advanced analytics, automation, and priority support.
- **Subscription Management**: Track transactions and history.

### 11. PDPO Compliance (Bangladesh)
- **Consent Management**: Store user consents with timestamps.
- **Data Requests**: Handle erasure and access requests via admin controls.
- **Audit Logs**: Record user ID, IP, entity, action, and timestamp.
- **Encryption**: Always encrypt NIDs, transaction IDs, and sensitive data.

### 12. Analytics & Reporting
- **Financial Dashboard**: Insights on occupancy, revenue, and leases.
- **Feedback Analytics**: Average ratings and sentiment patterns.
- **Maintenance Analytics**: Cost per unit and frequently serviced properties.
