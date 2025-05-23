﻿
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Domain.Interfeces;
using Pensistence.Context;
using Pensistence.Repositories;
using Pensistence.JWT;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Application.Interfeces;
using Application.Factory.Interfeces;
using Application.Factory.Models;


namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar AutoMapper y DI
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtService, JwtRepository>();
            builder.Services.AddScoped<IEncuestaRepository, EncuestaRepository>();
            builder.Services.AddScoped<IPreguntaService, PreguntaService>();
            builder.Services.AddScoped<IPreguntaRepository, PreguntaRepository>();
            builder.Services.AddScoped<EncuestaService>();
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<IRespuestaRepository, RespuestaRepository>();
            builder.Services.AddScoped<IRespuestaService, RespuestaService>();

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddLogging();
            


            // Configurar JsonOptions para evitar ciclos de referencia 
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Configurar JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };

                //Personalizar el mensaje de error
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token expirado", "true");
                        }
                        return Task.CompletedTask;
                    },

                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"error\": \"No estás autenticado.\"}");
                    },

                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"error\": \"No tienes permisos de administrador.\"}");
                    }
                };
            });

            // politica de autorizacion
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
            });

            // Configurar CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            // Swagger + JWT
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Encuesta", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Ingrese 'Bearer <token>'"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Conexión a base de datos
            builder.Services.AddDbContext<EncuestaContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!));

            var app = builder.Build();

            // Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
