using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add the Entity Framework Core DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<RaceDb>(options =>
options.UseSqlServer(connectionString));

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

// Get cars
app.MapGet("api/cars", 
    (RaceDb db) =>
{
    var cars = db.Cars.ToList();
    return Results.Ok(cars);
})
    .WithName("GetCars")
    .WithTags("Cars");

// Get car
app.MapGet("api/cars/{id}",
    (int id, RaceDb db) =>
    {
        var dbCar = db.Cars.FirstOrDefault(x => x.Id == id);

        if(dbCar == null)
        {
            return Results.NotFound($"Car with id: {id} isn't found");
        }

        return Results.Ok(dbCar);
    })
    .WithName("GetCar")
    .WithTags("Cars");

// Create car
app.MapPost("api/cars",
    (CarCreateModel carModel, RaceDb db) =>
    {
        var newCar = new Car
        {
            TeamName = carModel.TeamName,
            Speed = carModel.Speed,
            MelfunctionChance = carModel.MelfunctionChance
        };

        db.Cars.Add(newCar);
        db.SaveChanges();

        return Results.Ok(newCar);
    })
    .WithName("CreateCar")
    .WithTags("Cars");

// Update car
app.MapPut("api/cars/{id}",
    ([FromQuery] int id, [FromBody] CarCreateModel carModel, RaceDb db) =>
    {
        var dbCar = db.Cars.FirstOrDefault(x => x.Id == id);

        if (dbCar == null)
        {
            return Results.NotFound($"Car with id: {id} isn't found");
        }

        dbCar.TeamName = carModel.TeamName;
        dbCar.Speed = carModel.Speed;
        dbCar.MelfunctionChance = carModel.MelfunctionChance;
        db.SaveChanges();

        return Results.Ok(dbCar);
    })
    .WithName("UpdateCar")
    .WithTags("Cars");

// Delete car
app.MapDelete("api/cars/{id}",
    (int id, RaceDb db) =>
    {
        var dbCar = db.Cars.FirstOrDefault(x => x.Id == id);

        if (dbCar == null)
        {
            return Results.NotFound($"Car with id: {id} isn't found");
        }

        db.Remove(dbCar);
        db.SaveChanges();

        return Results.Ok($"Car with id: {id} was succesfully deleted");
    })
    .WithName("DeleteCar")
    .WithTags("Cars");

#endregion

#region Car races endpoints

// Get car races
app.MapGet("api/carraces",
    (RaceDb db) =>
    {
        var carRaces = db.CarRaces.Include(x => x.Cars).ToList();
        return Results.Ok(carRaces);
    })
    .WithName("GetCarRaces")
    .WithTags("Car races");

