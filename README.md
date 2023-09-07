
# Blog Engine API Documentation

## Introduction

This project leverages the ASP.NET Web API and ASP.NET Web App Razor Pages, both on the .NET 7 platform. It's essential to have the .NET 7 runtime for local execution. The project uses SQL Server for storage solutions and integrates Azure App Insights with Serilog for monitoring.

#### Time to code: `27 hours`.

![alt text](https://i.imgur.com/e1ZwEOw.png)

### Configuration

The project comes with an `appsettings.json` configuration file. Replace the placeholders marked as `[REPLACE IN SECRETS]` directly in the file or set them as secrets in your local environment.

```json
{
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      {
        "Name": "ApplicationInsights",
        "Args": {          
          "connectionString": "[REPLACE IN SECRETS]"
        }
      }
    ]
  },

  "ConnectionStrings": {
    "BlogConnection": "[REPLACE IN SECRETS]"
  },
  "JwtAuthentication": {
    "Key": "[REPLACE IN SECRETS]"
  },
  "InitialSeedDataOptions": {
    "FirstEditorUser": {
      "Username": "Igor Henriques",
      "Email": "henriquesigor@yahoo.com.br",
      "Password": "[REPLACE IN SECRETS]",
      "Role": "Editor"
    },
    "FirstWriterUser": {
      "Username": "Random Writer",
      "Email": "randomwriter@gmail.com",
      "Password": "[REPLACE IN SECRETS]",
      "Role": "Writer"
    }
  }
}
```

### Initial Data

On production startup, users defined in `InitialSeedDataOptions` are automatically created. In development, a variety of fake data is inserted into the database.

### Testing

Unit tests covering main business functions are available in the `BlogEngine.UnitTests` project.

### General Discussions

There's a broad discussion about whether or not to use CQRS Handlers within the Domain layer. To simplify, they were kept in the Domain layer for this project. The scope and timeline also justify the absence of query caching.

## Table of Contents

- [Introduction](#introduction)
- [Queries](#queries)
  - [Authenticate](#authenticate)
  - [Get Published Posts Paginated](#get-published-posts-paginated)
  - [Get Blog Post By Status](#get-blog-post-by-status)
  - [Get Blog Post By Author](#get-blog-post-by-author)
- [Commands](#commands-api-endpoints)
  - [Create a new user](#create-user)
  - [Update User Password](#update-user-password)
  - [Update User Role](#update-user-role)
  - [Create Blog Post](#create-blog-post)
  - [Update Blog Post](#update-blog-post)
  - [Update Blog Post Status](#update-blog-post-status)
  - [Reprove Blog Post](#reprove-blog-post)
  - [Add Blog Comment](#add-blog-comment)
  - [Add Blog Editor Comment](#add-blog-editor-comment)

---

## Introduction

This document outlines the API endpoints available in the Blog Engine application. Each section provides details on the endpoints, including the expected inputs and outputs.

---

## Queries

### Authenticate

**Endpoint:**  
`POST /api/v1/user/authenticate`

**Roles Required:**  
None

**Input:**  
```json
{
  "Email": "string",
  "Password": "string"
}
```

**Output:**  
`JwtToken`

```json
{
  "Token": "string",
  "ExpiresAt": "DateTime"
}
```

---

### Get Published Posts Paginated

**Endpoint:**  
`GET /api/v1/blog-post/get-published-posts-paginated`

**Roles Required:**  
None

**Query Parameters:**  
- `pageNumber`: Integer (optional)
- `itemsPerPage`: Integer (optional)

**Output:**  
`EntityQueryResultPaginated<BlogPostDto>`

```json
{
  "Data": [
    {
      "Id": "Guid",
      "Title": "string",
      "Content": "string",
      "PublishDate": "DateTime",
      "LastUpdateDateTime": "DateTime",
      "Status": "EPublishStatus",
      "Author": "UserDto",
      "Comments": [
        {
          "Username": "string",
          "Content": "string",
          "PublishDateTime": "DateTime"
        }
      ],
      "EditorComments": [
        {
          "Content": "string",
          "Editor": "UserDto",
          "PublishDateTime": "DateTime"
        }
      ]
    }
  ],
  "TotalCount": "int?",
  "TotalPages": "int?",
  "PageNumber": "int",
  "ItemsPerPage": "int"
}
```

---

### Get Blog Post By Status

**Endpoint:**  
`GET /api/v1/blog-post/get-by-status`

**Roles Required:**  
Editor

**Query Parameters:**  
- `statuses`: EPublishStatus

**Output:**  
`IEnumerable<BlogPostDto>`

---

### Get Blog Post By Author

**Endpoint:**  
`GET /api/v1/blog-post/get-by-author`

**Roles Required:**  
Editor, Writer

**Output:**  
`IEnumerable<BlogPostDto>`

```
[
    {
      "Id": "Guid",
      "Title": "string",
      "Content": "string",
      "PublishDate": "DateTime",
      "LastUpdateDateTime": "DateTime",
      "Status": "EPublishStatus",
      "Author": "UserDto",
      "Comments": [
        {
          "Username": "string",
          "Content": "string",
          "PublishDateTime": "DateTime"
        }
      ],
      "EditorComments": [
        {
          "Content": "string",
          "Editor": "UserDto",
          "PublishDateTime": "DateTime"
        }
      ]
    }
  ]
  ```

---

## Commands API Endpoints

### User Commands

#### Create User

- **Endpoint**: `POST /api/v1/user/create`
- **Required Role**: None
- **Request Body**:

  ```json
  {
    "Username": "string",
    "Email": "string",
    "Password": "string",
    "Role": "EUserRole"
  }
  ```

#### Update User Password

- **Endpoint**: `PATCH /api/v1/user/update-password`
- **Required Role**: None
- **Request Body**:

  ```json
  {
    "UserId": "Guid",
    "Password": "string"
  }
  ```

#### Update User Role

- **Endpoint**: `PATCH /api/v1/user/update-role`
- **Required Role**: None
- **Request Body**:

  ```json
  {
    "UserId": "Guid",
    "Role": "EUserRole"
  }
  ```

### Blog Post Commands

#### Create Blog Post

- **Endpoint**: `POST /api/v1/blog-post/create`
- **Required Role**: Writer
- **Request Body**:

  ```json
  {
    "Title": "string",
    "Content": "string"
  }
  ```

#### Update Blog Post

- **Endpoint**: `PUT /api/v1/blog-post/put`
- **Required Role**: Writer
- **Request Body**:

  ```json
  {
    "BlogPostId": "Guid",
    "Title": "string",
    "Content": "string",
    "SendToReview": "boolean"
  }
  ```

#### Update Blog Post Status

- **Endpoint**: `PATCH /api/v1/blog-post/update-status`
- **Required Role**: Editor
- **Request Body**:

  ```json
  {
    "BlogPostId": "Guid",
    "Status": "EPublishStatus"
  }
  ```

#### Reprove Blog Post

- **Endpoint**: `PUT /api/v1/blog-post/reprove`
- **Required Role**: Editor
- **Request Body**:

  ```json
  {
    "BlogPostId": "Guid",
    "Reason": "string"
  }
  ```

#### Create Blog Comment

- **Endpoint**: `POST /api/v1/blog-post/add-comment`
- **Required Role**: None
- **Request Body**:

  ```json
  {
    "Username": "string",
    "Email": "string",
    "BlogPostId": "Guid",
    "Content": "string"
  }
  ```

#### Create Blog Editor Comment

- **Endpoint**: `POST /api/v1/blog-post/add-editor-comment`
- **Required Role**: Editor
- **Request Body**:

  ```json
  {
    "Content": "string",
    "BlogPostId": "Guid"
  }
  ```
