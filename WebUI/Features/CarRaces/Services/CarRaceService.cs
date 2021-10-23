using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Features.CarRaces.Services
{
    public class CarRaceService
    {
        public CarRace RunRace(CarRace carRace)
        {
            var racers = new List<Car>();
            foreach (var car in carRace.Cars)
            {
                while (car.DistanceCoverdInMiles < carRace.Distance
                    && car.RacedForHours < carRace.TimeLimit)
                {
                    var random = new Random().Next(1, 101);
                    if(random <= car.MelfunctionChance)
                    {
                        car.MelfunctionChance++;
                    }
                    else
                    {
                        car.DistanceCoverdInMiles += car.Speed;
                    }
                    car.RacedForHours++;
                }

                if(car.DistanceCoverdInMiles >= carRace.Distance)
                {
                    car.FinishedRace = true;
                }

                racers.Add(car);
            }
            carRace.Cars = racers.OrderBy(x => x.FinishedRace)
                                 .ThenByDescending(x => x.DistanceCoverdInMiles)
                                 .ThenByDescending(x => x.RacedForHours)
                                 .ToList();

            return carRace;
        }
    }
}
