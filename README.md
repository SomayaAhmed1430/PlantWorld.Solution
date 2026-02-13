# PlantWorld 🌱

PlantWorld is a full-stack ASP.NET solution for an online plant store.

The solution consists of:
- **PlantWorld.ApiProvider** → RESTful Web API
- **PlantWorld.MvcConsumer** → ASP.NET MVC application consuming the API

---

## 🧩 Solution Structure
PlantWorld
│
├── PlantWorld.ApiProvider
│ ├── Products & Categories
│ ├── Cart (Session-based, no login)
│ ├── Checkout (Guest Orders)
│ └── Swagger API Documentation
│
├── PlantWorld.MvcConsumer
│ ├── Product Listing
│ ├── Cart UI
│ └── Checkout Flow
│
└── README.md

---

## 🚀 Features

### API
- CRUD Products & Categories
- Cart using SessionId (No authentication)
- Checkout & Orders
- Order status management
- Swagger for testing endpoints

### MVC Consumer
- Display products & categories
- Add products to cart
- Checkout without login
- Consumes API endpoints

---

## 🛠 Tech Stack
- ASP.NET Core
- Entity Framework Core
- SQL Server
- ASP.NET MVC
- Swagger

---

## 🧠 Notes
- No authentication (guest checkout for simplicity)
- Designed for learning & portfolio purposes

