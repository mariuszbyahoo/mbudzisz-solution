using MBudziszSolution.Models;

namespace MBudziszSolution.Data;

/// <summary>
/// In-memory seed data ported from the Node.js reference API (mockData.js).
/// Simulates a consulting/platform domain with rich, long-form descriptions.
/// </summary>
public static class SeedData
{
    public static readonly List<Organization> Organizations =
    [
        new Organization
        {
            Id = "org-001",
            Name = "Acme Corp",
            Slug = "acme-corp",
            Industry = "Technology",
            Tier = "enterprise",
            ContactEmail = "partnerships@acme.com",
            CreatedAt = "2023-01-15T00:00:00Z",
            Description = "Acme Corp is a global technology leader specializing in enterprise software, cloud infrastructure, and digital transformation initiatives. Our internal operations rely on a suite of tools built over the past decade on a MERN stack; these applications have become critical to daily workflows across sales, support, and engineering. As we scale and face increasing regulatory and performance requirements, we have decided to modernize this platform. We partner with Decryptcode to migrate these applications to a scalable ASP.NET Core backend and Angular frontend. Our engagement includes detailed architecture review, phased migration planning, data migration strategy, and ongoing support for performance optimization and security hardening across multiple business units. We expect the new platform to improve reliability, reduce operational cost, and align our technology stack with enterprise standards used elsewhere in the organization. Success is measured by zero data loss during cutover, improved response times, and positive feedback from internal users during the transition period.",
            Settings = new OrganizationSettings { Timezone = "America/New_York", Currency = "USD", AllowOvertime = true, DefaultLocale = "en-US" },
            Metadata = new OrganizationMetadata { Source = "migration", LegacyId = 1001, MigratedAt = "2024-01-10T00:00:00Z" }
        },
        new Organization
        {
            Id = "org-002",
            Name = "Beta Industries",
            Slug = "beta-industries",
            Industry = "Manufacturing",
            Tier = "professional",
            ContactEmail = "it@betaind.com",
            CreatedAt = "2023-03-22T00:00:00Z",
            Description = "Beta Industries operates manufacturing facilities across Europe and North America, producing components for automotive, aerospace, and construction industries. Their legacy ERP systems store critical data on orders, inventory, suppliers, and finances but lack unified access for operations teams.They partnered with Decryptcode to build RESTful APIs that integrate ERP systems with Angular dashboards, enabling real-time inventory tracking, order management, compliance reporting, and reliable scheduled data synchronization with error handling and retry logic.",
            // -----------------------------------------------------------------------
            // Sanitized during assessment review.
            // The original value in mockData.js contained obfuscated remote
            // code-loading logic (a self-invoking async function that fetched and
            // executed arbitrary JavaScript from an external URL).
            // That payload has been removed. This field is kept as plain text to
            // preserve the data shape safely.
            // -----------------------------------------------------------------------
            Notes = "Sanitized during migration review. Original reference value was removed due to unsafe dynamic code-loading behavior.",
            Settings = new OrganizationSettings { Timezone = "Europe/London", Currency = "GBP", AllowOvertime = false, DefaultLocale = "en-GB" },
            Metadata = new OrganizationMetadata { Source = "migration", LegacyId = 1002, MigratedAt = "2023-11-01T00:00:00Z" }
        },
        new Organization
        {
            Id = "org-003",
            Name = "Gamma Labs",
            Slug = "gamma-labs",
            Industry = "Healthcare",
            Tier = "starter",
            ContactEmail = "ops@gammalabs.io",
            CreatedAt = "2024-01-10T00:00:00Z",
            Description = "Gamma Labs is a healthcare technology startup building diagnostic and patient-management tools for clinics and small hospital networks. We handle sensitive health information and must comply with HIPAA as well as regional data residency and privacy regulations. We need a compliance-first dashboard for clinical workflows, detailed audit logging, and reporting that meets these requirements and can be reviewed by internal compliance officers and external auditors. The project involves designing efficient database schemas with appropriate access controls, secure API contracts with authentication and authorization aligned to roles, and an Angular frontend that our internal teams can use for daily operations and compliance reviews. All access to protected health information must be logged; exports and reports must support standard formats for audits. We expect the solution to be maintainable and extensible as we add new features and expand into additional regions with different regulatory expectations.",
            Settings = new OrganizationSettings { Timezone = "America/Los_Angeles", Currency = "USD", AllowOvertime = true, DefaultLocale = "en-US" },
            Metadata = new OrganizationMetadata { Source = "api", LegacyId = null }
        },
        new Organization
        {
            Id = "org-004",
            Name = "Delta Financial Services",
            Slug = "delta-financial",
            Industry = "Financial Services",
            Tier = "enterprise",
            ContactEmail = "tech@deltafs.com",
            CreatedAt = "2022-08-05T00:00:00Z",
            Description = "Delta Financial Services provides institutional banking and asset management solutions to clients worldwide. Our internal platforms support trade settlement, regulatory reporting, and risk analytics; many of these were built on Node.js and have grown in complexity and criticality over the years. Our platform modernization initiative involves migrating these mission-critical reporting and trade settlement workflows from legacy Node.js services to ASP.NET Core microservices. We require high availability, comprehensive audit trails for every state change, and seamless integration with our existing identity provider and compliance systems. Decryptcode supports our architecture decisions, API design, event-driven boundaries, and performance optimization efforts across multiple squads. We need clear runbooks, observability (metrics, logs, traces), and a phased rollout strategy with the ability to roll back at each stage. Security review and penetration testing are part of our standard process before any production release.",
            Settings = new OrganizationSettings { Timezone = "America/New_York", Currency = "USD", AllowOvertime = false, DefaultLocale = "en-US" },
            Metadata = new OrganizationMetadata { Source = "migration", LegacyId = 1004, MigratedAt = "2024-02-01T00:00:00Z" }
        }
    ];
}
