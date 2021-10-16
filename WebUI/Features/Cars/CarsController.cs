using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebUI.Features.Cars
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Car>> GetCars()
        {
            var cars = new List<Car>();
            var car1 = new Car
            {
                TeamName = "Team A",
                Speed = 100,
                MelfunctionChance = 0.2
            };
            var car2 = new Car
            {
                TeamName = "Team B",
                Speed = 90,
                MelfunctionChance = 0.15
            };
            cars.Add(car1);
            cars.Add(car2);

            return Ok(cars);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Car> GetCar(int id)
        {
            var car1 = new Car
            {
                TeamName = "Team A",
                Speed = 100,
                MelfunctionChance = 0.2
            };

            return Ok(car1);
        }

        [HttpPost]
        public ActionResult<Car> CreateCar(Car car)
        {
            var newCar = new Car
            {
                Id = car.Id,
                TeamName = car.TeamName,
                Speed = car.Speed,
                MelfunctionChance = car.MelfunctionChance
            };

            return Ok(newCar);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Car> UpdateCar(Car car)
        {
            var updateCar = new Car
            {
                Id = car.Id,
                TeamName = car.TeamName,
                Speed = car.Speed,
                MelfunctionChance = car.MelfunctionChance
            };

            return Ok(updateCar);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCar(int id)
        {
            return Ok($"Car with id {id} was succesfuly deleted");
        }
    }
}