// Get car race
app.MapGet("api/carraces/{id}",
    (int id, RaceDb db) =>
    {
        var carRace = db
               .CarRaces
               .Include(x => x.Cars)
               .FirstOrDefault(x => x.Id == id);

        if (carRace == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(carRace);
    })
    .WithName("GetCarRace")
    .WithTags("Car races");

// Create car race
app.MapPost("api/carraces/cars",
    (CarRaceCreateModel carRaceModel, RaceDb db) =>
    {
        var newCarRace = new CarRace
        {
            Name = carRaceModel.Name,
            Location = carRaceModel.Location,
            Distance = carRaceModel.Distance,
            TimeLimit = carRaceModel.TimeLimit,
            Status = "Created"
        };
        db.CarRaces.Add(newCarRace);
        db.SaveChanges();
        return Results.Ok(newCarRace);
    })
    .WithName("CreateCarRace")
    .WithTags("Car races");

// Update car race
app.MapPut("api/carraces/{id}",
    ([FromQuery] int id, [FromBody] CarRaceCreateModel carRaceModel, RaceDb db) =>
    {
        var dbCarRace = db
                .CarRaces
                .Include(x => x.Cars)
                .FirstOrDefault(x => x.Id == id);

        if (dbCarRace == null)
        {
            return Results.NotFound($"CarRace with id: {id} isn't found.");
        }

        dbCarRace.Location = carRaceModel.Location;
        dbCarRace.Name = carRaceModel.Name;
        dbCarRace.TimeLimit = carRaceModel.TimeLimit;
        dbCarRace.Distance = carRaceModel.Distance;
        db.SaveChanges();

        return Results.Ok(dbCarRace);
    })
    .WithName("UpdateCarRace")
    .WithTags("Car races");

// Delete car race
app.MapDelete("api/carraces/{id}",
    (int id, RaceDb db) =>
    {
        var dbCarRace = db
                .CarRaces
                .Include(x => x.Cars)
                .FirstOrDefault(dbCarRace => dbCarRace.Id == id);

        if (dbCarRace == null)
        {
            return Results.NotFound($"CarRace with id: {id} isn't found.");
        }

        db.Remove(dbCarRace);
        db.SaveChanges();

        return Results.Ok($"CarRace with id: {id} was successfuly deleted");
    })
    .WithName("DeleteCarRace")
    .WithTags("Car races");

// Add Car to car race
app.MapPut("{carRaceId}/addcar/{carId}",
    (int carRaceId, int carId, RaceDb db) =>
    {
        var dbCarRace = db
                .CarRaces
                .Include(x => x.Cars)
                .SingleOrDefault(x => x.Id == carRaceId);

        if (dbCarRace == null)
        {
            return Results.NotFound($"Car Race with id: {carRaceId} not found");
        }

        var dbCar = db.Cars.SingleOrDefault(x => x.Id == carId);

        if (dbCar == null)
        {
            return Results.NotFound($"Car with id: {carId} not found");
        }

        dbCarRace.Cars.Add(dbCar);
        db.SaveChanges();
        return Results.Ok(dbCarRace);
    })
    .WithName("AddCarToCarRace")
    .WithTags("Car races");

// Start car race
app.MapPut("{id}/start",
    (int id, RaceDb db) =>
    {
        var dbCarRace = db
                .CarRaces
                .Include(x => x.Cars)
                .FirstOrDefault(carRace => carRace.Id == id);

        if (dbCarRace == null)
        {
            return Results.NotFound($"Car Race with id: {id} not found");
        }

        dbCarRace.Status = "Started";
        db.SaveChanges();

        return Results.Ok(dbCarRace);
    })
    .WithName("StartCarRace")
    .WithTags("Car races");

#endregion

#region Motorbikes endpoints

// Get Motorbikes
app.MapGet("api/motorbikes", 
    (RaceDb db) =>
{
    var motorbikes = db.Motorbikes.ToList();
    return Results.Ok(motorbikes);
})
    .WithName("GetMotorbikes")
    .WithTags("Motorbikes");

// Get Motorbike
app.MapGet("api/motorbikes/{id}",
    (int id, RaceDb db) =>
    {
        var dbMotorbike = db.Motorbikes.FirstOrDefault(x => x.Id == id);

        if (dbMotorbike == null)
        {
            return Results.NotFound($"Motorbike with id: {id} isn't found");
        }

        return Results.Ok(dbMotorbike);
    })
    .WithName("GetMotorbike")
    .WithTags("Motorbikes");

// Create Motorbike
app.MapPost("api/motorbikes",
    (MotorbikeCreateModel motorbikeModel, RaceDb db) =>
    {
        var newMotorbike = new Motorbike
        {
            TeamName = motorbikeModel.TeamName,
            Speed = motorbikeModel.Speed,
            MelfunctionChance = motorbikeModel.MelfunctionChance
        };

        db.Motorbikes.Add(newMotorbike);
        db.SaveChanges();

        return Results.Ok(newMotorbike);
    })
    .WithName("CreateMotorbike")
    .WithTags("Motorbikes");

// Update Motorbike
app.MapPut("api/motorbikes/{id}",
    ([FromQuery] int id, [FromBody] MotorbikeCreateModel motorbikeModel, RaceDb db) =>
    {
        var dbMotorbike = db.Motorbikes.FirstOrDefault(x => x.Id == id);

        if (dbMotorbike == null)
        {
            return Results.NotFound($"Motorbike with id: {id} isn't found");
        }

        dbMotorbike.TeamName = motorbikeModel.TeamName;
        dbMotorbike.Speed = motorbikeModel.Speed;
        dbMotorbike.MelfunctionChance = motorbikeModel.MelfunctionChance;
        db.SaveChanges();

        return Results.Ok(dbMotorbike);
    })
    .WithName("UpdateMotorbike")
    .WithTags("Motorbikes");

// Delete Motorbike
app.MapDelete("api/motorbikes/{id}",
    (int id, RaceDb db) =>
    {
        var dbMotorbike = db.Motorbikes.FirstOrDefault(x => x.Id == id);

        if (dbMotorbike == null)
        {
            return Results.NotFound($"Motorbike with id: {id} isn't found");
        }

        db.Remove(dbMotorbike);
        db.SaveChanges();

        return Results.Ok($"Motorbike with id: {id} was succesfully deleted");
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

public record CarCreateModel
{
    public string TeamName { get; set; }
    public int Speed { get; set; }
    public double MelfunctionChance { get; set; }
}

public record CarRace
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int Distance { get; set; }
    public int TimeLimit { get; set; }
    public string Status { get; set; }
    public List<Car> Cars { get; set; } = new List<Car>();
}

public record CarRaceCreateModel
{
    public string Name { get; set; }
    public string Location { get; set; }
    public int Distance { get; set; }
    public int TimeLimit { get; set; }
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

public record MotorbikeCreateModel
{
    public string TeamName { get; set; }
    public int Speed { get; set; }
    public double MelfunctionChance { get; set; }
}

#endregion


// Persistance

public class RaceDb : DbContext
{
    public RaceDb(DbContextOptions<RaceDb> options) : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }

    public DbSet<CarRace> CarRaces { get; set; }

    public DbSet<Motorbike> Motorbikes { get; set; }
}
