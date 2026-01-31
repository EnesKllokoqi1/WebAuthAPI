# WebAuthAPI: Construction & Assignment Management

This project is a functional, detailed ASP.NET Core Web API designed to manage the practical workflows of a construction business.  
It focuses on property acquisition, workforce management, and a centralized assignment system, all built using PostgreSQL,C# and modern development patterns.

---

## 🏗️ The Assignment Workflow

The core of this application is the Assignment Engine.  
It is designed to handle the real-world complexity of connecting three distinct entities into a single task.

### Entities

- **Users**
  - Can acquire buildings
  - Responsible for initiating assignments

- **Buildings**
  - Specific locations or properties where work is performed

- **Workers**
  - Staff members created and managed by the Admin
  - Perform the assigned tasks

### Assignment Logic

When an assignment is created, it is not initially connected to any User, Building, or Worker.The system programmatically links the User, Building and the Worker using the required authenticated endpoints for connecting them. This allows for a clear audit trail of who is working where, which property is being serviced, and who is responsible for the assignment.

To Start, Complete, Confirm, Reject, Cancel assignments the assignment must be connected to a User, Worker and Building after Creation.

 ### Example: Create Assignment


```http
POST /api/assignments/create-assignment
```
 Response:

```json
{
  "status": "Requested",
  "assignmentId": "123e4567-e89b-12d3-a456-426614174000",
  "createdAt": "2026-01-31T10:15:30Z", 
  "startTime": null, 
  "endTime": null,  
  "description": "Electrical inspection for newly acquired property",
  "priority": "High"
}
```
StartTime and EndTime are null because we start and finish assignments on different
endpoints and we can't create  duplicate assignments.

---

##  🚀  Key Features

- 36 functional API endpoints
- Comprehensive user authentication and administrative tools
- JWT authentication with refresh token support
- Secure long-term user sessions
- Role-based permission system
  - Admins manage the workforce
  - Users manage properties and assignments
- Efficient and secure data transfer using 15+ specialized DTOs 
- Clean Data Handling
 - Separation of Concerns to keep controllers, services, and data independent
 - Dependency Injection for testable and decoupled components
-  Service–Interface pattern for organized and maintainable business logic

---

## 🛠️ Tech Stack

- **Framework:**  .NET 9.0 (ASP.NET Core)
- **Database:** PostgreSQL
- **Documentation:** Scalar (interactive API reference)
- **Authentication:** JWT with refresh tokens
- **Data Management:** Entity Framework Core
  - Automated migrations
  - Database seeding
  - Database Indexing for emails
---

## 📂 Project Structure

The project follows a standard Separation of Concerns approach.

- ```Controllers/```
  - Manages the 36 API endpoints and routes

- ```Services/```
  - Contains the business logic for:
    - Buying buildings
    - Creating Workers 
    - Creating assignments
    - Authentication related stuff
    
    

- ```DTOs/```
  - Categorized data transfer models
  - Includes Assignment, Building, User, and Worker DTOs

- ```Enums/```
  - Centralized definitions for:
    - Status
    - Priority
    - Role
    - Gender

---

## 📖 API Documentation

**Scalar** is integrated to provide a clean and interactive interface for exploring all API endpoints.

### Steps to Access

1. Run the project
2. Navigate to:

`https://localhost:{PORT}/scalar/v1`

or 

`https://localhost:{PORT}/scalar`

---

## 🚦 Getting Started

### 1.Clone the Repository

```bash
git clone https://github.com/YourUsername/WebAuthAPI.git
```
### 2.Configure the Database
   
Point the connection string in ```appsettings.json``` to your PostgreSQL instance.

Or create a ```.env``` file with a PostgreSQL database connection string variable.

### 3.Apply Migrations
```bash
dotnet ef database update
```

### 4. Launch the Application

Run the project via Visual Studio or use the command:

```bash
dotnet run
```
