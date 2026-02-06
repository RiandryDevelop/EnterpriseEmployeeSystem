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



Architecture Diagram
The system follows a modern cloud-native architecture optimized for Azure:


Frontend: Hosted on Azure Static Web Apps, communicating via HTTPS with the API.


API Gateway: A .NET 8 Web API hosted on Azure App Service.


Data Layer: Azure SQL Database managed through Entity Framework Core.

Monitoring & Alerts: Application Insights captures telemetry. When a 500 error occurs, a Notification Service triggers an alert.

Secrets Management
To comply with security best practices:


Production Secrets: Sensitive data like the SQL Connection String and Application Insights Connection String are stored exclusively in Azure App Settings (Environment Variables).

CI/CD Injection: The API_URL and App Insights Keys for the frontend are injected dynamically during the GitHub Actions build process using GitHub Secrets, ensuring no sensitive data is committed to the repository.

Observability & Notification Logic

Global Interceptor: The Angular frontend uses an HttpInterceptor to catch all server errors.


500 Error Alerts: The Backend includes a global exception handler that logs errors directly to Application Insights.


Email Simulation: Upon a critical failure, the system triggers a notification service that logs the event as a "Critical Email Alert" to the technical team, ensuring 24/7 monitoring awareness.

🚀 Live Deployment

🛠️ CI/CD Pipeline
The project uses a multi-stage GitHub Actions workflow:


Backend Quality Gate: .NET restore, build, and unit testing.

Frontend Build: Angular production compilation with environment injection.


Azure Deploy: Parallel deployment to App Service and Static Web Apps.