using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebUI.Features.CarRaces.Models;

namespace WebUI.Features.CarRaces
{
    [Route("api/carraces")]
    [ApiController]
    public class CarRacesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarRacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<CarRace>> GetCarRaces()
        {
            var carRaces = _context.CarRaces.Include(x => x.Cars).ToList();
            return Ok(carRaces);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetCarRace(int id)
        {
            var carRace = _context
                .CarRaces
                .Include(x => x.Cars)
                .FirstOrDefault(x => x.Id == id);

            if (carRace == null)
            {
                return NotFound();
            }

            return Ok(carRace);
        }

        [HttpPost]
        public ActionResult CreateCarRace(CarRaceCreateModel carRaceModel)
        {
            var newCarRace = new CarRace
            {
                Name = carRaceModel.Name,
                Location = carRaceModel.Location,
                Distance = carRaceModel.Distance,
                TimeLimit = carRaceModel.TimeLimit
            };
            _context.CarRaces.Add(newCarRace);
            _context.SaveChanges();
            return Ok(newCarRace);
        }

        [HttpPut]
        public ActionResult UpdateCarRace(CarRaceUpdateModel carRaceModel)
        {
            var dbCarRace = _context
                .CarRaces
                .Include(x => x.Cars)
                .FirstOrDefault(x => x.Id == carRaceModel.Id);

            if (dbCarRace == null)
            {
                return NotFound($"CarRace with id: {carRaceModel.Id} isn't found.");
            }

            dbCarRace.Location = carRaceModel.Location;
            dbCarRace.Name = carRaceModel.Name;
            dbCarRace.TimeLimit = carRaceModel.TimeLimit;
            dbCarRace.Distance = carRaceModel.Distance;
            _context.SaveChanges();

            return Ok(dbCarRace);
        }

        [HttpPut]
        [Route("{carRaceId}/addcar/{carId}")]
        public ActionResult AddCarToCarRace(int carRaceId, int carId)
        {
            var dbCarRace = _context
                .CarRaces
                .Include(x => x.Cars)
                .SingleOrDefault(x => x.Id == carRaceId);

            if (dbCarRace == null)
            {
                return NotFound($"Car Race with id: {carRaceId} not found");
            }

            var dbCar = _context.Cars.SingleOrDefault(x => x.Id == carId);

            if (dbCar == null)
            {
                return NotFound($"Car with id: {carId} not found");
            }

            dbCarRace.Cars.Add(dbCar);
            _context.SaveChanges();
            return Ok(dbCarRace);
        }

        [HttpPut]
        [Route("{id}/start")]
        public ActionResult StartCarRace(int id)
        {
            var carRace = _context
                .CarRaces
                .Include(x => x.Cars)
                .FirstOrDefault(carRace => carRace.Id == id);

            if (carRace == null)
            {
                return NotFound($"Car Race with id: {id} not found");
            }

            carRace.Status = "Started";
            _context.SaveChanges();

            return Ok(carRace);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCarRace(int id)
        {
            var dbCarRace = _context
                .CarRaces
                .Include(x => x.Cars)
                .FirstOrDefault(dbCarRace => dbCarRace.Id == id);

            if (dbCarRace == null)
            {
                return NotFound($"CarRace with id: {id} isn't found.");
            }

            _context.Remove(dbCarRace);
            _context.SaveChanges();

            return Ok($"CarRace with id: {id} was successfuly deleted");
        }
    }
}
