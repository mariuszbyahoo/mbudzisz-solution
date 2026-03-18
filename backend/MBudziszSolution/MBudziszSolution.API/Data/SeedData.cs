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

    public static readonly List<User> Users =
    [
        new User
        {
            Id = "user-001",
            OrgId = "org-001",
            Email = "alice@acme.com",
            Name = "Alice Chen",
            Role = "admin",
            Active = true,
            Bio = "Alice is the technical lead for platform modernization at Acme. She focuses on backend architecture, API design, and migration strategy, and works closely with product and DevOps to align timelines and rollout plans. She previously led similar MERN-to-.NET migrations at two other enterprises and brings strong opinions on incremental rollout, feature flags, and comprehensive test coverage. She advocates for clear API contracts and documentation so that frontend and backend teams can work in parallel. Outside of the migration, she mentors junior developers and contributes to internal tech talks and architecture review boards."
        },
        new User
        {
            Id = "user-002",
            OrgId = "org-001",
            Email = "bob@acme.com",
            Name = "Bob Smith",
            Role = "member",
            Active = true,
            Bio = "Bob is a full stack developer working on the Angular frontend and integration with the new ASP.NET Core APIs. He owns the shared component library, state management approach, and accessibility compliance (WCAG 2.1 AA) for the new application. He is experienced with both React and Angular in production environments and has been instrumental in defining the API contract and error-handling patterns between frontend and backend. He also maintains the Storybook for the design system and supports the rest of the team with code reviews and frontend best practices."
        },
        new User
        {
            Id = "user-003",
            OrgId = "org-001",
            Email = "carol@acme.com",
            Name = "Carol Davis",
            Role = "member",
            Active = false,
            Bio = "Carol was the primary backend developer on the legacy Node.js services for several years. She left the team after the initial discovery and audit phase; her documentation and handover sessions were completed in Q4 2023. She remains available for ad-hoc questions during the migration, especially around business rules, edge cases, and historical design decisions. Her written handover notes and Confluence pages are the main reference for the current team when interpreting legacy behavior."
        },
        new User
        {
            Id = "user-004",
            OrgId = "org-002",
            Email = "dave@beta.com",
            Name = "Dave Wilson",
            Role = "admin",
            Active = true,
            Bio = "Dave is IT director for Beta Industries and owns the overall ERP strategy, vendor relationships, and integration roadmap. He works closely with Decryptcode on requirements, prioritization, and acceptance criteria for the ERP integration and reporting projects. He also coordinates with operations, finance, and compliance to ensure that the new dashboards and APIs meet business needs and regulatory expectations. He has been with the company for over a decade and has deep context on how manufacturing, procurement, and logistics use the current systems."
        },
        new User
        {
            Id = "user-005",
            OrgId = "org-002",
            Email = "eve@beta.com",
            Name = "Eve Brown",
            Role = "member",
            Active = true,
            Bio = "Eve is the data and integration specialist on the Beta team. She is responsible for data mapping between the ERP and the new platform, ETL pipelines, and API consumption from the new backend. She ensures data quality and consistency across manufacturing sites and supports UAT with operations teams. She has a background in data engineering and works with Decryptcode to define sync schedules, idempotency, and conflict resolution rules. She also maintains the documentation for data flows and field mappings used by support and analytics."
        },
        new User
        {
            Id = "user-006",
            OrgId = "org-003",
            Email = "frank@gamma.io",
            Name = "Frank Miller",
            Role = "admin",
            Active = true,
            Bio = "Frank is head of engineering at Gamma Labs. He drives technical decisions for the compliance dashboard, security posture, and deployment pipeline, and ensures that engineering practices align with healthcare and privacy regulations. He coordinates with legal and compliance on requirements, audit readiness, and certification efforts. He has experience in regulated industries and insists on clear audit trails, access controls, and encryption standards. He also manages the engineering roadmap and works with Decryptcode on scope, timelines, and acceptance criteria for the compliance dashboard project."
        },
        new User
        {
            Id = "user-007",
            OrgId = "org-004",
            Email = "grace@deltafs.com",
            Name = "Grace Lee",
            Role = "admin",
            Active = true,
            Bio = "Grace is the enterprise architect for Delta Financial. She defines standards for microservices, event-driven design, and cross-team API contracts, and ensures that the migration aligns with the broader technology strategy. She leads the migration steering committee and works with Decryptcode on performance, resilience, and observability. She has led similar modernization efforts in the past and is responsible for architecture review, security sign-off, and alignment with other squads (identity, compliance, infrastructure). She also represents engineering in discussions with risk and audit."
        },
        new User
        {
            Id = "user-008",
            OrgId = "org-004",
            Email = "henry@deltafs.com",
            Name = "Henry Park",
            Role = "member",
            Active = true,
            Bio = "Henry is a senior developer on the settlement and reporting squad at Delta Financial. He implements and maintains ASP.NET Core services, database access layers, and integration tests, and has deep knowledge of the existing Node.js codebase and the business rules being migrated. He works closely with Decryptcode to ensure behavioral parity and to identify edge cases that need special handling. He is also responsible for the team's CI/CD pipeline and test automation. He has been with the company for five years and is the go-to person for questions about settlement logic and reporting calculations."
        }
    ];

    public static readonly List<Project> Projects =
    [
        new Project
        {
            Id = "proj-001",
            OrgId = "org-001",
            Name = "Platform Modernization",
            Status = "active",
            BudgetHours = 800,
            StartDate = "2024-01-01",
            EndDate = "2024-06-30",
            Description = "Full migration of the internal MERN stack application to ASP.NET Core backend and Angular frontend. This project includes the design and implementation of scalable backend services, RESTful APIs, and microservices where appropriate. It covers database schema design, data access layers, authentication and authorization alignment with existing SSO, performance optimization, and comprehensive documentation of architecture and migration runbooks. The work is delivered in phased releases to minimize risk and allow rollback at each stage. Each phase has clear success criteria, monitoring, and sign-off from product and operations. The team collaborates with frontend developers building the Angular application and with DevOps on deployment and feature-flag strategy. Post-launch, the project includes a stabilization period and knowledge transfer to the internal platform team."
        },
        new Project
        {
            Id = "proj-002",
            OrgId = "org-001",
            Name = "API Gateway",
            Status = "active",
            BudgetHours = 200,
            StartDate = "2024-02-01",
            EndDate = null,
            Description = "Introduction of a centralized API gateway in front of the new ASP.NET Core services. The gateway handles rate limiting, request validation, routing, and observability (metrics, logs, correlation IDs). It must integrate with the existing identity provider and support both internal Angular clients and future partner or mobile integrations. The project includes evaluation of gateway technology, configuration and deployment, and documentation and runbooks for operations. Security review is required to ensure that authentication and authorization are correctly enforced at the gateway layer and that no sensitive data is logged. The gateway will also be the single point for API versioning and deprecation policies."
        },
        new Project
        {
            Id = "proj-003",
            OrgId = "org-001",
            Name = "Legacy Audit",
            Status = "completed",
            BudgetHours = 120,
            StartDate = "2023-11-01",
            EndDate = "2023-12-15",
            Description = "Discovery and audit of the existing MERN application: full inventory of endpoints, data models, business rules, third-party dependencies, and integration points. The deliverable included a migration roadmap, risk assessment, and recommendations for phased cutover. All findings were documented in Confluence with diagrams, data flow descriptions, and a list of known technical debt and edge cases. This output was used as the primary input for the Platform Modernization and API Gateway projects. The audit also identified stakeholders, runbooks, and operational procedures that would need to be updated as part of the migration. Final review was completed with product and engineering leadership in December 2023."
        },
        new Project
        {
            Id = "proj-004",
            OrgId = "org-002",
            Name = "ERP Integration",
            Status = "active",
            BudgetHours = 500,
            StartDate = "2024-01-15",
            EndDate = "2024-09-30",
            Description = "Build and maintain RESTful APIs and background jobs that integrate with Beta Industries' ERP systems. The scope covers order sync, inventory levels, supplier data, and compliance-related exports. The frontend team consumes these APIs via Angular dashboards used by operations and planning. The project requires robust error handling, idempotency, and audit logging for financial and regulatory compliance. Data residency and retention policies must be respected across all regions where Beta operates. Additional requirements include documentation of data flows, field mappings, and sync schedules, as well as runbooks for troubleshooting and escalation. UAT is performed with operations and finance before each release."
        },
        new Project
        {
            Id = "proj-005",
            OrgId = "org-003",
            Name = "Compliance Dashboard",
            Status = "planned",
            BudgetHours = 180,
            StartDate = null,
            EndDate = null,
            Description = "Design and implement a compliance-first dashboard for Gamma Labs: clinical workflow views, detailed audit logs, and reporting that meet HIPAA and regional data protection requirements. The backend will be ASP.NET Core with careful attention to access control, encryption at rest and in transit, and audit trails for all access to protected health information. The Angular frontend will provide role-based views and export capabilities for compliance reviews and external auditors. The project will follow a defined security and privacy review process and must support the retention and export formats required by Gamma's compliance team. Success criteria include passing internal security review and readiness for external audit."
        },
        new Project
        {
            Id = "proj-006",
            OrgId = "org-004",
            Name = "Settlement & Reporting Microservices",
            Status = "active",
            BudgetHours = 600,
            StartDate = "2024-02-01",
            EndDate = "2024-08-31",
            Description = "Migration of trade settlement and reporting workflows from legacy Node.js to ASP.NET Core microservices. The project includes event-driven design for consistency across services, idempotent handlers, and integration with the existing message bus and identity systems. Delivery is in increments with feature flags and canary releases; each increment is reviewed by the settlement and reporting squads and signed off by risk and compliance where applicable. Performance and reliability are critical given the financial impact of settlement; full observability (metrics, distributed tracing, alerting) and runbooks are required. The team works with Decryptcode on architecture, code review, and production readiness. Security and penetration testing are part of the release process."
        }
    ];

    public static readonly List<Invoice> Invoices =
    [
        new Invoice
        {
            Id = "inv-001",
            OrgId = "org-001",
            ProjectId = "proj-001",
            Amount = 45000,
            Currency = "USD",
            Status = "sent",
            DueDate = "2024-04-15",
            IssuedAt = "2024-03-15",
            Description = "Platform Modernization \u2013 Phase 1: architecture review and decision records, backend solution design, initial API implementation (organizations, users, projects, time entries, invoices, dashboard), and migration planning including runbooks and rollback procedures. This invoice covers all professional services and deliverables through March 15, 2024. Payment terms: Net 30 from invoice date. Please reference this invoice number and your PO (if applicable) when remitting payment. For questions regarding this invoice, contact the Decryptcode accounts team. Thank you for your partnership."
        },
        new Invoice
        {
            Id = "inv-002",
            OrgId = "org-001",
            ProjectId = "proj-002",
            Amount = 12000,
            Currency = "USD",
            Status = "draft",
            DueDate = null,
            IssuedAt = null,
            Description = "API Gateway \u2013 design and initial implementation. This is a draft invoice and will be finalized upon completion of routing configuration, rate limiting, observability integration, and operations runbooks. The final invoice will include line items for development, testing, documentation, and handover. Do not remit payment against this draft. You will receive an updated invoice with correct amounts and payment instructions once the scope is complete and approved by the project lead."
        },
        new Invoice
        {
            Id = "inv-003",
            OrgId = "org-002",
            ProjectId = "proj-004",
            Amount = 28000,
            Currency = "GBP",
            Status = "paid",
            DueDate = "2024-03-30",
            IssuedAt = "2024-03-01",
            Description = "ERP Integration \u2013 discovery workshops, data mapping and technical specification, and first phase of API development and scheduled sync jobs. This invoice has been paid in full. Thank you for your partnership and timely payment. Please retain this document for your records and audit trail. If you require a duplicate or certified copy for compliance purposes, please contact the Decryptcode accounts team. We look forward to continuing the engagement through the next phases of the project."
        },
        new Invoice
        {
            Id = "inv-004",
            OrgId = "org-004",
            ProjectId = "proj-006",
            Amount = 52000,
            Currency = "USD",
            Status = "sent",
            DueDate = "2024-04-30",
            IssuedAt = "2024-04-01",
            Description = "Settlement & Reporting Microservices \u2013 Phase 1: architecture design and event contracts, first microservice implementation (settlement API and data access layer), and integration with identity and audit systems. This invoice covers professional services for February and March 2024. Payment terms: Net 30 from invoice date. Please remit to the address on file and include the invoice number with your payment to ensure proper application. For any discrepancies or questions, contact the Decryptcode project manager or accounts team before the due date."
        }
    ];
}
