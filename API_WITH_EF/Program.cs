using API_WITH_EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connection));
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();



app.MapGet("/api/users", async (MyDbContext db) => await db.Users.ToListAsync());
app.MapGet("/api/users/{id:int}", async(int id, MyDbContext db) =>
{
    User? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
    if (user != null)
        return Results.Json(user);
    else
        return Results.NotFound(new { message = $"User with id = {id} not found" });
});
app.MapDelete("/api/users/{id:int}", async (int id, MyDbContext db) =>
{
    User? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
    if (user != null)
    {
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return Results.Json(user);
    }
    else
        return Results.NotFound(new { message = $"User with id = {id} not found" });
});
app.MapPost("/api/users", async (User user, MyDbContext db) =>
{
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return user;
});
app.MapPut("/api/users", async (User data, MyDbContext db) =>
{
    var user = db.Users.FirstOrDefault(x => x.Id == data.Id);
    if(user==null)
        return Results.NotFound(new { message = $"User with id = {data.Id} not found" });
    user.Name = data.Name;
    user.Age = data.Age;
    await db.SaveChangesAsync();
    return Results.Json(user);
});

app.Run();