using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SampleAPI.Data;
using SampleAPI.Service;
using System.Text;

var policyName = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000","http://192.168.100.231:3000")
                                                  .AllowAnyMethod()
                                                   .AllowAnyHeader();
                      });
});

builder.Services.AddDbContext<SampleDBContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample_APIs", Version = "v1", Description = "Sample APIs" });

    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {

        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer {Access_Token}\"",
    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}

                    }
                });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer"; //JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = "Bearer"; //JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "Bearer"; //JwtBearerDefaults.AuthenticationScheme;

})
                    .AddJwtBearer(cfg =>
                    {
                        cfg.RequireHttpsMetadata = false;
                        cfg.SaveToken = true;
                        cfg.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                            ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
                            ClockSkew = TimeSpan.Zero
                        };
                    });


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthenService, AuthenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policyName);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
