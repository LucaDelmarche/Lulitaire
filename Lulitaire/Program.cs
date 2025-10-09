using System.Text;
using Application.Core.User;
using Infrastructure;
using Application.Core.User.Commands.PatchEmailAddress;
using Application.Core.User.Queries.GetById;
using Application.Core.User.Queries.GetByUsernameOrMail;
using Application.Core.User.Queries.GetByUsernameOrMailAndPassword;
using Application.Features.Item.Commands;
using Application.Features.Item.Commands.Create;
using Application.Features.Item.Commands.Delete;
using Application.Features.Item.Commands.Patch;
using Application.Features.Item.Queries;
using Application.Features.Item.Queries.GetAll;
using Application.Features.Zone.Commands;
using Application.Features.Zone.Commands.Create;
using Application.Features.Zone.Commands.Delete;
using Application.Features.Zone.Commands.PatchUsername;
using Application.Features.Zone.Queries;
using Application.Features.Zone.Queries.GetAll;
using Application.Services;
using Application.user.commands;
using Application.user.commands.create;
using Application.user.commands.delete;
using Application.user.commands.PatchUsername;
using Application.user.commands.put;
using Application.user.queries;
using Application.user.queries.getAll;
using Application.user.queries.GetByUsernameOrMailAndPassword;
using Application.utils;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repositories.User;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Lulitaire;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var secretKey = builder.Configuration["Jwt:SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new Exception("Secret key is missing.");
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("c11db5f94a1b8d2d19ee96a53608684251ba84288590ea1774b0e4ec729488b2")),
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["AuthToken"];
                        return Task.CompletedTask;
                    }
                };
            });

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true; 
                options.Cookie.SameSite = SameSiteMode.Lax;
            });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Lulitaire")));
        
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<TokenService>();
        builder.Services.AddHttpContextAccessor();
        
        // Enregistrement des handler
        //user
        builder.Services.AddScoped<ICommandHandler<UserCreateCommand,UserCreateOutput.UserCreateDto>, UserCreateHandler>();
        builder.Services.AddScoped<ICommandHandler<ZoneCreateCommand,ZoneCreateOutput.ZoneCreateDto>, ZoneCreateHandler>();
        builder.Services.AddScoped<ICommandHandler<ItemCreateCommand,ItemCreateOutput.ItemCreateDto>, ItemCreateHandler>();
        
        builder.Services.AddScoped<UserEmptyQueryProcessor>();
        builder.Services.AddScoped<ZoneEmptyQueryProcessor>(); 
        builder.Services.AddScoped<ItemEmptyQueryProcessor>();
        
        builder.Services.AddScoped<IEmptyQueryHandler<ZoneGetallOutput>, ZoneEmptyGetAllHandler>();
        builder.Services.AddScoped<IEmptyQueryHandler<ItemGetallOutput>, ItemEmptyGetAllHandler>();
        builder.Services.AddScoped<ZoneQueryProcessor>();
        builder.Services.AddScoped<UserQueryProcessor>();
        builder.Services.AddScoped<ItemQueryProcessor>();
        
        builder.Services.AddScoped<UserCommandProcessor>();
        builder.Services.AddScoped<ZoneCommandProcessor>();
        builder.Services.AddScoped<ItemCommandProcessor>();
        
        builder.Services.AddScoped<IEmptyQueryHandler<UserGetallOutput>, UserGetAllHandler>();
        builder.Services.AddScoped<IQueryHandler<ZoneGetAllQuery,ZoneGetallOutput>, ZoneGetAllHandler>();
        builder.Services.AddScoped<IQueryHandler<ItemGetAllQuery,ItemGetallOutput>, ItemGetAllHandler>();
        builder.Services.AddScoped<IEmptyQueryHandler<UserGetCurrentUserOutput>, UserGetCurrentUserHandler>();
        
        builder.Services.AddScoped<IQueryHandler<UserGetByUsernameOrMailAndPasswordQuery,UserGetByUsernameOrMailAndPasswordOutput>, UserGetByUsernameOrMailAndPasswordHandler>();
        builder.Services.AddScoped<IEmptyOutputCommandHandler<int>, UserDeleteHandler>();
        builder.Services.AddScoped<IEmptyOutputCommandHandler<ZoneDeleteCommand>, ZoneDeleteHandler>();
        builder.Services.AddScoped<IEmptyOutputCommandHandler<ItemDeleteCommand>, ItemDeleteHandler>();
        builder.Services.AddScoped<IEmptyOutputCommandHandler<UserPutCommand>,UserPutHandler>();
        builder.Services.AddScoped<IEmptyOutputCommandHandler<ItemPatchCommand>,ItemPatchHandler>();
        builder.Services.AddScoped<IEmptyOutputCommandHandler<UserPatchRoleCommand>,UserPatchRoleHandler>();
        builder.Services.AddScoped<IEmptyOutputCommandHandler<UserPatchUsernameCommand>,UserPatchUsernameHandler>();
        builder.Services.AddScoped<IEmptyOutputCommandHandler<ZonePatchCommand>,ZonePatchUsernameHandler>();
        builder.Services.AddScoped<IEmptyOutputCommandHandler<UserPatchEmailAddressCommand>,UserPatchEmailAddressHandler>();
        builder.Services.AddScoped<IQueryHandler<UserGetByUsernameOrMailQuery,UserGetByUsernameOrMailOutput>, UserGetByUsernameOrMailHandler>();
        builder.Services.AddScoped<IQueryHandler<UserGetByIdQuery,UserGetByIdOutput>, UserGetByIdHandler>();
        
        // Configuration d'AutoMapper
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        var configuration = builder.Configuration;
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Development", policyBuilder =>
            {
                policyBuilder.WithOrigins(configuration["ApiSettings:CorsAllowed"] ?? string.Empty)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials(); // Permet l'utilisation des credentials
            });
        });
        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }

        // Pipeline de traitement
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projet Palermo v1"));
            app.UseCors("Development");
        }
       // if (app.Environment.IsDevelopment())
        //{
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("Development");
        //}

        //app.UseHttpsRedirection();
        
        app.UseAuthentication();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
        
    }
}