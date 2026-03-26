# ApplicationSubmissionManagement — services + API Gateway

**Context.** A microservices-style backend for managing an application submission flow (users/auth, leads/bookings, college information, and applications) behind a single API Gateway.

## What this repo is

- **`APIGateway/`**: ASP.NET Core **reverse proxy** (YARP). Routes requests by path prefix and aggregates Swagger endpoints.
- **`ApplicationService/`**: Application domain service (JWT-protected API).
- **`LeadManagementService/`**: Lead / booking management service (JWT-protected API).
- **`CollegeInfoService/`**: College information service (JWT-protected API).
- **`SharedContracts/`**: Shared contracts used across services.

## How it works (high level)

- Clients talk to the **Gateway**.
- The Gateway forwards based on `APIGateway/appsettings.json` routes, e.g.:
  - `/auth/*` and `/user/*` → `userservice` cluster
  - `/bookings/*` → `leadservice` cluster
  - `/collegeinfo/*` → `collegeinfo` cluster
  - `/swagger/<service>/*` → proxied downstream Swagger UIs
- Each service exposes its own Swagger and uses **JWT Bearer** authentication (see each service `Program.cs`).

## Running (general)

- Start the downstream services first (each has its own `.sln` under its folder), then run `APIGateway`.
- In development, the Gateway Swagger UI is configured to point at localhost ports (see `APIGateway/Program.cs`).

