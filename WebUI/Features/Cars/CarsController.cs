using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebUI.Features.Cars.Models;

namespace WebUI.Features.Cars
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Car>> GetCars()
        {
            var cars = _context.Cars.ToList();
            return Ok(cars);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Car> GetCar(int id)
        {
            var car = _context.Cars.FirstOrDefault(x => x.Id == id);

            if(car == null)
            {
                return NotFound($"Car with id:{id} isn't found");
            }

            return Ok(car);
        }

        [HttpPost]
        public ActionResult<Car> CreateCar(CarCreateModel carModel)
        {
            var car = new Car
            {
                TeamName = carModel.TeamName,
                Speed = carModel.Speed,
                MelfunctionChance = carModel.MelfunctionChance
            };

            _context.Cars.Add(car);
            _context.SaveChanges();

            return Ok(carModel);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Car> UpdateCar(CarUpdateModel carModel)
        {
            var dbCar = _context.Cars.FirstOrDefault(x => x.Id == carModel.Id);

            if (dbCar == null)
            {
                return NotFound($"Car with id:{carModel.Id} isn't found");
            }

            dbCar.TeamName = carModel.TeamName;
            dbCar.Speed = carModel.Speed;
            dbCar.MelfunctionChance = carModel.MelfunctionChance;
            _context.SaveChanges();

            return Ok(dbCar);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCar(int id)
        {
            var dbCar = _context.Cars.FirstOrDefault(x => x.Id == id);

            if (dbCar == null)
            {
                return NotFound($"Car with id:{id} isn't found");
            }

            _context.Remove(dbCar);
            _context.SaveChanges();

            return Ok($"Car with id: {id} was successfuly deleted");
        }
    }
}
