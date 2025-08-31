# 🐱 FleduSnack

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=06luki06_FleduSnack&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=06luki06_FleduSnack)

FleduSnack is a simple web application for tracking what your cat(s) eat and how much they like it.  
It is built with **ASP.NET Core (Web API + Blazor)**, **PostgreSQL**, and runs in **Docker**.  

---

## 🚀 Features

- Manage **Cats** (add, edit, delete)
- Manage **Dishes** for each cat:
  - Brand, Flavor, Rating (😶 Not Reviewed, 😾 Low, 😐 Mid, 😻 High)
  - Optional photo upload
- **Blazor UI** (mobile-first, Bootstrap-based)
- REST API with **Swagger/OpenAPI**
- **Docker Compose** for running API + PostgreSQL database

---

## 🛠️ Tech Stack

- **Backend:** ASP.NET Core 8 (Web API + EF Core)
- **Frontend:** Blazor (Server + Bootstrap components)
- **Database:** PostgreSQL
- **Containerization:** Docker & Docker Compose

## ⚙️ Configuration

Copy the .env.template and adjust the corresponding values.

---

▶️ Run with Docker

Make sure you have Docker and Docker Compose installed.

docker-compose up --build

---
