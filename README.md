# Enterprise Employee Management System (EEMS)

## Overview
This is a comprehensive FullStack application designed to manage corporate workforce data. [cite_start]It provides a robust interface for CRUD operations, advanced filtering, and real-time monitoring[cite: 3, 7].

## Tech Stack
- [cite_start]**Backend:** .NET 8 Web API, Entity Framework Core (SQL Server)[cite: 8, 10].
- [cite_start]**Frontend:** Angular 17+, Material Design, RxJS[cite: 14, 16].
- [cite_start]**Cloud:** Azure App Service, Azure SQL, Application Insights[cite: 11, 19].
- [cite_start]**DevOps:** GitHub Actions (CI/CD)[cite: 21].

## Key Features
- [cite_start]**Advanced Dashboard:** Server-side pagination, sorting, and multi-criteria filtering[cite: 16].
- [cite_start]**Global Error Handling:** Centralized HTTP interceptor for user-friendly notifications and error tracking[cite: 17].
- [cite_start]**Observability:** Integrated Azure Application Insights for request monitoring and exception logging[cite: 11].
- [cite_start]**Notification System:** Automatic email alerts for critical (500) server errors[cite: 12].

## [cite_start]Setup Instructions [cite: 29]

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
``bash
cd EES.UI
npm install
ng serve