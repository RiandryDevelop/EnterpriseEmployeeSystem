```markdown
# EES API - Backend Service

## Features
- **CQRS Pattern:** Organized command and query handling.
- **EF Core Integration:** Data persistence with SQL Server[cite: 10].
- **Application Insights:** Telemetry tracking for requests and failures[cite: 11].
- **Email Alert Service:** Mock/Log implementation for notifying the tech team of 500 errors[cite: 12, 13].

## API Endpoints
- `GET /api/employees`: Paginated list with search[cite: 9].
- `POST /api/employees`: Create new record[cite: 9].
- `PUT /api/employees/{id}`: Update record[cite: 9].
- `DELETE /api/employees/{id}`: Remove record[cite: 9].
