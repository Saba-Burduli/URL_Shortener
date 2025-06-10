# URL Shortener

A high-performance, scalable **URL Shortener** service built with **.NET 9 Web API** and **Apache Cassandra**, containerized with **Docker Compose**.

---

## 📌 Features

* Shorten long URLs with custom or auto-generated short codes
* Expiration handling for temporary URLs
* Click tracking with user-agent and IP logging
* Base62 encoding for short code generation
* Clean Architecture with separate layers
* Dockerized for development and deployment

---

## 🚀 Tech Stack

* .NET 9 Web API
* Apache Cassandra 4.1
* Docker & Docker Compose
* CQL (Cassandra Query Language)

---

## 📅 Project Structure

```
UrlShortener/
├── docker/
│   └── cassandra-schema.cql         # Cassandra DB schema
├── src/
│   └── UrlShortener.Api/
│       ├── Controllers/             # API Controllers
│       ├── Data/                    # Cassandra session & factory
│       ├── Models/                  # DTOs and Entities
│       ├── Services/                # Business logic services
│       ├── Program.cs               # App entry point
│       └── Startup.cs               # DI and middleware setup
├── docker-compose.yml
└── README.md
```

---

## 🚧 Prerequisites

* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* [Docker](https://www.docker.com/products/docker-desktop)

---

## 💪 Run the Project

1. **Clone the repository**:

   ```bash
   git clone [https://github.com/your-username/UrlShortener.git](https://github.com/Saba-Burduli/URL_Shortener.git)
   cd UrlShortener
   ```

2. **Run using Docker Compose**:

   ```bash
   docker-compose up --build
   ```

3. **Access Swagger UI**:

   * Visit: `http://localhost:5000/swagger`

---

## 🚨 API Endpoints

### `POST /api/urls`

Create a new short URL.

```json
{
  "originalUrl": "https://example.com",
  "expirationDate": "2025-12-31T23:59:59Z",
  "customAlias": "my-alias"  // Optional
}
```

### `GET /{shortcode}`

Redirects to the original URL.

### `GET /api/urls/{shortcode}`

Get full details of the short URL.

### `PUT /api/urls/{shortcode}`

Update the original URL or expiration date.

### `DELETE /api/urls/{shortcode}`

Delete a short URL.

---

## ⏱️ Expiration Job

* Periodically deactivates expired URLs using Cassandra queries.
* Expired or deactivated URLs return `404`.

---

## 📃 Cassandra Schema

```sql
CREATE KEYSPACE IF NOT EXISTS urlshortener WITH replication = {'class': 'SimpleStrategy', 'replication_factor': '1'};

USE urlshortener;

CREATE TABLE urls (
    shortcode text PRIMARY KEY,
    originalurl text,
    createdat timestamp,
    expirationdate timestamp,
    clickcount int,
    isactive boolean
);

CREATE TABLE analytics (
    shortcode text,
    clickdate timestamp,
    useragent text,
    ipaddress text,
    PRIMARY KEY (shortcode, clickdate)
) WITH CLUSTERING ORDER BY (clickdate DESC);
```

---


