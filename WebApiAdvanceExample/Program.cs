using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebApiAdvanceExample.DAL.EFCore;
using WebApiAdvanceExample.Entities.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebApiAdvanceExampleDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddIdentity<AppUser<Guid>, IdentityRole>()
    .AddEntityFrameworkStores<WebApiAdvanceExampleDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.UseAuthorization();

app.MapControllers();

app.Run();