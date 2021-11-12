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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


#region Cars endpoints

app.MapGet("api/cars", () =>
{
    var car1 = new Car
    {
        TeamName = "Team A"
    };
    var car2 = new Car
    {
        TeamName = "Team B"
    };

    var cars = new List<Car>
    {
        car1, car2
    };

    return cars;
})
    .WithName("GetCars")
    .WithTags("Cars");

app.MapGet("api/cars/{id}",
    (int id) =>
    {
        var car1 = new Car
        {
            TeamName = "Team A"
        };
        return car1;
    })
    .WithName("GetCar")
    .WithTags("Cars");

app.MapPost("api/cars",
    (Car car) =>
    {
        return car;
    })
    .WithName("CreateCar")
    .WithTags("Cars");

app.MapPut("api/cars/{id}",
    (Car car) =>
    {
        return car;
    })
    .WithName("UpdateCar")
    .WithTags("Cars");

app.MapDelete("api/cars/{id}",
    (int id) =>
    {
        return $"Car with id: {id} was succesfully deleted";
    })
    .WithName("DeleteCar")
    .WithTags("Cars");

#endregion

#region Motorbikes endpoints

app.MapGet("api/motorbikes", () =>
{
    var motorbike1 = new Motorbike
    {
        TeamName = "Team A"
    };
    var motorbike2 = new Motorbike
    {
        TeamName = "Team B"
    };

    var cars = new List<Motorbike>
    {
        motorbike1, motorbike2
    };

    return cars;
})
    .WithName("GetMotorbikes")
    .WithTags("Motorbikes");

app.MapGet("api/motorbikes/{id}",
    (int id) =>
    {
        var motorbike1 = new Motorbike
        {
            TeamName = "Team A"
        };
        return motorbike1;
    })
    .WithName("GetMotorbike")
    .WithTags("Motorbikes");

app.MapPost("api/motorbikes",
    (Motorbike motorbike) =>
    {
        return motorbike;
    })
    .WithName("CreateMotorbike")
    .WithTags("Motorbikes");

app.MapPut("api/motorbikes/{id}",
    (Motorbike motorbike) =>
    {
        return motorbike;
    })
    .WithName("UpdateMotorbike")
    .WithTags("Motorbikes");

app.MapDelete("api/motorbikes/{id}",
    (int id) =>
    {
        return $"Motorbike with id: {id} was succesfully deleted";
    })
    .WithName("DeleteMotorbike")
    .WithTags("Motorbikes");

#endregion

#region Default endpoints

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithTags("Defaults"); ;

#endregion

app.Run();

// Models
#region Models

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public record Car
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public int Speed { get; set; }
    public double MelfunctionChance { get; set; }
    public int MelfunctionsOccured { get; set; }
    public int DistanceCoverdInMiles { get; set; }
    public bool FinishedRace { get; set; }
    public int RacedForHours { get; set; }
}

public record Motorbike
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public int Speed { get; set; }
    public double MelfunctionChance { get; set; }
    public int MelfunctionsOccured { get; set; }
    public int DistanceCoverdInMiles { get; set; }
    public bool FinishedRace { get; set; }
    public int RacedForHours { get; set; }
}

#endregion
