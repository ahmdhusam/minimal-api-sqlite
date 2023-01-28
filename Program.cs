using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using miniapi.Data;
using miniapi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

var app = builder.Build();

app.MapPost("/", async ([FromBody] UserInput userInput, AppDbContext db) =>
{
    var (Name, UserName, Email, Bio, BirthDate, Gender, Avatar, Header, Password) = userInput;
    await db.Users.AddAsync(new UserModel
    {
        Name = Name,
        UserName = UserName,
        Email = Email,
        Bio = Bio,
        BirthDate = BirthDate,
        Gender = Gender,
        Avatar = Avatar,
        Header = Header,
        Password = Password
    });
    await db.SaveChangesAsync();

    return Results.Ok(new { Name, UserName, Email, Bio, BirthDate, Gender, Avatar, Header, Password });
});

app.MapGet("/", async (AppDbContext db) =>
{
    var user_count = await db.Users.CountAsync();
    var skip = Random.Shared.Next(user_count - 50);
    var users = await db.Users.Skip(skip).Take(50).ToListAsync();
    return Results.Ok(users);
});

app.Run();
