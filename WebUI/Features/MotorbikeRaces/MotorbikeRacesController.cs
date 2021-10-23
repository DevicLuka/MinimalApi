using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebUI.Features.MotorbikeRaces.Models;
using WebUI.Features.MotorbikeRaces.Services;

namespace WebUI.Features.MotorbikeRaces
{
    [ApiController]
    [Route("api/motorbikeraces")]
    public class MotorbikeRacesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MotorbikeRacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public MotorbikeRaceService MotorbikeRaceService { get; set; } = new MotorbikeRaceService();


        [HttpGet]
        public ActionResult<List<MotorbikeRace>> GetMotorbikeRaces()
        {
            var motorbikeRaces = _context.MotorbikeRaces.ToList();
            return Ok(motorbikeRaces);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<MotorbikeRace> GetMotorbikeRace(int id)
        {
            var motorbikeRace = _context.MotorbikeRaces.FirstOrDefault(motorbikeRace => motorbikeRace.Id == id);

            if (motorbikeRace == null)
            {
                return NotFound();
            }

            return Ok(motorbikeRace);
        }

        [HttpPost]
        [Route("")]
        public ActionResult<MotorbikeRace> CreateMotorbikeRace(MotorbikeRacesCreateModel motorbikeRaceModel)
        {
            var motorbikeRace = new MotorbikeRace
            {
                Name = motorbikeRaceModel.Name,
                Location = motorbikeRaceModel.Location,
                Distance = motorbikeRaceModel.Distance,
                TimeLimit = motorbikeRaceModel.TimeLimit
            };
            _context.MotorbikeRaces.Add(motorbikeRace);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public ActionResult<MotorbikeRace> UpdateMotorbikeRace(MotorbikeRaceUpdateModel motorbikeRaceModel)
        {
            var dbMotorbikeRace = _context.MotorbikeRaces.FirstOrDefault(dbMotorbikeRace => dbMotorbikeRace.Id == motorbikeRaceModel.Id);

            if (dbMotorbikeRace == null)
            {
                return NotFound($"MotorbikeRace with id: {motorbikeRaceModel.Id} isn't found, please try another id.");
            }

            dbMotorbikeRace.Location = motorbikeRaceModel.Location;
            dbMotorbikeRace.Name = motorbikeRaceModel.Name;
            dbMotorbikeRace.TimeLimit = motorbikeRaceModel.TimeLimit;
            dbMotorbikeRace.Distance = motorbikeRaceModel.Distance;
            _context.SaveChanges();
            return Ok(dbMotorbikeRace);
        }

        [HttpPut]
        [Route("{motorbikeRaceId}/addmotorbike/{motorbikeId}")]
        public ActionResult<MotorbikeRace> AddMotorbikeToMotorbikeRace(int motorbikeRaceId, int motorbikeId)
        {
            var dbMotorbikeRace = _context.MotorbikeRaces.SingleOrDefault(dbMotorbikeRace => dbMotorbikeRace.Id == motorbikeRaceId);

            if (dbMotorbikeRace == null)
            {
                return NotFound($"Motorbike Race with id: {motorbikeRaceId} not found");
            }

            if (dbMotorbikeRace.Motorbikes == null)
            {
                dbMotorbikeRace.Motorbikes = new List<Motorbike>();
            }

            var dbMotorbike = _context.Motorbikes.SingleOrDefault(dbMotorbike => dbMotorbike.Id == motorbikeId);

            if (dbMotorbike == null)
            {
                return NotFound($"Motorbike with id: {motorbikeId} not found");
            }

            dbMotorbikeRace.Motorbikes.Add(dbMotorbike);
            _context.SaveChanges();
            return Ok(dbMotorbikeRace);
        }

        [HttpPut]
        [Route("{id}/start")]
        public ActionResult<MotorbikeRace> StartMotorbikeRace(int id)
        {
            var dbMotorbikeRace = _context.MotorbikeRaces.FirstOrDefault(dbMotorbikeRace => dbMotorbikeRace.Id == id);

            if (dbMotorbikeRace == null)
            {
                return NotFound();
            }

            dbMotorbikeRace.Status = "Started";
            MotorbikeRaceService.RunRace(dbMotorbikeRace);
            _context.SaveChanges();
            return Ok(dbMotorbikeRace);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteMotorbikeRace(int id)
        {
            var dbMotorbikeRace = _context.MotorbikeRaces.FirstOrDefault(dbMotorbikeRace => dbMotorbikeRace.Id == id);

            if (dbMotorbikeRace == null)
            {
                return NotFound($"Motorbike Race with id: {id} isn't found, please try another id.");
            }
            _context.Remove(dbMotorbikeRace);
            _context.SaveChanges();
            return Ok($"Motorbike Race with id: {id} was successfuly deleted");
        }
    }
}
