
# Blog Engine API Documentation

## Table of Contents

- [Introduction](#introduction)
- [Queries](#queries)
  - [Authenticate](#authenticate)
  - [Get Published Posts Paginated](#get-published-posts-paginated)
  - [Get Blog Post By Status](#get-blog-post-by-status)
  - [Get Blog Post By Author](#get-blog-post-by-author)

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

---
