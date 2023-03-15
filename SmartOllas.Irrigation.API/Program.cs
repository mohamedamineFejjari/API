using SmartOllas.Irrigation.API;

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

var plantes = new List<PlanteViewModel>{
    new PlanteViewModel {id = 1 , Name  ="olivier",Variety = "V1" , WaterNeeded=43, ClayJarId=1, SensorId=1},
    new PlanteViewModel{id = 2 , Name  ="orange",Variety = "V2" , WaterNeeded=78, ClayJarId=2, SensorId=2},
    new PlanteViewModel{id = 3 , Name  ="citron",Variety = "V3" , WaterNeeded=57, ClayJarId=3, SensorId=3},
    new PlanteViewModel{id = 4 , Name  ="pomme",Variety = "V4" , WaterNeeded=45, ClayJarId=4, SensorId=4},
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

app.MapPut("/plante/{id}", (PlanteViewModel updatedPlante, int id) =>
{
    var plante = plantes.Find(p => p.id == id);
    if (plante is null)
        return Results.NotFound("sorry,this plante doesn't exist");

    plante.ClayJarId = updatedPlante.ClayJarId;

    return Results.Ok(plante);
});
app.MapDelete("/plante/{id}", (int id) =>
{
    var plante = plantes.Find(p => p.id == id);
    if (plante is null)
        return Results.NotFound("sorry,this plante doesn't exist");
    plantes.Remove(plante);
    return Results.Ok(plante);
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
});
var capteurs = new List<Capteur>{
    new Capteur {id = 1 , ValeurHmidity  =19,ValeurTemperature = 27 , ClayJarId=1, PlanteId=1},
    new Capteur{id = 2 , ValeurHmidity  =20,ValeurTemperature =  39  , ClayJarId=2, PlanteId=2},
    new Capteur{id = 3 , ValeurHmidity  =55,ValeurTemperature =  63 ,  ClayJarId=3, PlanteId=3},
    new Capteur{id = 4 , ValeurHmidity  =90,ValeurTemperature = 43 ,  ClayJarId=4, PlanteId=4},
};
app.MapGet("/Capteur", () =>
{
    return capteurs;
});
app.MapGet("/Capteur/{id:int}", (int id) =>
{
    var Capteur = capteurs.Find(c => c.id == id);
    if (Capteur is null)
        return Results.NotFound("sorry, this sensor doesn't exist");
    return Results.Ok(Capteur);
});
app.MapPost("/Capteur", (Capteur capteur) =>
{
    capteurs.Add(capteur);
    return capteurs;
});
app.MapPut("/Capteur/{id}", (Capteur updatedCapteur, int id) =>
{
    var Capteur = capteurs.Find(c => c.id == id);
    if (Capteur is null)
        return Results.NotFound("sorry,this sensor doesn't exist");

    Capteur.id = updatedCapteur.id;

    return Results.Ok(Capteur);
});
app.MapDelete("/Capteur/{id}", (int id) =>
{
    var Capteur = capteurs.Find(c => c.id == id);
    if (Capteur is null)
        return Results.NotFound("sorry,this sensor doesn't exist");
    capteurs.Remove(Capteur);
    return Results.Ok(Capteur);
});

var jarres = new List<Jarre>{
    new Jarre {id = 1 , Volume  =150,SensorId=1, PlanteId=1},
    new Jarre{id = 2 , Volume  =90, SensorId=2, PlanteId=2},
    new Jarre{id = 3 , Volume  =110, SensorId=3, PlanteId=3},
    new Jarre{id = 4 , Volume  =90, SensorId=4, PlanteId=4},
};

app.MapGet("/Jarre", () =>
{
    return jarres;
});
app.MapGet("/Jarre/{id:int}", (int id) =>
{
    var Jarre = jarres.Find(c => c.id == id);
    if (Jarre is null)
        return Results.NotFound("sorry, this jarre doesn't exist");
    return Results.Ok(Jarre);
});
app.MapPost("/Jarre", (Jarre jarre) =>
{
    jarres.Add(jarre);
    return jarres;
});
app.MapPut("/Jarre/{id}", (Jarre updatedJarre, int id) =>
{
    var Jarre = jarres.Find(j => j.id == id);
    if (Jarre is null)
        return Results.NotFound("sorry,this jarre doesn't exist");

    Jarre.id = updatedJarre.id;

    return Results.Ok(Jarre);
});
app.MapDelete("/Jarre/{id}", (int id) =>
{
    var Jarre = jarres.Find(j => j.id == id);
    if (Jarre is null)
        return Results.NotFound("sorry,this Jarre doesn't exist");
    jarres.Remove(Jarre);
    return Results.Ok(Jarre);
});

var utilisateurs = new List<Utilisateur>{
    new Utilisateur {id = 1 , Name  ="aaa",age = 27 },
    new Utilisateur {id = 2 , Name  ="bbb",age =  39 },
    new Utilisateur {id = 3 , Name  ="ccc",age =  63 },
    new Utilisateur {id = 4 , Name  ="ddd",age = 43 },
};
app.MapGet("/Utilisateur", () =>
{
    return utilisateurs;
});
app.MapGet("/Utilisateur/{id:int}", (int id) =>
{
    var Utilisateur = utilisateurs.Find(u => u.id == id);
    if (Utilisateur is null)
        return Results.NotFound("sorry, this utilisateur doesn't exist");
    return Results.Ok(Utilisateur);
});
app.MapPost("/Utilisateur", (Utilisateur utilisateur) =>
{
    utilisateurs.Add(utilisateur);
    return utilisateurs;
});

app.MapPut("/Utilisateur/{id}", (Utilisateur utilisateur, int id) =>
{
    var Utilisateur = utilisateurs.Find(u => u.id == id);
    if (Utilisateur is null)
        return Results.NotFound("sorry,this utilisateur doesn't exist");

    Utilisateur.id = utilisateur.id;

    return Results.Ok(Utilisateur);
});
app.MapDelete("/Utilisateur/{id}", (int id) =>
    {
        var Utilisateur = utilisateurs.Find(u => u.id == id);
        if (Utilisateur is null)
            return Results.NotFound("sorry,this utilisateur doesn't exist");
        utilisateurs.Remove(Utilisateur);
        return Results.Ok(Utilisateur);
    })

    .WithName("DeleteUtilisateur")
    .WithOpenApi();

app.Run();

namespace SmartOllas.Irrigation.API
{
    internal class PlanteViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Variety { get; set; }
        public int WaterNeeded { get; set; }
        public int ClayJarId { get; set; }
        public int SensorId { get; set; }
    }

    internal class Capteur
    {
        public int id { get; set; }
        public int ValeurHmidity { get; set; }
        public int ValeurTemperature { get; set; }
        public int ClayJarId { get; set; }
        public int PlanteId { get; set; }
    }

    internal class Jarre
    {
        public int id { get; set; }
        public int Volume { get; set; }
        public int PlanteId { get; set; }
        public int SensorId { get; set; }
    }

    internal class Utilisateur
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int age { get; set; }
    }

    internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}