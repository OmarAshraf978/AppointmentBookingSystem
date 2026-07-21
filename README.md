# Appointment Booking System API

A professional Appointment Booking System built with ASP.NET Core 9 following Clean Architecture and CQRS principles.
The project provides a complete backend solution for booking appointments between users and service providers with secure authentication,
role-based authorization, appointment management, and reviews.

---

## Technologies

- ASP.NET Core 9 Web API
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Authentication
- Clean Architecture
- CQRS
- MediatR
- AutoMapper
- FluentValidation
- Result Pattern

---

# Architecture

The project follows Clean Architecture.

```
Presentation
│
Application
│
Domain
│
Persistence
│
Infrastructure
│
Shared
```

---

# Authentication & Authorization

- User Registration
- User Login
- JWT Authentication
- ASP.NET Core Identity
- Role-Based Authorization

Supported Roles:

- User
- Service Provider
- Admin

---

# Service Provider Module

### Features

- Register as Service Provider
- Update Profile
- Soft Delete
- Approve Service Provider (Admin)
- Reject Service Provider (Admin)
- Get By Id
- Get All Service Providers

---

# Services Module

### Features

- Create Service
- Update Service
- Delete Service (Soft Delete)
- Get Service By Id
- Get All Services

Each service contains:

- Name
- Description
- Price
- Duration
- Service Provider

---

# Appointment Module

### Features

- Create Appointment
- Cancel Appointment
- Approve Appointment
- Reject Appointment
- Get Appointment By Id
- Get My Appointments
- Get Appointments For Service Provider

### Appointment Rules

- Users cannot book their own services.
- Service Provider must be approved.
- Deleted services cannot be booked.
- Deleted providers cannot receive appointments.
- Appointment time conflicts are prevented.
- Appointment starts with **Pending** status.
- Only Pending appointments can be Approved or Rejected.

Appointment Status:

- Pending
- Approved
- Rejected
- Cancelled

---

# Review Module

### Features

- Create Review
- Delete Review (Soft Delete)
- Get Review By Id
- Get Reviews For Service

### Review Rules

- User must have an Approved appointment.
- Deleted services cannot receive reviews.
- Soft Delete support.
- Reviews are accessible only to:
  - Review Owner
  - Service Provider
  - Admin

---

# Business Rules

- Soft Delete supported.
- Authorization inside handlers.
- Validation using FluentValidation.
- Result Pattern for responses.
- Role-based access control.
- Prevent appointment conflicts.
- Secure JWT Authentication.

---

# Packages

- MediatR
- AutoMapper
- FluentValidation
- Entity Framework Core
- ASP.NET Identity
- JWT Bearer Authentication

---

# API Features

Authentication
Service Providers
Services
Appointments
Reviews

---

# Design Patterns

- Clean Architecture
- CQRS
- Repository Pattern
- Unit Of Work
- Result Pattern
- Dependency Injection

---

# Future Improvements

- Dashboard & Statistics
- Notifications
- Email Confirmation
- Images Upload
- Pagination
- Search & Filtering
- Swagger Documentation Improvements
