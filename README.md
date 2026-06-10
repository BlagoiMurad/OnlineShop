# OnlineShop API

A RESTful Web API for an online shop built with ASP.NET Core 8.0.

## Technologies
- ASP.NET Core 8.0
- Entity Framework Core 8.0
- Microsoft SQL Server
- ASP.NET Identity
- JWT Authentication
- NUnit & Moq
- Vanilla JS Frontend

## Project Structure
- **OnlineShopApi** - Main API project
- **OnlineShop.Core** - Entity models, interfaces, DTOs
- **OnlineShop.Infrastructure** - DbContext, services, data seeding
- **OnlineShop.Tests** - Unit tests
- **OnlineShop.Frontend** - Vanilla JS frontend

## Features
- Product management (CRUD)
- Category management (CRUD)
- Order processing
- User authentication with JWT
- Role-based authorization (User, Administrator)
- Admin panel
- Shopping cart
- Unit tests with 70%+ code coverage

## Default Admin Account
- Email: `admin@onlineshop.com`
- Password: `Admin123!`

## API Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/auth/register | Register new user |
| POST | /api/auth/login | Login and get JWT token |
| GET | /api/products | Get all products |
| GET | /api/products/{id} | Get product by ID |
| GET | /api/products/category/{id} | Get products by category |
| GET | /api/products/search | Search products |
| POST | /api/products | Create product (Admin) |
| PUT | /api/products/{id} | Update product (Admin) |
| DELETE | /api/products/{id} | Delete product (Admin) |
| GET | /api/categories | Get all categories |
| GET | /api/categories/{id} | Get category by ID |
| POST | /api/categories | Create category (Admin) |
| PUT | /api/categories/{id} | Update category (Admin) |
| DELETE | /api/categories/{id} | Delete category (Admin) |
| GET | /api/orders | Get all orders (Admin) |
| GET | /api/orders/{id} | Get order by ID |
| GET | /api/orders/user/{userId} | Get orders by user |
| POST | /api/orders | Create order |
| PUT | /api/orders/{id}/status | Update order status (Admin) |
| DELETE | /api/orders/{id} | Delete order (Admin) |
| GET | /api/admin/users | Get all users (Admin) |
| DELETE | /api/admin/users/{id} | Delete user (Admin) |
| POST | /api/admin/users/{id}/roles | Add role to user (Admin) |

## Setup
1. Clone the repository
2. Update connection string in `appsettings.json`
3. Run migrations: `Update-Database`
4. Run the project with F5
5. Open Swagger at `https://localhost:PORT/swagger`

## Running Tests
Open Test Explorer in Visual Studio and run all tests.
