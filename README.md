# Kitchen Routing System

## Overview

This project implements a simple **Restaurant Order Routing System**. It exposes an HTTP API that receives orders from multiple POS (Point of Sale) systems and routes each order item to an in-memory queue representing a specific kitchen area.

The solution was designed to be **simple, clear, and easy to extend**, focusing on code readability, separation of concerns, immutability, concurrency safety, and testability.

---

## Assumptions

1. An order contains one or more items.
2. Each item belongs to exactly one kitchen area.
3. Order items are routed independently to their corresponding kitchen area queues.
4. Multiple POS systems may send orders concurrently.
5. Orders are stored in memory only and are lost when the application restarts.
6. The system runs as a single application instance (no distributed processing).

---

## Kitchen Areas

The supported kitchen areas are:

| Value | Area   | Description      |
|-------|--------|------------------|
| 0     | Fries  | Fries station    |
| 1     | Grill  | Grill station    |
| 2     | Salad  | Salad station    |
| 3     | Drink  | Drink station    |
| 4     | Desert | Desert station   |

---

## API Design

### POST /orders

Receives an order and routes each item to its corresponding kitchen area queue.

**Request body example:**

```json
{
  "items": [
    { "itemName": "Burger", "destinationArea": "1" },
    { "itemName": "Fries", "destinationArea": "0" }
  ]
}
```

**Responses:**

* `202 Accepted` � Order successfully routed
* `400 Bad Request` � Invalid order payload

The API is documented using **Swagger/OpenAPI**, including XML comments for endpoints.

---

## Architecture

The project is organized into clear layers:

* **Controllers**: HTTP endpoints
* **Domain**: Core domain models (immutable)
* **DTOs**: API request models
* **Services**: Business logic (order routing)
* **Infrastructure**: In-memory storage and concurrency handling

Dependency Injection is used throughout the application.

---

## Concurrency Handling

To support multiple concurrent POS requests, the application uses thread-safe collections:

* `ConcurrentDictionary`
* `ConcurrentQueue`

This ensures safe access to shared in-memory queues without explicit locking.

---

## Tests

The solution includes automated tests:

### Unit Tests

* Validate that each order item is routed to the correct kitchen area queue.

### Integration Tests

* Validate the full HTTP pipeline by calling the `POST /orders` endpoint and verifying the HTTP response.

Tests are implemented using **xUnit** and **Microsoft.AspNetCore.Mvc.Testing**.

---

## How to Run

### Prerequisites

* .NET 8 SDK

### Run the API

```bash
dotnet run --project KitchenRouting
```

The API will be available at:

```
https://localhost:<port>/swagger
```

### Run Tests

```bash
dotnet test
```

---

## Notes

* No database or external dependencies are used.
* No reverse proxy or container setup is included, as it is outside the scope of this challenge.
* The solution was intentionally kept simple to allow easy extension in future steps.
