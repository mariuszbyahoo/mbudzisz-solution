# MBudzisz Solution — Decryptcode Take-Home Assessment

This repository contains my implementation of the Decryptcode take-home assessment.

The goal of the assignment was to migrate the provided reference application to:

- **ASP.NET Core** for the backend
- **Angular** for the frontend

while preserving the expected API contract and the core user-facing behavior of the original application.

---

## Overview

This solution includes:

- an **ASP.NET Core Web API** implementing the required endpoints
- an **Angular frontend** recreating the core pages and flows from the provided React application
- a backend **integration/unit tests** and Angular **service/component tests**
- a documented **security review** of the supplied reference implementation

The implementation intentionally prioritizes:

- correctness
- contract fidelity
- maintainability
- secure handling of untrusted input
- clear, reviewer-friendly code

---

## Security Review of the Reference Implementation

During initial review of the supplied reference repository, I found that the provided backend mock data layer contained an **obfuscated dynamic code-loading pattern**.

Specifically, the original Node.js reference backend included logic that:

- fetched JavaScript from an external URL
- dynamically executed the downloaded payload at runtime
- embedded that behavior inside the mock data structure

Because this introduced an unnecessary remote code execution risk, I **did not run the original backend locally as-is**.

Instead, I treated the provided repository as an **untrusted behavioral reference** and used it only for:

- reviewing expected routes and response shapes
- understanding the frontend structure and behavior
- safely inspecting and sanitizing the mock data

This repository uses only **sanitized local seed data**.  
The unsafe artifact was intentionally not carried into the final solution.

---

## What Was Implemented

### Backend
The ASP.NET Core backend implements the required endpoints:

- `GET /health`
- `GET /api/dashboard`
- `GET /api/organizations`
- `GET /api/organizations/:id`
- `GET /api/organizations/:id/summary`
- `GET /api/users`
- `GET /api/users/:id`
- `GET /api/projects`
- `GET /api/projects/:id`
- `GET /api/time-entries`
- `GET /api/invoices`

Filtering behavior from the reference implementation was preserved where applicable, including:

- organizations by `tier` and `industry`
- users by `orgId`, `role`, and `active`
- projects by `orgId` and `status`
- time entries by `userId`, `projectId`, `from`, and `to`
- invoices by `orgId` and `status`

### Frontend
The Angular frontend recreates the main pages of the provided React application:

- **Dashboard**
- **Organizations list**
- **Organization detail**
- **Projects list**
- **Project detail**

The original React frontend was used as a **behavioral reference**, while the final implementation follows Angular conventions and consumes the ASP.NET Core backend.

---

## Tech Stack

### Backend
- ASP.NET Core
- xUnit
- `Microsoft.AspNetCore.Mvc.Testing`

### Frontend
- Angular
- TypeScript
- Angular TestBed
- `HttpTestingController`

---

## Project Structure

```text
backend/
  MBudziszSolution/
    MBudziszSolution.API/
    MBudziszSolution.Tests.Integration/
    MBudziszSolution.Tests.Unit/

frontend/
  src/app/
    core/
    features/
      dashboard/
      organizations/
      projects/
    shared/
```

## Implementation Notes

### Backend

The backend is intentionally simple and contract-focused:
- controller-based HTTP API
- sanitized in-memory seed data
- explicit filtering and computed responses
- no unnecessary infrastructure

I intentionally avoided introducing complexity that was not justified by the scope of the task such as: 
- a database
- EF Core
- repository layers
- CQRS/MediatR
- excessive abstraction

### Frontend

The Angular frontend focuses on:
- simple route-driven navigation
- clear data flow
- loading and error states
- straightforward component structure
- relative API access via proxy

The emphasis was on producing a maintainable migration rather than reproducing the React implementation line-for-line. 

## Running the backend

From the repository root:
```
cd backend/MBudziszSolution/MBudziszSolution.API
dotnet restore
dotnet run

```

The backend is configured for local development on:

```

http://localhost:4001

```

Example endpoints: 

```

http://localhost:4001/health
http://localhost:4001/api/dashboard
http://localhost:4001/api/organizations

```

## Running the Frontend

From the repository root:

```

cd frontend
npm install
ng serve

```

The frontend runs on:

```

http://localhost:3001

```

The frontend is configured to proxy: 

- /api -> http://localhost:4001

- /health -> http://localhost:4001

This keeps frontend API calls relative and environment-friendly.

## Running Tests

### Backend Tests

```

cd backend/MBudziszSolution
dotnet test

```

### Frontend tests

```

cd frontend
npm test -- --watch=false

```

## API Documentation / Postman Collection

For convenient local API exploration and manual verification, the backend project includes a 
ready-to-import Postman collection documenting the implemented endpoints and common filtered 
requests.

Location:

```
backend/MBudziszSolution/MBudziszSolution.API/Decryptcode - MBudzisz.postman_collection.json
```

If needed, update the collection 'baseUrl' variable to match the local backend port used by the repository.

## Test Suite Summary

The final solution includes automated tests across both backend (46 tests) and frontend (42 tests).

### Testing Philosophy

I wanted the final test suite to reflect the way I approach engineering work in practice.

The test strategy intentionally focuses on:

- backend integration tests for API contract and observable behavior
- frontend service tests for correct API usage
- frontend component tests for rendering and user-visible states

This gives good confidence in correctness without overengineering the solution.

## Assumptions and Tradeoffs

### Intentional choices

- The backend uses sanitized in-memory seed data rather than a database because the reference 
implementation itself is mock-data based, and adding persistence would not improve the value of 
the exercise.

- The Angular frontend preserves the behavior and intent of the reference React app rather than
copying implementation details.

- The backend is treated as the source of truth for the final API contract.

- The solution favors clarity and maintainability over architectural ceremony.

### Reasonable omissions

- No frontend E2E tests - appropriate for take-home scope given the combination of backend 
integration tests and Angular component/service tests.

- No mutation tests or coverage gates — omitted intentionally for scope balance.

## Development History Highlights

A short summary of the most important implementation decisions:

1. Reviewed the supplied reference repository before execution
2. Identified unsafe remote-code-loading logic in the provided mock data
3. Chose not to run the original backend as-is
4. Sanitized the reference data and reimplemented the backend safely
5. Built the ASP.NET Core API around the expected contract
6. Migrated the frontend to Angular using the React app as a behavioral reference
7. Resolved frontend/backend contract mismatches during implementation
8. Expanded automated testing across backend and frontend

## Final Notes

This repository reflects not only the migration itself, but also the engineering approach I try 
to bring to production work:

- inspect supplied inputs critically

- avoid blindly trusting third-party code

- preserve contracts deliberately

- keep code maintainable

- back important behavior with tests