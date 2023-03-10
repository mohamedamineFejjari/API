using System.Numerics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var plantes = new List<Plante>{
    new Plante {id = 1 , type ="olivier"},
    new Plante{id = 2 , type ="orange"},
    new Plante{id = 3 , type ="citron"},
    new Plante{id = 4 , type ="pomme"},
};

app.MapGet("/plante", () =>
{
    return plantes;
});
app.MapGet("/plante/{id:int}", (int id) =>
{
    var plante = plantes.Find(p => p.id == id);
    if (plante is null)
        return Results.NotFound("sorry, this plante doesn't exist");
    return Results.Ok(plante);
});
app.MapPost("/plante", (Plante plante) =>
{
    plantes.Add(plante);
    return plantes;
});


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.Run();

class Plante
{
    public int id { get; set; }
    public required string type { get; set; }
}


internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
