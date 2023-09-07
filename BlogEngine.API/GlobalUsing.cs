﻿global using Microsoft.AspNetCore.Mvc;
global using Serilog;
global using System.Text.Json.Serialization;
global using BlogEngine.IoC.Injectors;
global using FluentValidation;
global using System.Text.Json;
global using BlogEngine.Infrastructure.Repository.DataContext;
global using Microsoft.EntityFrameworkCore;
global using BlogEngine.Domain.Commands.User.Create;
global using MediatR;
global using BlogEngine.API.Endpoints;
global using Microsoft.IO;
global using BlogEngine.Domain.Commands.User.PatchPassword;
global using BlogEngine.Domain.Commands.User.PatchRole;
global using BlogEngine.Domain.Exceptions;
global using BlogEngine.Domain.Queries.User;
global using System.Net;
global using BlogEngine.Domain.Commands.BlogPost.Create;
global using BlogEngine.Domain.Commands.BlogPost.Put;
global using BlogEngine.Domain.Commands.BlogPost.UpdateStatus;
global using BlogEngine.Domain.Commands.BlogComment.Create;
global using BlogEngine.Domain.Queries.BlogPost.GetPublishedPostsPaginated;
global using BlogEngine.Domain.Queries.BlogPost.GetBlogPostByAuthor;
global using BlogEngine.Core.Enums;
global using BlogEngine.Domain.Commands.BlogPost.Reprove;
global using BlogEngine.Domain.Queries.BlogPost.GetBlogPostByStatus;
global using System.Security.Claims;
global using static BlogEngine.Core.Shared.Constants;