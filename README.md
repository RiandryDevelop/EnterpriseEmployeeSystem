# Enterprise Employee Management System (EEMS)

## Overview
This is a comprehensive FullStack application designed to manage corporate workforce data. It provides a robust interface for CRUD operations, advanced filtering, and real-time monitoring.

## Tech Stack
- **Backend:** .NET 8 Web API, Entity Framework Core (SQL Server).
- **Frontend:** Angular 17+, Material Design, RxJS.
- **Cloud:** Azure App Service, Azure SQL, Application Insights.
- **DevOps:** GitHub Actions (CI/CD).

## Key Features
- **Advanced Dashboard:** Server-side pagination, sorting, and multi-criteria filtering.
- **Global Error Handling:** Centralized HTTP interceptor for user-friendly notifications and error tracking.
- **Observability:** Integrated Azure Application Insights for request monitoring and exception logging.
- **Notification System:** Automatic email alerts for critical (500) server errors.

## Setup Instructions 

### Prerequisites
- .NET 8 SDK
- Node.js & Angular CLI
- SQL Server (Local or Azure)

### Local Development
1. **Backend:**
   ```bash
   cd EES.API
   dotnet restore
   dotnet run
2. **Frontend:**
```bash
cd EES.UI
npm install
ng serve
