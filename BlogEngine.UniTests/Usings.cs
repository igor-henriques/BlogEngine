global using Xunit;
global using AutoMapper;
global using BlogEngine.Core.Enums;
global using BlogEngine.Domain.Dtos;
global using BlogEngine.Domain.Entities;
global using BlogEngine.Domain.Interfaces.Repositories;
global using BlogEngine.Domain.Interfaces.Repositories.Base;
global using BlogEngine.Domain.Interfaces.Services;
global using BlogEngine.Domain.Models;
global using BlogEngine.Core.Authentication;
global using BlogEngine.Domain.Queries.BlogPost.GetPublishedPostsPaginated;
global using BlogEngine.Domain.Queries.User;
global using Bogus;
global using FluentAssertions;
global using Moq;
global using System.Linq.Expressions;
global using System.Security.Claims;
global using BlogEngine.Domain.Queries.BlogPost.GetBlogPostByStatus;
global using BlogEngine.Domain.Queries.BlogPost.GetBlogPostByAuthor;
global using BlogEngine.Domain.Commands.BlogPost.Create;
global using BlogEngine.Domain.Commands.BlogPost.Put;
global using BlogEngine.Domain.Commands.BlogPost.Reprove;
global using MediatR;
global using BlogEngine.Domain.Commands.BlogPost.UpdateStatus;
global using BlogEngine.Domain.Commands.BlogEditorComment.Create;
global using BlogEngine.Domain.Commands.BlogComment.Create;
global using Microsoft.AspNetCore.Http;