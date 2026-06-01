# Office Manager API

## 📝 Description
Office Manager API is a backend application developed with ASP.NET Core Web API for managing room and equipment reservations within a company.
The application allows employees to reserve meeting rooms and equipment while administrators manage users and resources.

## 🛠 Technologies
* ASP.NET Core Web API (.NET 9)
* Entity Framework Core
* SQL Server
* JWT Authentication
* Scalar (OpenAPI) Documentation
* C#

## 🌟 Features

### Authentication
* User login
* JWT token generation
* Role-based authorization
* Email confirmation

### User Management
* Create users
* Update users
* Delete users
* Manage user roles

### Reservation Management
* Create reservation
* Update reservation
* Cancel reservation
* Transfer reservation
* Recurring reservations

### Resource Management
* Rooms management
* Equipment management
* Availability checking

## 🗄 Database
Main entities:
* Users
* Reservations
* Rooms
* Equipment

## 📡 API Endpoints

### Authentication
* `POST /api/auth/login`
* `POST /api/auth/register`

### Reservations
* `GET /api/reservations`
* `POST /api/reservations`
* `PUT /api/reservations/{id}`
* `DELETE /api/reservations/{id}`

## 🚀 Installation

1. **Clone the repository:**
```bash
git clone https://github.com/liudmyla53/office-manager-api.git
```

2. **Configure Database & JWT:**
Open `appsettings.json` and set your SQL Server connection string and JWT secret key.

3. **Restore packages and update database:**
```bash
dotnet restore
dotnet ef database update
```

4. **Run the application:**
```bash
dotnet run
```

---
**Author:** Liudmyla