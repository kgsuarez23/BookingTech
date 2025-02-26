using Api.Manager.Application.Behaivors;
using Api.Manager.Application.Entities;
using Api.Manager.Application.Mediator;
using Api.Manager.Application.UnitOfWork;
using Api.Manager.Application.Utils;
using Api.Manager.Application.Validator;
using Api.Manager.Application.Wrappers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Api.GestionHotel.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Registro de validadores y MediatR
            services.AddValidatorsFromAssemblyContaining<ValidatorTag>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaivor<,>));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MediatorTag).Assembly));

            // Registro de la unidad de trabajo
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUtils, Utils>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<PasswordHasher<DataUser>>();

            // Configuración de PasswordHasher
            services.Configure<PasswordHasherOptions>(options =>
            {
                options.IterationCount = 12000;
            });

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                options.UseSecurityTokenValidators = true;

                options.Events = new JwtBearerEvents
                {
                    // Cuando no se provee un token o este es inválido
                    OnChallenge = async context =>
                    {
                        // Evita el comportamiento por defecto
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = "application/json";

                        var responseModel = new ResponseError<string>
                        {
                            Success = false,
                            Message = "Not authenticated",
                            Errors = new List<string> { "The user is not authenticated." }
                        };

                        await context.Response.WriteAsync(JsonSerializer.Serialize(responseModel));
                    },
                    // Cuando el usuario está autenticado pero no tiene permisos
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = "application/json";

                        var responseModel = new ResponseError<string>
                        {
                            Success = false,
                            Message = "Unauthorized",
                            Errors = new List<string> { "The user does not have permissions to access this resource." }
                        };

                        await context.Response.WriteAsync(JsonSerializer.Serialize(responseModel));
                    },
                    // Opcional: para capturar errores en la autenticación (por ejemplo, token corrupto)
                    OnAuthenticationFailed = async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = "application/json";

                        var responseModel = new ResponseError<string>
                        {
                            Success = false,
                            Message = "Authentication error",
                            Errors = new List<string> { context.Exception?.Message ?? "Invalid token." }
                        };

                        await context.Response.WriteAsync(JsonSerializer.Serialize(responseModel));
                    }
                };
            });
            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Booking", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by your JWT token in the text field."
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                c.OperationFilter<ResponseErrorOperationFilter>();
                c.EnableAnnotations();

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                var xmlLibFile = "Api.Manager.xml"; 
                var xmlLibPath = Path.Combine(AppContext.BaseDirectory, xmlLibFile);
                c.IncludeXmlComments(xmlLibPath);
            });
            return services;
        }
    }
}
