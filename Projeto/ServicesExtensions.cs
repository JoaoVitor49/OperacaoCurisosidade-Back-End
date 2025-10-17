using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Projeto.Features.Client;
using Projeto.Features.Client.Handlers;
using Projeto.Features.Log;
using Projeto.Features.Log.Handlers;
using Projeto.Features.User;
using Projeto.Features.User.Handlers;
using Projeto.Repositories;
using System.Text;

namespace Projeto
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddCoreClient (this IServiceCollection services)
        {
            services.AddScoped<CreateClientHandler>();
            services.AddScoped<GetAllClientsHandler>();
            services.AddScoped<GetClientByIdHandler>();
            services.AddScoped<UpdateClientHandler>();
            services.AddScoped<DeleteClientHandler>();
            services.AddScoped<GetTotalClientHandler>();
            services.AddScoped<GetLastMonthClientHandler>();
            services.AddScoped<GetInactiveClientHandler>();
            services.AddScoped<ClientHandlerManager>();
            services.AddScoped<PrintClientHandler>();
            return services;
        }

        public static IServiceCollection AddCoreUser (this IServiceCollection services)
        {
            services.AddScoped<CreateUserHandler>();
            services.AddScoped<GetAllUsersHandler>();
            services.AddScoped<GetUserByIdHandler>();
            services.AddScoped<UpdateUserHandler>();
            services.AddScoped<DeleteUserHandler>();
            services.AddScoped<LoginUserHandler>();
            services.AddScoped<UserHandlerManager>();
            return services;
        }

        public static IServiceCollection AddCoreLog (this IServiceCollection services)
        {
            services.AddScoped<CreateLogHandler>();
            services.AddScoped<GetAllLogsHandler>();
            services.AddScoped<GetLogByIdHandler>();
            services.AddScoped<LogHandlerManager>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            return services;
        }

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
            });
            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Key.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }

        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "http://127.0.0.1:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
            return services;
        }
    }
}
