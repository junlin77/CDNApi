
## CDNApi

CDNApi is a RESTful API built with .NET 8.0 for user authentication, registration, and data management. The API provides endpoints for managing users, authenticating them with JWT, and handling the refresh token mechanism.

## Table of Contents

- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [API Endpoints](#api-endpoints)
  - [Users](#users)
  - [Authentication](#authentication)
- [Configuration](#configuration)

## Technologies Used

- **.NET 8.0**
- **Entity Framework Core** (EF Core) for database interaction
- **BCrypt.Net-Next** for hashing passwords
- **JWT Bearer Authentication** for securing endpoints
- **Swashbuckle** for generating Swagger API documentation
- **Npgsql** for PostgreSQL database connectivity
- **Memory Caching** for optimizing repeated queries

## Installation

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- PostgreSQL Database (can be replaced with other DBMS by adjusting connection settings)
- An IDE like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Steps

1. Clone the repository:

    ```bash
    git clone https://github.com/junlin77/CDNApi.git
    cd CDNApi
    ```

2. Install dependencies:

    ```bash
    dotnet restore
    ```

3. Configure your `appsettings.json` to set up database connection strings and JWT settings.

4. Run the application:

    ```bash
    dotnet run
    ```

5. Access the API at https://localhost:5001 (or your specified port).

## API Endpoints

### Users

#### Get All Users (Paginated)

- **GET /api/users**

This endpoint retrieves a paginated list of users. You can pass `pageNumber` and `pageSize` as query parameters.

- **Parameters:**
  - `pageNumber`: The page number (default: 1).
  - `pageSize`: The number of users per page (default: 10).

- **Response:**
  - `200 OK`: Returns a list of users.
  - `400 Bad Request`: Invalid parameters.

- **Example:**

    ```bash
    GET /api/users?pageNumber=1&pageSize=10
    ```

#### Get a Single User

- **GET /api/users/{id}**

This endpoint retrieves a single user by ID.

- **Parameters:**
  - `id`: The user ID.

- **Response:**
  - `200 OK`: Returns the user object.
  - `404 Not Found`: If the user does not exist.

- **Example:**

    ```bash
    GET /api/users/1
    ```

#### Create a New User

- **POST /api/users**

This endpoint creates a new user. The user object should include required fields like username, email, and password.

- **Request Body:**

    ```json
    {
      "username": "newuser",
      "email": "newuser@example.com",
      "phoneNumber": "123",
      "skillSets": "Coding",
      "hobby": "Sleeping"
    }
    ```

- **Response:**
  - `201 Created`: User created successfully.
  - `400 Bad Request`: Invalid data or missing fields.

- **Example:**

    ```bash
    POST /api/users
    ```

#### Update an Existing User

- **PUT /api/users/{id}**

This endpoint updates a user's details.

- **Parameters:**
  - `id`: The user ID.
  - `user`: The user object with updated information.

- **Response:**
  - `204 No Content`: Update successful.
  - `400 Bad Request`: Invalid data.
  - `404 Not Found`: User not found.

- **Example:**

    ```bash
    PUT /api/users/1
    ```

#### Delete a User

- **DELETE /api/users/{id}**

This endpoint deletes a user by ID.

- **Parameters:**
  - `id`: The user ID.

- **Response:**
  - `204 No Content`: Deletion successful.
  - `404 Not Found`: User not found.

- **Example:**

    ```bash
    DELETE /api/users/1
    ```

### Authentication

#### Register a New API User

- **POST /api/auth/register**

This endpoint registers a new user. The user object must include a hashed password.

- **Request Body:**

    ```json
    {
      "username": "newuser",
      "passwordHash": "password123"
    }
    ```

- **Response:**
  - `200 OK`: User registered successfully.
  - `400 Bad Request`: Invalid data.

- **Example:**

    ```bash
    POST /api/auth/register
    ```

#### Login

- **POST /api/auth/login**

This endpoint authenticates a user and returns an access token and refresh token.

- **Request Body:**

    ```json
    {
      "username": "existinguser",
      "passwordHash": "password123"
    }
    ```

- **Response:**
  - `200 OK`: Returns accessToken and refreshToken.
  - `401 Unauthorized`: Invalid credentials.

- **Example:**

    ```bash
    POST /api/auth/login
    ```

#### Refresh Token

- **POST /api/auth/refresh**

This endpoint allows the user to refresh their access token using a valid refresh token.

- **Request Body:**

    ```json
    {
      "accessToken": "currentAccessToken",
      "refreshToken": "currentRefreshToken"
    }
    ```

- **Response:**
  - `200 OK`: Returns new access and refresh tokens.
  - `401 Unauthorized`: Invalid or expired refresh token.

- **Example:**

    ```bash
    POST /api/auth/refresh
    ```

## Configuration

### appsettings.json

You need to configure the following settings in the `appsettings.json` file:

```json
{
  "JwtSettings": {
    "SecretKey": "your-secret-key",
    "Issuer": "CDNApi",
    "Audience": "CDNApiUsers",
    "AccessTokenExpirationMinutes": 15,
    "RefreshTokenExpirationDays": 7
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=cdnapi;Username=yourusername;Password=yourpassword"
  }
}
```
-   `SecretKey`: Your JWT secret key.
-   `AccessTokenExpirationMinutes`: The expiry time for the access token.
-   `RefreshTokenExpirationDays`: The expiry time for the refresh token.

