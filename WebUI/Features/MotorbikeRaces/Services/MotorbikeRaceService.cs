using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebUI.Features.MotorbikeRaces.Services
{
    public class MotorbikeRaceService
    {
        public MotorbikeRace RunRace(MotorbikeRace motorbikeRace)
        {
            var racers = new List<Motorbike>();
            foreach (var bike in motorbikeRace.Motorbikes)
            {
                while (bike.DistanceCoverdInMiles < motorbikeRace.Distance
                       && bike.RacedForHours < motorbikeRace.TimeLimit)
                {
                    bike.RacedForHours++;
                    var random = new Random().Next(0, 100);
                    if(random <= bike.MelfunctionChance)
                    {
                        bike.MelfunctionsOccured++;
                    }
                    else
                    {
                        bike.DistanceCoverdInMiles += bike.Speed;
                    }
                }
                if(bike.DistanceCoverdInMiles >= motorbikeRace.Distance)
                {
                    bike.FinishedRace = true;
                }

                racers.Add(bike);
            }
            motorbikeRace.Motorbikes = racers.OrderBy(x => x.FinishedRace)
                                             .ThenByDescending(x => x.DistanceCoverdInMiles)
                                             .ThenByDescending(x => x.RacedForHours)
                                             .ToList();

            return motorbikeRace;
        }
    }
}
