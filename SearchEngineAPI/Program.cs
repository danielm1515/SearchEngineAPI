using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SearchEngineAPI;
using SearchEngineAPI.DBContext;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//DB
builder.Services.AddDbContext<SearchEngineContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SearchEngineContext")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI
ManagersServiceCollection managersOwner = new ManagersServiceCollection(builder.Services);


builder.Services.AddAuthentication(auth => {
    auth.DefaultAuthenticateScheme = "search_auth_scheme";
    auth.DefaultChallengeScheme = "search_auth_scheme";
}).AddJwtBearer("search_auth_scheme", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_application_api_secret")),
        ValidAudience = "searchAudience",
        ValidIssuer = "searchIssuer",
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().WithOrigins(
                              "http://localhost:4200",
                              "http://localhost:4200/lobby",
                               "https://localhost:4200");
                      });
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use((context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
    return next.Invoke();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);



app.Run();
