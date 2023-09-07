﻿global using BlogEngine.Domain.Commands.User.Create;
global using FluentValidation;
global using Microsoft.Extensions.DependencyInjection;
global using MediatR;
global using BlogEngine.Domain.Pipelines;
global using BlogEngine.Core.Shared;    
global using System.Text;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using BlogEngine.Core.Enums;
global using System.Security.Claims;
global using BlogEngine.Domain.Interfaces.Repositories.Base;
global using BlogEngine.Infrastructure.Repository.Repositories.Persistance;
global using BlogEngine.Domain.Models.Options;
global using Microsoft.Extensions.Configuration;
global using BlogEngine.Domain.Commands.User.PatchPassword;
global using BlogEngine.Domain.Commands.User.PatchRole;
global using BlogEngine.Domain.Queries.User;
global using BlogEngine.Domain.Interfaces.Services;
global using BlogEngine.Infrastructure.Service.Services;
global using BlogEngine.Infrastructure.Repository.Repositories.ReadOnly;
global using BlogEngine.Domain.Commands.BlogPost.Create;
global using BlogEngine.Domain.Profiles;
global using BlogEngine.Domain.Commands.BlogPost.Put;
global using Microsoft.OpenApi.Models;
global using BlogEngine.Domain.Commands.BlogPost.UpdateStatus;
global using BlogEngine.Domain.Interfaces.Repositories;
global using BlogEngine.Domain.Entities.Base;
global using BlogEngine.Domain.Commands.BlogComment.Create;
global using BlogEngine.Domain.Queries.BlogPost.GetBlogPostByAuthor;
global using BlogEngine.Infrastructure.Repository.Repositories;